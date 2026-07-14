<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        cmbDay = New ComboBox()
        BtnPayCalc = New Button()
        lblOutput = New Label()
        NameLogo = New Label()
        DoW = New Label()
        ST = New Label()
        ET = New Label()
        lstShifts = New ListBox()
        btnAddShift = New Button()
        btnClearRoster = New Button()
        btnSaveRoster = New Button()
        btnLoadRoster = New Button()
        lblLaundry = New Label()
        lblLogs = New Label()
        lblHoursCount = New Label()
        Label1 = New Label()
        dtpStart = New DateTimePicker()
        dtpEnd = New DateTimePicker()
        chkPublicHoliday = New CheckBox()
        chkApplyTax = New CheckBox()
        btnOpenSettings = New Button()
        lblTaxDeduction = New Label()
        lblNetPay = New Label()
        SuspendLayout()
        ' 
        ' cmbDay
        ' 
        cmbDay.FormattingEnabled = True
        cmbDay.Items.AddRange(New Object() {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"})
        cmbDay.Location = New Point(162, 113)
        cmbDay.Name = "cmbDay"
        cmbDay.Size = New Size(121, 23)
        cmbDay.TabIndex = 0
        ' 
        ' BtnPayCalc
        ' 
        BtnPayCalc.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnPayCalc.Location = New Point(240, 257)
        BtnPayCalc.Name = "BtnPayCalc"
        BtnPayCalc.Size = New Size(191, 73)
        BtnPayCalc.TabIndex = 3
        BtnPayCalc.Text = "CALCULATE"
        BtnPayCalc.UseVisualStyleBackColor = True
        ' 
        ' lblOutput
        ' 
        lblOutput.AutoSize = True
        lblOutput.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblOutput.Location = New Point(23, 567)
        lblOutput.Name = "lblOutput"
        lblOutput.Size = New Size(242, 25)
        lblOutput.TabIndex = 4
        lblOutput.Text = "Total Estimated Pay: $0.00"
        ' 
        ' NameLogo
        ' 
        NameLogo.AutoSize = True
        NameLogo.Font = New Font("Impact", 48F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        NameLogo.Location = New Point(102, 9)
        NameLogo.Name = "NameLogo"
        NameLogo.RightToLeft = RightToLeft.Yes
        NameLogo.Size = New Size(243, 80)
        NameLogo.TabIndex = 5
        NameLogo.Text = "PayCalc"
        ' 
        ' DoW
        ' 
        DoW.AutoSize = True
        DoW.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DoW.Location = New Point(171, 89)
        DoW.Name = "DoW"
        DoW.Size = New Size(112, 21)
        DoW.TabIndex = 6
        DoW.Text = "Day of Week:"
        ' 
        ' ST
        ' 
        ST.AutoSize = True
        ST.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        ST.Location = New Point(12, 152)
        ST.Name = "ST"
        ST.Size = New Size(103, 15)
        ST.TabIndex = 6
        ST.Text = "Start Time (24H):"
        ' 
        ' ET
        ' 
        ET.AutoSize = True
        ET.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        ET.Location = New Point(338, 152)
        ET.Name = "ET"
        ET.Size = New Size(95, 15)
        ET.TabIndex = 6
        ET.Text = "End Time (24H):"
        ' 
        ' lstShifts
        ' 
        lstShifts.FormattingEnabled = True
        lstShifts.Location = New Point(12, 336)
        lstShifts.Name = "lstShifts"
        lstShifts.Size = New Size(419, 94)
        lstShifts.TabIndex = 7
        ' 
        ' btnAddShift
        ' 
        btnAddShift.Location = New Point(12, 199)
        btnAddShift.Name = "btnAddShift"
        btnAddShift.Size = New Size(196, 23)
        btnAddShift.TabIndex = 8
        btnAddShift.Text = "Add Shift"
        btnAddShift.UseVisualStyleBackColor = True
        ' 
        ' btnClearRoster
        ' 
        btnClearRoster.Location = New Point(12, 228)
        btnClearRoster.Name = "btnClearRoster"
        btnClearRoster.Size = New Size(196, 23)
        btnClearRoster.TabIndex = 9
        btnClearRoster.Text = "Clear"
        btnClearRoster.UseVisualStyleBackColor = True
        ' 
        ' btnSaveRoster
        ' 
        btnSaveRoster.Location = New Point(240, 199)
        btnSaveRoster.Name = "btnSaveRoster"
        btnSaveRoster.Size = New Size(193, 23)
        btnSaveRoster.TabIndex = 10
        btnSaveRoster.Text = "Save Roster"
        btnSaveRoster.UseVisualStyleBackColor = True
        ' 
        ' btnLoadRoster
        ' 
        btnLoadRoster.Location = New Point(240, 228)
        btnLoadRoster.Name = "btnLoadRoster"
        btnLoadRoster.Size = New Size(193, 23)
        btnLoadRoster.TabIndex = 11
        btnLoadRoster.Text = "Load Roster"
        btnLoadRoster.UseVisualStyleBackColor = True
        ' 
        ' lblLaundry
        ' 
        lblLaundry.AutoSize = True
        lblLaundry.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblLaundry.Location = New Point(23, 492)
        lblLaundry.Name = "lblLaundry"
        lblLaundry.Size = New Size(242, 25)
        lblLaundry.TabIndex = 4
        lblLaundry.Text = "Laundry Allowance: $0.00"
        ' 
        ' lblLogs
        ' 
        lblLogs.AutoSize = True
        lblLogs.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblLogs.Location = New Point(23, 467)
        lblLogs.Name = "lblLogs"
        lblLogs.Size = New Size(155, 25)
        lblLogs.TabIndex = 4
        lblLogs.Text = "Shifts Logged: 0"
        ' 
        ' lblHoursCount
        ' 
        lblHoursCount.AutoSize = True
        lblHoursCount.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        lblHoursCount.Location = New Point(23, 442)
        lblHoursCount.Name = "lblHoursCount"
        lblHoursCount.Size = New Size(161, 25)
        lblHoursCount.TabIndex = 4
        lblHoursCount.Text = "Hours Logged: 0"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 318)
        Label1.Name = "Label1"
        Label1.Size = New Size(74, 15)
        Label1.TabIndex = 12
        Label1.Text = "List of Shifts:"
        ' 
        ' dtpStart
        ' 
        dtpStart.CustomFormat = "HH:mm"
        dtpStart.Format = DateTimePickerFormat.Custom
        dtpStart.Location = New Point(12, 170)
        dtpStart.Name = "dtpStart"
        dtpStart.Size = New Size(196, 23)
        dtpStart.TabIndex = 13
        ' 
        ' dtpEnd
        ' 
        dtpEnd.CustomFormat = "HH:mm"
        dtpEnd.Format = DateTimePickerFormat.Custom
        dtpEnd.Location = New Point(240, 170)
        dtpEnd.Name = "dtpEnd"
        dtpEnd.Size = New Size(193, 23)
        dtpEnd.TabIndex = 13
        ' 
        ' chkPublicHoliday
        ' 
        chkPublicHoliday.AutoSize = True
        chkPublicHoliday.Location = New Point(12, 257)
        chkPublicHoliday.Name = "chkPublicHoliday"
        chkPublicHoliday.Size = New Size(103, 19)
        chkPublicHoliday.TabIndex = 14
        chkPublicHoliday.Text = "Public Holiday"
        chkPublicHoliday.UseVisualStyleBackColor = True
        ' 
        ' chkApplyTax
        ' 
        chkApplyTax.AutoSize = True
        chkApplyTax.Location = New Point(12, 282)
        chkApplyTax.Name = "chkApplyTax"
        chkApplyTax.Size = New Size(77, 19)
        chkApplyTax.TabIndex = 14
        chkApplyTax.Text = "Apply Tax"
        chkApplyTax.UseVisualStyleBackColor = True
        ' 
        ' btnOpenSettings
        ' 
        btnOpenSettings.Location = New Point(364, 9)
        btnOpenSettings.Name = "btnOpenSettings"
        btnOpenSettings.Size = New Size(69, 44)
        btnOpenSettings.TabIndex = 15
        btnOpenSettings.Text = "SETTINGS"
        btnOpenSettings.UseVisualStyleBackColor = True
        ' 
        ' lblTaxDeduction
        ' 
        lblTaxDeduction.AutoSize = True
        lblTaxDeduction.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTaxDeduction.Location = New Point(23, 517)
        lblTaxDeduction.Name = "lblTaxDeduction"
        lblTaxDeduction.Size = New Size(70, 25)
        lblTaxDeduction.TabIndex = 16
        lblTaxDeduction.Text = "Label2"
        ' 
        ' lblNetPay
        ' 
        lblNetPay.AutoSize = True
        lblNetPay.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblNetPay.Location = New Point(23, 542)
        lblNetPay.Name = "lblNetPay"
        lblNetPay.Size = New Size(70, 25)
        lblNetPay.TabIndex = 17
        lblNetPay.Text = "Label2"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(443, 596)
        Controls.Add(lblNetPay)
        Controls.Add(lblTaxDeduction)
        Controls.Add(btnOpenSettings)
        Controls.Add(chkApplyTax)
        Controls.Add(chkPublicHoliday)
        Controls.Add(dtpEnd)
        Controls.Add(dtpStart)
        Controls.Add(Label1)
        Controls.Add(btnLoadRoster)
        Controls.Add(btnSaveRoster)
        Controls.Add(btnClearRoster)
        Controls.Add(btnAddShift)
        Controls.Add(lstShifts)
        Controls.Add(ET)
        Controls.Add(ST)
        Controls.Add(DoW)
        Controls.Add(NameLogo)
        Controls.Add(lblHoursCount)
        Controls.Add(lblLogs)
        Controls.Add(lblLaundry)
        Controls.Add(lblOutput)
        Controls.Add(BtnPayCalc)
        Controls.Add(cmbDay)
        Name = "Form1"
        Text = "PayCalc"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cmbDay As ComboBox
    Friend WithEvents BtnPayCalc As Button
    Friend WithEvents lblOutput As Label
    Friend WithEvents NameLogo As Label
    Friend WithEvents DoW As Label
    Friend WithEvents ST As Label
    Friend WithEvents ET As Label
    Friend WithEvents lstShifts As ListBox
    Friend WithEvents btnAddShift As Button
    Friend WithEvents btnClearRoster As Button
    Friend WithEvents btnSaveRoster As Button
    Friend WithEvents btnLoadRoster As Button
    Friend WithEvents lblLaundry As Label
    Friend WithEvents lblLogs As Label
    Friend WithEvents lblHoursCount As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpStart As DateTimePicker
    Friend WithEvents dtpEnd As DateTimePicker
    Friend WithEvents chkPublicHoliday As CheckBox
    Friend WithEvents chkApplyTax As CheckBox
    Friend WithEvents btnOpenSettings As Button
    Friend WithEvents lblTaxDeduction As Label
    Friend WithEvents lblNetPay As Label

End Class
