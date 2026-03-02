Imports System.Data.OleDb
Imports System.Data

Public Class Form3

    Private ReadOnly connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb"
    )

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
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

    ' =========================
    ' FORGOT PASSWORD MODAL
    ' =========================
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked

        ' Step 1: Verification modal
        Using dlgVerify As New Form
            dlgVerify.Text = "Verify Account"
            dlgVerify.FormBorderStyle = FormBorderStyle.FixedDialog
            dlgVerify.StartPosition = FormStartPosition.CenterParent
            dlgVerify.MaximizeBox = False
            dlgVerify.MinimizeBox = False
            dlgVerify.Width = 350
            dlgVerify.Height = 300
            dlgVerify.ShowInTaskbar = False

            ' Controls
            Dim lblFull As New Label With {.Text = "Full Name:", .Top = 20, .Left = 10, .AutoSize = True}
            Dim txtFull As New TextBox With {.Top = 40, .Left = 10, .Width = 300}

            Dim lblMobile As New Label With {.Text = "Mobile Number:", .Top = 70, .Left = 10, .AutoSize = True}
            Dim txtMobile As New TextBox With {.Top = 90, .Left = 10, .Width = 300}

            Dim lblEmail As New Label With {.Text = "Email:", .Top = 120, .Left = 10, .AutoSize = True}
            Dim txtEmail As New TextBox With {.Top = 140, .Left = 10, .Width = 300}

            Dim lblDept As New Label With {.Text = "Department:", .Top = 170, .Left = 10, .AutoSize = True}
            Dim cmbDept As New ComboBox With {.Top = 190, .Left = 10, .Width = 300}
            cmbDept.Items.AddRange({"ICT", "HUMMS", "ABM", "STEM", "HE", "GAS", "EDUC", "PSYCHOLOGY", "CRIMINOLOGY", "CABAM", "Junior High School"})
            cmbDept.SelectedIndex = 0

            Dim btnVerify As New Button With {.Text = "Verify", .Top = 230, .Left = 120, .Width = 100}

            dlgVerify.Controls.AddRange({lblFull, txtFull, lblMobile, txtMobile, lblEmail, txtEmail, lblDept, cmbDept, btnVerify})

            Dim verified As Boolean = False

            AddHandler btnVerify.Click, Sub()
                                            Try
                                                If connect.State <> ConnectionState.Open Then connect.Open()

                                                Dim cmd As New OleDbCommand(
                                                    "SELECT COUNT(*) FROM [user] WHERE LCase(FullName)=? AND MobileN=? AND LCase(Email)=? AND LCase(Department)=?", connect)
                                                cmd.Parameters.Add("?", OleDbType.VarWChar).Value = txtFull.Text.Trim().ToLower()
                                                cmd.Parameters.Add("?", OleDbType.VarWChar).Value = txtMobile.Text.Trim()
                                                cmd.Parameters.Add("?", OleDbType.VarWChar).Value = txtEmail.Text.Trim().ToLower()
                                                cmd.Parameters.Add("?", OleDbType.VarWChar).Value = cmbDept.SelectedItem.ToString().ToLower()

                                                If CInt(cmd.ExecuteScalar()) > 0 Then
                                                    verified = True
                                                    dlgVerify.Close()
                                                Else
                                                    MsgBox("Details do not match.", MsgBoxStyle.Critical)
                                                End If
                                            Catch ex As Exception
                                                MsgBox("Error: " & ex.Message)
                                            End Try
                                        End Sub

            dlgVerify.ShowDialog()

            ' Step 2: Reset password modal
            If verified Then
                Using dlgReset As New Form
                    dlgReset.Text = "Reset Password"
                    dlgReset.FormBorderStyle = FormBorderStyle.FixedDialog
                    dlgReset.StartPosition = FormStartPosition.CenterParent
                    dlgReset.MaximizeBox = False
                    dlgReset.MinimizeBox = False
                    dlgReset.Width = 350
                    dlgReset.Height = 200
                    dlgReset.ShowInTaskbar = False

                    Dim lblPass As New Label With {.Text = "New Password:", .Top = 20, .Left = 10, .AutoSize = True}
                    Dim txtPass As New TextBox With {.Top = 40, .Left = 10, .Width = 300, .UseSystemPasswordChar = True}

                    Dim lblConfirm As New Label With {.Text = "Confirm Password:", .Top = 70, .Left = 10, .AutoSize = True}
                    Dim txtConfirm As New TextBox With {.Top = 90, .Left = 10, .Width = 300, .UseSystemPasswordChar = True}

                    Dim btnReset As New Button With {.Text = "Reset Password", .Top = 130, .Left = 120, .Width = 100}

                    dlgReset.Controls.AddRange({lblPass, txtPass, lblConfirm, txtConfirm, btnReset})

                    AddHandler btnReset.Click, Sub()
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
                                                       Dim cmd As New OleDbCommand(
                                                           "UPDATE [user] SET [password]=? WHERE LCase(FullName)=? AND MobileN=? AND LCase(Email)=? AND LCase(Department)=?", connect)
                                                       cmd.Parameters.Add("?", OleDbType.VarWChar).Value = txtPass.Text
                                                       cmd.Parameters.Add("?", OleDbType.VarWChar).Value = txtFull.Text.Trim().ToLower()
                                                       cmd.Parameters.Add("?", OleDbType.VarWChar).Value = txtMobile.Text.Trim()
                                                       cmd.Parameters.Add("?", OleDbType.VarWChar).Value = txtEmail.Text.Trim().ToLower()
                                                       cmd.Parameters.Add("?", OleDbType.VarWChar).Value = cmbDept.SelectedItem.ToString().ToLower()
                                                       cmd.ExecuteNonQuery()
                                                       MsgBox("Password reset successfully.")
                                                       dlgReset.Close()
                                                   Catch ex As Exception
                                                       MsgBox("Error: " & ex.Message)
                                                   End Try
                                               End Sub

                    dlgReset.ShowDialog()
                End Using
            End If
        End Using
    End Sub
End Class