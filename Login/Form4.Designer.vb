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
        DataGridView1 = New DataGridView()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        Button5 = New Button()
        Button6 = New Button()
        Button7 = New Button()
        Label1 = New Label()
        Label2 = New Label()
        Button8 = New Button()
        Label3 = New Label()
        TextBox1 = New TextBox()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(81, 160)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(901, 395)
        DataGridView1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(450, 570)
        Button1.Name = "Button1"
        Button1.Size = New Size(102, 27)
        Button1.TabIndex = 1
        Button1.Text = "Edit"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(558, 570)
        Button2.Name = "Button2"
        Button2.Size = New Size(102, 27)
        Button2.TabIndex = 2
        Button2.Text = "Delete"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(342, 570)
        Button3.Name = "Button3"
        Button3.Size = New Size(102, 27)
        Button3.TabIndex = 3
        Button3.Text = "Add"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(666, 570)
        Button4.Name = "Button4"
        Button4.Size = New Size(102, 27)
        Button4.TabIndex = 4
        Button4.Text = "Refresh"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(81, 570)
        Button5.Name = "Button5"
        Button5.Size = New Size(102, 27)
        Button5.TabIndex = 5
        Button5.Text = "Employees"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(81, 636)
        Button6.Name = "Button6"
        Button6.Size = New Size(102, 27)
        Button6.TabIndex = 6
        Button6.Text = "Request"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.Location = New Point(81, 603)
        Button7.Name = "Button7"
        Button7.Size = New Size(102, 27)
        Button7.TabIndex = 7
        Button7.Text = "Attendance"
        Button7.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Showcard Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(403, 34)
        Label1.Name = "Label1"
        Label1.Size = New Size(288, 33)
        Label1.TabIndex = 8
        Label1.Text = "Admin Dashboard"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Showcard Gothic", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(329, 109)
        Label2.Name = "Label2"
        Label2.Size = New Size(151, 33)
        Label2.TabIndex = 9
        Label2.Text = "Viewing:"
        ' 
        ' Button8
        ' 
        Button8.Font = New Font("Showcard Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button8.Location = New Point(81, 29)
        Button8.Name = "Button8"
        Button8.Size = New Size(139, 46)
        Button8.TabIndex = 13
        Button8.Text = "Log Out"
        Button8.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Showcard Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(699, 134)
        Label3.Name = "Label3"
        Label3.Size = New Size(63, 15)
        Label3.TabIndex = 29
        Label3.Text = "Search:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(768, 134)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(164, 23)
        TextBox1.TabIndex = 28
        ' 
        ' Form4
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1052, 672)
        Controls.Add(Label3)
        Controls.Add(TextBox1)
        Controls.Add(Button8)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Button7)
        Controls.Add(Button6)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(DataGridView1)
        Name = "Form4"
        Text = "Form4"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button8 As Button

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox1 As TextBox
End Class
