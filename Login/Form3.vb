Imports System.Data.OleDb
Imports System.Data

Public Class Form3

    Private ReadOnly connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb"
    )

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.UseSystemPasswordChar = True
    End Sub

    ' =========================
    ' LOGIN BUTTON
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text.Trim()

        If username = "" OrElse password = "" Then
            MsgBox("Please enter username and password", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            If connect.State <> ConnectionState.Open Then connect.Open()

            Dim query As String =
                "SELECT Role FROM [login] WHERE username=? AND [password]=?"

            Using cmd As New OleDbCommand(query, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = username
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = password

                Dim roleObj = cmd.ExecuteScalar()

                If roleObj Is Nothing Then
                    MsgBox("Invalid username or password", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Dim role As String = roleObj.ToString().ToLower()

                ' ✅ ONE DASHBOARD ONLY
                Dim dash As New Form5(username, role)
                dash.Show()
                Me.Hide()
            End Using

        Catch ex As Exception
            MsgBox("Login error: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            connect.Close()
        End Try

    End Sub

    ' =========================
    ' SHOW / HIDE PASSWORD
    ' =========================
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) _
        Handles CheckBox1.CheckedChanged
        TextBox2.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

    ' =========================
    ' REGISTER
    ' =========================
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
        Handles LinkLabel1.LinkClicked
        Form2.Show()
        Me.Hide()
    End Sub

    ' =========================
    ' BACK
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Show()
        Me.Hide()
    End Sub

End Class
