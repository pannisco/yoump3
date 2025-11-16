Imports System.Net
Imports Ionic.Zip
Imports System.Diagnostics
Imports System.IO
Imports System.Xml
Public Class Form1
    Private WithEvents client As WebClient
    Private latestVersion As String = ""
    Private zipPath As String = Path.Combine(Application.StartupPath, "temp.zip")
    Private updateurl As String = "https://raw.githubusercontent.com/pannisco/yoump3/refs/heads/main/update.xml"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getdata()
    End Sub

    Private Sub DownloadProgress(sender As Object, e As DownloadProgressChangedEventArgs)
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub DownloadCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)
        Try
            If e.Error IsNot Nothing Then
                MessageBox.Show("Download error: " & e.Error.Message)
                Exit Sub
            End If
            Using zip As ZipFile = ZipFile.Read(zipPath)
                For Each entry In zip
                    entry.Extract(Application.StartupPath, ExtractExistingFileAction.OverwriteSilently)
                Next
            End Using
            File.Delete(zipPath)
            Process.Start(Path.Combine(Application.StartupPath, "yoump3.exe"))
            Me.Opacity = 0
            MessageBox.Show("YouMP3 updated to version " & latestVersion & "!", "Update")
            End
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        ProgressBar1.Value = 0
    End Sub
    Public Sub getdata()
        Try
            Dim client As New WebClient()
            Dim data As String = client.DownloadString(updateurl)
            latestVersion = GetTagValue(data, "version")
            Dim updateLink As String = GetTagValue(data, "link")
            Dim forceUpdate As Boolean = Boolean.Parse(GetTagValue(data, "forceupdate"))
            ProgressBar1.Value = 0
            client = New WebClient()
            AddHandler client.DownloadProgressChanged, AddressOf DownloadProgress
            AddHandler client.DownloadFileCompleted, AddressOf DownloadCompleted
            client.DownloadFileAsync(New Uri(updateLink), zipPath)
            Return
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
End Class
