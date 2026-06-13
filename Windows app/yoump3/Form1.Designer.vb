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
        Button1 = New Button()
        TextBox1 = New TextBox()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        TextBox2 = New TextBox()
        Button2 = New Button()
        Label3 = New Label()
        RichTextBox1 = New RichTextBox()
        Label4 = New Label()
        CheckBox1 = New CheckBox()
        Button3 = New Button()
        ProgressBar1 = New ProgressBar()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.Red
        Button1.FlatAppearance.BorderColor = Color.Black
        Button1.FlatAppearance.BorderSize = 2
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Google Sans", 9.999999F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = Color.White
        Button1.Location = New Point(436, 12)
        Button1.Margin = New Padding(4, 7, 4, 7)
        Button1.Name = "Button1"
        Button1.Size = New Size(141, 48)
        Button1.TabIndex = 2
        Button1.Text = "Download"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' TextBox1
        ' 
        TextBox1.Font = New Font("Google Sans", 8F)
        TextBox1.Location = New Point(57, 14)
        TextBox1.Margin = New Padding(4, 7, 4, 7)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(371, 41)
        TextBox1.TabIndex = 3
        TextBox1.Text = "URL"
        TextBox1.TextAlign = HorizontalAlignment.Center
        ' 
        ' TextBox2
        ' 
        TextBox2.Font = New Font("Google Sans", 8F)
        TextBox2.Location = New Point(13, 416)
        TextBox2.Margin = New Padding(4, 7, 4, 7)
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(514, 38)
        TextBox2.TabIndex = 4
        TextBox2.Text = "Download Path"
        TextBox2.TextAlign = HorizontalAlignment.Center
        ' 
        ' Button2
        ' 
        Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), Image)
        Button2.BackgroundImageLayout = ImageLayout.Zoom
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Location = New Point(541, 408)
        Button2.Margin = New Padding(4, 7, 4, 7)
        Button2.Name = "Button2"
        Button2.Size = New Size(38, 58)
        Button2.TabIndex = 5
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Google Sans", 8F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(13, 461)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(219, 38)
        Label3.TabIndex = 6
        Label3.Text = "Made by Pannisco Software"
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.Location = New Point(13, 71)
        RichTextBox1.Margin = New Padding(4, 7, 4, 7)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.ReadOnly = True
        RichTextBox1.Size = New Size(564, 281)
        RichTextBox1.TabIndex = 7
        RichTextBox1.Text = "*crickets*"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Google Sans", 8F)
        Label4.Location = New Point(535, 366)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(42, 38)
        Label4.TabIndex = 8
        Label4.Text = "0/0"
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Font = New Font("Google Sans", 8F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        CheckBox1.Location = New Point(164, 276)
        CheckBox1.Margin = New Padding(4, 7, 4, 7)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(76, 42)
        CheckBox1.TabIndex = 11
        CheckBox1.Text = "LOG"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), Image)
        Button3.BackgroundImageLayout = ImageLayout.Zoom
        Button3.FlatAppearance.BorderSize = 0
        Button3.FlatStyle = FlatStyle.Flat
        Button3.Location = New Point(13, 6)
        Button3.Margin = New Padding(4, 7, 4, 7)
        Button3.Name = "Button3"
        Button3.Size = New Size(36, 58)
        Button3.TabIndex = 12
        Button3.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(13, 366)
        ProgressBar1.Margin = New Padding(4, 7, 4, 7)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(514, 36)
        ProgressBar1.TabIndex = 13
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 43F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(590, 495)
        Controls.Add(RichTextBox1)
        Controls.Add(ProgressBar1)
        Controls.Add(Button3)
        Controls.Add(CheckBox1)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Button2)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(Button1)
        Font = New Font("Google Sans", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 7, 4, 7)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Form1"
        Text = "YouMP3"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Button3 As Button
    Friend WithEvents ProgressBar1 As ProgressBar

End Class
