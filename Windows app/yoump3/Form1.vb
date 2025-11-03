Imports System.Diagnostics
Imports System.IO
Imports System.Security.Policy
Imports System.Threading.Tasks
Imports System.Xml
Imports System.Net
Imports System.Text.RegularExpressions

Public Class Form1
    Private temp As Boolean = True
    Private temp01 As Boolean = True
    Private temp02 As Boolean = True
    Private Const appversion As String = "1.0.1"
    Private Const updateurl As String = "https://raw.githubusercontent.com/pannisco/yoump3/refs/heads/main/update.xml"
    Private Async Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim args() As String = Environment.GetCommandLineArgs()
        If args.Length > 1 Then
            Dim rawArg As String = args(1)
            TextBox1.Text = Uri.UnescapeDataString(rawArg.Replace("yoump3:", ""))
            If TextBox2.Text = "" Or TextBox2.Text = "Download Path" Then
                MessageBox.Show("Please enter a download path or enable option save in settings.")
            Else
                download()
            End If
        End If
        Await Task.Delay(500)
        Await CheckFFMPEGAsync()
        Await CheckYTDLPAsync()
    End Sub

    Private Async Function CheckFFMPEGAsync() As Task
        Try
            Dim proc As New Process()
            proc.StartInfo.FileName = "ffmpeg"
            proc.StartInfo.Arguments = "-version"
            proc.StartInfo.RedirectStandardOutput = True
            proc.StartInfo.RedirectStandardError = True
            proc.StartInfo.UseShellExecute = False
            proc.StartInfo.CreateNoWindow = True
            proc.Start()
            Dim output As String = Await proc.StandardOutput.ReadToEndAsync()
            proc.WaitForExit()
            If output.ToLower().Contains("ffmpeg version") Then
                Label1.Text = "ffmpeg installed."
                Label1.ForeColor = Color.DarkGreen
            Else
                Label1.Text = "ffmpeg not installed."
                Label1.ForeColor = Color.DarkRed
            End If
        Catch ex As Exception
            Label1.Text = "ffmpeg not installed."
            Label1.ForeColor = Color.DarkRed
        End Try
    End Function

    Private Async Function CheckYTDLPAsync() As Task
        If My.Settings.autoupdate = True Then
            Label2.ForeColor = Color.DarkGoldenrod
            Label2.Text = "Checking ytdlp..."
            Dim procpath = Path.Combine(Application.StartupPath, "yt-dlp.exe")
            If Not File.Exists(procpath) Then
                Label2.ForeColor = Color.DarkRed
                Label2.Text = "ytdlp not accesible."
                Return
            End If
            Dim proc As New Process()
            proc.StartInfo.FileName = procpath
            proc.StartInfo.Arguments = "-U"
            proc.StartInfo.RedirectStandardOutput = True
            proc.StartInfo.RedirectStandardError = True
            proc.StartInfo.UseShellExecute = False
            proc.StartInfo.CreateNoWindow = True
            proc.Start()
            Dim output As String = proc.StandardOutput.ReadToEnd() & proc.StandardError.ReadToEnd()
            If output.Contains("up to date") Then
                Label2.ForeColor = Color.DarkGreen
                Label2.Text = "ytdlp is up to date."
                Button1.Enabled = True
                Button2.Enabled = True
            Else
                Label2.ForeColor = Color.LightGoldenrodYellow
                Label2.Text = "Updating ytdlp, please whait..."
            End If
            proc.WaitForExit()
            Label2.ForeColor = Color.DarkGreen
            Label2.Text = "Done!"
            Threading.Thread.Sleep(2000)
            Label2.Text = "ytdlp is up to date."
            Button1.Enabled = True
            Button2.Enabled = True
        Else
            Label2.ForeColor = Color.DarkGoldenrod
            Label2.Text = "Auto update disabled"
            Button1.Enabled = True
            Button2.Enabled = True
        End If
    End Function
    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        If temp01 = True Then
            TextBox2.Text = ""
            temp01 = False
        End If
    End Sub
    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        If temp = True Then
            TextBox1.Text = ""
            temp = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FolderBrowserDialog1.Description = "Select download folder"
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            TextBox2.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If temp02 = False Then
            For Each proc As Process In Process.GetProcessesByName("yt-dlp")
                proc.Kill()
            Next
            temp02 = True
            Button1.Text = "Download"
            Button1.ForeColor = Color.Black
            TextBox1.Text = "URL"
            TextBox1.Text = "Download Path"
            temp = True
            temp01 = True
            Label4.Text = "0/0"
            Button1.Enabled = True
            Button2.Enabled = True
        Else
            If TextBox1.Text = "" Or TextBox1.Text = "URL" Or TextBox2.Text = "" Or TextBox2.Text = "Download Path" Then
                MessageBox.Show("Please fill all reqests.")
            Else
                download()
            End If
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
                                                    Invoke(Sub() RichTextBox1.Text = e.Data)
                                                    UpdateDownloadCount(e.Data)
                                                End If
                                            End Sub
        AddHandler proc.ErrorDataReceived, Sub(s, e)
                                               If e.Data IsNot Nothing Then
                                                   Invoke(Sub() RichTextBox1.Text = e.Data)
                                                   UpdateDownloadCount(e.Data)
                                               End If
                                           End Sub
        proc.Start()
        proc.BeginOutputReadLine()
        proc.BeginErrorReadLine()
        proc.WaitForExit()
    End Sub
    Private Sub UpdateDownloadCount(line As String)
        Dim match = System.Text.RegularExpressions.Regex.Match(
        line.ToLower(),
        "(video|item|downloading)\s+(\d+)\s+of\s+(\d+)"
    )

        If match.Success Then
            Dim current = Integer.Parse(match.Groups(2).Value)
            Dim total = Integer.Parse(match.Groups(3).Value)
            Invoke(Sub()
                       Label4.Text = $"{current}/{total}"
                   End Sub)
        End If
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Me.Size = New Size(543, 243)
        Else
            Me.Size = New Size(258, 243)
        End If
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        settingspage.Show()
    End Sub
    Public Sub CheckForUpdate()
        Try
            Dim client As New WebClient()
            Dim data As String = client.DownloadString(updateurl)
            Dim remoteVersion As String = GetTagValue(data, "version")
            Dim updateLink As String = GetTagValue(data, "link")
            Dim forceUpdate As Boolean = Boolean.Parse(GetTagValue(data, "forceupdate"))
            If IsNewerVersion(remoteVersion, appversion) Then
                Dim msg As String = $"New version available ({remoteVersion})." & vbCrLf &
                                "Do you want to update now?"
                Dim result As DialogResult = MessageBox.Show(msg, "Update Available",
                                                         MessageBoxButtons.YesNo,
                                                         MessageBoxIcon.Information)
                If result = DialogResult.Yes Then
                    Try
                        Process.Start(New ProcessStartInfo With {
        .FileName = updateLink,
        .UseShellExecute = True
    })
                    Catch ex As Exception
                        MessageBox.Show("Unable to open update link: " & ex.Message)
                    End Try
                    Application.Exit()
                    Return
                Else
                    If forceUpdate Then
                        MessageBox.Show("This update is required to continue using the app.", "Update required",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        Dim match As System.Text.RegularExpressions.Match =
    System.Text.RegularExpressions.Regex.Match(source, pattern)
        If match.Success Then
            Return match.Groups(1).Value.Trim()
        Else
            Return ""
        End If
    End Function

    Private Function IsNewerVersion(remote As String, local As String) As Boolean
        Try
            Dim vRemote As Version = New Version(remote)
            Dim vLocal As Version = New Version(local)
            Return vRemote > vLocal
        Catch
            Return False
        End Try
    End Function
    Private Async Sub download()
        RichTextBox1.Text = "Starting download..." & Environment.NewLine
        Button1.Text = "STOP"
        temp02 = False
        Button1.ForeColor = Color.DarkRed
        Button2.Enabled = False
        Dim args As String = $"-f bestaudio --extract-audio --audio-format mp3 --audio-quality 0 --embed-thumbnail --add-metadata -o ""%(artist)s - %(title)s.%(ext)s"" -P ""{TextBox2.Text}"" {TextBox1.Text}"
        Await Task.Run(Sub() RunProcessLive(args))
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
        TextBox2.Text = "Download Path"
        temp = True
        temp01 = True
        Label4.Text = "0/0"
        If My.Settings.ac = True Then
            Application.Exit()
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        darkmode(My.Settings.darkmode)
        If My.Settings.saveoptions = True Then
            TextBox2.Text = My.Settings.option1
            CheckBox1.Checked = My.Settings.option2
        End If
        MyBase.TopMost = My.Settings.aot
        CheckForUpdate()
    End Sub
    Sub reloadsettings()
        My.Settings.Reload()
    End Sub
    Private Sub Form1_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        If My.Settings.saveoptions = True Then
            My.Settings.option1 = TextBox2.Text
            My.Settings.option2 = CheckBox1.Checked
        End If
        Application.Exit()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Try
            Process.Start(New ProcessStartInfo With {
.FileName = "https://github.com/pannisco/yoump3",
.UseShellExecute = True
})
        Catch ex As Exception
            MessageBox.Show("Unable to open update link: " & ex.Message)
        End Try
    End Sub
    Sub darkmode(mode As Boolean)
        If mode = True Then
            MyBase.BackColor = Color.Black
            Label3.ForeColor = Color.White
            Label4.ForeColor = Color.White
            CheckBox1.ForeColor = Color.White
            Button1.BackColor = Color.Black
            Button1.ForeColor = Color.White
            Button2.BackColor = Color.Black
            Button2.ForeColor = Color.White
            Button3.BackColor = Color.Black
            Button3.ForeColor = Color.White
            TextBox1.ForeColor = Color.White
            TextBox1.BackColor = Color.Black
            TextBox2.ForeColor = Color.White
            TextBox2.BackColor = Color.Black
            RichTextBox1.ForeColor = Color.White
            RichTextBox1.BackColor = Color.Black
            GroupBox1.ForeColor = Color.White
            GroupBox1.BackColor = Color.Black
        Else
            MyBase.BackColor = Color.White
            Label3.ForeColor = Color.Black
            Label4.ForeColor = Color.Black
            CheckBox1.ForeColor = Color.Black
            Button1.BackColor = Color.White
            Button1.ForeColor = Color.Black
            Button2.BackColor = Color.White
            Button2.ForeColor = Color.Black
            Button3.BackColor = Color.White
            Button3.ForeColor = Color.Black
            TextBox1.ForeColor = Color.Black
            TextBox1.BackColor = Color.White
            TextBox2.ForeColor = Color.Black
            TextBox2.BackColor = Color.White
            RichTextBox1.ForeColor = Color.Black
            RichTextBox1.BackColor = Color.White
            GroupBox1.ForeColor = Color.Black
            GroupBox1.BackColor = Color.White
        End If
    End Sub
End Class
