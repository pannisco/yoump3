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
        CheckBox2 = New CheckBox()
        Label1 = New Label()
        CheckBox3 = New CheckBox()
        CheckBox4 = New CheckBox()
        CheckBox5 = New CheckBox()
        Button1 = New Button()
        Label2 = New Label()
        Button2 = New Button()
        CheckBox1 = New CheckBox()
        ComboBox1 = New ComboBox()
        GroupBox1 = New GroupBox()
        PictureBox1 = New PictureBox()
        Label3 = New Label()
        GroupBox1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Location = New Point(16, 16)
        CheckBox2.Margin = New Padding(4, 7, 4, 7)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(150, 47)
        CheckBox2.TabIndex = 1
        CheckBox2.Text = "Save options"
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.FlatStyle = FlatStyle.Flat
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(16, 242)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(151, 25)
        Label1.TabIndex = 2
        Label1.Text = "App version: 1.1.5"
        ' 
        ' CheckBox3
        ' 
        CheckBox3.AutoSize = True
        CheckBox3.Location = New Point(15, 53)
        CheckBox3.Margin = New Padding(4, 7, 4, 7)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New Size(141, 47)
        CheckBox3.TabIndex = 3
        CheckBox3.Text = "Dark theme"
        CheckBox3.UseVisualStyleBackColor = True
        ' 
        ' CheckBox4
        ' 
        CheckBox4.AutoSize = True
        CheckBox4.Checked = True
        CheckBox4.CheckState = CheckState.Checked
        CheckBox4.Location = New Point(16, 92)
        CheckBox4.Margin = New Padding(4, 7, 4, 7)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(160, 47)
        CheckBox4.TabIndex = 4
        CheckBox4.Text = "Always on top"
        CheckBox4.UseVisualStyleBackColor = True
        ' 
        ' CheckBox5
        ' 
        CheckBox5.AutoSize = True
        CheckBox5.Location = New Point(16, 131)
        CheckBox5.Margin = New Padding(4, 7, 4, 7)
        CheckBox5.Name = "CheckBox5"
        CheckBox5.Size = New Size(261, 47)
        CheckBox5.TabIndex = 5
        CheckBox5.Text = "Auto close after download"
        CheckBox5.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Location = New Point(16, 192)
        Button1.Margin = New Padding(4, 7, 4, 7)
        Button1.Name = "Button1"
        Button1.Size = New Size(252, 43)
        Button1.TabIndex = 6
        Button1.Text = "Reset to default settings"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(16, 267)
        Label2.Name = "Label2"
        Label2.Size = New Size(135, 43)
        Label2.TabIndex = 7
        Label2.Text = "YtDLP version:"
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.Gainsboro
        Button2.BackgroundImage = My.Resources.Resources.update_30dp_000000_FILL0_wght400_GRAD0_opsz24
        Button2.BackgroundImageLayout = ImageLayout.Zoom
        Button2.FlatAppearance.BorderColor = Color.Black
        Button2.FlatStyle = FlatStyle.Flat
        Button2.ForeColor = Color.Transparent
        Button2.Location = New Point(21, 296)
        Button2.Name = "Button2"
        Button2.Size = New Size(34, 33)
        Button2.TabIndex = 8
        Button2.UseVisualStyleBackColor = False
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(27, 29)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(100, 47)
        CheckBox1.TabIndex = 9
        CheckBox1.Text = "Enable"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.Enabled = False
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(27, 70)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(194, 51)
        ComboBox1.TabIndex = 10
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(PictureBox1)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(ComboBox1)
        GroupBox1.Controls.Add(CheckBox1)
        GroupBox1.Location = New Point(284, 11)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(249, 385)
        GroupBox1.TabIndex = 11
        GroupBox1.TabStop = False
        GroupBox1.Text = "Web manage"
        GroupBox1.Visible = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(27, 155)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(194, 177)
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        PictureBox1.TabIndex = 12
        PictureBox1.TabStop = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(27, 121)
        Label3.Name = "Label3"
        Label3.Size = New Size(90, 43)
        Label3.TabIndex = 11
        Label3.Text = "Disabled"
        ' 
        ' settingspage
        ' 
        AutoScaleDimensions = New SizeF(10F, 43F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(282, 338)
        Controls.Add(GroupBox1)
        Controls.Add(Button2)
        Controls.Add(Label2)
        Controls.Add(Button1)
        Controls.Add(CheckBox5)
        Controls.Add(CheckBox4)
        Controls.Add(CheckBox3)
        Controls.Add(Label1)
        Controls.Add(CheckBox2)
        Font = New Font("Google Sans", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 7, 4, 7)
        MaximizeBox = False
        MinimizeBox = False
        Name = "settingspage"
        Text = "settings"
        TopMost = True
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
