Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Imaging
Imports QRCoder
Public Class settingspage
    Public Property Name As String
    Public Property IP As String
    Private Class NetworkInterfaceItem
        Public Property Name As String
        Public Property IP As String
    End Class
    Public Overrides Function ToString() As String
        Return Name
    End Function
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            MyBase.BackColor = System.Drawing.ColorTranslator.FromHtml("#28282B")
            CheckBox2.ForeColor = Color.White
            CheckBox3.ForeColor = Color.White
            CheckBox4.ForeColor = Color.White
            CheckBox5.ForeColor = Color.White
            CheckBox1.ForeColor = Color.White
            Label1.ForeColor = Color.White
            Label3.ForeColor = Color.White
            Label4.ForeColor = Color.White
            GroupBox1.ForeColor = Color.White
            Label2.ForeColor = Color.White
            Button1.ForeColor = Color.White
            Button1.BackColor = System.Drawing.ColorTranslator.FromHtml("#28282B")
            Form1.darkmode(True)
        Else
            MyBase.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4")
            CheckBox2.ForeColor = Color.Black
            CheckBox3.ForeColor = Color.Black
            Label1.ForeColor = Color.Black
            Label2.ForeColor = Color.Black
            GroupBox1.ForeColor = Color.Black
            Label3.ForeColor = Color.Black
            Label4.ForeColor = Color.Black
            CheckBox4.ForeColor = Color.Black
            CheckBox5.ForeColor = Color.Black
            CheckBox1.ForeColor = Color.Black
            Button1.ForeColor = Color.Black
            Button1.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4")
            Form1.darkmode(False)
        End If
    End Sub
    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        Form1.TopMost = CheckBox4.Checked
    End Sub
    Private Sub settingspage_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        My.Settings.saveoptions = CheckBox2.Checked
        My.Settings.darkmode = CheckBox3.Checked
        My.Settings.aot = CheckBox4.Checked
        My.Settings.ac = CheckBox5.Checked
        My.Settings.webmanage = CheckBox1.Checked
        If ComboBox1.SelectedItem IsNot Nothing Then
            My.Settings.webip = DirectCast(ComboBox1.SelectedItem, NetworkInterfaceItem).IP
        End If
        My.Settings.Save()
    End Sub

    Private Async Sub settingspage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Form1.ytver
        CheckBox2.Checked = My.Settings.saveoptions
        CheckBox3.Checked = My.Settings.darkmode
        CheckBox4.Checked = My.Settings.aot
        CheckBox5.Checked = My.Settings.ac
        Label1.Text = "App Version: " + Form1.appversion.ToString()
        loadinterfaces()
        CheckBox1.Checked = My.Settings.webmanage
        For i = 0 To ComboBox1.Items.Count - 1
            Dim item = DirectCast(ComboBox1.Items(i), NetworkInterfaceItem)
            If item.IP = My.Settings.webip Then
                ComboBox1.SelectedIndex = i
                Exit For
            End If
        Next
        If ComboBox1.SelectedIndex = -1 AndAlso ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
        End If
        If CheckBox1.Checked Then
            Label3.Text = If(My.Settings.webip <> "", My.Settings.webip, "—")
            Label3.Text &= $":{WebServer.Port}"
        End If

        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
        End If
        PictureBox1.Image = GenerateQRCode()
    End Sub
    Private Sub button1_click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.Reset()
        CheckBox2.Checked = My.Settings.saveoptions
        CheckBox3.Checked = My.Settings.darkmode
        CheckBox4.Checked = My.Settings.aot
        CheckBox5.Checked = My.Settings.ac
    End Sub
    Private Async Function UpdateYTDLP() As Task
        Label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#F9C70C")
        Label2.Text = "Checking ytdlp..."
        Dim procpath = Path.Combine(Application.StartupPath, "yt-dlp.exe")
        If Not File.Exists(procpath) Then
            Label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#B2022F")
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
            Label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#287233")
            Label2.Text = "ytdlp is up to date."
            Button2.Enabled = True
        Else
            Label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#AEA04B")
            Label2.Text = "Updating ytdlp, please wait..."
        End If
        proc.WaitForExit()
        Label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#287233")
        Label2.Text = "Done!"
        Threading.Thread.Sleep(2000)
        Label2.Text = "ytdlp is up to date."
        Button2.Enabled = True
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button2.Enabled = False
        UpdateYTDLP()
    End Sub
    Public Function GenerateQRCode() As Bitmap
        Using qrGenerator As New QRCodeGenerator()
            Using qrCodeData As QRCodeData = qrGenerator.CreateQrCode($"http://{Label3.Text}", QRCodeGenerator.ECCLevel.Q)
                Using qrCode As New QRCode(qrCodeData)
                    Dim qrCodeImage As Bitmap = qrCode.GetGraphic(20)
                    Return New Bitmap(qrCodeImage)
                End Using
            End Using
        End Using
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim selecteditem = DirectCast(ComboBox1.SelectedItem, NetworkInterfaceItem)
        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
        End If
        If CheckBox1.Checked Then
            Label3.Text = selecteditem.IP + ":47891"
        End If
        PictureBox1.Image = GenerateQRCode()
        PictureBox1.Enabled = CheckBox1.Enabled
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ComboBox1.Enabled = CheckBox1.Checked
        PictureBox1.Visible = CheckBox1.Checked
        If CheckBox1.Checked Then
            Dim selectedIP As String = "127.0.0.1"
            If ComboBox1.SelectedItem IsNot Nothing Then
                selectedIP = DirectCast(ComboBox1.SelectedItem, NetworkInterfaceItem).IP
            End If
            Form1.WebSrv.Start(selectedIP)
            Label3.Text = $"{selectedIP}:{WebServer.Port}"
        Else
            Form1.WebSrv.Stop()
            Label3.Text = "Disabled"
        End If
    End Sub
    Private Sub loadinterfaces()
        ComboBox1.Items.Clear()
        Dim interfaces As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
        For Each adapter As NetworkInterface In interfaces
            If adapter.OperationalStatus = OperationalStatus.Up Then
                Dim ips As IPInterfaceProperties = adapter.GetIPProperties()
                For Each ip As UnicastIPAddressInformation In ips.UnicastAddresses
                    If ip.Address.AddressFamily = AddressFamily.InterNetwork Then
                        Dim item As New NetworkInterfaceItem With {
                            .Name = adapter.Name,
                            .IP = ip.Address.ToString()
                        }
                        ComboBox1.Items.Add(item)
                        ComboBox1.DisplayMember = "Name"
                    End If
                Next
            End If
        Next
    End Sub

End Class