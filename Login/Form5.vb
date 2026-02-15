Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Imaging
Imports ZXing
Imports ZXing.Common
Imports ZXing.Rendering
Imports ZXing.Windows.Compatibility


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
            Using sfd As New SaveFileDialog()
                sfd.Filter = "PNG Image|*.png"
                sfd.FileName = Label4.Text & "_QR.png"
                If sfd.ShowDialog() = DialogResult.OK Then
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
        leaveForm.ShowDialog ' blocks until leave form closed

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

End Class
