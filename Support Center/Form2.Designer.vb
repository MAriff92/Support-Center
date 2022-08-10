<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_tableno = New System.Windows.Forms.Label()
        Me.lbl_description = New System.Windows.Forms.Label()
        Me.lbl_phonenumber = New System.Windows.Forms.Label()
        Me.lbl_lastupdate = New System.Windows.Forms.Label()
        Me.lbl_edit = New System.Windows.Forms.Label()
        Me.lbl_delete = New System.Windows.Forms.Label()
        Me.lbl_gsmstatus = New System.Windows.Forms.Label()
        Me.cmbbx_gsmstatus = New System.Windows.Forms.ComboBox()
        Me.lbl_baudrate = New System.Windows.Forms.Label()
        Me.txtbx_baudrate = New System.Windows.Forms.TextBox()
        Me.lbl_curport = New System.Windows.Forms.Label()
        Me.cmbbx_port = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Controls.Add(Me.lbl_gsmstatus)
        Me.Panel1.Controls.Add(Me.cmbbx_gsmstatus)
        Me.Panel1.Controls.Add(Me.lbl_baudrate)
        Me.Panel1.Controls.Add(Me.txtbx_baudrate)
        Me.Panel1.Controls.Add(Me.lbl_curport)
        Me.Panel1.Controls.Add(Me.cmbbx_port)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(516, 426)
        Me.Panel1.TabIndex = 2
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoScroll = True
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.27523!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.72477!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_tableno, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_description, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_phonenumber, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_lastupdate, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_edit, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_delete, 5, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 119)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.54135!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.45865!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(508, 154)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'lbl_tableno
        '
        Me.lbl_tableno.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_tableno.AutoSize = True
        Me.lbl_tableno.Location = New System.Drawing.Point(4, 5)
        Me.lbl_tableno.Name = "lbl_tableno"
        Me.lbl_tableno.Size = New System.Drawing.Size(26, 15)
        Me.lbl_tableno.TabIndex = 0
        Me.lbl_tableno.Text = "No."
        '
        'lbl_description
        '
        Me.lbl_description.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_description.AutoSize = True
        Me.lbl_description.Location = New System.Drawing.Point(38, 5)
        Me.lbl_description.Name = "lbl_description"
        Me.lbl_description.Size = New System.Drawing.Size(67, 15)
        Me.lbl_description.TabIndex = 1
        Me.lbl_description.Text = "Description"
        '
        'lbl_phonenumber
        '
        Me.lbl_phonenumber.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_phonenumber.AutoSize = True
        Me.lbl_phonenumber.Location = New System.Drawing.Point(143, 5)
        Me.lbl_phonenumber.Name = "lbl_phonenumber"
        Me.lbl_phonenumber.Size = New System.Drawing.Size(51, 15)
        Me.lbl_phonenumber.TabIndex = 2
        Me.lbl_phonenumber.Text = "Number"
        '
        'lbl_lastupdate
        '
        Me.lbl_lastupdate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_lastupdate.AutoSize = True
        Me.lbl_lastupdate.Location = New System.Drawing.Point(279, 5)
        Me.lbl_lastupdate.Name = "lbl_lastupdate"
        Me.lbl_lastupdate.Size = New System.Drawing.Size(69, 15)
        Me.lbl_lastupdate.TabIndex = 3
        Me.lbl_lastupdate.Text = "Last Update"
        '
        'lbl_edit
        '
        Me.lbl_edit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_edit.AutoSize = True
        Me.lbl_edit.Location = New System.Drawing.Point(413, 5)
        Me.lbl_edit.Name = "lbl_edit"
        Me.lbl_edit.Size = New System.Drawing.Size(27, 15)
        Me.lbl_edit.TabIndex = 4
        Me.lbl_edit.Text = "Edit"
        '
        'lbl_delete
        '
        Me.lbl_delete.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbl_delete.AutoSize = True
        Me.lbl_delete.Location = New System.Drawing.Point(460, 5)
        Me.lbl_delete.Name = "lbl_delete"
        Me.lbl_delete.Size = New System.Drawing.Size(40, 15)
        Me.lbl_delete.TabIndex = 5
        Me.lbl_delete.Text = "Delete"
        '
        'lbl_gsmstatus
        '
        Me.lbl_gsmstatus.AutoSize = True
        Me.lbl_gsmstatus.Location = New System.Drawing.Point(357, 12)
        Me.lbl_gsmstatus.Name = "lbl_gsmstatus"
        Me.lbl_gsmstatus.Size = New System.Drawing.Size(39, 15)
        Me.lbl_gsmstatus.TabIndex = 5
        Me.lbl_gsmstatus.Text = "Status"
        '
        'cmbbx_gsmstatus
        '
        Me.cmbbx_gsmstatus.FormattingEnabled = True
        Me.cmbbx_gsmstatus.Items.AddRange(New Object() {"ON", "OFF"})
        Me.cmbbx_gsmstatus.Location = New System.Drawing.Point(318, 30)
        Me.cmbbx_gsmstatus.Name = "cmbbx_gsmstatus"
        Me.cmbbx_gsmstatus.Size = New System.Drawing.Size(121, 23)
        Me.cmbbx_gsmstatus.TabIndex = 4
        '
        'lbl_baudrate
        '
        Me.lbl_baudrate.AutoSize = True
        Me.lbl_baudrate.Location = New System.Drawing.Point(222, 12)
        Me.lbl_baudrate.Name = "lbl_baudrate"
        Me.lbl_baudrate.Size = New System.Drawing.Size(60, 15)
        Me.lbl_baudrate.TabIndex = 3
        Me.lbl_baudrate.Text = "Baud Rate"
        '
        'txtbx_baudrate
        '
        Me.txtbx_baudrate.Location = New System.Drawing.Point(190, 30)
        Me.txtbx_baudrate.Name = "txtbx_baudrate"
        Me.txtbx_baudrate.Size = New System.Drawing.Size(122, 23)
        Me.txtbx_baudrate.TabIndex = 2
        '
        'lbl_curport
        '
        Me.lbl_curport.AutoSize = True
        Me.lbl_curport.Location = New System.Drawing.Point(109, 12)
        Me.lbl_curport.Name = "lbl_curport"
        Me.lbl_curport.Size = New System.Drawing.Size(29, 15)
        Me.lbl_curport.TabIndex = 1
        Me.lbl_curport.Text = "Port"
        '
        'cmbbx_port
        '
        Me.cmbbx_port.FormattingEnabled = True
        Me.cmbbx_port.Location = New System.Drawing.Point(63, 30)
        Me.cmbbx_port.Name = "cmbbx_port"
        Me.cmbbx_port.Size = New System.Drawing.Size(121, 23)
        Me.cmbbx_port.TabIndex = 0
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(539, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lbl_tableno As Label
    Friend WithEvents lbl_description As Label
    Friend WithEvents lbl_phonenumber As Label
    Friend WithEvents lbl_lastupdate As Label
    Friend WithEvents lbl_edit As Label
    Friend WithEvents lbl_delete As Label
    Friend WithEvents lbl_gsmstatus As Label
    Friend WithEvents cmbbx_gsmstatus As ComboBox
    Friend WithEvents lbl_baudrate As Label
    Friend WithEvents txtbx_baudrate As TextBox
    Friend WithEvents lbl_curport As Label
    Friend WithEvents cmbbx_port As ComboBox
End Class
