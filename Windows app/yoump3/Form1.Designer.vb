<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Label1 = New Label()
        Label2 = New Label()
        Button1 = New Button()
        TextBox1 = New TextBox()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        TextBox2 = New TextBox()
        Button2 = New Button()
        Label3 = New Label()
        RichTextBox1 = New RichTextBox()
        Label4 = New Label()
        GroupBox1 = New GroupBox()
        CheckBox1 = New CheckBox()
        Button3 = New Button()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 130)
        Label1.Name = "Label1"
        Label1.Size = New Size(72, 20)
        Label1.TabIndex = 0
        Label1.Text = "Loading..."
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 110)
        Label2.Name = "Label2"
        Label2.Size = New Size(72, 20)
        Label2.TabIndex = 1
        Label2.Text = "Loading..."
        ' 
        ' Button1
        ' 
        Button1.Enabled = False
        Button1.Location = New Point(12, 78)
        Button1.Name = "Button1"
        Button1.Size = New Size(180, 29)
        Button1.TabIndex = 2
        Button1.Text = "Download"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(12, 12)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(180, 27)
        TextBox1.TabIndex = 3
        TextBox1.Text = "URL"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(12, 45)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(180, 27)
        TextBox2.TabIndex = 4
        TextBox2.Text = "Download Path"
        ' 
        ' Button2
        ' 
        Button2.Enabled = False
        Button2.Location = New Point(198, 45)
        Button2.Name = "Button2"
        Button2.Size = New Size(30, 27)
        Button2.TabIndex = 5
        Button2.Text = "..."
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(12, 163)
        Label3.Name = "Label3"
        Label3.Size = New Size(137, 20)
        Label3.TabIndex = 6
        Label3.Text = "(c) Pannisco SW ltd."
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.Location = New Point(10, 19)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.ReadOnly = True
        RichTextBox1.Size = New Size(259, 140)
        RichTextBox1.TabIndex = 7
        RichTextBox1.Text = "*crickets*"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(193, 82)
        Label4.Name = "Label4"
        Label4.Size = New Size(31, 20)
        Label4.TabIndex = 8
        Label4.Text = "0/0"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(RichTextBox1)
        GroupBox1.Location = New Point(235, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(275, 172)
        GroupBox1.TabIndex = 10
        GroupBox1.TabStop = False
        GroupBox1.Text = "LOG:"
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(170, 110)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(58, 24)
        CheckBox1.TabIndex = 11
        CheckBox1.Text = "LOG"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.BackgroundImage = My.Resources.Resources.Grafikartes_Flat_Retro_Modern_Settings_96
        Button3.BackgroundImageLayout = ImageLayout.Stretch
        Button3.Location = New Point(198, 12)
        Button3.Name = "Button3"
        Button3.Size = New Size(30, 27)
        Button3.TabIndex = 12
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(236, 192)
        Controls.Add(Button3)
        Controls.Add(CheckBox1)
        Controls.Add(GroupBox1)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Button2)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(Button1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Form1"
        Text = "YouMP3"
        TopMost = True
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Button3 As Button

End Class
