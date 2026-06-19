Imports System.Net
Imports Ionic.Zip
Imports System.Diagnostics
Imports System.IO
Imports System.Xml
Public Class Form1
    Private WithEvents client As WebClient
    Private latestVersion As String = ""
    Private updVersion As String = "1.1"
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
            Dim updlatest As String = GetTagValue(data, "updtversion")
            Dim updlink As String = GetTagValue(data, "updtlink")
            If updlatest > updVersion Then
                SelfUpdateUpdater(updlink)
                Return
            End If

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

    Private Sub SelfUpdateUpdater(updlink As String)
        Try
            Dim tempFolder As String = Path.Combine(Path.GetTempPath(), "UpdaterSelfUpdate_" & Guid.NewGuid().ToString("N"))
            Dim updZipPath As String = Path.Combine(Path.GetTempPath(), "updater_new.zip")
            Dim extractFolder As String = Path.Combine(tempFolder, "extracted")
            Dim batchPath As String = Path.Combine(Path.GetTempPath(), "update_updater_" & Guid.NewGuid().ToString("N") & ".bat")

            Dim currentUpdaterFolder As String = Application.StartupPath
            Dim currentUpdaterExeName As String = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName)
            Dim currentUpdaterExePath As String = Path.Combine(currentUpdaterFolder, currentUpdaterExeName)
            Dim currentPid As Integer = Process.GetCurrentProcess().Id

            Directory.CreateDirectory(tempFolder)
            Directory.CreateDirectory(extractFolder)

            Using wc As New WebClient()
                wc.DownloadFile(New Uri(updlink), updZipPath)
            End Using

            Using zip As ZipFile = ZipFile.Read(updZipPath)
                For Each entry As ZipEntry In zip
                    entry.Extract(extractFolder, ExtractExistingFileAction.OverwriteSilently)
                Next
            End Using

            CreateUpdateBatchScript(batchPath, currentPid, extractFolder, currentUpdaterFolder, currentUpdaterExePath, tempFolder, updZipPath)


            Dim psi As New ProcessStartInfo() With {
                .FileName = batchPath,
                .WindowStyle = ProcessWindowStyle.Hidden,
                .CreateNoWindow = True,
                .UseShellExecute = True
            }
            Process.Start(psi)

            Environment.Exit(0)

        Catch ex As Exception
            MessageBox.Show("Errore durante l'aggiornamento dell'updater: " & ex.Message)
        End Try
    End Sub
    Private Sub CreateUpdateBatchScript(batchPath As String,
                                          pidToWaitFor As Integer,
                                          sourceFolder As String,
                                          destinationFolder As String,
                                          exeToRestart As String,
                                          tempFolderToDelete As String,
                                          zipToDelete As String)

        Dim sb As New System.Text.StringBuilder()

        sb.AppendLine("@echo off")
        sb.AppendLine("setlocal")
        sb.AppendLine()
        sb.AppendLine(":waitloop")
        sb.AppendLine($"tasklist /FI ""PID eq {pidToWaitFor}"" 2>NUL | find /I ""{pidToWaitFor}"" >NUL")
        sb.AppendLine("if not errorlevel 1 (")
        sb.AppendLine("    timeout /t 1 /nobreak > nul")
        sb.AppendLine("    goto waitloop")
        sb.AppendLine(")")
        sb.AppendLine()
        sb.AppendLine("timeout /t 1 /nobreak > nul")
        sb.AppendLine()
        sb.AppendLine($"xcopy ""{sourceFolder}\*"" ""{destinationFolder}\"" /E /Y /I /Q")
        sb.AppendLine()
        sb.AppendLine($"rmdir /S /Q ""{tempFolderToDelete}""")
        sb.AppendLine($"del /F /Q ""{zipToDelete}""")
        sb.AppendLine()
        sb.AppendLine($"start """" ""{exeToRestart}""")
        sb.AppendLine()
        sb.AppendLine("(goto) 2>nul & del ""%~f0""")
        File.WriteAllText(batchPath, sb.ToString(), System.Text.Encoding.Default)
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