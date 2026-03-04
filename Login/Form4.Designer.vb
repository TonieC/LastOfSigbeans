<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form4
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form4))
        DataGridView1 = New DataGridView()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        Button6 = New Button()
        Button7 = New Button()
        Button8 = New Button()
        TextBox1 = New TextBox()
        DateTimePicker1 = New DateTimePicker()
        Button9 = New Button()
        Button10 = New Button()
        Label4 = New Label()
        Button5 = New Button()
        PictureBox2 = New PictureBox()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(162, 247)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(901, 395)
        DataGridView1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(521, 648)
        Button1.Name = "Button1"
        Button1.Size = New Size(102, 27)
        Button1.TabIndex = 1
        Button1.Text = "Edit"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(629, 648)
        Button2.Name = "Button2"
        Button2.Size = New Size(102, 27)
        Button2.TabIndex = 2
        Button2.Text = "Delete"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(400, 648)
        Button3.Name = "Button3"
        Button3.Size = New Size(102, 27)
        Button3.TabIndex = 3
        Button3.Text = "Add"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(737, 648)
        Button4.Name = "Button4"
        Button4.Size = New Size(102, 27)
        Button4.TabIndex = 4
        Button4.Text = "Refresh"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(1044, 133)
        Button6.Name = "Button6"
        Button6.Size = New Size(102, 27)
        Button6.TabIndex = 6
        Button6.Text = "Request"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.Location = New Point(869, 133)
        Button7.Name = "Button7"
        Button7.Size = New Size(102, 27)
        Button7.TabIndex = 7
        Button7.Text = "Attendance"
        Button7.UseVisualStyleBackColor = True
        ' 
        ' Button8
        ' 
        Button8.Font = New Font("Showcard Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button8.Location = New Point(12, 12)
        Button8.Name = "Button8"
        Button8.Size = New Size(139, 46)
        Button8.TabIndex = 13
        Button8.Text = "Close"
        Button8.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(247, 122)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(164, 23)
        TextBox1.TabIndex = 28
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Format = DateTimePickerFormat.Short
        DateTimePicker1.Location = New Point(504, 122)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(81, 23)
        DateTimePicker1.TabIndex = 30
        ' 
        ' Button9
        ' 
        Button9.Font = New Font("Showcard Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button9.Location = New Point(924, 20)
        Button9.Name = "Button9"
        Button9.Size = New Size(139, 46)
        Button9.TabIndex = 31
        Button9.Text = "Logs"
        Button9.UseVisualStyleBackColor = True
        ' 
        ' Button10
        ' 
        Button10.Location = New Point(1005, 648)
        Button10.Name = "Button10"
        Button10.Size = New Size(58, 26)
        Button10.TabIndex = 34
        Button10.Text = "Print"
        Button10.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Showcard Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(295, 187)
        Label4.Name = "Label4"
        Label4.Size = New Size(24, 33)
        Label4.TabIndex = 35
        Label4.Text = "."
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(726, 133)
        Button5.Name = "Button5"
        Button5.Size = New Size(102, 27)
        Button5.TabIndex = 36
        Button5.Text = "Employees"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(-3, -8)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(1224, 701)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 37
        PictureBox2.TabStop = False
        ' 
        ' Form4
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 687)
        Controls.Add(Button5)
        Controls.Add(Label4)
        Controls.Add(Button10)
        Controls.Add(Button9)
        Controls.Add(DateTimePicker1)
        Controls.Add(TextBox1)
        Controls.Add(Button8)
        Controls.Add(Button7)
        Controls.Add(Button6)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(DataGridView1)
        Controls.Add(PictureBox2)
        Name = "Form4"
        Text = "Form4"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Button9 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents PictureBox2 As PictureBox
End Class
