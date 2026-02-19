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
        ComboBox1 = New ComboBox()
        Button5 = New Button()
        Label14 = New Label()
        TextBox1 = New TextBox()
        Button1 = New Button()
        Label1 = New Label()
        Button2 = New Button()
        Label2 = New Label()
        Label3 = New Label()
        SuspendLayout()
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(680, 341)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(148, 23)
        ComboBox1.TabIndex = 27
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(408, 385)
        Button5.Name = "Button5"
        Button5.Size = New Size(118, 33)
        Button5.TabIndex = 26
        Button5.Text = "File Leave"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Font = New Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label14.Location = New Point(478, 48)
        Label14.Name = "Label14"
        Label14.Size = New Size(136, 23)
        Label14.TabIndex = 25
        Label14.Text = "File a Leave"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(408, 234)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(266, 130)
        TextBox1.TabIndex = 24
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(25, 22)
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
        Label1.Location = New Point(436, 22)
        Label1.Name = "Label1"
        Label1.Size = New Size(220, 23)
        Label1.TabIndex = 29
        Label1.Text = "Leave Request for:"
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(555, 385)
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
        Label2.Location = New Point(408, 208)
        Label2.Name = "Label2"
        Label2.Size = New Size(88, 23)
        Label2.TabIndex = 31
        Label2.Text = "Reason"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(680, 315)
        Label3.Name = "Label3"
        Label3.Size = New Size(138, 20)
        Label3.TabIndex = 32
        Label3.Text = "Type of Leave"
        ' 
        ' Form7
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1046, 673)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Button2)
        Controls.Add(Label1)
        Controls.Add(Button1)
        Controls.Add(ComboBox1)
        Controls.Add(Button5)
        Controls.Add(Label14)
        Controls.Add(TextBox1)
        Name = "Form7"
        Text = "Form7"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button5 As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
