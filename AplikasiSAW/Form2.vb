Imports System.Data.OleDb

Public Class Form2
    Sub menampilkan()
        da = New OleDbDataAdapter("select * from TabelBobot", konn)
        ds = New DataSet
        da.Fill(ds, "TabelBobot")
        DataGridView1.DataSource = ds.Tables("TabelBobot")
        DataGridView1.Columns(0).Visible = False
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Bukakoneksi()
        menampilkan()
    End Sub

    Private Sub UpdateData()
        Dim kon As String
        kon = "Provider=Microsoft.jet.oledb.4.0; data source=AppSAW.mdb"
        Dim con As New OleDbConnection(kon)
        Dim cmd As New OleDbCommand
        Dim x1, x2, x3, x4, x5 As Double
        Dim idx As Integer

        idx = DataGridView1.Rows(0).Cells(0).Value
        x1 = DataGridView1.Rows(0).Cells(1).Value
        x2 = DataGridView1.Rows(0).Cells(2).Value
        x3 = DataGridView1.Rows(0).Cells(3).Value
        x4 = DataGridView1.Rows(0).Cells(4).Value
        x5 = DataGridView1.Rows(0).Cells(5).Value

        If (Val(x1 + x2 + x3 + x4 + x5) = 1) Then
            Try
                con.Open()

                cmd.Connection = con
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "Update TabelBobot SET x1 = ?, x2 = ?, x3 = ?, x4 = ?, x5 = ? WHERE ID = ?"

                cmd.Parameters.Add("@x1", OleDbType.Double).Value = x1
                cmd.Parameters.Add("@x2", OleDbType.Double).Value = x2
                cmd.Parameters.Add("@x3", OleDbType.Double).Value = x3
                cmd.Parameters.Add("@x4", OleDbType.Double).Value = x4
                cmd.Parameters.Add("@x5", OleDbType.Double).Value = x5
                cmd.Parameters.Add("@ID", OleDbType.Integer).Value = idx

                cmd.ExecuteNonQuery()

                con.Close()
                MessageBox.Show("Pengubahan Nilai Bobot Berhasil.")
            Catch ex As Exception
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                MessageBox.Show("Gagal Merubah Nilai Bobot")
            End Try
        Else
            MessageBox.Show("Jumlah Total Bobot Harus sama dengan 1.")
        End If

    End Sub

    Private Sub DataGridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DataGridView1.KeyPress
        If e.KeyChar = Chr(13) Then
            Bukakoneksi()
            Call UpdateData()
        End If
    End Sub
End Class