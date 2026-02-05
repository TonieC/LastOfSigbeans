Imports System.Data.OleDb
Imports System.Data

Public Class Form1
    ' Database connection
    Dim connect As New OleDbConnection
    Dim command As OleDbCommand
    Dim sql As String = Nothing

    ' Link to registration form
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        Form2.Show()
    End Sub

    ' Load event
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Replace the path below with where your Access DB actually is
        connect.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb"
    End Sub

    ' Login button click
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MsgBox("Please enter your username and password", MsgBoxStyle.Exclamation, "Input Required")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            sql = "SELECT COUNT(*) FROM [login] WHERE username = ? AND [password] = ?"
            command = New OleDbCommand(sql, connect)
            command.Parameters.AddWithValue("?", TextBox1.Text.Trim())
            command.Parameters.AddWithValue("?", TextBox2.Text.Trim())

            Dim count As Integer = CInt(command.ExecuteScalar())

            If count > 0 Then
                MsgBox("Login Successful", MsgBoxStyle.Information, "Welcome")
            Else
                MsgBox("Invalid username or password", MsgBoxStyle.Critical, "Login Failed")
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            connect.Close()
        End Try
    End Sub


End Class
