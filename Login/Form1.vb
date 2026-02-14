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
        Label1.Text = "Status: Idle"

        ' Password elements always visible
        Label2.Visible = True
        Label2.Text = "Password:"
        TextBox1.Visible = True
        TextBox1.UseSystemPasswordChar = True
        Button5.Visible = True
        Button5.Text = "Submit Password"
    End Sub

    ' Start Scan (Button2)
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If videoDevices Is Nothing OrElse videoDevices.Count = 0 Then Return
        videoSource = New VideoCaptureDevice(videoDevices(2).MonikerString)
        AddHandler videoSource.NewFrame, AddressOf Video_NewFrame
        videoSource.Start()
        scanning = True
        UpdateStatusLabel()
    End Sub

    ' Stop Scan (Button3)
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        StopCamera()
        UpdateStatusLabel()
    End Sub

    Private Sub StopCamera()
        Task.Run(Sub()
                     If videoSource IsNot Nothing AndAlso videoSource.IsRunning Then
                         videoSource.SignalToStop()
                         videoSource.WaitForStop()
                     End If
                     scanning = False
                     If PictureBox1.InvokeRequired Then
                         PictureBox1.Invoke(Sub() PictureBox1.Image = Nothing)
                     Else
                         PictureBox1.Image = Nothing
                     End If
                 End Sub)
    End Sub

    ' Always update Label1 with current status
    Private Sub UpdateStatusLabel()
        Me.Invoke(Sub()
                      If scanning Then
                          If pendingUsername <> "" Then
                              Label1.Text = $"QR scanned: {pendingUsername} (enter password)"
                          Else
                              Label1.Text = "Scanning..."
                          End If
                      Else
                          Label1.Text = "Camera stopped"
                      End If
                  End Sub)
    End Sub

    ' Camera Feed + QR Decode
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
            Try
                If connect.State = ConnectionState.Closed Then connect.Open()

                Using cmd As New OleDbCommand("SELECT FullName, [password] FROM [login] WHERE username=?", connect)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = result.Text
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            pendingUsername = result.Text
                            pendingFullName = reader("FullName").ToString()
                        End If
                    End Using
                End Using

            Catch
            Finally
                connect.Close()
            End Try

            UpdateStatusLabel()
        End If

        frame.Dispose()
    End Sub

    ' Submit password for attendance
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If pendingUsername = "" Then Return
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

            ' Check if attendance already exists
            Dim exists As Boolean = False
            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM [attendance] WHERE Username=? AND LoginDate=?", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = pendingUsername
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now.Date
                exists = CInt(cmd.ExecuteScalar()) > 0
            End Using

            ' Insert attendance if first time today
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

                MsgBox($"Time In successfully added for {pendingFullName} ({pendingUsername})")
            Else
                MsgBox("Attendance already recorded for today")
            End If

            pendingUsername = ""
            pendingFullName = ""

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            connect.Close()
        End Try

        UpdateStatusLabel()
    End Sub

    ' Cleanup on Form Closing
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        StopCamera()
    End Sub

    ' Manual Navigate to Form3 (Button1)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Hide()
        Dim f3 As New Form3
        f3.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form2.Show()
        Me.Hide()
    End Sub

    ' Optional: Label1 click does nothing
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

End Class
