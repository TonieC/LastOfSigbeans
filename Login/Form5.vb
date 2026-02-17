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
    Private loggedInRole As String
    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    ' =========================
    ' Constructor
    ' =========================
    Public Sub New(username As String, role As String)
        InitializeComponent()
        loggedInUsername = username
        loggedInRole = role.ToLower()
    End Sub

    ' =========================
    ' Form Load
    ' =========================
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployeeData()
        LoadAttendanceData()
        Label14.Text = "Attendance Records"
        Button7.Text = "Show Leave Requests"

        If Label4.Text <> "" Then
            GenerateQRCode(Label4.Text)
        End If

        ' ===== Role-based Button4 logic =====
        Select Case loggedInRole
            Case "admin"
                Button4.Visible = True
                Button4.Text = "Open Admin Panel"
            Case "staff"
                Button4.Visible = True
                Button4.Text = "Open Staff Panel"
            Case Else
                Button4.Visible = False
        End Select
    End Sub

    ' =========================
    ' Load Employee Data
    ' =========================
    Private Sub LoadEmployeeData()
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql As String =
            "SELECT EID, username, FullName, MobileN, Email, Address, Gender, Age, Department " &
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
                        Label20.Text = reader("Gender").ToString()
                        Label18.Text = reader("Age").ToString()
                        Label21.Text = reader("Department").ToString()
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
                .Options = New EncodingOptions() With {.Width = 200, .Height = 200, .Margin = 1}
            }
            PictureBox1.Image = writer.Write(qrText)
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
    ' Admin/Staff Button
    ' =========================
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Select Case loggedInRole
            Case "admin"
                Dim adminForm As New Form4(loggedInUsername)
                adminForm.Show()
            Case "staff"
                Dim staffForm As New Form6(loggedInUsername)
                staffForm.Show()
            Case Else
                Button4.Visible = False
        End Select
    End Sub

    ' =========================
    ' Last Leave Request Status
    ' =========================
    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Dim sql As String = "SELECT TOP 1 Status FROM [request] WHERE EID=? ORDER BY RequestDate DESC"
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
        leaveForm.ShowDialog()
        Label16_Click(Label16, EventArgs.Empty)
    End Sub

    ' =========================
    ' Change Password
    ' =========================
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim oldPassword As String = InputBox("Enter your current password:", "Change Password")
        If oldPassword = "" Then Exit Sub

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Dim checkSql As String = "SELECT COUNT(*) FROM [login] WHERE username=? AND [password]=?"
            Using checkCmd As New OleDbCommand(checkSql, connect)
                checkCmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                checkCmd.Parameters.Add("?", OleDbType.VarChar).Value = oldPassword
                If Convert.ToInt32(checkCmd.ExecuteScalar()) = 0 Then
                    MsgBox("Incorrect current password.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End Using

            Dim newPassword As String = InputBox("Enter your NEW password:", "Change Password")
            If newPassword = "" Then Exit Sub
            Dim confirmPassword As String = InputBox("Confirm your NEW password:", "Change Password")
            If newPassword <> confirmPassword Then
                MsgBox("Passwords do not match.", MsgBoxStyle.Critical)
                Exit Sub
            End If

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
    ' Switch Attendance / Leave Requests
    ' =========================
    Private showingAttendance As Boolean = True
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If showingAttendance Then
            Try
                If connect.State = ConnectionState.Closed Then connect.Open()
                Dim sql = "SELECT Request, Status FROM [request] WHERE EID=? ORDER BY RequestDate DESC"
                Using cmd As New OleDbCommand(sql, connect)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = Label2.Text
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
            LoadAttendanceData()
            showingAttendance = True
            Label14.Text = "Attendance Records"
            Button7.Text = "Show Leave Requests"
        End If
    End Sub

    ' =========================
    ' Date Filter
    ' =========================
    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Dim cmd As New OleDbCommand() With {.Connection = connect}
            If showingAttendance Then
                cmd.CommandText = "SELECT LoginDate, LoginTime, Status FROM [attendance] WHERE Username=? AND LoginDate=? ORDER BY LoginDate DESC, LoginTime DESC"
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTimePicker1.Value.Date
            Else
                cmd.CommandText = "SELECT Request, Status FROM [request] WHERE EID=? AND RequestDate=? ORDER BY RequestDate DESC"
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

    ' =========================
    ' Export to PDF
    ' =========================
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

                    doc.Add(New Paragraph(If(showingAttendance, "Attendance Records", "Request Records") & " - " & DateTime.Now.ToString("g")))
                    doc.Add(New Paragraph(" "))

                    Dim pdfTable As New PdfPTable(DataGridView1.Columns.Count)
                    pdfTable.WidthPercentage = 100

                    For Each col As DataGridViewColumn In DataGridView1.Columns
                        pdfTable.AddCell(New Phrase(col.HeaderText))
                    Next
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

    ' =========================
    ' Edit Profile
    ' =========================
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If MsgBox("Do you want to change any of your profile information?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Edit") = MsgBoxResult.No Then Exit Sub

        Dim fieldToChange As String = InputBox("Which field do you want to change? (FullName, MobileN, Email, Address, Gender, Age, Department)", "Select Field").Trim()
        If String.IsNullOrEmpty(fieldToChange) Then Exit Sub

        Dim validFields As String() = {"FullName", "MobileN", "Email", "Address", "Gender", "Age", "Department"}
        If Not validFields.Contains(fieldToChange) Then
            MsgBox("Invalid field selected.")
            Exit Sub
        End If

        Dim newValue As String = InputBox($"Enter new value for {fieldToChange}:", "New Value").Trim()
        If String.IsNullOrEmpty(newValue) Then
            MsgBox("New value cannot be empty.")
            Exit Sub
        End If

        Dim password As String = InputBox("Enter your current password to confirm changes:", "Verify Password").Trim()
        If String.IsNullOrEmpty(password) Then Exit Sub

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()
            Dim checkSql As String = "SELECT COUNT(*) FROM [login] WHERE username=? AND [password]=?"
            Using checkCmd As New OleDbCommand(checkSql, connect)
                checkCmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                checkCmd.Parameters.Add("?", OleDbType.VarChar).Value = password
                If Convert.ToInt32(checkCmd.ExecuteScalar()) = 0 Then
                    MsgBox("Incorrect password. Cannot update profile.", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End Using

            Dim updateSql As String = $"UPDATE [login] SET [{fieldToChange}]=? WHERE username=?"
            Using updateCmd As New OleDbCommand(updateSql, connect)
                If fieldToChange = "Age" Then
                    updateCmd.Parameters.Add("?", OleDbType.Integer).Value = CInt(newValue)
                Else
                    updateCmd.Parameters.Add("?", OleDbType.VarChar).Value = newValue
                End If
                updateCmd.Parameters.Add("?", OleDbType.VarChar).Value = loggedInUsername
                If updateCmd.ExecuteNonQuery() > 0 Then
                    MsgBox($"{fieldToChange} updated successfully!")
                    Select Case fieldToChange
                        Case "FullName" : Label6.Text = newValue
                        Case "MobileN" : Label8.Text = newValue
                        Case "Email" : Label10.Text = newValue
                        Case "Address" : Label12.Text = newValue
                        Case "Gender" : Label20.Text = newValue
                        Case "Age" : Label18.Text = newValue
                        Case "Department" : Label21.Text = newValue
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
