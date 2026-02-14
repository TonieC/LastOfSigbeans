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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' List cameras
        videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
        Label1.Text = "Status:" ' default
    End Sub

    ' Start Scan (Button2)
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If videoDevices Is Nothing OrElse videoDevices.Count = 0 Then Return

        videoSource = New VideoCaptureDevice(videoDevices(2).MonikerString)
        AddHandler videoSource.NewFrame, AddressOf Video_NewFrame
        videoSource.Start()
        scanning = True
    End Sub

    ' Stop Scan (Button3)
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        StopCamera()
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

    ' Camera Feed + QR Decode
    Private Sub Video_NewFrame(sender As Object, eventArgs As NewFrameEventArgs)
        If Not scanning Then Return

        Dim frame As Bitmap = CType(eventArgs.Frame.Clone(), Bitmap)
        frame.RotateFlip(RotateFlipType.RotateNoneFlipX)

        ' Draw QR rectangle
        Dim graphics As Graphics = Graphics.FromImage(frame)
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
                graphics.DrawRectangle(pen, Rectangle.Round(rect))
            End Using
        End If

        graphics.Dispose()

        ' Update PictureBox
        If PictureBox1.InvokeRequired Then
            PictureBox1.Invoke(Sub() PictureBox1.Image = CType(frame.Clone(), Bitmap))
        Else
            PictureBox1.Image = CType(frame.Clone(), Bitmap)
        End If

        ' Handle QR in background
        If result IsNot Nothing Then
            Dim qrText As String = result.Text
            Task.Run(Sub() HandleAttendanceAndUpdateLabel(qrText))
        End If

        frame.Dispose()
    End Sub

    ' Attendance Handling + Update Label
    Private Sub HandleAttendanceAndUpdateLabel(username As String)
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim fullName As String = ""
            Using cmd As New OleDbCommand("SELECT FullName FROM [login] WHERE username=?", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = username
                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        fullName = reader("FullName").ToString()
                    Else
                        Return
                    End If
                End Using
            End Using

            Dim exists As Boolean = False
            Using cmd As New OleDbCommand("SELECT COUNT(*) FROM [attendance] WHERE Username=? AND LoginDate=?", connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = username
                cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now.Date
                exists = CInt(cmd.ExecuteScalar()) > 0
            End Using

            ' Insert if not exists
            If Not exists Then
                Using cmd As New OleDbCommand(
                    "INSERT INTO [attendance] (Username, FullName, LoginDate, LoginTime) VALUES (?, ?, ?, ?)", connect)
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = username
                    cmd.Parameters.Add("?", OleDbType.VarChar).Value = fullName
                    cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now.Date
                    cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now
                    cmd.ExecuteNonQuery()
                End Using
            End If

            ' Update Label1 safely with attendance info
            Me.Invoke(Sub()
                          Label1.Text = $"Status: {fullName} ({username})"
                      End Sub)

        Catch ex As Exception
            ' silently ignore errors
        Finally
            connect.Close()
        End Try
    End Sub

    ' Cleanup on Form Closing
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        StopCamera()
    End Sub

    ' Manual Navigate to Form3 (Button1)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Dim f3 As New Form3
        f3.Show()
    End Sub

    ' Label1 Click (empty)
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Keep empty
    End Sub

End Class
