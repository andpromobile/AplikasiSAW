Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Form4
    Dim kon As String = "Provider=Microsoft.jet.oledb.4.0; data source=AppSAW.mdb"
    Dim con As New OleDbConnection(kon)
    Dim cmd As New OleDbCommand
    Dim emptyCMD As New SqlCommand(kon)

    Dim MaxMin(2, 5) As Integer


    Sub Menampilkan()
        da = New OleDbDataAdapter("select ID, PengKerja as C1, HubInterpersonal as C2, Motivasi as C3, Wawasan as C4, Kinerja as C5  from TabelCalon", con)
        ds = New DataSet
        da.Fill(ds, "TabelCalon")
        DataGridView1.DataSource = ds.Tables("TabelCalon")
        DataGridView1.Columns(0).Visible = False
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Bukakoneksi()
        Menampilkan()
    End Sub

    Private Sub ProsesNormalisasi()
        Dim r1, r2, r3, r4, r5 As Double
        Dim reader As OleDbDataReader
        Dim j As Integer

        ' Try
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT MAX(PengKerja), MAX(HubInterpersonal), MAX(Motivasi), MAX(Wawasan), MAX(Kinerja) FROM TabelCalon"
        reader = cmd.ExecuteReader()

        While reader.Read()
            MaxMin(0, 0) = reader.GetValue(0)
            MaxMin(0, 1) = reader.GetValue(1)
            MaxMin(0, 2) = reader.GetValue(2)
            MaxMin(0, 3) = reader.GetValue(3)
            MaxMin(0, 4) = reader.GetValue(4)
        End While

        reader.Close()
        cmd.CommandText = "SELECT MIN(PengKerja), MIN(HubInterpersonal), MIN(Motivasi), MIN(Wawasan), MIN(Kinerja) FROM TabelCalon"
        reader = cmd.ExecuteReader()

        While reader.Read()
            MaxMin(1, 0) = reader.GetValue(0)
            MaxMin(1, 1) = reader.GetValue(1)
            MaxMin(1, 2) = reader.GetValue(2)
            MaxMin(1, 3) = reader.GetValue(3)
            MaxMin(1, 4) = reader.GetValue(4)
        End While

        reader.Close()

        cmd.CommandText = "DELETE FROM TabelNormalisasi WHERE ID<>0"
        cmd.ExecuteNonQuery()

        For j = 0 To DataGridView1.Rows.Count - 2
            r1 = DataGridView1.Rows(j).Cells(1).Value / MaxMin(0, 0)
            r2 = DataGridView1.Rows(j).Cells(2).Value / MaxMin(0, 1)
            r3 = DataGridView1.Rows(j).Cells(3).Value / MaxMin(0, 2)
            r4 = DataGridView1.Rows(j).Cells(4).Value / MaxMin(0, 3)
            r5 = DataGridView1.Rows(j).Cells(5).Value / MaxMin(0, 4)

            cmd.CommandText = "INSERT INTO TabelNormalisasi (R1, R2, R3, R4, R5) VALUES (@r1, @r2, @r3, @r4, @r5)"
            cmd.Parameters.AddWithValue("@r1", SqlDbType.Decimal).Value = Math.Round(r1, 2, MidpointRounding.AwayFromZero)
            cmd.Parameters.AddWithValue("@r2", SqlDbType.Decimal).Value = Math.Round(r2, 2, MidpointRounding.AwayFromZero)
            cmd.Parameters.AddWithValue("@r3", SqlDbType.Decimal).Value = Math.Round(r3, 2, MidpointRounding.AwayFromZero)
            cmd.Parameters.AddWithValue("@r4", SqlDbType.Decimal).Value = Math.Round(r4, 2, MidpointRounding.AwayFromZero)
            cmd.Parameters.AddWithValue("@r5", SqlDbType.Decimal).Value = Math.Round(r5, 2, MidpointRounding.AwayFromZero)

            cmd.ExecuteNonQuery()
        Next

        con.Close()
        MessageBox.Show("Proses Normalisasi Berhasil")
        'Catch ex As Exception
        '    If con.State = ConnectionState.Open Then
        '        con.Close()
        '    End If
        '    MessageBox.Show("Gagal Melakukan Proses Normalisasi.")
        'End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call ProsesNormalisasi()
    End Sub
End Class