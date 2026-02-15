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

        If username = "" OrElse password = "" Then
            MsgBox("Please enter username and password")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql As String =
                "SELECT Role FROM [login] WHERE username=? AND [password]=?"

            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = username
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = password

                Dim roleObj = cmd.ExecuteScalar()

                If roleObj Is Nothing Then
                    MsgBox("Invalid username or password")
                    Exit Sub
                End If

                Dim role As String = roleObj.ToString()

                Select Case role.ToLower()

                    Case "admin"
                        MsgBox("Admin login successful")
                        Dim adminPanel As New Form4(username)
                        adminPanel.Show()

                    Case "staff"
                        MsgBox("Staff login successful")
                        Dim staffPanel As New Form6(username)
                        staffPanel.Show()

                    Case "employee"
                        MsgBox("Login successful")
                        Dim userPanel As New Form5(username)
                        userPanel.Show()

                    Case Else
                        MsgBox("Unknown role: " & role)
                        Exit Sub

                End Select

                Me.Hide()
            End Using

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
