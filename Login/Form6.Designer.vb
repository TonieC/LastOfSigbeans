<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form6
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
        Button8 = New Button()
        Label2 = New Label()
        Label1 = New Label()
        Button7 = New Button()
        Button6 = New Button()
        Button5 = New Button()
        Button4 = New Button()
        Button3 = New Button()
        Button1 = New Button()
        DataGridView1 = New DataGridView()
        Button2 = New Button()
        TextBox1 = New TextBox()
        Label3 = New Label()
        Button9 = New Button()
        DateTimePicker1 = New DateTimePicker()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button8
        ' 
        Button8.Font = New Font("Showcard Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button8.Location = New Point(105, 16)
        Button8.Name = "Button8"
        Button8.Size = New Size(139, 46)
        Button8.TabIndex = 24
        Button8.Text = "Log Out"
        Button8.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Showcard Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(353, 96)
        Label2.Name = "Label2"
        Label2.Size = New Size(151, 33)
        Label2.TabIndex = 23
        Label2.Text = "Viewing:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Showcard Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(397, 29)
        Label1.Name = "Label1"
        Label1.Size = New Size(276, 33)
        Label1.TabIndex = 22
        Label1.Text = "Staff Dashboard"
        ' 
        ' Button7
        ' 
        Button7.Location = New Point(105, 590)
        Button7.Name = "Button7"
        Button7.Size = New Size(102, 27)
        Button7.TabIndex = 21
        Button7.Text = "Attendance"
        Button7.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(105, 623)
        Button6.Name = "Button6"
        Button6.Size = New Size(102, 27)
        Button6.TabIndex = 20
        Button6.Text = "Request"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(105, 557)
        Button5.Name = "Button5"
        Button5.Size = New Size(102, 27)
        Button5.TabIndex = 19
        Button5.Text = "Employees"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(641, 578)
        Button4.Name = "Button4"
        Button4.Size = New Size(102, 27)
        Button4.TabIndex = 18
        Button4.Text = "Refresh"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(340, 578)
        Button3.Name = "Button3"
        Button3.Size = New Size(102, 27)
        Button3.TabIndex = 17
        Button3.Text = "Approve"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(491, 578)
        Button1.Name = "Button1"
        Button1.Size = New Size(102, 27)
        Button1.TabIndex = 15
        Button1.Text = "Reject"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(105, 156)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(832, 395)
        DataGridView1.TabIndex = 14
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(786, 578)
        Button2.Name = "Button2"
        Button2.Size = New Size(102, 27)
        Button2.TabIndex = 25
        Button2.Text = "Print"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(749, 127)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(188, 23)
        TextBox1.TabIndex = 26
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Showcard Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(680, 129)
        Label3.Name = "Label3"
        Label3.Size = New Size(63, 15)
        Label3.TabIndex = 27
        Label3.Text = "Search:"
        ' 
        ' Button9
        ' 
        Button9.Font = New Font("Showcard Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button9.Location = New Point(838, 29)
        Button9.Name = "Button9"
        Button9.Size = New Size(139, 46)
        Button9.TabIndex = 28
        Button9.Text = "Logs"
        Button9.UseVisualStyleBackColor = True
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Format = DateTimePickerFormat.Short
        DateTimePicker1.Location = New Point(943, 129)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(81, 23)
        DateTimePicker1.TabIndex = 29
        ' 
        ' Form6
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1043, 667)
        Controls.Add(DateTimePicker1)
        Controls.Add(Button9)
        Controls.Add(Label3)
        Controls.Add(TextBox1)
        Controls.Add(Button2)
        Controls.Add(Button8)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Button7)
        Controls.Add(Button6)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button1)
        Controls.Add(DataGridView1)
        Name = "Form6"
        Text = "Form6"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button8 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Button9 As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
End Class
