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
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(9, 7)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(832, 395)
        DataGridView1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(287, 429)
        Button1.Name = "Button1"
        Button1.Size = New Size(102, 27)
        Button1.TabIndex = 1
        Button1.Text = "Edit"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(485, 429)
        Button2.Name = "Button2"
        Button2.Size = New Size(102, 27)
        Button2.TabIndex = 2
        Button2.Text = "Delete"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Form4
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(853, 475)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(DataGridView1)
        Name = "Form4"
        Text = "Form4"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
