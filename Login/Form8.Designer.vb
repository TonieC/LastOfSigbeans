<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form8
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
        Label1 = New Label()
        DataGridView1 = New DataGridView()
        Button1 = New Button()
        Label2 = New Label()
        TextBox1 = New TextBox()
        Button2 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Showcard Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(494, 10)
        Label1.Name = "Label1"
        Label1.Size = New Size(91, 18)
        Label1.TabIndex = 0
        Label1.Text = "Logs for:"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(15, 43)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(1014, 443)
        DataGridView1.TabIndex = 1
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(12, 9)
        Button1.Name = "Button1"
        Button1.Size = New Size(110, 28)
        Button1.TabIndex = 2
        Button1.Text = "Staff Dashboard"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Showcard Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(819, 19)
        Label2.Name = "Label2"
        Label2.Size = New Size(77, 18)
        Label2.TabIndex = 3
        Label2.Text = "Search:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(898, 19)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(104, 23)
        TextBox1.TabIndex = 4
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(919, 492)
        Button2.Name = "Button2"
        Button2.Size = New Size(110, 28)
        Button2.TabIndex = 5
        Button2.Text = "Print"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Form8
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1045, 674)
        Controls.Add(Button2)
        Controls.Add(TextBox1)
        Controls.Add(Label2)
        Controls.Add(Button1)
        Controls.Add(DataGridView1)
        Controls.Add(Label1)
        Name = "Form8"
        Text = "Form8"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button2 As Button
End Class
