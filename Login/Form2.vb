Imports System.Data.OleDb
Imports System.Data
Imports System.Text.RegularExpressions

Public Class Form2

    Dim connect As New OleDbConnection
    Dim command As OleDbCommand
    Dim sql As String = Nothing

    ' Dynamic Role controls
    Private cmbRole As New ComboBox
    Private lblRole As New Label With {.Text = "Role:", .AutoSize = True}

    ' Back to LOGIN (Form3)
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim login As New Form3
        login.Show()
        Me.Hide()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        connect.ConnectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb"

        TextBox1.UseSystemPasswordChar = True
        TextBox4.MaxLength = 11

        ' Gender
        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange({"Male", "Female", "Prefer not to say"})
        ComboBox1.SelectedIndex = 0

        ' Age
        ComboBox2.Items.Clear()
        For i As Integer = 18 To 65
            ComboBox2.Items.Add(i.ToString())
        Next
        ComboBox2.SelectedIndex = 0

        ' Department
        ComboBox3.Items.Clear()
        ComboBox3.Items.AddRange({
            "ICT",
            "HUMMS",
            "ABM",
            "STEM",
            "HE",
            "GAS",
            "EDUC",
            "PSYCHOLOGY",
            "CRIMINOLOGY",
            "CABAM",
            "Junior High School",
            "SHOTS",
            "Basic Educ"
        })
        ComboBox3.SelectedIndex = 0

        ' Setup dynamic Role ComboBox
        cmbRole.Top = ComboBox3.Top + 40
        cmbRole.Left = ComboBox3.Left
        cmbRole.Width = 300
        cmbRole.Visible = False
        Me.Controls.Add(cmbRole)

        lblRole.Top = cmbRole.Top - 20
        lblRole.Left = cmbRole.Left
        lblRole.Visible = False
        Me.Controls.Add(lblRole)
    End Sub

    ' SIGN UP BUTTON
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim su_username As String = TextBox2.Text.Trim()
        Dim su_password As String = TextBox1.Text.Trim()

        Dim firstName As String = TextBox8.Text.Trim()
        Dim middleName As String = TextBox7.Text.Trim()
        Dim lastName As String = TextBox3.Text.Trim()

        Dim su_fullname As String
        If middleName = "" Then
            su_fullname = firstName & " " & lastName
        Else
            su_fullname = firstName & " " & middleName & " " & lastName
        End If

        Dim su_mobile As String = TextBox4.Text.Trim()
        Dim su_email As String = TextBox5.Text.Trim()
        Dim su_address As String = TextBox6.Text.Trim()
        Dim su_gender As String = ComboBox1.SelectedItem.ToString()
        Dim su_age As Integer = CInt(ComboBox2.SelectedItem.ToString())
        Dim su_department As String = ComboBox3.SelectedItem.ToString()
        Dim su_role As String = If(cmbRole.Visible, cmbRole.SelectedItem.ToString(), "")
        Dim su_eid As String = ""

        ' Validation: required fields (middle name excluded)
        If su_username = "" Or su_password = "" Or firstName = "" Or lastName = "" _
            Or su_mobile = "" Or su_email = "" Or su_address = "" _
            Or su_department = "" Or (cmbRole.Visible And su_role = "") Then
            MsgBox("All fields are required except Middle Name")
            Exit Sub
        End If

        ' Validation: Names (letters only)
        If Not Regex.IsMatch(firstName, "^[a-zA-Z\s]+$") _
            Or Not Regex.IsMatch(lastName, "^[a-zA-Z\s]+$") _
            Or (middleName <> "" And Not Regex.IsMatch(middleName, "^[a-zA-Z\s]+$")) Then
            MsgBox("Names cannot contain numbers or special characters")
            Exit Sub
        End If

        ' Validation: Mobile Number (exactly 11 digits)
        If Not Regex.IsMatch(su_mobile, "^\d{11}$") Then
            MsgBox("Mobile Number must be exactly 11 digits")
            Exit Sub
        End If

        ' Validation: Password (8+ characters with special character)
        Dim passwordPattern As String = "^(?=.*[!@#$%^&*(),.?""{}|<>]).{8,}$"
        If Not Regex.IsMatch(su_password, passwordPattern) Then
            MsgBox("Password must be at least 8 characters long and include at least one special character (!@#$%^&* etc.)")
            Exit Sub
        End If

        ' Validation: Email (no spaces allowed)
        If su_email.Contains(" ") Then
            MsgBox("Email cannot contain spaces")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            ' CHECK DUPLICATE USERNAME
            sql = "SELECT COUNT(*) FROM [user] WHERE username=?"
            command = New OleDbCommand(sql, connect)
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_username

            If CInt(command.ExecuteScalar()) > 0 Then
                MsgBox("Username already exists")
                Exit Sub
            End If

            ' GET NEXT EID
            sql = "SELECT MAX(EID) FROM [user]"
            command = New OleDbCommand(sql, connect)
            Dim result As Object = command.ExecuteScalar()
            Dim nextId As Integer
            If IsDBNull(result) Or result Is Nothing Then
                nextId = 1
            Else
                nextId = CInt(result) + 1
            End If
            su_eid = nextId.ToString("000")

            ' INSERT USER DATA
            sql = "INSERT INTO [user] " &
                  "(EID, username, [password], FullName, MobileN, Email, Address, Gender, Age, Department, Role) " &
                  "VALUES (?,?,?,?,?,?,?,?,?,?,?)"

            command = New OleDbCommand(sql, connect)
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_eid
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_username
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_password
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_fullname
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_mobile
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_email
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_address
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_gender
            command.Parameters.Add("?", OleDbType.Integer).Value = su_age
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_department
            command.Parameters.Add("?", OleDbType.VarChar).Value = su_role

            command.ExecuteNonQuery()

            MsgBox("Sign Up Successful. Employee ID: " & su_eid)

            ' Clear fields
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            TextBox7.Clear()
            TextBox8.Clear()
            ComboBox1.SelectedIndex = 0
            ComboBox2.SelectedIndex = 0
            ComboBox3.SelectedIndex = 0
            cmbRole.Visible = False

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

    ' Restrict TextBox4 to digits only
    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
    End Sub

    ' Show dynamic Role based on Department
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        cmbRole.Items.Clear()
        cmbRole.Items.AddRange({"Teacher", "Supervisor", "Strand Coordinator"})
        cmbRole.SelectedIndex = 0
        cmbRole.Visible = True
        lblRole.Visible = True
    End Sub

End Class