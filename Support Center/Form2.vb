Imports System.IO.Ports
Imports MySql.Data.MySqlClient

Public Class Form2

    Dim ConnectionStatus As Boolean = False
    ReadOnly connection As New MySqlConnection()

    'ReadOnly mysqlserver As String = "Localhost"
    'ReadOnly mysqlusername As String = "root"
    ReadOnly mysqlserver As String = "192.168.11.219"
    ReadOnly mysqlusername As String = "ariff"
    ReadOnly mysqlpassword As String = "tw_mysql_root"
    ReadOnly mysqldatabase As String = "moe"
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetSerialPortNames()
        GetCurrentGSM()
        GetGSMReseiver()
    End Sub
    Private Sub ConnectMysql()
        connection.ConnectionString = "server =" + mysqlserver + ";user id =" + mysqlusername + ";password =" + mysqlpassword + ";database =" + mysqldatabase
        Try
            connection.Open()
            'MessageBox.Show("Connection successful")
            ConnectionStatus = True
        Catch ex As Exception

            MessageBox.Show("Connection failed")
            connection.Close()
            ConnectionStatus = False
        End Try

    End Sub

    Sub GetSerialPortNames()
        ' Show all available COM ports.
        'Dim serialPort As New IO.Ports.SerialPort
        Dim availableports = SerialPort.GetPortNames()
        For Each port As String In availableports
            cmbbx_port.Items.Add(port)
        Next
    End Sub

    Sub GetCurrentGSM()
        ConnectMysql()
        If ConnectionStatus = True Then
            Dim sqlcommand As New MySqlCommand("Select * From _gsm_port ORDER BY autoid DESC LIMIT 1", connection)
            Dim sqlreader As MySqlDataReader
            Dim gsmstatus As String
            sqlreader = sqlcommand.ExecuteReader()

            If sqlreader.Read() Then
                If IsDBNull(sqlreader(1)) Then
                    connection.Close()
                Else
                    cmbbx_port.Text = sqlreader(1)
                    txtbx_baudrate.Text = sqlreader(3)
                    If sqlreader(2) = 1 Then
                        gsmstatus = "ON"
                    Else
                        gsmstatus = "OFF"
                    End If
                    cmbbx_gsmstatus.Text = gsmstatus
                    connection.Close()
                End If
            Else
                connection.Close()
            End If
        End If
    End Sub

    Sub GetGSMReseiver()
        ConnectMysql()
        If ConnectionStatus = True Then
            Dim sqlcommand As New MySqlCommand("Select * From _gsm_port", connection)
            Dim sqlreader As MySqlDataReader
            sqlreader = sqlcommand.ExecuteReader()
            MessageBox.Show(sqlreader.Depth.ToString())

            For Each rowread In sqlreader(1)
                'MessageBox.Show(rowread.ToString)

                'If sqlreader.Read() Then
                '    If IsDBNull(sqlreader(1)) Then
                '        connection.Close()
                '    Else
                '        cmbbx_port.Text = sqlreader(1)
                '        txtbx_baudrate.Text = sqlreader(3)
                '        If sqlreader(2) = 1 Then
                '            gsmstatus = "ON"
                '        Else
                '            gsmstatus = "OFF"
                '        End If
                '        cmbbx_gsmstatus.Text = gsmstatus
                '        connection.Close()
                '    End If
                'Else
                '    connection.Close()
                'End If
            Next
        End If
    End Sub
End Class