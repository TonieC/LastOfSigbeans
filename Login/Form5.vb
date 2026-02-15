Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Imaging
Imports ZXing
Imports ZXing.Common
Imports ZXing.Rendering
Imports ZXing.Windows.Compatibility
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO


Public Class Form5


    Private loggedInUsername As String
    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    ' Constructor for Form5
    Public Sub New(username As String)
        InitializeComponent()
        loggedInUsername = username
    End Sub

    ' =========================
    ' Form Load
    ' =========================
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployeeData()
        LoadAttendanceData()
        Label14.Text = "Attendance Records" ' Default
        Button7.Text = "Show Leave Requests"

        If Label4.Text <> "" Then
            GenerateQRCode(Label4.Text)
        End If
    End Sub

    ' =========================
    ' Load Employee Data
    ' =========================
    Private Sub LoadEmployeeData()
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql As String =
                "SELECT EID, username, FullName, MobileN, Email, Address " &
                "FROM [login] WHERE username=?"

            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername

                Using reader As OleDbDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Label2.Text = reader("EID").ToString()
                        Label4.Text = reader("username").ToString()
                        Label6.Text = reader("FullName").ToString()
                        Label8.Text = reader("MobileN").ToString()
                        Label10.Text = reader("Email").ToString()
                        Label12.Text = reader("Address").ToString()
                    Else
                        MsgBox("Employee record not found")
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Dashboard error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' Logout
    ' =========================
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.Yes Then
            Dim loginForm As New Form3
            loginForm.Show()
            Close()
        End If
    End Sub

    ' =========================
    ' Generate QR Code
    ' =========================
    Private Sub GenerateQRCode(qrText As String)
        Try
            Dim renderer As IBarcodeRenderer(Of Bitmap) = New BitmapRenderer()
            Dim writer As New BarcodeWriter(Of Bitmap)() With {
                .Renderer = renderer,
                .Format = BarcodeFormat.QR_CODE,
                .Options = New EncodingOptions() With {
                    .Width = 200,
                    .Height = 200,
                    .Margin = 1
                }
            }

            Dim qrBitmap As Bitmap = writer.Write(qrText)
            PictureBox1.Image = qrBitmap

        Catch ex As Exception
            MsgBox("QR code generation error: " & ex.Message)
        End Try
    End Sub

    ' =========================
    ' Save QR Code
    ' =========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If PictureBox1.Image IsNot Nothing Then
            Using sfd As New SaveFileDialog
                sfd.Filter = "PNG Image|*.png"
                sfd.FileName = Label4.Text & "_QR.png"
                If sfd.ShowDialog = DialogResult.OK Then
                    PictureBox1.Image.Save(sfd.FileName, ImageFormat.Png)
                    MsgBox("QR code saved successfully!")
                End If
            End Using
        Else
            MsgBox("QR code not available")
        End If
    End Sub

    ' =========================
    ' Refresh QR Code
    ' =========================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Label4.Text <> "" Then
            GenerateQRCode(Label4.Text)
            MsgBox("QR code refreshed!")
        Else
            MsgBox("Cannot refresh QR code: missing username")
        End If
    End Sub

    ' =========================
    ' Time Out / Logout
    ' =========================
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.Yes Then
            Try
                If connect.State = ConnectionState.Closed Then connect.Open()

                Using cmd As New OleDbCommand(
                    "INSERT INTO [attendance] (Username, FullName, LoginDate, LoginTime, Status) VALUES (?, ?, ?, ?, ?)", connect)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = Label6.Text
                    cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now.Date
                    cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = "Time Out"
                    cmd.ExecuteNonQuery()
                End Using

                MsgBox($"Time Out recorded for {Label6.Text} ({loggedInUsername})")

            Catch ex As Exception
                MsgBox("Error recording Time Out: " & ex.Message)
            Finally
                connect.Close()
            End Try

            Dim loginForm As New Form3
            loginForm.Show()
            Close()
        End If
    End Sub

    ' =========================
    ' Last Leave Request Status
    ' =========================
    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql As String =
                "SELECT TOP 1 Status FROM [request] WHERE EID=? ORDER BY RequestDate DESC"

            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = Label2.Text

                Dim result = cmd.ExecuteScalar()
                Label16.Text = If(result IsNot Nothing, result.ToString(), "No requests")
            End Using

        Catch ex As Exception
            MsgBox("Status load error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' Open Leave Request Form
    ' =========================
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim leaveForm As New Form7(loggedInUsername)
        leaveForm.ShowDialog() ' blocks until leave form closed

        ' Refresh last request status if user submitted a new request
        Label16_Click(Label16, EventArgs.Empty)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        ' 1. Ask for current password
        Dim oldPassword As String = InputBox("Enter your current password:", "Change Password")
        If oldPassword = "" Then Exit Sub

        ' 2. Verify old password from database
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim checkSql As String = "SELECT COUNT(*) FROM [login] WHERE username=? AND [password]=?"
            Using checkCmd As New OleDbCommand(checkSql, connect)
                checkCmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                checkCmd.Parameters.Add("?", OleDbType.VarChar).Value = oldPassword

                Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                If exists = 0 Then
                    MsgBox("Incorrect current password.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End Using

            ' 3. Ask for new password
            Dim newPassword As String = InputBox("Enter your NEW password:", "Change Password")
            If newPassword = "" Then
                MsgBox("New password cannot be empty.")
                Exit Sub
            End If

            ' 4. Confirm new password
            Dim confirmPassword As String = InputBox("Confirm your NEW password:", "Change Password")
            If newPassword <> confirmPassword Then
                MsgBox("Passwords do not match.", MsgBoxStyle.Critical)
                Exit Sub
            End If

            ' 5. Update password
            Dim updateSql As String = "UPDATE [login] SET [password]=? WHERE username=?"
            Using updateCmd As New OleDbCommand(updateSql, connect)
                updateCmd.Parameters.Add("?", OleDbType.VarChar).Value = newPassword
                updateCmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                updateCmd.ExecuteNonQuery()
            End Using

            MsgBox("Password changed successfully.", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox("Password change error: " & ex.Message)
        Finally
            connect.Close()
        End Try

    End Sub

    ' =========================
    ' Load Attendance Data
    ' =========================
    Private Sub LoadAttendanceData()
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql As String = "SELECT LoginDate, LoginTime, Status FROM [attendance] WHERE Username=? ORDER BY LoginDate DESC, LoginTime DESC"

            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername

                Dim adapter As New OleDbDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                DataGridView1.DataSource = dt

                ' Optional: make columns look nicer
                DataGridView1.Columns("LoginDate").HeaderText = "Date"
                DataGridView1.Columns("LoginTime").HeaderText = "Time"
                DataGridView1.Columns("Status").HeaderText = "Status"
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End Using

        Catch ex As Exception
            MsgBox("Error loading attendance: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

    ' =========================
    ' Switch to showing Attendance
    ' =========================
    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click
        LoadAttendanceData()
    End Sub

    ' =========================
    ' Switch to showing Leave Requests
    ' =========================
    ' Track current view
    Private showingAttendance As Boolean = True

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If showingAttendance Then
            ' Switch to Leave Requests
            Try
                If connect.State = ConnectionState.Closed Then connect.Open()

                Dim sql = "SELECT Request, Status FROM [request] WHERE EID=? ORDER BY RequestDate DESC"
                Using cmd As New OleDbCommand(sql, connect)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = Label2.Text ' EID of logged-in user

                    Dim adapter As New OleDbDataAdapter(cmd)
                    Dim dt As New DataTable
                    adapter.Fill(dt)

                    DataGridView1.DataSource = dt
                    DataGridView1.Columns("Request").HeaderText = "Leave Request"
                    DataGridView1.Columns("Status").HeaderText = "Status"
                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End Using

                showingAttendance = False
                Label14.Text = "Request Records"
                Button7.Text = "Show Attendance"

            Catch ex As Exception
                MsgBox("Error loading leave requests: " & ex.Message)
            Finally
                connect.Close()
            End Try
        Else
            ' Switch to Attendance
            LoadAttendanceData()
            showingAttendance = True
            Label14.Text = "Attendance Records"
            Button7.Text = "Show Leave Requests"
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql As String
            Dim cmd As New OleDbCommand()
            cmd.Connection = connect

            If showingAttendance Then
                ' Filter attendance
                sql = "SELECT LoginDate, LoginTime, Status FROM [attendance] WHERE Username=? AND LoginDate=? ORDER BY LoginDate DESC, LoginTime DESC"
                cmd.CommandText = sql
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTimePicker1.Value.Date
            Else
                ' Filter leave requests (assuming RequestDate exists)
                sql = "SELECT Request, Status FROM [request] WHERE EID=? AND RequestDate=? ORDER BY RequestDate DESC"
                cmd.CommandText = sql
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = Label2.Text
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTimePicker1.Value.Date
            End If

            Dim adapter As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            DataGridView1.DataSource = dt

        Catch ex As Exception
            MsgBox("Error filtering by date: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("No data to export.")
            Exit Sub
        End If

        Using sfd As New SaveFileDialog
            sfd.Filter = "PDF File|*.pdf"
            sfd.FileName = If(showingAttendance, "AttendanceRecords.pdf", "RequestRecords.pdf")
            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Dim doc As New Document(PageSize.A4, 10, 10, 10, 10)
                    PdfWriter.GetInstance(doc, New FileStream(sfd.FileName, FileMode.Create))
                    doc.Open()

                    ' Add title
                    Dim title As String = If(showingAttendance, "Attendance Records", "Request Records")
                    doc.Add(New Paragraph(title & " - " & DateTime.Now.ToString("g")))
                    doc.Add(New Paragraph(" "))

                    ' Add table
                    Dim pdfTable As New PdfPTable(DataGridView1.Columns.Count)
                    pdfTable.WidthPercentage = 100

                    ' Add headers
                    For Each col As DataGridViewColumn In DataGridView1.Columns
                        pdfTable.AddCell(New Phrase(col.HeaderText))
                    Next

                    ' Add rows
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If Not row.IsNewRow Then
                            For Each cell As DataGridViewCell In row.Cells
                                pdfTable.AddCell(If(cell.Value?.ToString(), ""))
                            Next
                        End If
                    Next

                    doc.Add(pdfTable)
                    doc.Close()

                    MsgBox("PDF exported successfully!")

                Catch ex As Exception
                    MsgBox("Error exporting PDF: " & ex.Message)
                End Try
            End If
        End Using
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ' Step 1: Confirm user wants to edit
        If MsgBox("Do you want to change any of your profile information?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Edit") = MsgBoxResult.No Then
            Exit Sub
        End If

        ' Step 2: Ask which field to change
        Dim fieldToChange As String = InputBox("Which field do you want to change? (username, FullName, MobileN, Email, Address)", "Select Field").Trim()
        If String.IsNullOrEmpty(fieldToChange) Then Exit Sub

        ' Validate field
        Dim validFields As String() = {"username", "FullName", "MobileN", "Email", "Address"}
        If Not validFields.Contains(fieldToChange) Then
            MsgBox("Invalid field selected.")
            Exit Sub
        End If

        ' Step 3: Ask for new value
        Dim newValue As String = InputBox($"Enter new value for {fieldToChange}:", "New Value").Trim()
        If String.IsNullOrEmpty(newValue) Then
            MsgBox("New value cannot be empty.")
            Exit Sub
        End If

        ' Step 4: Ask for password to verify
        Dim password As String = InputBox("Enter your current password to confirm changes:", "Verify Password").Trim()
        If String.IsNullOrEmpty(password) Then Exit Sub

        ' Step 5: Verify password and update
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            ' Check password
            Dim checkSql As String = "SELECT COUNT(*) FROM [login] WHERE username=? AND [password]=?"
            Using checkCmd As New OleDbCommand(checkSql, connect)
                checkCmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                checkCmd.Parameters.Add("?", OleDbType.VarChar).Value = password

                Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                If exists = 0 Then
                    MsgBox("Incorrect password. Cannot update profile.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End Using

            ' Update the selected field
            Dim updateSql As String = $"UPDATE [login] SET [{fieldToChange}]=? WHERE username=?"
            Using updateCmd As New OleDbCommand(updateSql, connect)
                updateCmd.Parameters.Add("?", OleDbType.VarChar).Value = newValue
                updateCmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                Dim rowsAffected As Integer = updateCmd.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MsgBox($"{fieldToChange} updated successfully!")

                    ' Update local label if applicable
                    Select Case fieldToChange
                        Case "username" : Label4.Text = newValue
                        Case "FullName" : Label6.Text = newValue
                        Case "MobileN" : Label8.Text = newValue
                        Case "Email" : Label10.Text = newValue
                        Case "Address" : Label12.Text = newValue
                    End Select
                Else
                    MsgBox("Update failed. No changes made.")
                End If
            End Using

        Catch ex As Exception
            MsgBox("Error updating profile: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

End Class

