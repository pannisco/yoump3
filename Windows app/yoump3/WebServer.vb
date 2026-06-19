Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Text
Imports System.Threading

Public Class WebServer
    Private listener As HttpListener
    Private listenerThread As Thread
    Public IsRunning As Boolean = False
    Public Const Port As Integer = 47891

    Public Sub Start(ip As String)
        If IsRunning Then Return
        Try
            listener = New HttpListener()
            listener.Prefixes.Add($"http://{ip}:{Port}/")
            If ip <> "127.0.0.1" Then
                listener.Prefixes.Add($"http://127.0.0.1:{Port}/")
            End If
            listener.Start()
        Catch ex As HttpListenerException When ex.ErrorCode = 5
            RegisterPrefix(ip)
            If ip <> "127.0.0.1" Then RegisterPrefix("127.0.0.1")
            Try
                listener = New HttpListener()
                listener.Prefixes.Add($"http://{ip}:{Port}/")
                If ip <> "127.0.0.1" Then
                    listener.Prefixes.Add($"http://127.0.0.1:{Port}/")
                End If
                listener.Start()
            Catch ex2 As Exception
                MessageBox.Show($"Web server error after registration: {ex2.Message}", "YouMP3")
                Return
            End Try
        Catch ex As Exception
            MessageBox.Show($"Web server error: {ex.Message}", "YouMP3")
            Return
        End Try
        IsRunning = True
        listenerThread = New Thread(AddressOf ListenLoop) With {.IsBackground = True}
        listenerThread.Start()
    End Sub

    Public Sub [Stop]()
        If Not IsRunning Then Return
        Try
            IsRunning = False
            listener.Stop()
            listener.Close()
        Catch
        End Try
    End Sub

    Private Sub ListenLoop()
        While IsRunning
            Try
                Dim context As HttpListenerContext = listener.GetContext()
                Dim t As New Thread(Sub() HandleRequest(context)) With {.IsBackground = True}
                t.Start()
            Catch
                Exit While
            End Try
        End While
    End Sub

    Private Sub HandleRequest(context As HttpListenerContext)
        Dim req As HttpListenerRequest = context.Request
        Dim res As HttpListenerResponse = context.Response
        res.Headers.Add("Access-Control-Allow-Origin", "*")
        res.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS")
        res.Headers.Add("Access-Control-Allow-Headers", "Content-Type")
        If req.HttpMethod = "OPTIONS" Then
            res.StatusCode = 200
            res.Close()
            Return
        End If
        Dim path As String = req.Url.AbsolutePath.ToLower().TrimEnd("/"c)
        Try
            Select Case path
                Case "", "/", "/index", "/index.html"
                    ServeHTML(res)
                Case "/status"
                    ServeStatus(res)
                Case "/download"
                    If req.HttpMethod = "POST" Then HandleDownload(req, res) Else Send404(res)
                Case "/stop"
                    If req.HttpMethod = "POST" Then HandleStop(res) Else Send404(res)
                Case "/files"
                    ServeFileList(res)
                Case "/files/zip"
                    ServeZip(res)
                Case Else
                    If path.StartsWith("/files/") Then
                        ServeSingleFile(res, req.Url.AbsolutePath.Substring(7))
                    Else
                        Send404(res)
                    End If
            End Select
        Catch ex As Exception
            Try
                SendJSON(res, $"{{""error"":""{EscapeJson(ex.Message)}""}}", 500)
            Catch
            End Try
        End Try
    End Sub

    ' ── HTML ────────────────────────────────────────────────────────────────
    Private Sub ServeHTML(res As HttpListenerResponse)
        Dim htmlPath As String = Path.Combine(Application.StartupPath, "index.html")
        Dim html As String
        If File.Exists(htmlPath) Then
            html = File.ReadAllText(htmlPath, Encoding.UTF8)
        Else
            html = "<h2 style='font-family:sans-serif;padding:20px'>Error: index.html not found.</h2>"
        End If
        Dim bytes = Encoding.UTF8.GetBytes(html)
        res.ContentType = "text/html; charset=utf-8"
        res.ContentLength64 = bytes.Length
        res.OutputStream.Write(bytes, 0, bytes.Length)
        res.Close()
    End Sub

    ' ── Status — legge variabili pubbliche, ZERO Invoke ─────────────────────
    Private Sub ServeStatus(res As HttpListenerResponse)
        Dim status As String = If(Form1.IsDownloading, "downloading", "idle")
        Dim progress As String = If(Form1.CurrentProgress <> "", Form1.CurrentProgress, "0/0")
        Dim log As String = If(Form1.CurrentLog IsNot Nothing, Form1.CurrentLog, "")
        Dim dlPath As String = If(Form1.CurrentDownloadPath IsNot Nothing, Form1.CurrentDownloadPath, "")
        Dim json = $"{{""status"":""{status}"",""progress"":""{progress}"",""log"":""{EscapeJson(log)}"",""downloadPath"":""{EscapeJson(dlPath)}""}}"
        SendJSON(res, json)
    End Sub

    ' ── Download ─────────────────────────────────────────────────────────────
    Private Sub HandleDownload(req As HttpListenerRequest, res As HttpListenerResponse)
        Dim body As String = ""
        Using reader As New StreamReader(req.InputStream, Encoding.UTF8)
            body = reader.ReadToEnd()
        End Using
        Dim url As String = ExtractJsonValue(body, "url")
        Dim dlPath As String = ExtractJsonValue(body, "path")
        If String.IsNullOrWhiteSpace(url) Then
            SendJSON(res, $"{{""error"":""missing url""}}", 400)
            Return
        End If
        ' Scrive variabili condivise, il timer su UI thread le legge
        Form1.TextBox2.Text = url
        Form1.PendingDownloadPath = dlPath
        Form1.download()
        SendJSON(res, $"{{""ok"":true}}")
    End Sub

    Private Sub HandleStop(res As HttpListenerResponse)
        Form1.stopdwld()
        SendJSON(res, $"{{""ok"":true}}")
    End Sub

    ' ── File list — legge CurrentDownloadPath, ZERO Invoke ───────────────────
    Private Sub ServeFileList(res As HttpListenerResponse)
        Dim dlPath As String = Form1.CurrentDownloadPath
        If dlPath = "" OrElse dlPath = "Download Path" Then
            dlPath = My.Settings.option1
        End If
        If Not Directory.Exists(dlPath) Then
            SendJSON(res, "{""files"":[]}")
            Return
        End If
        Dim files = Directory.GetFiles(dlPath, "*.mp3")
        Dim entries As New StringBuilder("[")
        For i = 0 To files.Length - 1
            Dim info As New FileInfo(files(i))
            Dim sizekb = Math.Round(info.Length / 1024.0, 1)
            entries.Append($"{{""name"":""{EscapeJson(info.Name)}"",""size"":{sizekb}}}")
            If i < files.Length - 1 Then entries.Append(",")
        Next
        entries.Append("]")
        SendJSON(res, $"{{""files"":{entries}}}")
    End Sub

    ' ── File singolo — legge CurrentDownloadPath, ZERO Invoke ────────────────
    Private Sub ServeSingleFile(res As HttpListenerResponse, fileName As String)
        Dim dlPath As String = Form1.CurrentDownloadPath
        If dlPath = "" OrElse dlPath = "Download Path" Then dlPath = My.Settings.option1
        Dim fullPath = Path.Combine(dlPath, Path.GetFileName(Uri.UnescapeDataString(fileName)))
        If Not File.Exists(fullPath) Then
            Send404(res)
            Return
        End If
        res.ContentType = "audio/mpeg"
        res.AddHeader("Content-Disposition", $"attachment; filename=""{Path.GetFileName(fullPath)}""")
        Dim bytes = File.ReadAllBytes(fullPath)
        res.ContentLength64 = bytes.Length
        res.OutputStream.Write(bytes, 0, bytes.Length)
        res.Close()
    End Sub

    ' ── ZIP — legge CurrentDownloadPath, ZERO Invoke ─────────────────────────
    Private Sub ServeZip(res As HttpListenerResponse)
        Dim dlPath As String = Form1.CurrentDownloadPath
        If dlPath = "" OrElse dlPath = "Download Path" Then dlPath = My.Settings.option1
        If Not Directory.Exists(dlPath) Then
            Send404(res)
            Return
        End If
        Dim files = Directory.GetFiles(dlPath, "*.mp3")
        If files.Length = 0 Then
            SendJSON(res, $"{{""error"":""no files""}}", 404)
            Return
        End If
        res.ContentType = "application/zip"
        res.AddHeader("Content-Disposition", "attachment; filename=""yoump3_files.zip""")
        Using zipStream As New ZipArchive(res.OutputStream, ZipArchiveMode.Create, True)
            For Each f In files
                zipStream.CreateEntryFromFile(f, Path.GetFileName(f), CompressionLevel.Fastest)
            Next
        End Using
        res.Close()
    End Sub

    ' ── Registra prefisso netsh (una tantum, richiede UAC) ───────────────────
    Public Shared Sub RegisterPrefix(ip As String)
        Try
            Dim url = $"http://{ip}:{Port}/"
            Dim psi As New ProcessStartInfo("netsh", $"http add urlacl url={url} user=Everyone")
            psi.Verb = "runas"
            psi.UseShellExecute = True
            psi.CreateNoWindow = True
            Dim p = Process.Start(psi)
            p.WaitForExit()
        Catch ex As Exception
            MessageBox.Show($"Could not register URL prefix: {ex.Message}", "YouMP3")
        End Try
    End Sub

    ' ── Helper ───────────────────────────────────────────────────────────────
    Private Sub SendJSON(res As HttpListenerResponse, json As String, Optional statusCode As Integer = 200)
        Dim bytes = Encoding.UTF8.GetBytes(json)
        res.StatusCode = statusCode
        res.ContentType = "application/json; charset=utf-8"
        res.ContentLength64 = bytes.Length
        res.OutputStream.Write(bytes, 0, bytes.Length)
        res.Close()
    End Sub

    Private Sub Send404(res As HttpListenerResponse)
        res.StatusCode = 404
        res.Close()
    End Sub

    Private Function EscapeJson(s As String) As String
        If s Is Nothing Then Return ""
        Return s.Replace("\", "\\").Replace("""", "\""").Replace(vbCr, "").Replace(vbLf, "\n").Replace(vbTab, "\t")
    End Function

    Private Function ExtractJsonValue(json As String, key As String) As String
        Dim pattern = $"""{key}""\s*:\s*""(.*?)"""
        Dim m = System.Text.RegularExpressions.Regex.Match(json, pattern)
        Return If(m.Success, m.Groups(1).Value, "")
    End Function

End Class