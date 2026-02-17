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
        LinkLabel1 = New LinkLabel()
        Button1 = New Button()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        TextBox2 = New TextBox()
        TextBox1 = New TextBox()
        CheckBox1 = New CheckBox()
        TextBox3 = New TextBox()
        TextBox4 = New TextBox()
        Label4 = New Label()
        Label5 = New Label()
        TextBox5 = New TextBox()
        Label6 = New Label()
        TextBox6 = New TextBox()
        Label7 = New Label()
        Label8 = New Label()
        ComboBox1 = New ComboBox()
        Label9 = New Label()
        ComboBox2 = New ComboBox()
        SuspendLayout()
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.AutoSize = True
        LinkLabel1.Location = New Point(486, 601)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(127, 15)
        LinkLabel1.TabIndex = 13
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "Log In to your account"
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Showcard Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.Location = New Point(486, 494)
        Button1.Name = "Button1"
        Button1.Size = New Size(130, 38)
        Button1.TabIndex = 12
        Button1.Text = "SIGN UP"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(293, 203)
        Label3.Name = "Label3"
        Label3.Size = New Size(197, 19)
        Label3.TabIndex = 11
        Label3.Text = "PASSWORD"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(293, 146)
        Label2.Name = "Label2"
        Label2.Size = New Size(199, 19)
        Label2.TabIndex = 10
        Label2.Text = "USERNAME"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Wide Latin", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(250, 33)
        Label1.Name = "Label1"
        Label1.Size = New Size(558, 79)
        Label1.TabIndex = 9
        Label1.Text = "SIGN UP"
        ' 
        ' TextBox2
        ' 
        TextBox2.AccessibleRole = AccessibleRole.IpAddress
        TextBox2.Location = New Point(288, 168)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(204, 23)
        TextBox2.TabIndex = 8
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(288, 225)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(204, 23)
        TextBox1.TabIndex = 7
        TextBox1.UseSystemPasswordChar = True
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(661, 225)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(138, 19)
        CheckBox1.TabIndex = 14
        CheckBox1.Text = "Show/Hide Password"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(288, 285)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(204, 23)
        TextBox3.TabIndex = 15
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(288, 342)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(204, 23)
        TextBox4.TabIndex = 16
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(323, 263)
        Label4.Name = "Label4"
        Label4.Size = New Size(160, 19)
        Label4.TabIndex = 17
        Label4.Text = "Full Name"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(288, 320)
        Label5.Name = "Label5"
        Label5.Size = New Size(231, 19)
        Label5.TabIndex = 18
        Label5.Text = "Mobile Number"
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(288, 393)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(206, 23)
        TextBox5.TabIndex = 19
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(340, 371)
        Label6.Name = "Label6"
        Label6.Size = New Size(98, 19)
        Label6.TabIndex = 20
        Label6.Text = "Email"
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(288, 441)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(206, 23)
        TextBox6.TabIndex = 21
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(323, 419)
        Label7.Name = "Label7"
        Label7.Size = New Size(128, 19)
        Label7.TabIndex = 22
        Label7.Text = "Address"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(532, 203)
        Label8.Name = "Label8"
        Label8.Size = New Size(115, 19)
        Label8.TabIndex = 23
        Label8.Text = "Gender"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(532, 225)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(115, 23)
        ComboBox1.TabIndex = 24
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Wide Latin", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(553, 263)
        Label9.Name = "Label9"
        Label9.Size = New Size(63, 19)
        Label9.TabIndex = 25
        Label9.Text = "Age"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.FormattingEnabled = True
        ComboBox2.Location = New Point(532, 285)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(115, 23)
        ComboBox2.TabIndex = 26
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1038, 675)
        Controls.Add(ComboBox2)
        Controls.Add(Label9)
        Controls.Add(ComboBox1)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(TextBox6)
        Controls.Add(Label6)
        Controls.Add(TextBox5)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(TextBox4)
        Controls.Add(TextBox3)
        Controls.Add(CheckBox1)
        Controls.Add(LinkLabel1)
        Controls.Add(Button1)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Name = "Form2"
        Text = "Form2"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Button1 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents ComboBox2 As ComboBox
End Class
