<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        Button1 = New Button()
        LinkLabel1 = New LinkLabel()
        CheckBox1 = New CheckBox()
        LinkLabel2 = New LinkLabel()
        Button2 = New Button()
        PictureBox2 = New PictureBox()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(597, 265)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(204, 23)
        TextBox1.TabIndex = 0
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(597, 370)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(204, 23)
        TextBox2.TabIndex = 1
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Showcard Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.Location = New Point(681, 439)
        Button1.Name = "Button1"
        Button1.Size = New Size(102, 38)
        Button1.TabIndex = 5
        Button1.Text = "LOGIN"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.AutoSize = True
        LinkLabel1.Location = New Point(669, 588)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(144, 15)
        LinkLabel1.TabIndex = 6
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "Register an Account Here."
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(900, 399)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(138, 19)
        CheckBox1.TabIndex = 7
        CheckBox1.Text = "Show/Hide Password"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' LinkLabel2
        ' 
        LinkLabel2.AutoSize = True
        LinkLabel2.Location = New Point(688, 421)
        LinkLabel2.Name = "LinkLabel2"
        LinkLabel2.Size = New Size(95, 15)
        LinkLabel2.TabIndex = 9
        LinkLabel2.TabStop = True
        LinkLabel2.Text = "Forgot Password"
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(27, 23)
        Button2.Name = "Button2"
        Button2.Size = New Size(113, 34)
        Button2.TabIndex = 10
        Button2.Text = "Attendance"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(-23, -2)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(1223, 683)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 21
        PictureBox2.TabStop = False
        ' 
        ' Form3
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1176, 677)
        Controls.Add(Button2)
        Controls.Add(LinkLabel2)
        Controls.Add(CheckBox1)
        Controls.Add(LinkLabel1)
        Controls.Add(Button1)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(PictureBox2)
        Name = "Form3"
        Text = "Form1"
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents Button2 As Button
    Friend WithEvents PictureBox2 As PictureBox

End Class
