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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        PictureBox1 = New PictureBox()
        Button3 = New Button()
        Label1 = New Label()
        TextBox1 = New TextBox()
        Label2 = New Label()
        Button5 = New Button()
        PictureBox2 = New PictureBox()
        Button1 = New Button()
        Button2 = New Button()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(345, 160)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(774, 341)
        PictureBox1.TabIndex = 12
        PictureBox1.TabStop = False
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(716, 593)
        Button3.Name = "Button3"
        Button3.Size = New Size(76, 28)
        Button3.TabIndex = 14
        Button3.Text = "Time Out"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.FromArgb(CByte(37), CByte(129), CByte(51))
        Label1.Font = New Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(345, 504)
        Label1.Name = "Label1"
        Label1.Size = New Size(94, 23)
        Label1.TabIndex = 15
        Label1.Text = "Status:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(598, 556)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(194, 23)
        TextBox1.TabIndex = 17
        TextBox1.Visible = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.FromArgb(CByte(37), CByte(129), CByte(51))
        Label2.Font = New Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(598, 530)
        Label2.Name = "Label2"
        Label2.Size = New Size(194, 23)
        Label2.TabIndex = 18
        Label2.Text = "Enter Password"
        Label2.Visible = False
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(598, 593)
        Button5.Name = "Button5"
        Button5.Size = New Size(85, 28)
        Button5.TabIndex = 19
        Button5.Text = "Time In"
        Button5.UseVisualStyleBackColor = False
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(2, 2)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(1223, 644)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 20
        PictureBox2.TabStop = False
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.FromArgb(CByte(37), CByte(129), CByte(51))
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Showcard Gothic", 21.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = Color.FloralWhite
        Button1.Location = New Point(1037, 519)
        Button1.Name = "Button1"
        Button1.Size = New Size(141, 49)
        Button1.TabIndex = 21
        Button1.Text = "Log In"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.FromArgb(CByte(37), CByte(129), CByte(51))
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Font = New Font("Showcard Gothic", 21.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button2.ForeColor = Color.FloralWhite
        Button2.Location = New Point(1037, 574)
        Button2.Name = "Button2"
        Button2.Size = New Size(141, 49)
        Button2.TabIndex = 22
        Button2.Text = "Sign In"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1224, 642)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Button5)
        Controls.Add(Label2)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        Controls.Add(Button3)
        Controls.Add(PictureBox1)
        Controls.Add(PictureBox2)
        Name = "Form1"
        Text = "Form3"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button


End Class
