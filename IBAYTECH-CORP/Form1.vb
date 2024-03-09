Imports Npgsql
Public Class Form1
    Dim con As New NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=password;Database=db")
    Dim command As NpgsqlCommand
    Dim adapter As New NpgsqlDataAdapter
    Dim dt As New DataTable
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshData()
    End Sub
    Private Sub refreshData()
        dt.Clear()
        con.Open()
        Dim query As String = "SELECT * FROM public.database"
        adapter = New NpgsqlDataAdapter(query, con)
        adapter.Fill(dt)
        DataGridView1.DataSource = dt
        con.Close()
    End Sub
    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        con.Open()
        Dim query As String = "INSERT INTO database (fname, lname) VALUES (@val1, @val2)"
        command = New NpgsqlCommand(query, con)
        command.Parameters.AddWithValue("@val1", txtValue1.Text)
        command.Parameters.AddWithValue("@val2", txtValue2.Text)
        command.ExecuteNonQuery()
        con.Close()
        refreshData()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        con.Open()
        Dim query As String = "UPDATE database SET fname=@val1, lname=@val2 WHERE ID=@id"
        command = New NpgsqlCommand(query, con)
        command.Parameters.AddWithValue("@val1", txtValue1.Text)
        command.Parameters.AddWithValue("@val2", txtValue2.Text)
        command.Parameters.AddWithValue("@id", DataGridView1.CurrentRow.Cells(0).Value)
        command.ExecuteNonQuery()
        con.Close()
        refreshData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        con.Open()
        Dim query As String = "DELETE FROM database WHERE ID=@id"
        command = New NpgsqlCommand(query, con)
        command.Parameters.AddWithValue("@id", DataGridView1.CurrentRow.Cells(0).Value)
        command.ExecuteNonQuery()
        con.Close()
        refreshData()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow
            row = Me.DataGridView1.Rows(e.RowIndex)
            txtValue1.Text = row.Cells("Column1").Value.ToString
            txtValue2.Text = row.Cells("Column2").Value.ToString
        End If
    End Sub
End Class
