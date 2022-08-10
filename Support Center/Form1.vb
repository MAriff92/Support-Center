Imports System.ComponentModel
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.ServiceProcess
Imports System.Windows
Imports Microsoft.VisualBasic.FileIO
Imports MySql.Data.MySqlClient

Public Class Form1

    Dim FormActive As Boolean
    Dim btnaction As String
    Dim selectedservice As String
    Dim ConnectionStatus As Boolean = False
    ReadOnly connection As New MySqlConnection()

    'ReadOnly mysqlserver As String = "Localhost"
    'ReadOnly mysqlusername As String = "root"
    ReadOnly mysqlserver As String = "192.168.11.219"
    ReadOnly mysqlusername As String = "ariff"
    ReadOnly mysqlpassword As String = "tw_mysql_root"
    ReadOnly mysqldatabase As String = "moe"




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populatelist()

        ipadmin()

        getthisstation()

        FormActive = True
    End Sub
    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If FormActive = False Then
            ipadmin()

            getthisstation()

            FormActive = True
        End If
    End Sub

    Private Sub Form1_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        FormActive = False
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbbx_servlist.SelectedIndexChanged
        getservstats()
    End Sub


    Private Sub txtbx_search_KeyUp(sender As Object, e As KeyEventArgs) Handles txtbx_search.KeyUp
        Dim availableservice As ServiceController

        cmbbx_servlist.Items.Clear()
        For Each availableservice In ServiceController.GetServices().Where(Function(x) x.ServiceName.ToUpper().Contains(txtbx_search.Text.ToUpper().Trim))
            Dim servicename = availableservice.ServiceName.ToString()
            cmbbx_servlist.Items.Add(servicename)
        Next
    End Sub
    Private Sub getservstats()
        Dim availableservice As New ServiceController(cmbbx_servlist.Text)
        Dim servicestatus = availableservice.Status.ToString()
        Dim servicename = availableservice.ServiceName.ToString()
        'MessageBox.Show(servicestatus)
        lbl_servstats.Text = servicename + ":  " + servicestatus
        If (servicestatus = "Running") Then
            btn_reset.Enabled = True
            btn_stop.Enabled = True
            btn_start.Enabled = False
        ElseIf (servicestatus = "Stopped") Then
            btn_reset.Enabled = False
            btn_stop.Enabled = False
            btn_start.Enabled = True
        End If
    End Sub
    Private Sub populatelist()
        For Each availableservice As ServiceController In ServiceController.GetServices()
            Dim servicename = availableservice.ServiceName.ToString()
            cmbbx_servlist.Items.Add(servicename)
        Next
    End Sub
    Private Sub ipadmin()
        Dim host As String = System.Net.Dns.GetHostName()
        Dim pingreturn As New Ping
        Dim pinglatency As String = ""
        Dim localip As String = ""
        For Each thisip In Net.Dns.GetHostEntry(host).AddressList()
            localip = thisip.ToString
            If String.Concat(localip.AsSpan(localip.Length - 1).ToString) = "5" And localip.Contains("172") Then

                'MessageBox.Show(Net.Dns.GetHostEntry(host).AddressList().Count.ToString())
                Dim teomip = localip.Substring(0, localip.Length - 1) + "14"
                Dim noxip = localip.Substring(0, localip.Length - 1) + "42"
                Dim so2ip = localip.Substring(0, localip.Length - 1) + "43"
                Dim coip = localip.Substring(0, localip.Length - 1) + "48"
                Dim o3ip = localip.Substring(0, localip.Length - 1) + "49"
                Dim calip = localip.Substring(0, localip.Length - 1) + "146"
                Dim amaip = localip.Substring(0, localip.Length - 1) + "150"

                Dim instrumentsip
                Dim instrumentsiplabel As Array = New String() {"teomip", "noxip", "so2ip", "coip", "o3ip", "calip", "amaip"}

                For Each intrumentlist In instrumentsiplabel

                    instrumentsip = ""
                    If intrumentlist = "teomip" Then
                        instrumentsip = teomip
                    ElseIf intrumentlist = "noxip" Then
                        instrumentsip = noxip
                    ElseIf intrumentlist = "so2ip" Then
                        instrumentsip = so2ip
                    ElseIf intrumentlist = "coip" Then
                        instrumentsip = coip
                    ElseIf intrumentlist = "teomip" Then
                        instrumentsip = teomip
                    ElseIf intrumentlist = "o3ip" Then
                        instrumentsip = o3ip
                    ElseIf intrumentlist = "calip" Then
                        instrumentsip = calip
                    ElseIf intrumentlist = "amaip" Then
                        instrumentsip = amaip
                    End If
                    Dim ctrlinstrument = Me.Controls.Find(String.Format("txtbx_{0}", intrumentlist.ToString), True)
                    If pingreturn.Send(instrumentsip).RoundtripTime <= 1 Then
                        pinglatency = "1"
                    Else
                        pinglatency = pingreturn.Send(instrumentsip).RoundtripTime.ToString
                    End If
                    If My.Computer.Network.Ping(instrumentsip) Then 'My.Computer.Network.Ping("198.01.01.01") Then 
                        ctrlinstrument(0).Text = instrumentsip + "".PadRight(6) + "[" + pinglatency + "ms]"
                    Else
                        ctrlinstrument(0).Text = instrumentsip + "".PadRight(6) + "[Failed]"
                    End If
                Next
            End If
        Next


    End Sub

    Private Sub ChangeIP(IPset)
        Dim newIP = "172.16." + IPset + ".5"
        Dim newSubNet = "255.255.255.0"
        Dim newGateway = "172.16." + IPset + ".1"

        Dim psi As New ProcessStartInfo("Netsh")
        psi.Verb = "runas" ' aka run as administrator
        psi.FileName = "Netsh"
        psi.Arguments = "interface ip set address name=""Ethernet"" static " + newIP + " 255.255.255.0 " + newGateway ' <- pass arguments for the command you want to run
        psi.UseShellExecute = False
        psi.RedirectStandardInput = True
        psi.RedirectStandardOutput = True
        MessageBox.Show(psi.Arguments)

        Try
            Process.Start(psi) ' <- run the process (user will be prompted to run with administrator access)
        Catch
            ' exception raised if user declines the admin prompt\
            MessageBox.Show("Set Local IP Failed")
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Askserviceto(selectedservice, 1000, btnaction)
    End Sub

    Public Sub Askserviceto(serviceName, timeoutMilliseconds, servjob)
        Dim availableservice As New ServiceController(serviceName)
        If servjob = "Reset" Then
            'MessageBox.Show(servjob)
            Dim millisec1 = Environment.TickCount
            Dim timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds)
            availableservice.Stop()
            availableservice.WaitForStatus(ServiceControllerStatus.Stopped, timeout)
            'count the rest of the timeout
            Dim millisec2 = Environment.TickCount
            timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1))
            availableservice.Start()
            availableservice.WaitForStatus(ServiceControllerStatus.Running, timeout)
        End If
        If servjob = "Start" Then
            'MessageBox.Show(servjob)
            Dim timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds)
            Dim millisec2 = Environment.TickCount
            timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - millisec2)
            availableservice.Start()
        End If
        If servjob = "Stop" Then
            'MessageBox.Show(servjob)
            Dim millisec1 = Environment.TickCount
            Dim timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds)
            availableservice.Stop()
            availableservice.WaitForStatus(ServiceControllerStatus.Stopped, timeout)
        End If
    End Sub
    Private Sub btn_reset_Click(sender As Object, e As EventArgs) Handles btn_reset.Click
        'MessageBox.Show(cmbbx_servlist.Text)
        selectedservice = cmbbx_servlist.Text
        btnaction = btn_reset.Text
        If selectedservice IsNot "" Then
            BackgroundWorker1.RunWorkerAsync(selectedservice)
            progbar_service.Visible = True
        Else
            MessageBox.Show("Service cannot be emptry")
        End If
    End Sub

    Private Sub btn_stop_Click(sender As Object, e As EventArgs) Handles btn_stop.Click
        'MessageBox.Show(cmbbx_servlist.Text)
        selectedservice = cmbbx_servlist.Text
        btnaction = btn_stop.Text
        If selectedservice IsNot "" Then
            BackgroundWorker1.RunWorkerAsync(selectedservice)
            progbar_service.Visible = True
        Else
            MessageBox.Show("Service cannot be emptry")
        End If
    End Sub

    Private Sub btn_start_Click(sender As Object, e As EventArgs) Handles btn_start.Click
        'MessageBox.Show(cmbbx_servlist.Text)
        selectedservice = cmbbx_servlist.Text
        btnaction = btn_start.Text
        If selectedservice IsNot "" Then
            BackgroundWorker1.RunWorkerAsync(selectedservice)
            progbar_service.Visible = True
        Else
            MessageBox.Show("Service cannot be emptry")
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        progbar_service.Visible = False
        cmbbx_servlist.Items.Clear()
        populatelist()
        getservstats()
    End Sub

    Private Sub connectmysql()
        connection.ConnectionString = "server=" + mysqlserver + ";database=" + mysqldatabase + ";user id=" + mysqlusername + ";password=" + mysqlpassword + ";"
        'MessageBox.Show(connection.ConnectionString)

        Try
            connection.Open()
            'MessageBox.Show("Connection successful")
            ConnectionStatus = True
        Catch ex As Exception

            MessageBox.Show("Connection failed " + ex.ToString)
            connection.Close()
            ConnectionStatus = False
        End Try

    End Sub

    Private Sub getthisstation()
        connectmysql()

        If ConnectionStatus = True Then
            txtbx_StationID.Text = ""
            txtbx_StationName.Text = ""
            txtbx_Address.Text = ""
            txtbx_City.Text = ""
            txtbx_State.Text = ""
            txtbx_StationIP.Text = ""
            txtbx_StationType.Text = ""
            txtbx_StationCategory.Text = ""
            Dim sqlcommand As New MySqlCommand("Select * From _mstr_station WHERE _autoid = 1", connection)
            Dim sqlreader As MySqlDataReader
            sqlreader = sqlcommand.ExecuteReader()

            If sqlreader.Read() Then
                If IsDBNull(sqlreader(1)) Then

                    connection.Close()
                    SetDefaultStation()
                Else
                    txtbx_StationID.Text = sqlreader(1)
                    txtbx_StationName.Text = sqlreader(2)
                    txtbx_Address.Text = sqlreader(5)
                    txtbx_City.Text = sqlreader(4)
                    txtbx_State.Text = sqlreader(6)
                    txtbx_StationIP.Text = sqlreader(3)
                    txtbx_StationType.Text = sqlreader(12)
                    txtbx_StationCategory.Text = sqlreader(13)
                    If sqlreader(7) = "1" Then
                        txtbx_StationStatus.Text = "Active"
                    Else
                        txtbx_StationStatus.Text = "Not Active"
                    End If

                    connection.Close()
                End If
            Else
                connection.Close()
            End If
        End If
    End Sub
    Private Sub SetDefaultStation()
        connectmysql()

        If ConnectionStatus = True Then
            Dim mysqlcommand As New MySqlCommand("UPDATE _mstr_station Set _stationID = @stationID, _stationName = @stationName, _stationIP = '172.16.81.5', _stationAddress = @stationAddress, _stationCity = @stationCity, _stationState = @stationState, _stationCode = @stationID, _station_modified_date = '2022-01-01 00:00:00', _stationStatus = @stationStatus, _stationCategories = @stationType, _station_lat = '', _station_long = '', _stationActive = '1' WHERE _autoid = 1;
INSERT INTO _dr_ftp (_hostname, _port, _uname, _pword, flag, _cert, _modified_date, _encryption) VALUES ('175.136.253.77', '9951', 'CAQMv2', 'pstw2020_caqm', 1,'','2022-01-01 00:00:00','');
INSERT INTO _edc_ftp (_hostname, _port, _uname, _pword, flag, _cert, _modified_date, _encryption) VALUES ('eqmpdata1.doe.gov.my', '9951', 'CAQMv2', 'pstw2020_caqm', 1,'','2022-01-01 00:00:00','');
INSERT INTO _hq_ftp (_hostname, _port, _uname, _pword, flag, _cert, _modified_date, _encryption) VALUES ('60.51.18.26', '21', 'CAQMv2', 'pstw2020_caqm', 1,'','2022-01-01 00:00:00','');
INSERT INTO _sms_receiver (_num, flag, _desc, modified_date) VALUES ('01112580833', 1,'HQ','2022-01-01 00:00:00');
INSERT INTO _mstr_analyzer (name, _model_name, comm_addrs, com_port, flag, _stationId, analyzer_port, period, time_out, poll_control, unit_id, database_name, modified_date) VALUES ('Teom', '1405 TEOM', '172.16.81.14', 'COM3', @teom_stats, @stationID, '',  12,  '3000',  '1',  '',  '', '2022-01-01 00:00:00') ;
INSERT INTO _mstr_analyzer (name, _model_name, comm_addrs, com_port, flag, _stationId, analyzer_port, period, time_out, poll_control, unit_id, database_name, modified_date) VALUES ('NOx', '42i  (NO-NO 2 -NO x )', '172.16.81.42', '', @nox_stats, @stationID, '502', 12, '', '1', '42', '', '2022-01-01 00:00:00');
INSERT INTO _mstr_analyzer (name, _model_name, comm_addrs, com_port, flag, _stationId, analyzer_port, period, time_out, poll_control, unit_id, database_name, modified_date) VALUES ('SO2', '43i  (SO2)', '172.16.81.43', '', @so2_stats, @stationID, '502', 12, '', '1', '43', '', '2022-01-01 00:00:00');
INSERT INTO _mstr_analyzer (name, _model_name, comm_addrs, com_port, flag, _stationId, analyzer_port, period, time_out, poll_control, unit_id, database_name, modified_date) VALUES ('CO', '48i  (CO)', '172.16.81.48', '', @co_stats, @stationID, '502', 12, '', '1', '48', '', '2022-01-01 00:00:00');
INSERT INTO _mstr_analyzer (name, _model_name, comm_addrs, com_port, flag, _stationId, analyzer_port, period, time_out, poll_control, unit_id, database_name, modified_date) VALUES ('O3', '49i  (O3)', '172.16.81.49', '', @o3_stats, @stationID, '502', 12, '', '1', '49', '', '2022-01-01 00:00:00');
INSERT INTO _mstr_analyzer (name, _model_name, comm_addrs, com_port, flag, _stationId, analyzer_port, period, time_out, poll_control, unit_id, database_name, modified_date) VALUES ('Cal', '146i (Dynamic Gas Calibrator)', '172.16.81.146', '', @cal_stats, @stationID, '502', 12, '', '1', '51', '', '2022-01-01 00:00:00');
INSERT INTO _mstr_analyzer (name, _model_name, comm_addrs, com_port, flag, _stationId, analyzer_port, period, time_out, poll_control, unit_id, database_name, modified_date) VALUES ('OP', 'AMA GC5000 series (Ozone Precursor)', '172.16.81.150', '', @op_stats, @stationID, '', 12, '', '1', '', '', '2022-01-01 00:00:00');
INSERT INTO _mstr_analyzer (name, _model_name, comm_addrs, com_port, flag, _stationId, analyzer_port, period, time_out, poll_control, unit_id, database_name, modified_date) VALUES ('AIO', 'AIO ', '','COM1', @aio_stats, @stationID, '', 12, '3000', '1', '', '', '2022-01-01 00:00:00')", connection)
            Dim stationNumber As String
            Dim stationID As String
            Dim stationName As String
            Dim stationLoc As String
            Dim stationCity As String
            Dim stationState As String
            Dim stationType As String
            Dim stationStatus As String
            Dim teom_stats As String = ""
            Dim nox_stats As String = ""
            Dim so2_stats As String = ""
            Dim co_stats As String = ""
            Dim o3_stats As String = ""
            Dim cal_stats As String = ""
            Dim op_stats As String = ""
            Dim aio_stats As String = ""
            Dim setstationNumber As String = txtbx_setstation.Text.ToString
            Dim stationinfo() = Array.Empty(Of Object)()
            Dim csvfilename = IO.File.OpenText("Station Info.csv")
            Dim tfp As New TextFieldParser(csvfilename) With {
            .Delimiters = New String() {","},
            .TextFieldType = FieldType.Delimited
        }

            While tfp.EndOfData = False
                Dim fields = tfp.ReadFields()
                stationNumber = fields(0)
                stationID = fields(1)
                stationName = fields(2)
                stationLoc = fields(3)
                stationCity = fields(4)
                stationState = fields(5)
                stationType = fields(6)
                stationStatus = "Continuos"
                If stationNumber = "81" Then
                    'MessageBox.Show(String.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6}", stationNumber, stationID, stationName, stationLoc, stationCity, stationState, stationType))
                    stationinfo = {stationNumber, stationID, stationName, stationLoc, stationCity, stationState, stationType, stationStatus}
                    If stationType = "Urban" Then
                        teom_stats = "1"
                        nox_stats = "1"
                        so2_stats = "1"
                        co_stats = "1"
                        o3_stats = "1"
                        cal_stats = "1"
                        op_stats = "1"
                        aio_stats = "1"
                    ElseIf stationType = "Sub Urban" Then
                        teom_stats = "1"
                        nox_stats = "1"
                        so2_stats = "1"
                        co_stats = "1"
                        o3_stats = "1"
                        cal_stats = "1"
                        op_stats = "0"
                        aio_stats = "1"
                    ElseIf stationType = "Industry" Then
                        teom_stats = "1"
                        nox_stats = "1"
                        so2_stats = "1"
                        co_stats = "0"
                        o3_stats = "0"
                        cal_stats = "1"
                        op_stats = "0"
                        aio_stats = "1"
                    ElseIf stationType = "Rural" Then
                        teom_stats = "1"
                        nox_stats = "0"
                        so2_stats = "0"
                        co_stats = "0"
                        o3_stats = "0"
                        cal_stats = "0"
                        op_stats = "0"
                        aio_stats = "1"

                    End If
                End If
            End While
            mysqlcommand.Parameters.Add("@stationID", MySqlDbType.VarChar).Value = stationinfo(1)
            mysqlcommand.Parameters.Add("@stationName", MySqlDbType.VarChar).Value = stationinfo(2)
            mysqlcommand.Parameters.Add("@stationAddress", MySqlDbType.VarChar).Value = stationinfo(3)
            mysqlcommand.Parameters.Add("@stationCity", MySqlDbType.VarChar).Value = stationinfo(4)
            mysqlcommand.Parameters.Add("@stationState", MySqlDbType.VarChar).Value = stationinfo(5)
            mysqlcommand.Parameters.Add("@stationType", MySqlDbType.VarChar).Value = stationinfo(6)
            mysqlcommand.Parameters.Add("@stationStatus", MySqlDbType.VarChar).Value = "Continuos"
            mysqlcommand.Parameters.Add("@teom_stats", MySqlDbType.VarChar).Value = teom_stats
            mysqlcommand.Parameters.Add("@nox_stats", MySqlDbType.VarChar).Value = nox_stats
            mysqlcommand.Parameters.Add("@so2_stats", MySqlDbType.VarChar).Value = so2_stats
            mysqlcommand.Parameters.Add("@co_stats", MySqlDbType.VarChar).Value = co_stats
            mysqlcommand.Parameters.Add("@o3_stats", MySqlDbType.VarChar).Value = o3_stats
            mysqlcommand.Parameters.Add("@cal_stats", MySqlDbType.VarChar).Value = cal_stats
            mysqlcommand.Parameters.Add("@op_stats", MySqlDbType.VarChar).Value = op_stats
            mysqlcommand.Parameters.Add("@aio_stats", MySqlDbType.VarChar).Value = aio_stats

            If mysqlcommand.ExecuteNonQuery() = 13 Then
                MessageBox.Show("Default Stored")

                connection.Close()
                getthisstation()
            Else
                MessageBox.Show("Store Dafult Fail")
                connection.Close()
            End If
        End If
    End Sub

    Private Sub setthisstation()
        connectmysql()

        If ConnectionStatus = True Then

            Dim stationNumber As String
            Dim stationID As String
            Dim stationName As String
            Dim stationLoc As String
            Dim stationCity As String
            Dim stationState As String
            Dim stationType As String
            Dim stationStatus As String
            Dim teom_stats As String = ""
            Dim nox_stats As String = ""
            Dim so2_stats As String = ""
            Dim co_stats As String = ""
            Dim o3_stats As String = ""
            Dim cal_stats As String = ""
            Dim op_stats As String = ""
            Dim aio_stats As String = ""
            Dim setstationNumber As String = txtbx_setstation.Text.ToString
            Dim stationinfo() = Array.Empty(Of Object)()
            Dim csvfilename = IO.File.OpenText("Station Info.csv")
            Dim tfp As New TextFieldParser(csvfilename) With {
            .Delimiters = New String() {","},
            .TextFieldType = FieldType.Delimited
        }

            Dim mysqlcommand As New MySqlCommand("UPDATE _mstr_station Set _stationID = @stationID, _stationName = @stationName, _stationIP = '172.16." + setstationNumber + ".5', _stationAddress = @stationAddress, _stationCity = @stationCity, _stationState = @stationState, _stationCode = @stationID, _station_modified_date = @currDate, _stationStatus = @stationStatus, _stationCategories = @stationType, _station_lat = '', _station_long = '', _stationActive = '1' WHERE _autoid = 1;
UPDATE _dr_ftp SET _hostname = '175.136.253.77', _port = '9951', _uname = 'CAQMv2', _pword = 'pstw2020_caqm', flag = 1, _cert = '', _modified_date = '2022-01-01 00:00:00', _encryption = '' WHERE autoid = 1;
UPDATE _edc_ftp SET _hostname = 'eqmpdata1.doe.gov.my', _port = '9951', _uname = 'CAQMv2', _pword = 'pstw2020_caqm', flag = 1, _cert = '', _modified_date = '2022-01-01 00:00:00', _encryption = '' WHERE autoid = 1;
UPDATE _hq_ftp SET _hostname = '60.51.18.26', _port = '21', _uname = 'CAQMv2', _pword = 'pstw2020_caqm', flag = 1, _cert = '', _modified_date = '2022-01-01 00:00:00', _encryption = '' WHERE autoid = 1;
UPDATE _sms_receiver SET _num = '01112580833', flag = 1, _desc = 'HQ', modified_date = '2022-01-01 00:00:00' WHERE autoid = 1;
UPDATE _gsm_port SET _port_name = 'COM4', flag = 1, _baud_rate = '115200', modified_date = '2022-01-01 00:00:00' WHERE autoid = 1;
UPDATE _mstr_analyzer SET comm_addrs = '172.16." + setstationNumber + ".14', com_port = 'COM3', flag = @teom_stats, _stationId = @stationID, analyzer_port = '', period = 12, time_out = '3000', poll_control = '1', unit_id = '', database_name = '', modified_date = '2022-01-01 00:00:00' WHERE autoid = 1;
UPDATE _mstr_analyzer SET comm_addrs = '172.16." + setstationNumber + ".42', com_port = '', flag = @nox_stats, _stationId = @stationID, analyzer_port = '502', period = 12, time_out = '', poll_control = '1', unit_id = '42', database_name = '', modified_date = '2022-01-01 00:00:00' WHERE autoid = 2;
UPDATE _mstr_analyzer SET comm_addrs = '172.16." + setstationNumber + ".43', com_port = '', flag = @so2_stats, _stationId = @stationID, analyzer_port = '502', period = 12, time_out = '', poll_control = '1', unit_id = '43', database_name = '', modified_date = '2022-01-01 00:00:00' WHERE autoid = 3;
UPDATE _mstr_analyzer SET comm_addrs = '172.16." + setstationNumber + ".48', com_port = '', flag = @co_stats, _stationId = @stationID, analyzer_port = '502', period = 12, time_out = '', poll_control = '1', unit_id = '48', database_name = '', modified_date = '2022-01-01 00:00:00' WHERE autoid = 4;
UPDATE _mstr_analyzer SET comm_addrs = '172.16." + setstationNumber + ".49', com_port = '', flag = @o3_stats, _stationId = @stationID, analyzer_port = '502', period = 12, time_out = '', poll_control = '1', unit_id = '49', database_name = '', modified_date = '2022-01-01 00:00:00' WHERE autoid = 5;
UPDATE _mstr_analyzer SET comm_addrs = '172.16." + setstationNumber + ".146', com_port = '', flag = @cal_stats, _stationId = @stationID, analyzer_port = '502', period = 12, time_out = '', poll_control = '1', unit_id = '51', database_name = '', modified_date = '2022-01-01 00:00:00' WHERE autoid = 6;
UPDATE _mstr_analyzer SET comm_addrs = '172.16." + setstationNumber + ".150', com_port = '', flag = @op_stats, _stationId = @stationID, analyzer_port = '', period = 12, time_out = '', poll_control = '1', unit_id = '', database_name = '', modified_date = '2022-01-01 00:00:00' WHERE autoid = 7;
UPDATE _mstr_analyzer SET comm_addrs = '', com_port = 'COM1', flag = @aio_stats, _stationId = @stationID, analyzer_port = '', period = 12, time_out = '3000', poll_control = '1', unit_id = '42', database_name = '', modified_date = '2022-01-01 00:00:00' WHERE autoid = 8", connection)

            tfp.ReadLine() ' skip header
            While tfp.EndOfData = False
                Dim fields = tfp.ReadFields()
                stationNumber = fields(0)
                stationID = fields(1)
                stationName = fields(2)
                stationLoc = fields(3)
                stationCity = fields(4)
                stationState = fields(5)
                stationType = fields(6)
                stationStatus = "Continuos"
                If stationNumber = setstationNumber Then
                    'MessageBox.Show(String.Format("{0} - {1} - {2} - {3} - {4} - {5} - {6}", stationNumber, stationID, stationName, stationLoc, stationCity, stationState, stationType))
                    stationinfo = {stationNumber, stationID, stationName, stationLoc, stationCity, stationState, stationType, stationStatus}
                    If stationType = "Urban" Then
                        teom_stats = "1"
                        nox_stats = "1"
                        so2_stats = "1"
                        co_stats = "1"
                        o3_stats = "1"
                        cal_stats = "1"
                        op_stats = "1"
                        aio_stats = "1"
                    ElseIf stationType = "Sub Urban" Then
                        teom_stats = "1"
                        nox_stats = "1"
                        so2_stats = "1"
                        co_stats = "1"
                        o3_stats = "1"
                        cal_stats = "1"
                        op_stats = "0"
                        aio_stats = "1"
                    ElseIf stationType = "Industry" Then
                        teom_stats = "1"
                        nox_stats = "1"
                        so2_stats = "1"
                        co_stats = "0"
                        o3_stats = "0"
                        cal_stats = "1"
                        op_stats = "0"
                        aio_stats = "1"
                    ElseIf stationType = "Rural" Then
                        teom_stats = "1"
                        nox_stats = "0"
                        so2_stats = "0"
                        co_stats = "0"
                        o3_stats = "0"
                        cal_stats = "0"
                        op_stats = "0"
                        aio_stats = "1"

                    End If
                End If
            End While

            If stationinfo.Length > 0 Then
                mysqlcommand.Parameters.Add("@stationID", MySqlDbType.VarChar).Value = stationinfo(1)
                mysqlcommand.Parameters.Add("@stationName", MySqlDbType.VarChar).Value = stationinfo(2)
                mysqlcommand.Parameters.Add("@stationAddress", MySqlDbType.VarChar).Value = stationinfo(3)
                mysqlcommand.Parameters.Add("@stationCity", MySqlDbType.VarChar).Value = stationinfo(4)
                mysqlcommand.Parameters.Add("@stationState", MySqlDbType.VarChar).Value = stationinfo(5)
                mysqlcommand.Parameters.Add("@stationType", MySqlDbType.VarChar).Value = stationinfo(6)
                mysqlcommand.Parameters.Add("@stationStatus", MySqlDbType.VarChar).Value = stationinfo(7)
                mysqlcommand.Parameters.Add("@currDate", MySqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                mysqlcommand.Parameters.Add("@setstationNumber", MySqlDbType.VarChar).Value = setstationNumber
                mysqlcommand.Parameters.Add("@teom_stats", MySqlDbType.VarChar).Value = teom_stats
                mysqlcommand.Parameters.Add("@nox_stats", MySqlDbType.VarChar).Value = nox_stats
                mysqlcommand.Parameters.Add("@so2_stats", MySqlDbType.VarChar).Value = so2_stats
                mysqlcommand.Parameters.Add("@co_stats", MySqlDbType.VarChar).Value = co_stats
                mysqlcommand.Parameters.Add("@o3_stats", MySqlDbType.VarChar).Value = o3_stats
                mysqlcommand.Parameters.Add("@cal_stats", MySqlDbType.VarChar).Value = cal_stats
                mysqlcommand.Parameters.Add("@op_stats", MySqlDbType.VarChar).Value = op_stats
                mysqlcommand.Parameters.Add("@aio_stats", MySqlDbType.VarChar).Value = aio_stats

                If mysqlcommand.ExecuteNonQuery() = 14 Then
                    MessageBox.Show("Data Updated")

                    Dim SourcePath As String
                    Dim TextToFind As String

                    Dim FilenameToFind = (DateTime.Now.ToString("yyyyMMdd"))
                    SourcePath = "C:\air\data"
                    TextToFind = FilenameToFind
                    Dim CurrentStationID = txtbx_StationID.Text
                    For Each SearchedDir In Directory.GetDirectories(SourcePath, TextToFind)
                        My.Computer.FileSystem.RenameDirectory(SearchedDir, "Last(" + CurrentStationID + ")-" + FilenameToFind)
                    Next

                    connection.Close()
                    getthisstation()
                    ChangeIP(setstationNumber)
                    ipadmin()
                Else
                    MessageBox.Show("Update Fail")

                    connection.Close()
                End If
            Else
                MessageBox.Show("No Available Station")

                connection.Close()
            End If
        End If
    End Sub

    Private Sub btn_setstation_Click(sender As Object, e As EventArgs) Handles btn_setstation.Click
        Dim result As DialogResult = MessageBox.Show("Confirm update?", "Title", MessageBoxButtons.YesNo)

        If (result = DialogResult.Yes) Then
            setthisstation()
        Else
        End If
    End Sub

    Private Sub txtbx_setstation_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbx_setstation.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim result As DialogResult = MessageBox.Show("Confirm update?", "Title", MessageBoxButtons.YesNo)

            If (result = DialogResult.Yes) Then
                setthisstation()
            Else
            End If
        End If
    End Sub

    Private Sub btn_refreshping_Click(sender As Object, e As EventArgs) Handles btn_refreshping.Click
        ipadmin()
    End Sub

    Private Sub btn_sms_Click(sender As Object, e As EventArgs) Handles btn_sms.Click
        Dim form2 = New Form2()
        form2.Show()
    End Sub

End Class
