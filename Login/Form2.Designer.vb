<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        LinkLabel1 = New LinkLabel()
        Button1 = New Button()
        TextBox2 = New TextBox()
        TextBox1 = New TextBox()
        CheckBox1 = New CheckBox()
        TextBox3 = New TextBox()
        TextBox4 = New TextBox()
        TextBox5 = New TextBox()
        TextBox6 = New TextBox()
        ComboBox1 = New ComboBox()
        ComboBox2 = New ComboBox()
        ComboBox3 = New ComboBox()
        TextBox7 = New TextBox()
        TextBox8 = New TextBox()
        PictureBox2 = New PictureBox()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.AutoSize = True
        LinkLabel1.Location = New Point(567, 628)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(127, 15)
        LinkLabel1.TabIndex = 13
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "Log In to your account"
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Showcard Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.Location = New Point(704, 586)
        Button1.Name = "Button1"
        Button1.Size = New Size(130, 38)
        Button1.TabIndex = 12
        Button1.Text = "SIGN UP"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.AccessibleRole = AccessibleRole.IpAddress
        TextBox2.Location = New Point(292, 208)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(204, 23)
        TextBox2.TabIndex = 8
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(292, 294)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(204, 23)
        TextBox1.TabIndex = 7
        TextBox1.UseSystemPasswordChar = True
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(454, 260)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(138, 19)
        CheckBox1.TabIndex = 14
        CheckBox1.Text = "Show/Hide Password"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(292, 573)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(210, 23)
        TextBox3.TabIndex = 15
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(686, 212)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(204, 23)
        TextBox4.TabIndex = 16
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(686, 294)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(206, 23)
        TextBox5.TabIndex = 19
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(686, 398)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(213, 23)
        TextBox6.TabIndex = 21
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(686, 483)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(115, 23)
        ComboBox1.TabIndex = 24
        ' 
        ' ComboBox2
        ' 
        ComboBox2.FormattingEnabled = True
        ComboBox2.Location = New Point(884, 483)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(115, 23)
        ComboBox2.TabIndex = 26
        ' 
        ' ComboBox3
        ' 
        ComboBox3.FormattingEnabled = True
        ComboBox3.Location = New Point(884, 527)
        ComboBox3.Name = "ComboBox3"
        ComboBox3.Size = New Size(115, 23)
        ComboBox3.TabIndex = 28
        ' 
        ' TextBox7
        ' 
        TextBox7.Location = New Point(292, 483)
        TextBox7.Name = "TextBox7"
        TextBox7.Size = New Size(210, 23)
        TextBox7.TabIndex = 29
        ' 
        ' TextBox8
        ' 
        TextBox8.Location = New Point(292, 398)
        TextBox8.Name = "TextBox8"
        TextBox8.Size = New Size(204, 23)
        TextBox8.TabIndex = 31
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(-23, 13)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(1223, 644)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 33
        PictureBox2.TabStop = False
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1177, 670)
        Controls.Add(TextBox8)
        Controls.Add(TextBox7)
        Controls.Add(ComboBox3)
        Controls.Add(ComboBox2)
        Controls.Add(ComboBox1)
        Controls.Add(TextBox6)
        Controls.Add(TextBox5)
        Controls.Add(TextBox4)
        Controls.Add(TextBox3)
        Controls.Add(CheckBox1)
        Controls.Add(LinkLabel1)
        Controls.Add(Button1)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(PictureBox2)
        Name = "Form2"
        Text = "Form2"
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents PictureBox2 As PictureBox
End Class
