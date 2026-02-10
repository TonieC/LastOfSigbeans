Imports System.Data.OleDb

Public Class Form1
    ' Database connection
    Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Administrator\OneDrive\Documents\Login.accdb")
    Dim dt As New DataTable

    ' Load attendance when the form opens
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAttendance()
    End Sub

    ' Button to open Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
        Me.Hide()
    End Sub

    ' Button to mark attendance
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Check if username and full name are entered
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MessageBox.Show("Please enter your username and full name.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            conn.Open()
            Dim query As String = "INSERT INTO Attendance (Username, FullName, LoginDate, LoginTime) VALUES (@Username, @FullName, @LoginDate, @LoginTime)"
            Using cmd As New OleDbCommand(query, conn)
                cmd.Parameters.Add("@Username", OleDbType.VarChar).Value = TextBox1.Text
                cmd.Parameters.Add("@FullName", OleDbType.VarChar).Value = TextBox2.Text
                cmd.Parameters.Add("@LoginDate", OleDbType.Date).Value = DateTime.Now.Date
                cmd.Parameters.Add("@LoginTime", OleDbType.Date).Value = DateTime.Now
                cmd.ExecuteNonQuery()
            End Using


            MessageBox.Show("Attendance marked successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadAttendance() ' Refresh the DataGridView
        Catch ex As Exception
            MessageBox.Show("Error marking attendance: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    ' Method to load attendance into the DataGridView
    Private Sub LoadAttendance()
        Try
            Dim query As String = "SELECT Username, FullName, LoginDate, LoginTime FROM Attendance ORDER BY LoginDate DESC, LoginTime DESC"
            Dim da As New OleDbDataAdapter(query, conn)
            dt.Clear()
            da.Fill(dt)
            DataGridView1.DataSource = dt

            ' Make grid read-only, auto-size columns, full row select
            DataGridView1.ReadOnly = True
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            ' Format LoginTime column to show only time
            If DataGridView1.Columns.Contains("LoginTime") Then
                DataGridView1.Columns("LoginTime").DefaultCellStyle.Format = "HH:mm:ss"
            End If

            ' Format LoginDate column to show only date
            If DataGridView1.Columns.Contains("LoginDate") Then
                DataGridView1.Columns("LoginDate").DefaultCellStyle.Format = "dd/MM/yyyy"
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading attendance: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
