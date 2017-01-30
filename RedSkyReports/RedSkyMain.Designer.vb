<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RedSkyMain
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
        Me.btnRunReports = New System.Windows.Forms.Button()
        Me.btnConfiguration = New System.Windows.Forms.Button()
        Me.lblDaily = New System.Windows.Forms.Label()
        Me.lblWeekly = New System.Windows.Forms.Label()
        Me.lblMonthly = New System.Windows.Forms.Label()
        Me.lblDailyStatus = New System.Windows.Forms.Label()
        Me.lblWeeklyStatus = New System.Windows.Forms.Label()
        Me.lblMonthlyStatus = New System.Windows.Forms.Label()
        Me.btnStopReports = New System.Windows.Forms.Button()
        Me.lblDailyRunStatus = New System.Windows.Forms.Label()
        Me.lblWeeklyRunStatus = New System.Windows.Forms.Label()
        Me.lblMonthlyRunStatus = New System.Windows.Forms.Label()
        Me.btnReloadConfiguration = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnRunReports
        '
        Me.btnRunReports.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRunReports.ForeColor = System.Drawing.Color.DarkGreen
        Me.btnRunReports.Location = New System.Drawing.Point(34, 144)
        Me.btnRunReports.Name = "btnRunReports"
        Me.btnRunReports.Size = New System.Drawing.Size(164, 42)
        Me.btnRunReports.TabIndex = 0
        Me.btnRunReports.Text = "Run Reports"
        Me.btnRunReports.UseVisualStyleBackColor = True
        '
        'btnConfiguration
        '
        Me.btnConfiguration.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfiguration.Location = New System.Drawing.Point(34, 192)
        Me.btnConfiguration.Name = "btnConfiguration"
        Me.btnConfiguration.Size = New System.Drawing.Size(164, 42)
        Me.btnConfiguration.TabIndex = 1
        Me.btnConfiguration.Text = "Configuration"
        Me.btnConfiguration.UseVisualStyleBackColor = True
        '
        'lblDaily
        '
        Me.lblDaily.AutoSize = True
        Me.lblDaily.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDaily.Location = New System.Drawing.Point(12, 18)
        Me.lblDaily.Name = "lblDaily"
        Me.lblDaily.Size = New System.Drawing.Size(97, 18)
        Me.lblDaily.TabIndex = 2
        Me.lblDaily.Text = "Daily Reports: "
        '
        'lblWeekly
        '
        Me.lblWeekly.AutoSize = True
        Me.lblWeekly.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeekly.Location = New System.Drawing.Point(12, 60)
        Me.lblWeekly.Name = "lblWeekly"
        Me.lblWeekly.Size = New System.Drawing.Size(112, 18)
        Me.lblWeekly.TabIndex = 3
        Me.lblWeekly.Text = "Weekly Reports: "
        '
        'lblMonthly
        '
        Me.lblMonthly.AutoSize = True
        Me.lblMonthly.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthly.Location = New System.Drawing.Point(11, 101)
        Me.lblMonthly.Name = "lblMonthly"
        Me.lblMonthly.Size = New System.Drawing.Size(118, 18)
        Me.lblMonthly.TabIndex = 4
        Me.lblMonthly.Text = "Monthly Reports: "
        '
        'lblDailyStatus
        '
        Me.lblDailyStatus.AutoSize = True
        Me.lblDailyStatus.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyStatus.Location = New System.Drawing.Point(157, 18)
        Me.lblDailyStatus.Name = "lblDailyStatus"
        Me.lblDailyStatus.Size = New System.Drawing.Size(48, 18)
        Me.lblDailyStatus.TabIndex = 5
        Me.lblDailyStatus.Text = "Label4"
        '
        'lblWeeklyStatus
        '
        Me.lblWeeklyStatus.AutoSize = True
        Me.lblWeeklyStatus.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeeklyStatus.Location = New System.Drawing.Point(157, 60)
        Me.lblWeeklyStatus.Name = "lblWeeklyStatus"
        Me.lblWeeklyStatus.Size = New System.Drawing.Size(48, 18)
        Me.lblWeeklyStatus.TabIndex = 6
        Me.lblWeeklyStatus.Text = "Label5"
        '
        'lblMonthlyStatus
        '
        Me.lblMonthlyStatus.AutoSize = True
        Me.lblMonthlyStatus.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthlyStatus.Location = New System.Drawing.Point(157, 101)
        Me.lblMonthlyStatus.Name = "lblMonthlyStatus"
        Me.lblMonthlyStatus.Size = New System.Drawing.Size(48, 18)
        Me.lblMonthlyStatus.TabIndex = 7
        Me.lblMonthlyStatus.Text = "Label6"
        '
        'btnStopReports
        '
        Me.btnStopReports.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStopReports.ForeColor = System.Drawing.Color.DarkRed
        Me.btnStopReports.Location = New System.Drawing.Point(219, 144)
        Me.btnStopReports.Name = "btnStopReports"
        Me.btnStopReports.Size = New System.Drawing.Size(164, 42)
        Me.btnStopReports.TabIndex = 11
        Me.btnStopReports.Text = "Stop Reports"
        Me.btnStopReports.UseVisualStyleBackColor = True
        '
        'lblDailyRunStatus
        '
        Me.lblDailyRunStatus.AutoSize = True
        Me.lblDailyRunStatus.Font = New System.Drawing.Font("Calibri", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyRunStatus.Location = New System.Drawing.Point(325, 18)
        Me.lblDailyRunStatus.Name = "lblDailyRunStatus"
        Me.lblDailyRunStatus.Size = New System.Drawing.Size(48, 18)
        Me.lblDailyRunStatus.TabIndex = 8
        Me.lblDailyRunStatus.Text = "Label7"
        '
        'lblWeeklyRunStatus
        '
        Me.lblWeeklyRunStatus.AutoSize = True
        Me.lblWeeklyRunStatus.Font = New System.Drawing.Font("Calibri", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeeklyRunStatus.Location = New System.Drawing.Point(325, 60)
        Me.lblWeeklyRunStatus.Name = "lblWeeklyRunStatus"
        Me.lblWeeklyRunStatus.Size = New System.Drawing.Size(48, 18)
        Me.lblWeeklyRunStatus.TabIndex = 9
        Me.lblWeeklyRunStatus.Text = "Label8"
        '
        'lblMonthlyRunStatus
        '
        Me.lblMonthlyRunStatus.AutoSize = True
        Me.lblMonthlyRunStatus.Font = New System.Drawing.Font("Calibri", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthlyRunStatus.Location = New System.Drawing.Point(325, 101)
        Me.lblMonthlyRunStatus.Name = "lblMonthlyRunStatus"
        Me.lblMonthlyRunStatus.Size = New System.Drawing.Size(48, 18)
        Me.lblMonthlyRunStatus.TabIndex = 10
        Me.lblMonthlyRunStatus.Text = "Label9"
        '
        'btnReloadConfiguration
        '
        Me.btnReloadConfiguration.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReloadConfiguration.Location = New System.Drawing.Point(219, 192)
        Me.btnReloadConfiguration.Name = "btnReloadConfiguration"
        Me.btnReloadConfiguration.Size = New System.Drawing.Size(164, 42)
        Me.btnReloadConfiguration.TabIndex = 12
        Me.btnReloadConfiguration.Text = "Reload Configuration"
        Me.btnReloadConfiguration.UseVisualStyleBackColor = True
        '
        'RedSkyMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 258)
        Me.Controls.Add(Me.btnReloadConfiguration)
        Me.Controls.Add(Me.lblMonthlyRunStatus)
        Me.Controls.Add(Me.lblWeeklyRunStatus)
        Me.Controls.Add(Me.lblDailyRunStatus)
        Me.Controls.Add(Me.lblMonthlyStatus)
        Me.Controls.Add(Me.lblWeeklyStatus)
        Me.Controls.Add(Me.lblDailyStatus)
        Me.Controls.Add(Me.lblMonthly)
        Me.Controls.Add(Me.lblWeekly)
        Me.Controls.Add(Me.lblDaily)
        Me.Controls.Add(Me.btnConfiguration)
        Me.Controls.Add(Me.btnRunReports)
        Me.Controls.Add(Me.btnStopReports)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "RedSkyMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RedSky Reports"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnRunReports As Button
    Friend WithEvents btnConfiguration As Button
    Friend WithEvents lblDaily As Label
    Friend WithEvents lblWeekly As Label
    Friend WithEvents lblMonthly As Label
    Friend WithEvents lblDailyStatus As Label
    Friend WithEvents lblWeeklyStatus As Label
    Friend WithEvents lblMonthlyStatus As Label
    Friend WithEvents btnStopReports As Button
    Friend WithEvents lblDailyRunStatus As Label
    Friend WithEvents lblWeeklyRunStatus As Label
    Friend WithEvents lblMonthlyRunStatus As Label
    Friend WithEvents btnReloadConfiguration As Button
End Class
