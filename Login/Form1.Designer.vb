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
        Button2 = New Button()
        Button3 = New Button()
        Label1 = New Label()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(896, 614)
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
        Label11.Location = New Point(458, 9)
        Label11.Name = "Label11"
        Label11.Size = New Size(141, 23)
        Label11.TabIndex = 11
        Label11.Text = "Attendance"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(115, 76)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(770, 521)
        PictureBox1.TabIndex = 12
        PictureBox1.TabStop = False
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(330, 614)
        Button2.Name = "Button2"
        Button2.Size = New Size(102, 45)
        Button2.TabIndex = 13
        Button2.Text = "Scan"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(587, 614)
        Button3.Name = "Button3"
        Button3.Size = New Size(102, 45)
        Button3.TabIndex = 14
        Button3.Text = "Stop"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(309, 39)
        Label1.Name = "Label1"
        Label1.Size = New Size(87, 23)
        Label1.TabIndex = 15
        Label1.Text = "Status"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1046, 671)
        Controls.Add(Label1)
        Controls.Add(Button3)
        Controls.Add(Button2)
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
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label1 As Label


End Class
