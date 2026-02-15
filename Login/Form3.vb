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

        Dim username = TextBox1.Text.Trim
        Dim password = TextBox2.Text.Trim

        If username = "" OrElse password = "" Then
            MsgBox("Please enter username and password")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open

            Dim sql =
                "SELECT Role FROM [login] WHERE username=? AND [password]=?"

            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = username
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = password

                Dim roleObj = cmd.ExecuteScalar

                If roleObj Is Nothing Then
                    MsgBox("Invalid username or password")
                    Exit Sub
                End If

                Dim role = roleObj.ToString

                Select Case role.ToLower

                    Case "admin"
                        MsgBox("Admin login successful")
                        Dim adminPanel As New Form4(username)
                        adminPanel.Show

                    Case "staff"
                        MsgBox("Staff login successful")
                        Dim staffPanel As New Form6(username)
                        staffPanel.Show

                    Case "employee"
                        MsgBox("Login successful")
                        Dim userPanel As New Form5(username)
                        userPanel.Show

                    Case Else
                        MsgBox("Unknown role: " & role)
                        Exit Sub

                End Select

                Hide
            End Using

        Catch ex As Exception
            MsgBox("Login error: " & ex.Message)
        Finally
            connect.Close
        End Try

    End Sub


    ' SHOW / HIDE PASSWORD
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBox2.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form2.Show
        Hide

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked

        ' Step 1: Ask for Employee ID
        Dim eid As String = InputBox("Enter your Employee ID:", "Forgot Password").Trim()
        If eid = "" Then Exit Sub

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            ' Step 2: Get user info
            Dim sql As String =
                "SELECT MobileN, Address FROM [login] WHERE EID=?"

            Dim mobile As String = ""
            Dim address As String = ""

            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = eid

                Using reader = cmd.ExecuteReader()
                    If Not reader.Read() Then
                        MsgBox("Employee ID not found.", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    mobile = reader("MobileN").ToString()
                    address = reader("Address").ToString()
                End Using
            End Using

            ' Step 3: Randomly choose question
            Dim rnd As New Random()
            Dim askPhone As Boolean = rnd.Next(0, 2) = 0

            Dim userAnswer As String
            Dim correctAnswer As String

            If askPhone Then
                userAnswer = InputBox("Security Check:" & vbCrLf &
                                      "What is your registered phone number?",
                                      "Verification").Trim()
                correctAnswer = mobile
            Else
                userAnswer = InputBox("Security Check:" & vbCrLf &
                                      "What is your registered address?",
                                      "Verification").Trim()
                correctAnswer = address
            End If

            If userAnswer = "" Then Exit Sub

            ' Step 4: Case-insensitive comparison
            If String.Compare(userAnswer, correctAnswer, True) <> 0 Then
                MsgBox("Verification failed. Information does not match.",
                       MsgBoxStyle.Critical)
                Exit Sub
            End If

            ' Step 5: Ask for new password
            Dim newPassword As String =
                InputBox("Verification successful." & vbCrLf &
                         "Enter your NEW password:",
                         "Reset Password").Trim()

            If newPassword = "" Then
                MsgBox("Password cannot be empty.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            Dim confirmPassword As String =
                InputBox("Confirm your NEW password:",
                         "Reset Password").Trim()

            If newPassword <> confirmPassword Then
                MsgBox("Passwords do not match.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            ' Step 6: Update password
            Dim updateSql As String =
                "UPDATE [login] SET [password]=? WHERE EID=?"

            Using updateCmd As New OleDbCommand(updateSql, connect)
                updateCmd.Parameters.Add("?", OleDbType.VarChar).Value = newPassword
                updateCmd.Parameters.Add("?", OleDbType.VarChar).Value = eid
                updateCmd.ExecuteNonQuery()
            End Using

            MsgBox("Password reset successful. You may now log in.",
                   MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox("Forgot password error: " & ex.Message)
        Finally
            connect.Close()
        End Try

    End Sub

End Class
