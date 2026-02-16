Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports ZXing
Imports ZXing.Windows.Compatibility
Imports System.Threading.Tasks

Public Class Form1

    ' Database connection
    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    ' Camera and scanner
    Private videoDevices As FilterInfoCollection
    Private videoSource As VideoCaptureDevice
    Private scanner As New BarcodeReader()
    Private scanning As Boolean = False

    ' Track current username pending attendance
    Private pendingUsername As String = ""
    Private pendingFullName As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
        Label1.Text = "Waiting for QR scan..."

        ' Hide password elements initially
        Label2.Visible = False
        TextBox1.Visible = False
        Button5.Visible = True
        Button5.Text = "Time In"

        ' Start camera immediately
        If videoDevices IsNot Nothing AndAlso videoDevices.Count > 0 Then
            videoSource = New VideoCaptureDevice(videoDevices(2).MonikerString)
            AddHandler videoSource.NewFrame, AddressOf Video_NewFrame
            videoSource.Start()
            scanning = True
        Else
            MsgBox("No camera detected")
        End If

        ' Set Button3 as Time Out
        Button3.Text = "Time Out"
    End Sub

    ' Time In (password submit)
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If pendingUsername = "" Then
            MsgBox("Scan QR code first")
            Return
        End If

        If TextBox1.Text.Trim() = "" Then
            MsgBox("Enter password first")
            Return
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim dbPassword As Object = Nothing

            ' Get password from login table
            Using cmd As New OleDbCommand("SELECT [password] FROM [login] WHERE username=?", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingUsername
                dbPassword = cmd.ExecuteScalar()
            End Using

            ' Check password
            If dbPassword Is DBNull.Value OrElse dbPassword.ToString() <> TextBox1.Text Then
                MsgBox("Incorrect password")
                Return
            End If

            ' Check if Time In already exists today
            Dim exists As Boolean = False
            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM [attendance] WHERE Username=? AND LoginDate=? AND Status='Time In'", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingUsername
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now.Date
                exists = CInt(cmd.ExecuteScalar()) > 0
            End Using

            ' Insert Time In if not exists
            If Not exists Then
                Using cmd As New OleDbCommand(
                    "INSERT INTO [attendance] (Username, FullName, LoginDate, LoginTime, Status) VALUES (?, ?, ?, ?, ?)", connect)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingUsername
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingFullName
                    cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now.Date
                    cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = "Time In"
                    cmd.ExecuteNonQuery()
                End Using
                MsgBox($"Time In recorded for {pendingFullName}")
            Else
                MsgBox("Time In already recorded for today")
            End If

            pendingUsername = ""
            pendingFullName = ""
            TextBox1.Clear()

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            connect.Close()
        End Try

        scanning = True
        Label2.Visible = False
        TextBox1.Visible = False
        UpdateStatusLabel()
    End Sub

    ' Time Out button
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If pendingUsername = "" Then
            MsgBox("Scan QR code first")
            Return
        End If

        ' Require password for Time Out as well
        If TextBox1.Text.Trim() = "" Then
            MsgBox("Enter password before Time Out")
            Return
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            ' Verify password first
            Dim dbPassword As Object = Nothing
            Using cmd As New OleDbCommand("SELECT [password] FROM [login] WHERE username=?", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingUsername
                dbPassword = cmd.ExecuteScalar()
            End Using

            If dbPassword Is DBNull.Value OrElse dbPassword.ToString() <> TextBox1.Text Then
                MsgBox("Incorrect password")
                Return
            End If

            ' Check if Time In exists and Time Out not exists
            Dim hasTimeIn As Boolean = False
            Dim hasTimeOut As Boolean = False

            Using cmd As New OleDbCommand(
                "SELECT COUNT(*) FROM attendance WHERE Username=? AND LoginDate=? AND Status='Time In'", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingUsername
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now.Date
                hasTimeIn = CInt(cmd.ExecuteScalar()) > 0
            End Using

            Using cmd As New OleDbCommand(
                "SELECT COUNT(*) FROM attendance WHERE Username=? AND LoginDate=? AND Status='Time Out'", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingUsername
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now.Date
                hasTimeOut = CInt(cmd.ExecuteScalar()) > 0
            End Using

            If Not hasTimeIn Then
                MsgBox("Cannot Time Out without first Time In")
                Return
            End If

            If hasTimeOut Then
                MsgBox("Time Out already recorded")
                Return
            End If

            ' Insert Time Out
            Using cmd As New OleDbCommand(
                "INSERT INTO [attendance] (Username, FullName, LoginDate, LoginTime, Status) VALUES (?, ?, ?, ?, ?)", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingUsername
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingFullName
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now.Date
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = "Time Out"
                cmd.ExecuteNonQuery()
            End Using

            MsgBox($"Time Out recorded for {pendingFullName}")

            pendingUsername = ""
            pendingFullName = ""
            TextBox1.Clear()

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            connect.Close()
        End Try

        scanning = True
        Label2.Visible = False
        TextBox1.Visible = False
        UpdateStatusLabel()
    End Sub

    ' Always update Label1 with current status
    Private Sub UpdateStatusLabel()
        Me.Invoke(Sub()
                      If scanning Then
                          If pendingUsername <> "" Then
                              Label1.Text = $"User scanned: {pendingUsername}"
                          Else
                              Label1.Text = "Waiting for QR scan..."
                          End If
                      Else
                          Label1.Text = "Processing..."
                      End If
                  End Sub)
    End Sub

    ' Camera feed + QR decode
    Private Sub Video_NewFrame(sender As Object, eventArgs As NewFrameEventArgs)
        If Not scanning Then Return

        Dim frame As Bitmap = CType(eventArgs.Frame.Clone(), Bitmap)
        frame.RotateFlip(RotateFlipType.RotateNoneFlipX)

        Dim decodeBitmap As Bitmap = CType(frame.Clone(), Bitmap)
        Dim result = scanner.Decode(decodeBitmap)
        decodeBitmap.Dispose()

        If result IsNot Nothing AndAlso result.ResultPoints IsNot Nothing AndAlso result.ResultPoints.Length >= 2 Then
            Dim pts = result.ResultPoints
            Dim minX As Single = pts.Min(Function(p) p.X)
            Dim minY As Single = pts.Min(Function(p) p.Y)
            Dim maxX As Single = pts.Max(Function(p) p.X)
            Dim maxY As Single = pts.Max(Function(p) p.Y)
            Dim rect As New RectangleF(minX, minY, maxX - minX, maxY - minY)

            Using pen As New Pen(Color.Red, 2)
                Using graphics As Graphics = Graphics.FromImage(frame)
                    graphics.DrawRectangle(pen, Rectangle.Round(rect))
                End Using
            End Using
        End If

        ' Update PictureBox safely
        If PictureBox1.InvokeRequired Then
            PictureBox1.Invoke(Sub()
                                   If PictureBox1.Image IsNot Nothing Then PictureBox1.Image.Dispose()
                                   PictureBox1.Image = CType(frame.Clone(), Bitmap)
                               End Sub)
        Else
            PictureBox1.Image = CType(frame.Clone(), Bitmap)
        End If

        ' Handle QR scan
        If result IsNot Nothing Then
            scanning = False ' pause scanning until action taken

            Try
                If connect.State = ConnectionState.Closed Then connect.Open()

                Using cmd As New OleDbCommand("SELECT FullName FROM [login] WHERE username=?", connect)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = result.Text
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            pendingUsername = result.Text
                            pendingFullName = reader("FullName").ToString()

                            ' Show password fields only after QR scan (on UI thread)
                            Me.Invoke(Sub()
                                          Label2.Visible = True
                                          TextBox1.Visible = True
                                      End Sub)
                        End If
                    End Using
                End Using

            Catch
            Finally
                connect.Close()
            End Try

            UpdateStatusLabel()
        End If

    End Sub

    ' Cleanup on Form Closing
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If videoSource IsNot Nothing AndAlso videoSource.IsRunning Then
            videoSource.SignalToStop()
            videoSource.WaitForStop()
        End If
    End Sub

    ' Manual Navigate to Form3 (Button1)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Hide()
        Dim f3 As New Form3
        f3.Show()
    End Sub

    ' Show Form2 (Button4)
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form2.Show()
        Me.Hide()
    End Sub

End Class
