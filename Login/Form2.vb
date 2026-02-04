Imports System.Data.OleDb
Imports System.Data

Public Class Form2
    Dim connect As New OleDbConnection
    Dim command As OleDbCommand
    Dim sql As String = Nothing
    Public su_username As String
    Public su_password As String



    ' Go back to login form
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        Form1.Show()
    End Sub

    ' Form load event
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Update path to your actual Access DB
        connect.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb"
    End Sub

    ' Sign Up button click
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check empty fields first
        If String.IsNullOrWhiteSpace(su_username) Or String.IsNullOrWhiteSpace(su_password) Then
            MsgBox("Please enter your username and password", MsgBoxStyle.Exclamation, "Input Required")
            Return
        End If

        Try
            ' Open connection if closed
            If connect.State = ConnectionState.Closed Then connect.Open()

            ' Check if username exists
            Dim checkData As String = "SELECT COUNT(*) FROM [user] WHERE username = ?"
            command = New OleDbCommand(checkData, connect)
            command.Parameters.AddWithValue("?", su_username)
            Dim check As Integer = Convert.ToInt32(command.ExecuteScalar())

            If check > 0 Then
                MsgBox("Username already exists", MsgBoxStyle.Critical, "Error")
            Else
                ' Insert new user
                sql = "INSERT INTO [user] ([username], [password]) VALUES (?, ?)"
                command = New OleDbCommand(sql, connect)
                command.Parameters.Clear()
                command.Parameters.AddWithValue("?", su_username)
                command.Parameters.AddWithValue("?", su_password)
                command.ExecuteNonQuery()

                MsgBox("Sign Up Successful", MsgBoxStyle.Information, "Welcome")
                su_username = ""
                su_password = ""
            End If

        Catch ex As Exception
            MsgBox("Database error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            ' Close connection
            If connect.State = ConnectionState.Open Then connect.Close()
        End Try
    End Sub
End Class
