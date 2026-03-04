Imports System.Data.OleDb
Imports System.Data
Imports System.Net
Imports System.Net.Mail

Public Class Form3
    Private resetCode As String = ""

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
                "SELECT Role FROM [user] WHERE username=? AND [password]=?"

            Using cmd As New OleDbCommand(query, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = username
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = password

                Dim roleObj = cmd.ExecuteScalar()

                If roleObj Is Nothing Then
                    MsgBox("Invalid username or password", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                Dim role As String = roleObj.ToString().ToLower()

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

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked

        Using dlgVerify As New Form
            dlgVerify.Text = "Verify Account"
            dlgVerify.FormBorderStyle = FormBorderStyle.FixedDialog
            dlgVerify.StartPosition = FormStartPosition.CenterParent
            dlgVerify.MaximizeBox = False
            dlgVerify.MinimizeBox = False
            dlgVerify.Width = 350
            dlgVerify.Height = 320
            dlgVerify.ShowInTaskbar = False

            ' Controls
            Dim lblFull As New Label With {.Text = "Full Name:", .Top = 20, .Left = 10}
            Dim txtFull As New TextBox With {.Top = 40, .Left = 10, .Width = 300}

            Dim lblMobile As New Label With {.Text = "Mobile Number:", .Top = 70, .Left = 10}
            Dim txtMobile As New TextBox With {.Top = 90, .Left = 10, .Width = 300}

            Dim lblEmail As New Label With {.Text = "Email:", .Top = 120, .Left = 10}
            Dim txtEmail As New TextBox With {.Top = 140, .Left = 10, .Width = 300}

            Dim lblDept As New Label With {.Text = "Department:", .Top = 170, .Left = 10}
            Dim cmbDept As New ComboBox With {.Top = 190, .Left = 10, .Width = 300}
            cmbDept.Items.AddRange({"ICT", "HUMMS", "ABM", "STEM", "HE", "GAS", "EDUC", "PSYCHOLOGY", "CRIMINOLOGY", "CABAM", "Junior High School"})
            cmbDept.SelectedIndex = 0

            Dim btnVerify As New Button With {.Text = "Verify & Send Code", .Top = 230, .Left = 90, .Width = 160}

            dlgVerify.Controls.AddRange({lblFull, txtFull, lblMobile, txtMobile, lblEmail, txtEmail, lblDept, cmbDept, btnVerify})

            Dim verified As Boolean = False

            AddHandler btnVerify.Click, Sub()

                                            Try
                                                If connect.State <> ConnectionState.Open Then connect.Open()

                                                Dim cmd As New OleDbCommand(
                                                "SELECT COUNT(*) FROM [user] WHERE LCase(FullName)=? AND MobileN=? AND LCase(Email)=? AND LCase(Department)=?", connect)

                                                cmd.Parameters.AddWithValue("?", txtFull.Text.Trim.ToLower)
                                                cmd.Parameters.AddWithValue("?", txtMobile.Text.Trim)
                                                cmd.Parameters.AddWithValue("?", txtEmail.Text.Trim.ToLower)
                                                cmd.Parameters.AddWithValue("?", cmbDept.SelectedItem.ToString.ToLower)

                                                If CInt(cmd.ExecuteScalar()) > 0 Then
                                                    verified = True
                                                Else
                                                    MsgBox("Details do not match.", MsgBoxStyle.Critical)
                                                    Exit Sub
                                                End If

                                            Catch ex As Exception
                                                MsgBox("Verification Error: " & ex.Message, MsgBoxStyle.Critical)
                                                Exit Sub
                                            Finally
                                                If connect.State = ConnectionState.Open Then connect.Close()
                                            End Try

                                            ' =========================
                                            ' GENERATE RESET CODE
                                            ' =========================
                                            Dim rnd As New Random()
                                            resetCode = rnd.Next(100000, 999999).ToString()

                                            ' =========================
                                            ' SEND EMAIL
                                            ' =========================
                                            Try
                                                Dim smtp As New SmtpClient("smtp.gmail.com", 587)
                                                smtp.EnableSsl = True
                                                smtp.UseDefaultCredentials = False
                                                smtp.Credentials = New NetworkCredential("cadizal06@gmail.com", "ptpz oiah zzuj kjfj")

                                                Dim mail As New MailMessage()
                                                mail.From = New MailAddress("cadizal06@gmail.com")
                                                mail.To.Add(txtEmail.Text.Trim)
                                                mail.Subject = "Password Reset Code"
                                                mail.Body = "Your reset code is: " & resetCode

                                                smtp.Send(mail)

                                                MsgBox("Reset code sent to your email.", MsgBoxStyle.Information)
                                                dlgVerify.Close()

                                            Catch ex As Exception
                                                MsgBox("Email Error: " & ex.Message, MsgBoxStyle.Critical)
                                            End Try

                                        End Sub

            dlgVerify.ShowDialog()

            ' =========================
            ' STEP 2: VERIFY CODE + RESET PASSWORD
            ' =========================
            If verified Then

                Using dlgReset As New Form
                    dlgReset.Text = "Enter Code & Reset Password"
                    dlgReset.FormBorderStyle = FormBorderStyle.FixedDialog
                    dlgReset.StartPosition = FormStartPosition.CenterParent
                    dlgReset.Width = 350
                    dlgReset.Height = 250

                    Dim lblCode As New Label With {.Text = "Enter Reset Code:", .Top = 20, .Left = 10}
                    Dim txtCode As New TextBox With {.Top = 40, .Left = 10, .Width = 300}

                    Dim lblPass As New Label With {.Text = "New Password:", .Top = 70, .Left = 10}
                    Dim txtPass As New TextBox With {.Top = 90, .Left = 10, .Width = 300, .UseSystemPasswordChar = True}

                    Dim lblConfirm As New Label With {.Text = "Confirm Password:", .Top = 120, .Left = 10}
                    Dim txtConfirm As New TextBox With {.Top = 140, .Left = 10, .Width = 300, .UseSystemPasswordChar = True}

                    Dim btnReset As New Button With {.Text = "Reset Password", .Top = 170, .Left = 100, .Width = 120}

                    dlgReset.Controls.AddRange({lblCode, txtCode, lblPass, txtPass, lblConfirm, txtConfirm, btnReset})

                    AddHandler btnReset.Click, Sub()

                                                   If txtCode.Text.Trim <> resetCode Then
                                                       MsgBox("Invalid reset code.", MsgBoxStyle.Critical)
                                                       Exit Sub
                                                   End If

                                                   If txtPass.Text <> txtConfirm.Text Then
                                                       MsgBox("Passwords do not match.", MsgBoxStyle.Critical)
                                                       Exit Sub
                                                   End If

                                                   If txtPass.Text.Length < 8 OrElse Not System.Text.RegularExpressions.Regex.IsMatch(txtPass.Text, "[^a-zA-Z0-9]") Then
                                                       MsgBox("Password must be at least 8 characters and include a special character.", MsgBoxStyle.Exclamation)
                                                       Exit Sub
                                                   End If

                                                   Try
                                                       If connect.State <> ConnectionState.Open Then connect.Open()

                                                       Dim cmdUpdate As New OleDbCommand(
                                                       "UPDATE [user] SET [password]=? WHERE LCase(Email)=?", connect)

                                                       cmdUpdate.Parameters.AddWithValue("?", txtPass.Text.Trim)
                                                       cmdUpdate.Parameters.AddWithValue("?", txtEmail.Text.Trim.ToLower)

                                                       cmdUpdate.ExecuteNonQuery()

                                                       MsgBox("Password reset successfully.", MsgBoxStyle.Information)
                                                       dlgReset.Close()

                                                   Catch ex As Exception
                                                       MsgBox("Reset Error: " & ex.Message, MsgBoxStyle.Critical)
                                                   Finally
                                                       If connect.State = ConnectionState.Open Then connect.Close()
                                                   End Try

                                               End Sub

                    dlgReset.ShowDialog()

                End Using

            End If

        End Using

    End Sub

End Class