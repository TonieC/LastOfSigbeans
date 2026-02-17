Imports System.Data.OleDb
Imports System.Data

Public Class Form2

    Dim connect As New OleDbConnection
    Dim command As OleDbCommand
    Dim sql As String = Nothing

    ' Back to LOGIN (Form3)
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim login As New Form3
        login.Show()
        Me.Hide()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect.ConnectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb"

        TextBox1.UseSystemPasswordChar = True

        ' Populate ComboBoxes
        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange({"Male", "Female", "Prefer not to say"})
        ComboBox1.SelectedIndex = 0

        ComboBox2.Items.Clear()
        For i As Integer = 16 To 65
            ComboBox2.Items.Add(i.ToString())
        Next
        ComboBox2.SelectedIndex = 0
    End Sub

    ' SIGN UP BUTTON
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim su_username As String = TextBox2.Text.Trim()
        Dim su_password As String = TextBox1.Text.Trim()
        Dim su_fullname As String = TextBox3.Text.Trim()
        Dim su_mobile As String = TextBox4.Text.Trim()
        Dim su_email As String = TextBox5.Text.Trim()
        Dim su_address As String = TextBox6.Text.Trim()
        Dim su_gender As String = ComboBox1.SelectedItem.ToString()
        Dim su_age As String = ComboBox2.SelectedItem.ToString()
        Dim su_eid As String = ""

        If su_username = "" Or su_password = "" Or su_fullname = "" _
        Or su_mobile = "" Or su_email = "" Or su_address = "" Then
            MsgBox("All fields are required")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            ' CHECK DUPLICATE USERNAME
            sql = "SELECT COUNT(*) FROM [login] WHERE username=?"
            command = New OleDbCommand(sql, connect)
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_username

            If CInt(command.ExecuteScalar()) > 0 Then
                MsgBox("Username already exists")
                Exit Sub
            End If

            ' GET NEXT EID
            sql = "SELECT MAX(EID) FROM [login]"
            command = New OleDbCommand(sql, connect)

            Dim result As Object = command.ExecuteScalar()
            Dim nextId As Integer
            If IsDBNull(result) Or result Is Nothing Then
                nextId = 1
            Else
                nextId = CInt(result.ToString())
                nextId += 1
            End If

            su_eid = nextId.ToString("000")   ' 001, 002, 003

            ' INSERT USER DATA (INCLUDING EID, Gender, Age)
            sql = "INSERT INTO [login] (EID, username, [password], FullName, MobileN, Email, Address, Gender, Age) " &
                  "VALUES (?,?,?,?,?,?,?,?,?)"

            command = New OleDbCommand(sql, connect)

            ' ORDER MATTERS
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_eid
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_username
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_password
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_fullname
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_mobile
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_email
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_address
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_gender
            command.Parameters.Add("?", OleDbType.Integer).Value = CInt(su_age)

            command.ExecuteNonQuery()

            MsgBox("Sign Up Successful. Employee ID: " & su_eid)

            ' Clear all inputs
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            ComboBox1.SelectedIndex = 0
            ComboBox2.SelectedIndex = 0

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            connect.Close()
        End Try

    End Sub

    ' SHOW / HIDE PASSWORD
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBox1.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

End Class
