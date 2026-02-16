<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Button1 = New Button()
        Label11 = New Label()
        PictureBox1 = New PictureBox()
        Button3 = New Button()
        Label1 = New Label()
        Button4 = New Button()
        TextBox1 = New TextBox()
        Label2 = New Label()
        Button5 = New Button()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(918, 74)
        Button1.Name = "Button1"
        Button1.Size = New Size(102, 45)
        Button1.TabIndex = 0
        Button1.Text = "Login"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label11.Location = New Point(450, 20)
        Label11.Name = "Label11"
        Label11.Size = New Size(141, 23)
        Label11.TabIndex = 11
        Label11.Text = "Attendance"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(211, 74)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(642, 390)
        PictureBox1.TabIndex = 12
        PictureBox1.TabStop = False
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(546, 522)
        Button3.Name = "Button3"
        Button3.Size = New Size(76, 28)
        Button3.TabIndex = 14
        Button3.Text = "Time Out"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(415, 48)
        Label1.Name = "Label1"
        Label1.Size = New Size(94, 23)
        Label1.TabIndex = 15
        Label1.Text = "Status:"
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(918, 12)
        Button4.Name = "Button4"
        Button4.Size = New Size(102, 45)
        Button4.TabIndex = 16
        Button4.Text = "Sign Up"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(434, 493)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(188, 23)
        TextBox1.TabIndex = 17
        TextBox1.Visible = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(428, 467)
        Label2.Name = "Label2"
        Label2.Size = New Size(194, 23)
        Label2.TabIndex = 18
        Label2.Text = "Enter Password"
        Label2.Visible = False
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(434, 522)
        Button5.Name = "Button5"
        Button5.Size = New Size(85, 28)
        Button5.TabIndex = 19
        Button5.Text = "Time In"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1046, 671)
        Controls.Add(Button5)
        Controls.Add(Label2)
        Controls.Add(TextBox1)
        Controls.Add(Button4)
        Controls.Add(Label1)
        Controls.Add(Button3)
        Controls.Add(PictureBox1)
        Controls.Add(Label11)
        Controls.Add(Button1)
        Name = "Form1"
        Text = "Form3"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button5 As Button


End Class
