<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class settingspage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(settingspage))
        CheckBox1 = New CheckBox()
        CheckBox2 = New CheckBox()
        Label1 = New Label()
        CheckBox3 = New CheckBox()
        CheckBox4 = New CheckBox()
        CheckBox5 = New CheckBox()
        Button1 = New Button()
        SuspendLayout()
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Checked = True
        CheckBox1.CheckState = CheckState.Checked
        CheckBox1.Location = New Point(12, 42)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(152, 24)
        CheckBox1.TabIndex = 0
        CheckBox1.Text = "Auto update ytdlp"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Location = New Point(12, 12)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(116, 24)
        CheckBox2.TabIndex = 1
        CheckBox2.Text = "Save options"
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.FlatStyle = FlatStyle.Flat
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(12, 188)
        Label1.Name = "Label1"
        Label1.Size = New Size(90, 20)
        Label1.TabIndex = 2
        Label1.Text = "Version 1.1.3"
        ' 
        ' CheckBox3
        ' 
        CheckBox3.AutoSize = True
        CheckBox3.Location = New Point(12, 72)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New Size(108, 24)
        CheckBox3.TabIndex = 3
        CheckBox3.Text = "Dark theme"
        CheckBox3.UseVisualStyleBackColor = True
        ' 
        ' CheckBox4
        ' 
        CheckBox4.AutoSize = True
        CheckBox4.Checked = True
        CheckBox4.CheckState = CheckState.Checked
        CheckBox4.Location = New Point(12, 102)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(125, 24)
        CheckBox4.TabIndex = 4
        CheckBox4.Text = "Always on top"
        CheckBox4.UseVisualStyleBackColor = True
        ' 
        ' CheckBox5
        ' 
        CheckBox5.AutoSize = True
        CheckBox5.Location = New Point(12, 132)
        CheckBox5.Name = "CheckBox5"
        CheckBox5.Size = New Size(205, 24)
        CheckBox5.TabIndex = 5
        CheckBox5.Text = "auto close after download"
        CheckBox5.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Location = New Point(12, 156)
        Button1.Name = "Button1"
        Button1.Size = New Size(202, 29)
        Button1.TabIndex = 6
        Button1.Text = "Reset to default settings"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' settingspage
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(226, 217)
        Controls.Add(Button1)
        Controls.Add(CheckBox5)
        Controls.Add(CheckBox4)
        Controls.Add(CheckBox3)
        Controls.Add(Label1)
        Controls.Add(CheckBox2)
        Controls.Add(CheckBox1)
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "settingspage"
        Text = "settings"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents Button1 As Button
End Class
