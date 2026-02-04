Imports System.Data.OleDb
Imports System.Data
Public Class Form1
    Dim connect As New OleDbConnection
    Dim command As OleDbCommand
    Dim sql As String = Nothing
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        Form2.Show()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect.ConnectionString = & quot;Provider=Microsoft.ACE.OLEDB.12.0;Data
Source = C : \Users\sirtr\Downloads\mercurydb.accdb&quot;
End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If connect.State = ConnectionState.Closed Then
            connect.Open()
            If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Then
                MsgBox(& quot;Please enter your username And password&quot;, MsgBoxStyle.Exclamation, & quot;Input Required&quot;)
            End If
        Else
            sql = & quot;Select COUNT(*) FROM [user] WHERE username = ? And password = ?&quot;
command = New OleDbCommand(sql, connect)
            command.Parameters.AddWithValue(& quot;?&quot;, TextBox1.Text)
            command.Parameters.AddWithValue(& quot;?&quot;, TextBox2.Text)
            Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
            If count & gt; 0 Then
MsgBox(& quot;Login Successful&quot;, MsgBoxStyle.Information, & quot;Welcome&quot;)
            Else
                MsgBox(& quot;Invalid username Or password&quot;, MsgBoxStyle.Critical, & quot;Login Failed&quot;)
            End If
        End If
    End Sub
End Class