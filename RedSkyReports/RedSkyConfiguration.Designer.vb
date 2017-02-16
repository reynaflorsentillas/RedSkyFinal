<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RedSkyConfiguration
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkOverwiteExistingFiles = New System.Windows.Forms.CheckBox()
        Me.btnSaveReportConfiguration = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkUseTemplateMonthly = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblMonthyNextReport = New System.Windows.Forms.Label()
        Me.lblMonthlyLastReport = New System.Windows.Forms.Label()
        Me.cboMonthlyStatus = New System.Windows.Forms.ComboBox()
        Me.dtMonthly = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkUseTemplateWeekly = New System.Windows.Forms.CheckBox()
        Me.lblWeeklyDay = New System.Windows.Forms.Label()
        Me.cboWeeklyDay = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblWeeklyGenerationTime = New System.Windows.Forms.Label()
        Me.lblWeeklyNextReport = New System.Windows.Forms.Label()
        Me.lblWeeklyLastReport = New System.Windows.Forms.Label()
        Me.cboWeeklyStatus = New System.Windows.Forms.ComboBox()
        Me.dtWeekly = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkUseTemplateDaily = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblDailyGenerationTime = New System.Windows.Forms.Label()
        Me.lblDailyNextReport = New System.Windows.Forms.Label()
        Me.lblDailyLastReport = New System.Windows.Forms.Label()
        Me.cboDailyStatus = New System.Windows.Forms.ComboBox()
        Me.dtDaily = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.lblDailyTo = New System.Windows.Forms.Label()
        Me.lblDailyFrom = New System.Windows.Forms.Label()
        Me.dtDailyFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtDailyTo = New System.Windows.Forms.DateTimePicker()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cboSMTPUseTLSSSL = New System.Windows.Forms.ComboBox()
        Me.lblSMTPId = New System.Windows.Forms.Label()
        Me.btnSaveMailingConfiguration = New System.Windows.Forms.Button()
        Me.lblSMTPPassword = New System.Windows.Forms.Label()
        Me.lblSMTPUsername = New System.Windows.Forms.Label()
        Me.lblSMTPUseTLSSSL = New System.Windows.Forms.Label()
        Me.lblSMTPPortSSL = New System.Windows.Forms.Label()
        Me.lblSMTPServer = New System.Windows.Forms.Label()
        Me.txtSMTPPassword = New System.Windows.Forms.TextBox()
        Me.txtSMTPUsername = New System.Windows.Forms.TextBox()
        Me.txtSMTPPortSSL = New System.Windows.Forms.TextBox()
        Me.txtSMTPServer = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.btnMailingListCancel = New System.Windows.Forms.Button()
        Me.lblMailingListId = New System.Windows.Forms.Label()
        Me.btnMailingListEdit = New System.Windows.Forms.Button()
        Me.btnMailingListAddNew = New System.Windows.Forms.Button()
        Me.btnMailingListSave = New System.Windows.Forms.Button()
        Me.chkMonthly = New System.Windows.Forms.CheckBox()
        Me.chkWeekly = New System.Windows.Forms.CheckBox()
        Me.chkDaily = New System.Windows.Forms.CheckBox()
        Me.cboMailingListStatus = New System.Windows.Forms.ComboBox()
        Me.txtMailingListEmailAddress = New System.Windows.Forms.TextBox()
        Me.lblMailingListStatus = New System.Windows.Forms.Label()
        Me.lblMailingListEmailAddress = New System.Windows.Forms.Label()
        Me.gvMailingList = New System.Windows.Forms.DataGridView()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.txtOtherConfigurationDomain = New System.Windows.Forms.TextBox()
        Me.lblOtherConfigurationDomain = New System.Windows.Forms.Label()
        Me.btnSaveOtherConfiguration = New System.Windows.Forms.Button()
        Me.txtOtherConfigurationGroup = New System.Windows.Forms.TextBox()
        Me.lblOtherConfigurationGroup = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.gvMailingList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(624, 559)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.chkOverwiteExistingFiles)
        Me.TabPage1.Controls.Add(Me.btnSaveReportConfiguration)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(616, 528)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Report Configuration"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chkOverwiteExistingFiles
        '
        Me.chkOverwiteExistingFiles.AutoSize = True
        Me.chkOverwiteExistingFiles.Location = New System.Drawing.Point(12, 492)
        Me.chkOverwiteExistingFiles.Name = "chkOverwiteExistingFiles"
        Me.chkOverwiteExistingFiles.Size = New System.Drawing.Size(172, 22)
        Me.chkOverwiteExistingFiles.TabIndex = 5
        Me.chkOverwiteExistingFiles.Text = "Overwrite Existing Files"
        Me.chkOverwiteExistingFiles.UseVisualStyleBackColor = True
        '
        'btnSaveReportConfiguration
        '
        Me.btnSaveReportConfiguration.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveReportConfiguration.Location = New System.Drawing.Point(493, 483)
        Me.btnSaveReportConfiguration.Name = "btnSaveReportConfiguration"
        Me.btnSaveReportConfiguration.Size = New System.Drawing.Size(117, 39)
        Me.btnSaveReportConfiguration.TabIndex = 3
        Me.btnSaveReportConfiguration.Text = "Save"
        Me.btnSaveReportConfiguration.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkUseTemplateMonthly)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.lblMonthyNextReport)
        Me.GroupBox3.Controls.Add(Me.lblMonthlyLastReport)
        Me.GroupBox3.Controls.Add(Me.cboMonthlyStatus)
        Me.GroupBox3.Controls.Add(Me.dtMonthly)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 341)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(610, 136)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Monthly Summary Report"
        '
        'chkUseTemplateMonthly
        '
        Me.chkUseTemplateMonthly.AutoSize = True
        Me.chkUseTemplateMonthly.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseTemplateMonthly.Location = New System.Drawing.Point(359, 42)
        Me.chkUseTemplateMonthly.Name = "chkUseTemplateMonthly"
        Me.chkUseTemplateMonthly.Size = New System.Drawing.Size(111, 22)
        Me.chkUseTemplateMonthly.TabIndex = 8
        Me.chkUseTemplateMonthly.Text = "Use Template"
        Me.chkUseTemplateMonthly.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label6.Location = New System.Drawing.Point(87, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 18)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Status: "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label5.Location = New System.Drawing.Point(6, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(134, 18)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Time of Generation: "
        '
        'lblMonthyNextReport
        '
        Me.lblMonthyNextReport.AutoSize = True
        Me.lblMonthyNextReport.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthyNextReport.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblMonthyNextReport.Location = New System.Drawing.Point(356, 104)
        Me.lblMonthyNextReport.Name = "lblMonthyNextReport"
        Me.lblMonthyNextReport.Size = New System.Drawing.Size(48, 18)
        Me.lblMonthyNextReport.TabIndex = 3
        Me.lblMonthyNextReport.Text = "Label6"
        '
        'lblMonthlyLastReport
        '
        Me.lblMonthlyLastReport.AutoSize = True
        Me.lblMonthlyLastReport.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthlyLastReport.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblMonthlyLastReport.Location = New System.Drawing.Point(356, 77)
        Me.lblMonthlyLastReport.Name = "lblMonthlyLastReport"
        Me.lblMonthlyLastReport.Size = New System.Drawing.Size(48, 18)
        Me.lblMonthlyLastReport.TabIndex = 2
        Me.lblMonthlyLastReport.Text = "Label5"
        '
        'cboMonthlyStatus
        '
        Me.cboMonthlyStatus.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.cboMonthlyStatus.FormattingEnabled = True
        Me.cboMonthlyStatus.Items.AddRange(New Object() {"ENABLED", "DISABLED"})
        Me.cboMonthlyStatus.Location = New System.Drawing.Point(152, 77)
        Me.cboMonthlyStatus.Name = "cboMonthlyStatus"
        Me.cboMonthlyStatus.Size = New System.Drawing.Size(147, 26)
        Me.cboMonthlyStatus.TabIndex = 1
        '
        'dtMonthly
        '
        Me.dtMonthly.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.dtMonthly.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtMonthly.Location = New System.Drawing.Point(152, 40)
        Me.dtMonthly.Name = "dtMonthly"
        Me.dtMonthly.ShowUpDown = True
        Me.dtMonthly.Size = New System.Drawing.Size(147, 26)
        Me.dtMonthly.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkUseTemplateWeekly)
        Me.GroupBox2.Controls.Add(Me.lblWeeklyDay)
        Me.GroupBox2.Controls.Add(Me.cboWeeklyDay)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.lblWeeklyGenerationTime)
        Me.GroupBox2.Controls.Add(Me.lblWeeklyNextReport)
        Me.GroupBox2.Controls.Add(Me.lblWeeklyLastReport)
        Me.GroupBox2.Controls.Add(Me.cboWeeklyStatus)
        Me.GroupBox2.Controls.Add(Me.dtWeekly)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 201)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(610, 140)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Weekly Summary Report"
        '
        'chkUseTemplateWeekly
        '
        Me.chkUseTemplateWeekly.AutoSize = True
        Me.chkUseTemplateWeekly.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseTemplateWeekly.Location = New System.Drawing.Point(359, 35)
        Me.chkUseTemplateWeekly.Name = "chkUseTemplateWeekly"
        Me.chkUseTemplateWeekly.Size = New System.Drawing.Size(111, 22)
        Me.chkUseTemplateWeekly.TabIndex = 7
        Me.chkUseTemplateWeekly.Text = "Use Template"
        Me.chkUseTemplateWeekly.UseVisualStyleBackColor = True
        '
        'lblWeeklyDay
        '
        Me.lblWeeklyDay.AutoSize = True
        Me.lblWeeklyDay.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblWeeklyDay.Location = New System.Drawing.Point(14, 39)
        Me.lblWeeklyDay.Name = "lblWeeklyDay"
        Me.lblWeeklyDay.Size = New System.Drawing.Size(126, 18)
        Me.lblWeeklyDay.TabIndex = 7
        Me.lblWeeklyDay.Text = "Day of Generation: "
        '
        'cboWeeklyDay
        '
        Me.cboWeeklyDay.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.cboWeeklyDay.FormattingEnabled = True
        Me.cboWeeklyDay.Items.AddRange(New Object() {"SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY"})
        Me.cboWeeklyDay.Location = New System.Drawing.Point(152, 31)
        Me.cboWeeklyDay.Name = "cboWeeklyDay"
        Me.cboWeeklyDay.Size = New System.Drawing.Size(147, 26)
        Me.cboWeeklyDay.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label4.Location = New System.Drawing.Point(87, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 18)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Status: "
        '
        'lblWeeklyGenerationTime
        '
        Me.lblWeeklyGenerationTime.AutoSize = True
        Me.lblWeeklyGenerationTime.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblWeeklyGenerationTime.Location = New System.Drawing.Point(6, 72)
        Me.lblWeeklyGenerationTime.Name = "lblWeeklyGenerationTime"
        Me.lblWeeklyGenerationTime.Size = New System.Drawing.Size(134, 18)
        Me.lblWeeklyGenerationTime.TabIndex = 4
        Me.lblWeeklyGenerationTime.Text = "Time of Generation: "
        '
        'lblWeeklyNextReport
        '
        Me.lblWeeklyNextReport.AutoSize = True
        Me.lblWeeklyNextReport.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeeklyNextReport.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblWeeklyNextReport.Location = New System.Drawing.Point(356, 106)
        Me.lblWeeklyNextReport.Name = "lblWeeklyNextReport"
        Me.lblWeeklyNextReport.Size = New System.Drawing.Size(48, 18)
        Me.lblWeeklyNextReport.TabIndex = 3
        Me.lblWeeklyNextReport.Text = "Label4"
        '
        'lblWeeklyLastReport
        '
        Me.lblWeeklyLastReport.AutoSize = True
        Me.lblWeeklyLastReport.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeeklyLastReport.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblWeeklyLastReport.Location = New System.Drawing.Point(356, 74)
        Me.lblWeeklyLastReport.Name = "lblWeeklyLastReport"
        Me.lblWeeklyLastReport.Size = New System.Drawing.Size(48, 18)
        Me.lblWeeklyLastReport.TabIndex = 2
        Me.lblWeeklyLastReport.Text = "Label3"
        '
        'cboWeeklyStatus
        '
        Me.cboWeeklyStatus.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.cboWeeklyStatus.FormattingEnabled = True
        Me.cboWeeklyStatus.Items.AddRange(New Object() {"ENABLED", "DISABLED"})
        Me.cboWeeklyStatus.Location = New System.Drawing.Point(152, 100)
        Me.cboWeeklyStatus.Name = "cboWeeklyStatus"
        Me.cboWeeklyStatus.Size = New System.Drawing.Size(147, 26)
        Me.cboWeeklyStatus.TabIndex = 1
        '
        'dtWeekly
        '
        Me.dtWeekly.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.dtWeekly.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtWeekly.Location = New System.Drawing.Point(152, 66)
        Me.dtWeekly.Name = "dtWeekly"
        Me.dtWeekly.ShowUpDown = True
        Me.dtWeekly.Size = New System.Drawing.Size(147, 26)
        Me.dtWeekly.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkUseTemplateDaily)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblDailyGenerationTime)
        Me.GroupBox1.Controls.Add(Me.lblDailyNextReport)
        Me.GroupBox1.Controls.Add(Me.lblDailyLastReport)
        Me.GroupBox1.Controls.Add(Me.cboDailyStatus)
        Me.GroupBox1.Controls.Add(Me.dtDaily)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 75)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(610, 126)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Daily Summary Report"
        '
        'chkUseTemplateDaily
        '
        Me.chkUseTemplateDaily.AutoSize = True
        Me.chkUseTemplateDaily.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseTemplateDaily.Location = New System.Drawing.Point(359, 28)
        Me.chkUseTemplateDaily.Name = "chkUseTemplateDaily"
        Me.chkUseTemplateDaily.Size = New System.Drawing.Size(111, 22)
        Me.chkUseTemplateDaily.TabIndex = 6
        Me.chkUseTemplateDaily.Text = "Use Template"
        Me.chkUseTemplateDaily.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label2.Location = New System.Drawing.Point(87, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Status: "
        '
        'lblDailyGenerationTime
        '
        Me.lblDailyGenerationTime.AutoSize = True
        Me.lblDailyGenerationTime.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblDailyGenerationTime.Location = New System.Drawing.Point(6, 32)
        Me.lblDailyGenerationTime.Name = "lblDailyGenerationTime"
        Me.lblDailyGenerationTime.Size = New System.Drawing.Size(134, 18)
        Me.lblDailyGenerationTime.TabIndex = 4
        Me.lblDailyGenerationTime.Text = "Time of Generation: "
        '
        'lblDailyNextReport
        '
        Me.lblDailyNextReport.AutoSize = True
        Me.lblDailyNextReport.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyNextReport.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblDailyNextReport.Location = New System.Drawing.Point(356, 99)
        Me.lblDailyNextReport.Name = "lblDailyNextReport"
        Me.lblDailyNextReport.Size = New System.Drawing.Size(48, 18)
        Me.lblDailyNextReport.TabIndex = 3
        Me.lblDailyNextReport.Text = "Label2"
        '
        'lblDailyLastReport
        '
        Me.lblDailyLastReport.AutoSize = True
        Me.lblDailyLastReport.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyLastReport.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblDailyLastReport.Location = New System.Drawing.Point(356, 68)
        Me.lblDailyLastReport.Name = "lblDailyLastReport"
        Me.lblDailyLastReport.Size = New System.Drawing.Size(48, 18)
        Me.lblDailyLastReport.TabIndex = 2
        Me.lblDailyLastReport.Text = "Label1"
        '
        'cboDailyStatus
        '
        Me.cboDailyStatus.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.cboDailyStatus.FormattingEnabled = True
        Me.cboDailyStatus.Items.AddRange(New Object() {"ENABLED", "DISABLED"})
        Me.cboDailyStatus.Location = New System.Drawing.Point(152, 60)
        Me.cboDailyStatus.Name = "cboDailyStatus"
        Me.cboDailyStatus.Size = New System.Drawing.Size(147, 26)
        Me.cboDailyStatus.TabIndex = 1
        '
        'dtDaily
        '
        Me.dtDaily.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.dtDaily.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtDaily.Location = New System.Drawing.Point(152, 26)
        Me.dtDaily.Name = "dtDaily"
        Me.dtDaily.ShowUpDown = True
        Me.dtDaily.Size = New System.Drawing.Size(147, 26)
        Me.dtDaily.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblDailyTo)
        Me.GroupBox4.Controls.Add(Me.lblDailyFrom)
        Me.GroupBox4.Controls.Add(Me.dtDailyFrom)
        Me.GroupBox4.Controls.Add(Me.dtDailyTo)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(610, 72)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Filters"
        '
        'lblDailyTo
        '
        Me.lblDailyTo.AutoSize = True
        Me.lblDailyTo.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblDailyTo.Location = New System.Drawing.Point(356, 31)
        Me.lblDailyTo.Name = "lblDailyTo"
        Me.lblDailyTo.Size = New System.Drawing.Size(63, 18)
        Me.lblDailyTo.TabIndex = 9
        Me.lblDailyTo.Text = "Daily To: "
        '
        'lblDailyFrom
        '
        Me.lblDailyFrom.AutoSize = True
        Me.lblDailyFrom.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblDailyFrom.Location = New System.Drawing.Point(59, 31)
        Me.lblDailyFrom.Name = "lblDailyFrom"
        Me.lblDailyFrom.Size = New System.Drawing.Size(81, 18)
        Me.lblDailyFrom.TabIndex = 8
        Me.lblDailyFrom.Text = "Daily From: "
        '
        'dtDailyFrom
        '
        Me.dtDailyFrom.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.dtDailyFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtDailyFrom.Location = New System.Drawing.Point(152, 25)
        Me.dtDailyFrom.Name = "dtDailyFrom"
        Me.dtDailyFrom.ShowUpDown = True
        Me.dtDailyFrom.Size = New System.Drawing.Size(147, 26)
        Me.dtDailyFrom.TabIndex = 6
        '
        'dtDailyTo
        '
        Me.dtDailyTo.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.dtDailyTo.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtDailyTo.Location = New System.Drawing.Point(431, 25)
        Me.dtDailyTo.Name = "dtDailyTo"
        Me.dtDailyTo.ShowUpDown = True
        Me.dtDailyTo.Size = New System.Drawing.Size(147, 26)
        Me.dtDailyTo.TabIndex = 7
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cboSMTPUseTLSSSL)
        Me.TabPage2.Controls.Add(Me.lblSMTPId)
        Me.TabPage2.Controls.Add(Me.btnSaveMailingConfiguration)
        Me.TabPage2.Controls.Add(Me.lblSMTPPassword)
        Me.TabPage2.Controls.Add(Me.lblSMTPUsername)
        Me.TabPage2.Controls.Add(Me.lblSMTPUseTLSSSL)
        Me.TabPage2.Controls.Add(Me.lblSMTPPortSSL)
        Me.TabPage2.Controls.Add(Me.lblSMTPServer)
        Me.TabPage2.Controls.Add(Me.txtSMTPPassword)
        Me.TabPage2.Controls.Add(Me.txtSMTPUsername)
        Me.TabPage2.Controls.Add(Me.txtSMTPPortSSL)
        Me.TabPage2.Controls.Add(Me.txtSMTPServer)
        Me.TabPage2.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(616, 528)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Mailing Configuration"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cboSMTPUseTLSSSL
        '
        Me.cboSMTPUseTLSSSL.FormattingEnabled = True
        Me.cboSMTPUseTLSSSL.Items.AddRange(New Object() {"TRUE", "FALSE"})
        Me.cboSMTPUseTLSSSL.Location = New System.Drawing.Point(268, 177)
        Me.cboSMTPUseTLSSSL.Name = "cboSMTPUseTLSSSL"
        Me.cboSMTPUseTLSSSL.Size = New System.Drawing.Size(247, 26)
        Me.cboSMTPUseTLSSSL.TabIndex = 14
        '
        'lblSMTPId
        '
        Me.lblSMTPId.AutoSize = True
        Me.lblSMTPId.Location = New System.Drawing.Point(268, 51)
        Me.lblSMTPId.Name = "lblSMTPId"
        Me.lblSMTPId.Size = New System.Drawing.Size(58, 18)
        Me.lblSMTPId.TabIndex = 13
        Me.lblSMTPId.Text = "SMTP ID"
        Me.lblSMTPId.Visible = False
        '
        'btnSaveMailingConfiguration
        '
        Me.btnSaveMailingConfiguration.Location = New System.Drawing.Point(398, 318)
        Me.btnSaveMailingConfiguration.Name = "btnSaveMailingConfiguration"
        Me.btnSaveMailingConfiguration.Size = New System.Drawing.Size(117, 39)
        Me.btnSaveMailingConfiguration.TabIndex = 12
        Me.btnSaveMailingConfiguration.Text = "Save"
        Me.btnSaveMailingConfiguration.UseVisualStyleBackColor = True
        '
        'lblSMTPPassword
        '
        Me.lblSMTPPassword.AutoSize = True
        Me.lblSMTPPassword.Location = New System.Drawing.Point(104, 274)
        Me.lblSMTPPassword.Name = "lblSMTPPassword"
        Me.lblSMTPPassword.Size = New System.Drawing.Size(111, 18)
        Me.lblSMTPPassword.TabIndex = 11
        Me.lblSMTPPassword.Text = "SMTP Password: "
        '
        'lblSMTPUsername
        '
        Me.lblSMTPUsername.AutoSize = True
        Me.lblSMTPUsername.Location = New System.Drawing.Point(100, 227)
        Me.lblSMTPUsername.Name = "lblSMTPUsername"
        Me.lblSMTPUsername.Size = New System.Drawing.Size(115, 18)
        Me.lblSMTPUsername.TabIndex = 10
        Me.lblSMTPUsername.Text = "SMTP Username: "
        '
        'lblSMTPUseTLSSSL
        '
        Me.lblSMTPUseTLSSSL.AutoSize = True
        Me.lblSMTPUseTLSSSL.Location = New System.Drawing.Point(54, 180)
        Me.lblSMTPUseTLSSSL.Name = "lblSMTPUseTLSSSL"
        Me.lblSMTPUseTLSSSL.Size = New System.Drawing.Size(110, 18)
        Me.lblSMTPUseTLSSSL.TabIndex = 9
        Me.lblSMTPUseTLSSSL.Text = "SMTP Enable SSL"
        '
        'lblSMTPPortSSL
        '
        Me.lblSMTPPortSSL.AutoSize = True
        Me.lblSMTPPortSSL.Location = New System.Drawing.Point(104, 133)
        Me.lblSMTPPortSSL.Name = "lblSMTPPortSSL"
        Me.lblSMTPPortSSL.Size = New System.Drawing.Size(111, 18)
        Me.lblSMTPPortSSL.TabIndex = 8
        Me.lblSMTPPortSSL.Text = "SMTP Port (SSL): "
        '
        'lblSMTPServer
        '
        Me.lblSMTPServer.AutoSize = True
        Me.lblSMTPServer.Location = New System.Drawing.Point(123, 87)
        Me.lblSMTPServer.Name = "lblSMTPServer"
        Me.lblSMTPServer.Size = New System.Drawing.Size(92, 18)
        Me.lblSMTPServer.TabIndex = 6
        Me.lblSMTPServer.Text = "SMTP Server: "
        '
        'txtSMTPPassword
        '
        Me.txtSMTPPassword.Location = New System.Drawing.Point(268, 271)
        Me.txtSMTPPassword.Name = "txtSMTPPassword"
        Me.txtSMTPPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSMTPPassword.Size = New System.Drawing.Size(247, 26)
        Me.txtSMTPPassword.TabIndex = 5
        '
        'txtSMTPUsername
        '
        Me.txtSMTPUsername.Location = New System.Drawing.Point(268, 224)
        Me.txtSMTPUsername.Name = "txtSMTPUsername"
        Me.txtSMTPUsername.Size = New System.Drawing.Size(247, 26)
        Me.txtSMTPUsername.TabIndex = 4
        Me.txtSMTPUsername.Text = " "
        '
        'txtSMTPPortSSL
        '
        Me.txtSMTPPortSSL.Location = New System.Drawing.Point(268, 130)
        Me.txtSMTPPortSSL.Name = "txtSMTPPortSSL"
        Me.txtSMTPPortSSL.Size = New System.Drawing.Size(247, 26)
        Me.txtSMTPPortSSL.TabIndex = 2
        '
        'txtSMTPServer
        '
        Me.txtSMTPServer.Location = New System.Drawing.Point(268, 84)
        Me.txtSMTPServer.Name = "txtSMTPServer"
        Me.txtSMTPServer.Size = New System.Drawing.Size(247, 26)
        Me.txtSMTPServer.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnMailingListCancel)
        Me.TabPage3.Controls.Add(Me.lblMailingListId)
        Me.TabPage3.Controls.Add(Me.btnMailingListEdit)
        Me.TabPage3.Controls.Add(Me.btnMailingListAddNew)
        Me.TabPage3.Controls.Add(Me.btnMailingListSave)
        Me.TabPage3.Controls.Add(Me.chkMonthly)
        Me.TabPage3.Controls.Add(Me.chkWeekly)
        Me.TabPage3.Controls.Add(Me.chkDaily)
        Me.TabPage3.Controls.Add(Me.cboMailingListStatus)
        Me.TabPage3.Controls.Add(Me.txtMailingListEmailAddress)
        Me.TabPage3.Controls.Add(Me.lblMailingListStatus)
        Me.TabPage3.Controls.Add(Me.lblMailingListEmailAddress)
        Me.TabPage3.Controls.Add(Me.gvMailingList)
        Me.TabPage3.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage3.Location = New System.Drawing.Point(4, 27)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(616, 528)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Mailing List"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btnMailingListCancel
        '
        Me.btnMailingListCancel.Enabled = False
        Me.btnMailingListCancel.Location = New System.Drawing.Point(533, 60)
        Me.btnMailingListCancel.Name = "btnMailingListCancel"
        Me.btnMailingListCancel.Size = New System.Drawing.Size(75, 33)
        Me.btnMailingListCancel.TabIndex = 12
        Me.btnMailingListCancel.Text = "Cancel"
        Me.btnMailingListCancel.UseVisualStyleBackColor = True
        '
        'lblMailingListId
        '
        Me.lblMailingListId.AutoSize = True
        Me.lblMailingListId.Location = New System.Drawing.Point(105, 4)
        Me.lblMailingListId.Name = "lblMailingListId"
        Me.lblMailingListId.Size = New System.Drawing.Size(21, 18)
        Me.lblMailingListId.TabIndex = 11
        Me.lblMailingListId.Text = "ID"
        Me.lblMailingListId.Visible = False
        '
        'btnMailingListEdit
        '
        Me.btnMailingListEdit.Location = New System.Drawing.Point(533, 17)
        Me.btnMailingListEdit.Name = "btnMailingListEdit"
        Me.btnMailingListEdit.Size = New System.Drawing.Size(75, 33)
        Me.btnMailingListEdit.TabIndex = 10
        Me.btnMailingListEdit.Text = "Edit"
        Me.btnMailingListEdit.UseVisualStyleBackColor = True
        '
        'btnMailingListAddNew
        '
        Me.btnMailingListAddNew.Location = New System.Drawing.Point(452, 17)
        Me.btnMailingListAddNew.Name = "btnMailingListAddNew"
        Me.btnMailingListAddNew.Size = New System.Drawing.Size(75, 33)
        Me.btnMailingListAddNew.TabIndex = 9
        Me.btnMailingListAddNew.Text = "Add New"
        Me.btnMailingListAddNew.UseVisualStyleBackColor = True
        '
        'btnMailingListSave
        '
        Me.btnMailingListSave.Enabled = False
        Me.btnMailingListSave.Location = New System.Drawing.Point(452, 60)
        Me.btnMailingListSave.Name = "btnMailingListSave"
        Me.btnMailingListSave.Size = New System.Drawing.Size(75, 33)
        Me.btnMailingListSave.TabIndex = 8
        Me.btnMailingListSave.Text = "Save"
        Me.btnMailingListSave.UseVisualStyleBackColor = True
        '
        'chkMonthly
        '
        Me.chkMonthly.AutoSize = True
        Me.chkMonthly.Enabled = False
        Me.chkMonthly.Location = New System.Drawing.Point(349, 73)
        Me.chkMonthly.Name = "chkMonthly"
        Me.chkMonthly.Size = New System.Drawing.Size(79, 22)
        Me.chkMonthly.TabIndex = 7
        Me.chkMonthly.Text = "Monthly"
        Me.chkMonthly.UseVisualStyleBackColor = True
        '
        'chkWeekly
        '
        Me.chkWeekly.AutoSize = True
        Me.chkWeekly.Enabled = False
        Me.chkWeekly.Location = New System.Drawing.Point(349, 45)
        Me.chkWeekly.Name = "chkWeekly"
        Me.chkWeekly.Size = New System.Drawing.Size(73, 22)
        Me.chkWeekly.TabIndex = 6
        Me.chkWeekly.Text = "Weekly"
        Me.chkWeekly.UseVisualStyleBackColor = True
        '
        'chkDaily
        '
        Me.chkDaily.AutoSize = True
        Me.chkDaily.Enabled = False
        Me.chkDaily.Location = New System.Drawing.Point(349, 17)
        Me.chkDaily.Name = "chkDaily"
        Me.chkDaily.Size = New System.Drawing.Size(58, 22)
        Me.chkDaily.TabIndex = 5
        Me.chkDaily.Text = "Daily"
        Me.chkDaily.UseVisualStyleBackColor = True
        '
        'cboMailingListStatus
        '
        Me.cboMailingListStatus.Enabled = False
        Me.cboMailingListStatus.FormattingEnabled = True
        Me.cboMailingListStatus.Items.AddRange(New Object() {"ACTIVE", "INACTIVE"})
        Me.cboMailingListStatus.Location = New System.Drawing.Point(108, 60)
        Me.cboMailingListStatus.Name = "cboMailingListStatus"
        Me.cboMailingListStatus.Size = New System.Drawing.Size(220, 26)
        Me.cboMailingListStatus.TabIndex = 4
        '
        'txtMailingListEmailAddress
        '
        Me.txtMailingListEmailAddress.Enabled = False
        Me.txtMailingListEmailAddress.Location = New System.Drawing.Point(108, 25)
        Me.txtMailingListEmailAddress.Name = "txtMailingListEmailAddress"
        Me.txtMailingListEmailAddress.Size = New System.Drawing.Size(220, 26)
        Me.txtMailingListEmailAddress.TabIndex = 3
        '
        'lblMailingListStatus
        '
        Me.lblMailingListStatus.AutoSize = True
        Me.lblMailingListStatus.Location = New System.Drawing.Point(49, 63)
        Me.lblMailingListStatus.Name = "lblMailingListStatus"
        Me.lblMailingListStatus.Size = New System.Drawing.Size(53, 18)
        Me.lblMailingListStatus.TabIndex = 2
        Me.lblMailingListStatus.Text = "Status: "
        '
        'lblMailingListEmailAddress
        '
        Me.lblMailingListEmailAddress.AutoSize = True
        Me.lblMailingListEmailAddress.Location = New System.Drawing.Point(8, 28)
        Me.lblMailingListEmailAddress.Name = "lblMailingListEmailAddress"
        Me.lblMailingListEmailAddress.Size = New System.Drawing.Size(102, 18)
        Me.lblMailingListEmailAddress.TabIndex = 1
        Me.lblMailingListEmailAddress.Text = "Email Address: "
        '
        'gvMailingList
        '
        Me.gvMailingList.AllowUserToAddRows = False
        Me.gvMailingList.AllowUserToDeleteRows = False
        Me.gvMailingList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.gvMailingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gvMailingList.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gvMailingList.Location = New System.Drawing.Point(0, 190)
        Me.gvMailingList.Name = "gvMailingList"
        Me.gvMailingList.ReadOnly = True
        Me.gvMailingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gvMailingList.Size = New System.Drawing.Size(616, 338)
        Me.gvMailingList.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.txtOtherConfigurationDomain)
        Me.TabPage4.Controls.Add(Me.lblOtherConfigurationDomain)
        Me.TabPage4.Controls.Add(Me.btnSaveOtherConfiguration)
        Me.TabPage4.Controls.Add(Me.txtOtherConfigurationGroup)
        Me.TabPage4.Controls.Add(Me.lblOtherConfigurationGroup)
        Me.TabPage4.Location = New System.Drawing.Point(4, 27)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(616, 528)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Other Configuration"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'txtOtherConfigurationDomain
        '
        Me.txtOtherConfigurationDomain.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.txtOtherConfigurationDomain.Location = New System.Drawing.Point(236, 39)
        Me.txtOtherConfigurationDomain.Name = "txtOtherConfigurationDomain"
        Me.txtOtherConfigurationDomain.Size = New System.Drawing.Size(148, 26)
        Me.txtOtherConfigurationDomain.TabIndex = 16
        '
        'lblOtherConfigurationDomain
        '
        Me.lblOtherConfigurationDomain.AutoSize = True
        Me.lblOtherConfigurationDomain.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblOtherConfigurationDomain.Location = New System.Drawing.Point(167, 47)
        Me.lblOtherConfigurationDomain.Name = "lblOtherConfigurationDomain"
        Me.lblOtherConfigurationDomain.Size = New System.Drawing.Size(63, 18)
        Me.lblOtherConfigurationDomain.TabIndex = 15
        Me.lblOtherConfigurationDomain.Text = "Domain: "
        '
        'btnSaveOtherConfiguration
        '
        Me.btnSaveOtherConfiguration.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveOtherConfiguration.Location = New System.Drawing.Point(400, 309)
        Me.btnSaveOtherConfiguration.Name = "btnSaveOtherConfiguration"
        Me.btnSaveOtherConfiguration.Size = New System.Drawing.Size(117, 39)
        Me.btnSaveOtherConfiguration.TabIndex = 14
        Me.btnSaveOtherConfiguration.Text = "Save"
        Me.btnSaveOtherConfiguration.UseVisualStyleBackColor = True
        '
        'txtOtherConfigurationGroup
        '
        Me.txtOtherConfigurationGroup.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.txtOtherConfigurationGroup.Location = New System.Drawing.Point(236, 80)
        Me.txtOtherConfigurationGroup.Name = "txtOtherConfigurationGroup"
        Me.txtOtherConfigurationGroup.Size = New System.Drawing.Size(148, 26)
        Me.txtOtherConfigurationGroup.TabIndex = 13
        '
        'lblOtherConfigurationGroup
        '
        Me.lblOtherConfigurationGroup.AutoSize = True
        Me.lblOtherConfigurationGroup.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.lblOtherConfigurationGroup.Location = New System.Drawing.Point(167, 88)
        Me.lblOtherConfigurationGroup.Name = "lblOtherConfigurationGroup"
        Me.lblOtherConfigurationGroup.Size = New System.Drawing.Size(53, 18)
        Me.lblOtherConfigurationGroup.TabIndex = 12
        Me.lblOtherConfigurationGroup.Text = "Group: "
        '
        'RedSkyConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 559)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "RedSkyConfiguration"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "  RedSky Configuration"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.gvMailingList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents btnSaveReportConfiguration As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblMonthyNextReport As Label
    Friend WithEvents lblMonthlyLastReport As Label
    Friend WithEvents cboMonthlyStatus As ComboBox
    Friend WithEvents dtMonthly As DateTimePicker
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lblWeeklyGenerationTime As Label
    Friend WithEvents lblWeeklyNextReport As Label
    Friend WithEvents lblWeeklyLastReport As Label
    Friend WithEvents cboWeeklyStatus As ComboBox
    Friend WithEvents dtWeekly As DateTimePicker
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lblDailyGenerationTime As Label
    Friend WithEvents lblDailyNextReport As Label
    Friend WithEvents lblDailyLastReport As Label
    Friend WithEvents cboDailyStatus As ComboBox
    Friend WithEvents dtDaily As DateTimePicker
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents btnSaveMailingConfiguration As Button
    Friend WithEvents lblSMTPPassword As Label
    Friend WithEvents lblSMTPUsername As Label
    Friend WithEvents lblSMTPUseTLSSSL As Label
    Friend WithEvents lblSMTPPortSSL As Label
    Friend WithEvents lblSMTPServer As Label
    Friend WithEvents txtSMTPPassword As TextBox
    Friend WithEvents txtSMTPUsername As TextBox
    Friend WithEvents txtSMTPPortSSL As TextBox
    Friend WithEvents txtSMTPServer As TextBox
    Friend WithEvents gvMailingList As DataGridView
    Friend WithEvents lblSMTPId As Label
    Friend WithEvents cboSMTPUseTLSSSL As ComboBox
    Friend WithEvents btnMailingListEdit As Button
    Friend WithEvents btnMailingListAddNew As Button
    Friend WithEvents btnMailingListSave As Button
    Friend WithEvents chkMonthly As CheckBox
    Friend WithEvents chkWeekly As CheckBox
    Friend WithEvents chkDaily As CheckBox
    Friend WithEvents cboMailingListStatus As ComboBox
    Friend WithEvents txtMailingListEmailAddress As TextBox
    Friend WithEvents lblMailingListStatus As Label
    Friend WithEvents lblMailingListEmailAddress As Label
    Friend WithEvents lblMailingListId As Label
    Friend WithEvents btnMailingListCancel As Button
    Friend WithEvents lblDailyTo As Label
    Friend WithEvents lblDailyFrom As Label
    Friend WithEvents dtDailyTo As DateTimePicker
    Friend WithEvents dtDailyFrom As DateTimePicker
    Friend WithEvents lblWeeklyDay As Label
    Friend WithEvents cboWeeklyDay As ComboBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents btnSaveOtherConfiguration As Button
    Friend WithEvents txtOtherConfigurationGroup As TextBox
    Friend WithEvents lblOtherConfigurationGroup As Label
    Friend WithEvents txtOtherConfigurationDomain As TextBox
    Friend WithEvents lblOtherConfigurationDomain As Label
    Friend WithEvents chkUseTemplateDaily As CheckBox
    Friend WithEvents chkUseTemplateMonthly As CheckBox
    Friend WithEvents chkUseTemplateWeekly As CheckBox
    Friend WithEvents chkOverwiteExistingFiles As CheckBox
End Class
