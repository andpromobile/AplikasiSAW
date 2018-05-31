Imports System.Data.OleDb

Module Module1
    Public konn As OleDbConnection
    Public cmd As OleDbCommand
    Public ds As New DataSet
    Public da As OleDbDataAdapter
    Public rd As OleDbDataReader
    Public lokasi As String

    Sub Bukakoneksi()
        lokasi = "Provider=Microsoft.jet.oledb.4.0; data source=AppSAW.mdb"
        konn = New OleDbConnection(lokasi)
        If konn.State = ConnectionState.Closed Then
            konn.Open()
        End If
    End Sub
End Module
