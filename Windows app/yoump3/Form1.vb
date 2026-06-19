Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks

Public Class Form1
    Private temp As Boolean = True
    Private temp01 As Boolean = True
    Private temp02 As Boolean = True
    Public WebSrv As New WebServer()
    Public Const appversion As String = "1.2.6"
    Private Const updateurl As String = "https://raw.githubusercontent.com/pannisco/yoump3/refs/heads/main/update.xml"
    Private ffmpegdir As String = Path.Join(Application.StartupPath, "ffmpeg").ToString()
    Public IsDownloading As Boolean = False
    Public CurrentLog As String = ""
    Public CurrentProgress As String = "0/0"
    Public CurrentDownloadPath As String = ""
    Public PendingDownloadPath As String = ""

    ' ── Form Load / Shown ────────────────────────────────────────────────────
    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        darkmode(My.Settings.darkmode)
        If My.Settings.saveoptions = True Then
            TextBox2.Text = My.Settings.option1
        End If
        ' Inizializza CurrentDownloadPath subito
        If My.Settings.option1 <> "" Then
            CurrentDownloadPath = My.Settings.option1
        End If
        If My.Settings.webmanage = True Then
            Dim ip As String = If(My.Settings.webip = "", "127.0.0.1", My.Settings.webip)
            WebSrv.Start(ip)
        End If
        MyBase.TopMost = My.Settings.aot
        Await Task.Delay(500)
        Task.Run(Sub() Getver())
        CheckForUpdate()
    End Sub

    Private Async Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim args() As String = Environment.GetCommandLineArgs()
        If args.Length > 1 Then
            Dim rawArg As String = args(1)
            TextBox1.Text = Uri.UnescapeDataString(rawArg.Replace("yoump3:", ""))
            If TextBox2.Text = "" Or TextBox2.Text = "Download Path" Then
                MessageBox.Show("Please enter a download path or enable option save in settings.", "YouMP3", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                download()
            End If
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        If My.Settings.saveoptions = True Then
            My.Settings.option1 = TextBox2.Text
        End If
        WebSrv.Stop()
        Application.Exit()
    End Sub

    ' ── TextBox handlers ─────────────────────────────────────────────────────
    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        If temp = True Then
            TextBox1.Text = ""
            temp = False
        End If
    End Sub

    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        If temp01 = True Then
            TextBox2.Text = ""
            temp01 = False
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text <> "Download Path" AndAlso TextBox2.Text <> "" Then
            CurrentDownloadPath = TextBox2.Text
        End If
    End Sub

    Sub stopdwld()
        For Each proc As Process In Process.GetProcessesByName("yt-dlp")
            proc.Kill()
        Next
        temp02 = True
        Button1.Text = "Download"
        TextBox1.Text = "URL"
        TextBox2.Text = "Download Path"
        temp = True
        temp01 = True
        Label4.Text = "0/0"
        IsDownloading = False
        CurrentLog = ""
        CurrentProgress = "0/0"
        Button1.Enabled = True
        Button2.Enabled = True
    End Sub
    ' ── Buttons ──────────────────────────────────────────────────────────────
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If temp02 = False Then
            stopdwld()
        Else
            If TextBox1.Text = "" Or TextBox1.Text = "URL" Or TextBox2.Text = "" Or TextBox2.Text = "Download Path" Then
                MessageBox.Show("Please fill all requests.")
            Else
                download()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FolderBrowserDialog1.Description = "Select download folder"
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            TextBox2.Text = FolderBrowserDialog1.SelectedPath
            CurrentDownloadPath = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        settingspage.Show()
    End Sub


    ' ── Download core ────────────────────────────────────────────────────────
    Async Sub download()
        ' Cattura subito i valori — prima di qualsiasi await
        Dim urlToDownload As String = TextBox1.Text
        Dim pathToDownload As String = TextBox2.Text

        IsDownloading = True
        CurrentLog = "Starting download..."
        CurrentProgress = "0/0"
        RichTextBox1.Text = "Starting download..." & Environment.NewLine
        Button1.Text = "STOP"
        temp02 = False
        Button1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#B2022F")
        Button2.Enabled = False

        Dim args As String = $"-f bestaudio --extract-audio --ffmpeg-location ""{ffmpegdir}"" --audio-format mp3 --audio-quality 0 --embed-thumbnail --add-metadata -o ""%(artist)s - %(title)s.%(ext)s"" -P ""{pathToDownload}"" {urlToDownload}"

        Await Task.Run(Sub() RunProcessLive(args))

        ' Download completato
        CurrentLog = "Download finished."
        CurrentProgress = "0/0"
        IsDownloading = False
        RichTextBox1.Text = "Download finished." & Environment.NewLine
        Button1.Text = "Done!"
        Button1.Enabled = True
        Button2.Enabled = True
        Threading.Thread.Sleep(1000)
        Button1.Text = "Download"
        If My.Settings.darkmode = True Then
            Button1.ForeColor = Color.White
        Else
            Button1.ForeColor = Color.Black
        End If
        TextBox1.Text = "URL"
        temp = True
        temp01 = True
        temp02 = True
        Label4.Text = "0/0"
        MessageBox.Show("Download completed!", "YouMP3", MessageBoxButtons.OK, MessageBoxIcon.Information)
        If My.Settings.ac = True Then
            Application.Exit()
        End If
    End Sub

    Private Sub RunProcessLive(args As String)
        Dim proc As New Process()
        proc.StartInfo.FileName = Path.Combine(Application.StartupPath, "yt-dlp.exe")
        proc.StartInfo.Arguments = args
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardError = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        AddHandler proc.OutputDataReceived, Sub(s, e)
                                                If e.Data IsNot Nothing Then
                                                    CurrentLog = e.Data
                                                    UpdateDownloadCount(e.Data)
                                                    If Me.IsHandleCreated Then
                                                        Invoke(Sub()
                                                                   RichTextBox1.Text = e.Data
                                                               End Sub)
                                                    End If
                                                End If
                                            End Sub
        AddHandler proc.ErrorDataReceived, Sub(s, e)
                                               If e.Data IsNot Nothing Then
                                                   CurrentLog = e.Data
                                                   UpdateDownloadCount(e.Data)
                                                   If Me.IsHandleCreated Then
                                                       Invoke(Sub()
                                                                  RichTextBox1.Text = e.Data
                                                              End Sub)
                                                   End If
                                               End If
                                           End Sub
        proc.Start()
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()
        proc.WaitForExit()
    End Sub

    Private Sub UpdateDownloadCount(line As String)
        Dim match = Regex.Match(line.ToLower(), "(video|item|downloading)\s+(\d+)\s+of\s+(\d+)")
        If match.Success Then
            Dim current = Integer.Parse(match.Groups(2).Value)
            Dim total = Integer.Parse(match.Groups(3).Value)
            CurrentProgress = $"{current}/{total}"
            If Me.IsHandleCreated Then
                Invoke(Sub()
                           Label4.Text = CurrentProgress
                       End Sub)
            End If
        End If
    End Sub

    ' ── Web download pubblici ────────────────────────────────────────────────
    Public Sub StartDownloadFromWeb(url As String, Optional dlPath As String = "")
        If IsDownloading Then
            CurrentLog = "Already downloading."
            Return
        End If

        Dim finalPath As String = ""
        If dlPath <> "" Then
            finalPath = dlPath
        ElseIf CurrentDownloadPath <> "" AndAlso CurrentDownloadPath <> "Download Path" Then
            finalPath = CurrentDownloadPath
        ElseIf TextBox2.Text <> "" AndAlso TextBox2.Text <> "Download Path" Then
            finalPath = TextBox2.Text
        End If

        If finalPath = "" Then
            CurrentLog = "ERROR: No download path. Set it in the app first."
            Return
        End If

        ' Setta tutto PRIMA di chiamare download() per evitare race condition
        TextBox1.Text = url
        TextBox2.Text = finalPath
        CurrentDownloadPath = finalPath
        temp = False
        temp01 = False
        temp02 = False
        IsDownloading = True
        CurrentLog = $"Starting: {url}"
        download()
    End Sub

    Public Sub StopDownloadFromWeb()
        If Not IsDownloading Then Return
        For Each proc As Process In Process.GetProcessesByName("yt-dlp")
            proc.Kill()
        Next
        IsDownloading = False
        CurrentLog = ""
        CurrentProgress = "0/0"
        Button1.Text = "Download"
        TextBox1.Text = "URL"
        temp = True
        temp01 = True
        temp02 = True
        Label4.Text = "0/0"
        Button1.Enabled = True
        Button2.Enabled = True
    End Sub


    ' ── Update ───────────────────────────────────────────────────────────────
    Public Sub CheckForUpdate()
        Try
            Dim client As New WebClient()
            Dim data As String = client.DownloadString(updateurl)
            Dim remoteVersion As String = GetTagValue(data, "version")
            Dim updateLink As String = GetTagValue(data, "link")
            Dim forceUpdate As Boolean = Boolean.Parse(GetTagValue(data, "forceupdate"))
            If IsNewerVersion(remoteVersion, appversion) Then
                Dim msg As String = $"New version available ({remoteVersion})." & vbCrLf & "Do you want to update now?"
                Dim result As DialogResult = MessageBox.Show(msg, "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If result = DialogResult.Yes Then
                    Try
                        Dim startInfo As New ProcessStartInfo()
                        With startInfo
                            .FileName = Path.Combine(Application.StartupPath, "youpdater.exe")
                            .CreateNoWindow = True
                            .UseShellExecute = True
                            .Verb = "runas"
                        End With
                        Process.Start(startInfo)
                    Catch ex As Exception
                        MessageBox.Show("Unable to open update: " & ex.Message)
                    End Try
                    Application.Exit()
                    Return
                Else
                    If forceUpdate Then
                        MessageBox.Show("This update is required to continue using the app.", "Update required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Application.Exit()
                        Return
                    Else
                        Me.Opacity = 100
                    End If
                End If
            Else
                Me.Opacity = 100
            End If
        Catch ex As Exception
            MessageBox.Show("Error during update check: " & ex.Message)
        End Try
    End Sub

    Private Function GetTagValue(source As String, tag As String) As String
        Dim pattern As String = $"<{tag}>(.*?)</{tag}>"
        Dim match As Match = Regex.Match(source, pattern)
        Return If(match.Success, match.Groups(1).Value.Trim(), "")
    End Function

    Private Function IsNewerVersion(remote As String, local As String) As Boolean
        Try
            Return New Version(remote) > New Version(local)
        Catch
            Return False
        End Try
    End Function

    ' ── YtDLP version ────────────────────────────────────────────────────────
    Public ytver As String
    Private Sub Getver()
        Dim proc As New Process()
        proc.StartInfo.FileName = Path.Combine(Application.StartupPath, "yt-dlp.exe")
        proc.StartInfo.Arguments = "--version"
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardError = True
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        AddHandler proc.OutputDataReceived, Sub(s, e)
                                                If e.Data IsNot Nothing Then
                                                    Invoke(Sub() ytver = "YtDLP version: " & e.Data)
                                                End If
                                            End Sub
        AddHandler proc.ErrorDataReceived, Sub(s, e)
                                               If e.Data IsNot Nothing Then
                                                   Invoke(Sub() ytver = e.Data)
                                               End If
                                           End Sub
        proc.Start()
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()
        proc.WaitForExit()
    End Sub

    ' ── Dark mode ────────────────────────────────────────────────────────────
    Sub darkmode(mode As Boolean)
        If mode = True Then
            MyBase.BackColor = System.Drawing.ColorTranslator.FromHtml("#28282B")
            Label4.ForeColor = Color.White
            Button2.BackColor = System.Drawing.ColorTranslator.FromHtml("#28282B")
            Button2.ForeColor = Color.White
            Button3.BackColor = System.Drawing.ColorTranslator.FromHtml("#28282B")
            Button3.ForeColor = Color.White
            TextBox1.ForeColor = Color.White
            TextBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#28282B")
            TextBox2.ForeColor = Color.White
            TextBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#28282B")
            RichTextBox1.ForeColor = Color.White
            RichTextBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#28282B")
        Else
            MyBase.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4")
            Label4.ForeColor = Color.Black
            Button2.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4")
            Button2.ForeColor = Color.Black
            Button3.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4")
            Button3.ForeColor = Color.Black
            TextBox1.ForeColor = Color.Black
            TextBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4")
            TextBox2.ForeColor = Color.Black
            TextBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4")
            RichTextBox1.ForeColor = Color.Black
            RichTextBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4")
        End If
    End Sub

    ' ── RichTextBox progress ─────────────────────────────────────────────────
    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        If RichTextBox1.Text.Contains("[youtube]") Then
            ProgressBar1.Value = 15
        ElseIf RichTextBox1.Text.Contains("[info]") Then
            ProgressBar1.Value = 25
        ElseIf RichTextBox1.Text.Contains("[download]") Then
            ProgressBar1.Value = 50
        ElseIf RichTextBox1.Text.Contains("[ExtractAudio]") Then
            ProgressBar1.Value = 75
        ElseIf RichTextBox1.Text.Contains("[Metadata]") Then
            ProgressBar1.Value = 95
        ElseIf RichTextBox1.Text.Contains("Download finished.") Then
            ProgressBar1.Value = 0
        End If
    End Sub

    ' ── Label link ───────────────────────────────────────────────────────────
    Private Sub Label3_Click(sender As Object, e As EventArgs)
        Try
            Process.Start(New ProcessStartInfo With {
                .FileName = "https://github.com/pannisco/yoump3",
                .UseShellExecute = True
            })
        Catch ex As Exception
            MessageBox.Show("Unable to open link: " & ex.Message)
        End Try
    End Sub

    Sub reloadsettings()
        My.Settings.Reload()
    End Sub

End Class