<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form7
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form7))
        ComboBox1 = New ComboBox()
        Button5 = New Button()
        TextBox1 = New TextBox()
        Button1 = New Button()
        Label1 = New Label()
        Button2 = New Button()
        Label2 = New Label()
        Label3 = New Label()
        PictureBox2 = New PictureBox()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(800, 351)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(148, 23)
        ComboBox1.TabIndex = 27
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(408, 526)
        Button5.Name = "Button5"
        Button5.Size = New Size(118, 33)
        Button5.TabIndex = 26
        Button5.Text = "File Leave"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(408, 291)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(375, 220)
        TextBox1.TabIndex = 24
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(12, 37)
        Button1.Name = "Button1"
        Button1.Size = New Size(148, 49)
        Button1.TabIndex = 28
        Button1.Text = "Profile"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(426, 134)
        Label1.Name = "Label1"
        Label1.Size = New Size(220, 23)
        Label1.TabIndex = 29
        Label1.Text = "Leave Request for:"
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(664, 517)
        Button2.Name = "Button2"
        Button2.Size = New Size(119, 33)
        Button2.TabIndex = 30
        Button2.Text = "Cancel last request"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(408, 265)
        Label2.Name = "Label2"
        Label2.Size = New Size(88, 23)
        Label2.TabIndex = 31
        Label2.Text = "Reason"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(800, 315)
        Label3.Name = "Label3"
        Label3.Size = New Size(138, 20)
        Label3.TabIndex = 32
        Label3.Text = "Type of Leave"
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(-27, -3)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(1223, 661)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 33
        PictureBox2.TabStop = False
        ' 
        ' Form7
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1169, 673)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Button2)
        Controls.Add(Label1)
        Controls.Add(Button1)
        Controls.Add(ComboBox1)
        Controls.Add(Button5)
        Controls.Add(TextBox1)
        Controls.Add(PictureBox2)
        Name = "Form7"
        Text = "Form7"
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button5 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox2 As PictureBox
End Class
