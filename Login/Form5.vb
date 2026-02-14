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

    Public Sub New(username As String)
        InitializeComponent()
        loggedInUsername = username
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployeeData()
        If Label4.Text <> "" Then
            GenerateQRCode(Label4.Text)
        End If
    End Sub

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.Yes Then
            Dim loginForm As New Form3
            loginForm.Show()
            Close()
        End If
    End Sub

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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim qrContent As String = Label4.Text
        If qrContent <> "" Then
            GenerateQRCode(qrContent)
            MsgBox("QR code refreshed!")
        Else
            MsgBox("Cannot refresh QR code: missing username")
        End If
    End Sub

    ' Logout / Time Out
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.Yes Then
            Try
                If connect.State = ConnectionState.Closed Then connect.Open()

                ' Insert a Time Out record in attendance table
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

            ' Go back to login
            Dim loginForm As New Form3
            loginForm.Show()
            Close()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        ' Validate request message
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MsgBox("Please enter your leave request.")
            Exit Sub
        End If

        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Using cmd As New OleDbCommand(
                "INSERT INTO [request] (EID, FullName, [Request], Status, RequestDate) VALUES (?, ?, ?, ?, ?)",
                connect)

                ' IMPORTANT: Order matters in OLEDB
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = Label2.Text        ' EID
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = Label6.Text        ' FullName
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = TextBox1.Text.Trim ' Request
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = "Pending"          ' Status
                cmd.Parameters.Add("?", OleDbType.Date).Value = Date.Now              ' RequestDate

                cmd.ExecuteNonQuery()
            End Using

            MsgBox("Leave request submitted successfully.")
            TextBox1.Clear()

        Catch ex As Exception
            MsgBox("Leave request error: " & ex.Message)
        Finally
            connect.Close()
        End Try

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click
        Try
            If connect.State = ConnectionState.Closed Then connect.Open()

            Dim sql As String =
                "SELECT TOP 1 Status " &
                "FROM [request] " &
                "WHERE EID=? " &
                "ORDER BY RequestDate DESC"

            Using cmd As New OleDbCommand(sql, connect)
                cmd.Parameters.Add("?", OleDbType.VarChar).Value = Label2.Text ' EID

                Dim result = cmd.ExecuteScalar()

                If result IsNot Nothing Then
                    Label16.Text = result.ToString()
                Else
                    Label16.Text = "No requests"
                End If
            End Using

        Catch ex As Exception
            MsgBox("Status load error: " & ex.Message)
        Finally
            connect.Close()
        End Try
    End Sub

End Class
