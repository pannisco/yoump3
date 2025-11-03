Public Class settingspage
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            MyBase.BackColor = Color.Black
            CheckBox1.ForeColor = Color.White
            CheckBox2.ForeColor = Color.White
            CheckBox3.ForeColor = Color.White
            CheckBox4.ForeColor = Color.White
            CheckBox5.ForeColor = Color.White
            Label1.ForeColor = Color.White
            Button1.ForeColor = Color.White
            Button1.BackColor = Color.Black
            Form1.darkmode(True)
        Else
            MyBase.BackColor = Color.White
            CheckBox1.ForeColor = Color.Black
            CheckBox2.ForeColor = Color.Black
            CheckBox3.ForeColor = Color.Black
            Label1.ForeColor = Color.Black
            CheckBox4.ForeColor = Color.Black
            CheckBox5.ForeColor = Color.Black
            Button1.ForeColor = Color.Black
            Button1.BackColor = Color.White
            Form1.darkmode(False)
        End If
    End Sub
    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        Form1.TopMost = CheckBox4.Checked
    End Sub
    Private Sub settingspage_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        My.Settings.saveoptions = CheckBox2.Checked
        My.Settings.autoupdate = CheckBox1.Checked
        My.Settings.darkmode = CheckBox3.Checked
        My.Settings.aot = CheckBox4.Checked
        My.Settings.ac = CheckBox5.Checked
        My.Settings.Save()
    End Sub
    Private Sub settingspage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox2.Checked = My.Settings.saveoptions
        CheckBox1.Checked = My.Settings.autoupdate
        CheckBox3.Checked = My.Settings.darkmode
        CheckBox4.Checked = My.Settings.aot
        CheckBox5.Checked = My.Settings.ac
    End Sub
    Private Sub button1_click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.Reset()
        CheckBox2.Checked = My.Settings.saveoptions
        CheckBox1.Checked = My.Settings.autoupdate
        CheckBox3.Checked = My.Settings.darkmode
        CheckBox4.Checked = My.Settings.aot
        CheckBox5.Checked = My.Settings.ac
    End Sub
End Class