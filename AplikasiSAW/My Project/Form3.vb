Imports System.Data.OleDb

Public Class Form3
    Dim kon As String = "Provider=Microsoft.jet.oledb.4.0; data source=AppSAW.mdb"
    Dim con As New OleDbConnection(kon)
    Dim cmd As New OleDbCommand

    Dim NamaCalon As String
    Dim PengKerja, HubInterpersonal, Motivasi, Wawasan, Kinerja As Integer
    Dim idx As Integer

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If (e.RowIndex >= 0) Then
            idx = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            NamaCalonTxt.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            PengKerjaTxt.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            HubInterpersonalTxt.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            MotivasiTxt.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            WawasanTxt.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
            KinerjaTxt.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
            BtnSimpan.Enabled = False
        End If
    End Sub

    Private Sub BtnUbah_Click(sender As Object, e As EventArgs) Handles BtnUbah.Click
        Call UbahData()

        BtnSimpan.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        Call InputData()
    End Sub

    Sub Menampilkan()
        da = New OleDbDataAdapter("select * from TabelCalon", con)
        ds = New DataSet
        da.Fill(ds, "TabelCalon")
        DataGridView1.DataSource = ds.Tables("TabelCalon")
        DataGridView1.Columns(0).Visible = False
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles BtnHapus.Click
        Call HapusData()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Bukakoneksi()
        Menampilkan()
    End Sub

    Private Sub UbahData()
        NamaCalon = NamaCalonTxt.Text
        PengKerja = Val(PengKerjaTxt.Text)
        HubInterpersonal = Val(HubInterpersonalTxt.Text)
        Motivasi = Val(MotivasiTxt.Text)
        Wawasan = Val(WawasanTxt.Text)
        Kinerja = Val(KinerjaTxt.Text)

        Try
            con.Open()

            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "UPDATE TabelCalon SET NamaCalon = ?, PengKerja = ?, HubInterpersonal = ?, Motivasi = ?, Wawasan = ?, Kinerja = ? WHERE ID = ? "

            cmd.Parameters.Add("@NamaCalon", OleDbType.BSTR).Value = NamaCalon
            cmd.Parameters.Add("@PengKerja", OleDbType.Integer).Value = PengKerja
            cmd.Parameters.Add("@HubInterpersonal", OleDbType.Integer).Value = HubInterpersonal
            cmd.Parameters.Add("@Motivasi", OleDbType.Integer).Value = Motivasi
            cmd.Parameters.Add("@Wawasan", OleDbType.Integer).Value = Wawasan
            cmd.Parameters.Add("@Kinerja", OleDbType.Integer).Value = Kinerja
            cmd.Parameters.Add("@ID", OleDbType.Integer).Value = idx

            cmd.ExecuteNonQuery()

            con.Close()
            MessageBox.Show("Ubah Data Berhasil.")
            Call Menampilkan()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            MessageBox.Show("Gagal Mengubah Data.")
        End Try
    End Sub
    Private Sub HapusData()
        If (idx >= 0) Then

            Try
                con.Open()

                cmd.Connection = con
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "DELETE FROM TabelCalon WHERE ID = " & idx & ""

                cmd.ExecuteNonQuery()

                con.Close()
                MessageBox.Show("Hapus Data Berhasil.")
                Call Menampilkan()
            Catch ex As Exception
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                MessageBox.Show("Gagal Menyimpan Data.")
            End Try
        Else
            MessageBox.Show("Pilih Terlebih Dahulu Data Yang Ingin DI Hapus!.")
        End If

    End Sub
    Private Sub InputData()

        NamaCalon = NamaCalonTxt.Text
        PengKerja = Val(PengKerjaTxt.Text)
        HubInterpersonal = Val(HubInterpersonalTxt.Text)
        Motivasi = Val(MotivasiTxt.Text)
        Wawasan = Val(WawasanTxt.Text)
        Kinerja = Val(KinerjaTxt.Text)

        Try
            con.Open()

            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "INSERT INTO TabelCalon (NamaCalon, PengKerja, HubInterpersonal, Motivasi, Wawasan, Kinerja) VALUES ('" &
                NamaCalon & "', " & PengKerja & ", " & HubInterpersonal & ", " & Motivasi & ", " & Wawasan & ", " & Kinerja & ")"

            cmd.ExecuteNonQuery()

            con.Close()
            MessageBox.Show("Input Data Berhasil.")
            Call Menampilkan()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            MessageBox.Show("Gagal Menyimpan Data.")
        End Try

    End Sub

End Class