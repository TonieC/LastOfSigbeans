Imports System.Data.OleDb
Imports System.Data

Public Class Form3

    Dim connect As New OleDbConnection
    Dim command As OleDbCommand
    Dim sql As String = Nothing
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect.ConnectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb"

        TextBox2.UseSystemPasswordChar = True
    End Sub

    ' LOGIN BUTTON
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()

        If username = "" Or password = "" Then
            MsgBox("Please enter your username and password")
            Exit Sub
        End If

        ' ===== ADMIN LOGIN =====
        If username = "admin" And password = "restricted" Then
            MsgBox("Admin login successful")

            Dim adminPanel As New Form4
            adminPanel.Show()
            Me.Hide()
            Exit Sub
        End If

        ' ===== NORMAL USER LOGIN =====
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            sql = "SELECT COUNT(*) FROM [login] WHERE username=? AND [password]=?"
            command = New OleDbCommand(sql, connect)
            command.Parameters.Add("?", OleDbType.VarChar).Value = username
            command.Parameters.Add("?", OleDbType.VarChar).Value = password

            If CInt(command.ExecuteScalar()) > 0 Then
                MsgBox("Login Successful")

                Dim userPanel As New Form5(username)
                userPanel.Show()
                Me.Hide()

            Else
                MsgBox("Invalid username or password")
            End If

        Catch ex As Exception
            MsgBox("Login error: " & ex.Message)
        Finally
            connect.Close()
        End Try

    End Sub


    ' SHOW / HIDE PASSWORD
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBox2.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form2.Show()
        Me.Hide()

    End Sub


End Class
