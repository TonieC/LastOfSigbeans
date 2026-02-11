Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing.Drawing2D

Public Class Form5

    Private loggedInUsername As String

    Private connect As New OleDbConnection(
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")

    ' RECEIVE USERNAME FROM LOGIN FORM
    Public Sub New(username As String)
        InitializeComponent()
        loggedInUsername = username
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEmployeeData()
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
                        ' Update your labels
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
        ' Optional: confirm logout
        If MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Logout") = MsgBoxResult.Yes Then
            ' Show login form again
            Dim loginForm As New Form3
            loginForm.Show()

            ' Close current dashboard
            Me.Close()
        End If
    End Sub

End Class
