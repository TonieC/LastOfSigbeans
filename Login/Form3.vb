Imports System.Data.OleDb
Imports System.Data

Public Class Form3

    Dim connect As New OleDbConnection
    Dim command As OleDbCommand
    Dim sql As String = Nothing

    ' Go to SIGN UP (Form2)
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim signup As New Form2
        signup.Show()
        Me.Hide()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect.ConnectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb"

        TextBox2.UseSystemPasswordChar = True ' password textbox
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MsgBox("Please enter your username and password")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            sql = "SELECT COUNT(*) FROM [login] WHERE username=? AND [password]=?"
            command = New OleDbCommand(sql, connect)
            command.Parameters.AddWithValue("?", TextBox1.Text.Trim())
            command.Parameters.AddWithValue("?", TextBox2.Text.Trim())

            If CInt(command.ExecuteScalar()) > 0 Then
                MsgBox("Login Successful")
            Else
                MsgBox("Invalid username or password")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' SHOW / HIDE PASSWORD (FIXED)
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBox2.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

End Class
