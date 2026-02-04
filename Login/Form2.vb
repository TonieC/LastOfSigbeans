PuImports System.Data.OleDb
Imports System.Data

Public Class Form2
    Dim connect As New OleDbConnection()
    Dim command As OleDbCommand
    Dim sql As String = Nothing

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\sirtr\Downloads\mercurydb.accdb"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MsgBox("Please enter your username And password", MsgBoxStyle.Exclamation, "Input Required")
            Return
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            sql = "SELECT COUNT(*) FROM [user] WHERE username = ?"
            command = New OleDbCommand(sql, connect)
            command.Parameters.Clear()
            command.Parameters.AddWithValue("?", TextBox1.Text)
            Dim check As Integer = Convert.ToInt32(command.ExecuteScalar())

            If check > 0 Then
                MsgBox("Username already exists", MsgBoxStyle.Exclamation, "Duplicate")
            Else
                sql = "INSERT INTO [user] ([username], [password]) VALUES (?, ?)"
                command = New OleDbCommand(sql, connect)
                command.Parameters.Clear()
                command.Parameters.AddWithValue("?", TextBox1.Text)
                command.Parameters.AddWithValue("?", TextBox2.Text)
                command.ExecuteNonQuery()
                MsgBox("Sign Up Successful", MsgBoxStyle.Information, "Welcome")
                TextBox2.Clear()
                TextBox1.Clear()
            End If
        Catch ex As Exception
            MsgBox("An error occurred: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            If connect.State = ConnectionState.Open Then connect.Close()
        End Try
    End Sub
End Class