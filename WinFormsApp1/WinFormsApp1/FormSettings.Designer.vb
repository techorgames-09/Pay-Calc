<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSettings
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
        txtBaseRate = New TextBox()
        txtCasualLoading = New TextBox()
        txtSunday = New TextBox()
        txtSundayNight = New TextBox()
        txtSaturday = New TextBox()
        txtSaturdayNight = New TextBox()
        txtWeekdayEvening = New TextBox()
        txtWeekdayNight = New TextBox()
        txtHolidayRate = New TextBox()
        txtTaxRate = New TextBox()
        btnSaveSettings = New Button()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        txtLaundryAllowance = New TextBox()
        SuspendLayout()
        ' 
        ' txtBaseRate
        ' 
        txtBaseRate.Location = New Point(181, 31)
        txtBaseRate.Name = "txtBaseRate"
        txtBaseRate.Size = New Size(100, 23)
        txtBaseRate.TabIndex = 0
        ' 
        ' txtCasualLoading
        ' 
        txtCasualLoading.Location = New Point(181, 60)
        txtCasualLoading.Name = "txtCasualLoading"
        txtCasualLoading.Size = New Size(100, 23)
        txtCasualLoading.TabIndex = 0
        ' 
        ' txtSunday
        ' 
        txtSunday.Location = New Point(181, 109)
        txtSunday.Name = "txtSunday"
        txtSunday.Size = New Size(100, 23)
        txtSunday.TabIndex = 0
        ' 
        ' txtSundayNight
        ' 
        txtSundayNight.Location = New Point(181, 138)
        txtSundayNight.Name = "txtSundayNight"
        txtSundayNight.Size = New Size(100, 23)
        txtSundayNight.TabIndex = 0
        ' 
        ' txtSaturday
        ' 
        txtSaturday.Location = New Point(181, 167)
        txtSaturday.Name = "txtSaturday"
        txtSaturday.Size = New Size(100, 23)
        txtSaturday.TabIndex = 0
        ' 
        ' txtSaturdayNight
        ' 
        txtSaturdayNight.Location = New Point(181, 196)
        txtSaturdayNight.Name = "txtSaturdayNight"
        txtSaturdayNight.Size = New Size(100, 23)
        txtSaturdayNight.TabIndex = 0
        ' 
        ' txtWeekdayEvening
        ' 
        txtWeekdayEvening.Location = New Point(181, 225)
        txtWeekdayEvening.Name = "txtWeekdayEvening"
        txtWeekdayEvening.Size = New Size(100, 23)
        txtWeekdayEvening.TabIndex = 0
        ' 
        ' txtWeekdayNight
        ' 
        txtWeekdayNight.Location = New Point(181, 254)
        txtWeekdayNight.Name = "txtWeekdayNight"
        txtWeekdayNight.Size = New Size(100, 23)
        txtWeekdayNight.TabIndex = 0
        ' 
        ' txtHolidayRate
        ' 
        txtHolidayRate.Location = New Point(181, 306)
        txtHolidayRate.Name = "txtHolidayRate"
        txtHolidayRate.Size = New Size(100, 23)
        txtHolidayRate.TabIndex = 0
        ' 
        ' txtTaxRate
        ' 
        txtTaxRate.Location = New Point(181, 335)
        txtTaxRate.Name = "txtTaxRate"
        txtTaxRate.Size = New Size(100, 23)
        txtTaxRate.TabIndex = 0
        ' 
        ' btnSaveSettings
        ' 
        btnSaveSettings.Location = New Point(12, 430)
        btnSaveSettings.Name = "btnSaveSettings"
        btnSaveSettings.Size = New Size(269, 23)
        btnSaveSettings.TabIndex = 1
        btnSaveSettings.Text = "SAVE SETTINGS"
        btnSaveSettings.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 34)
        Label1.Name = "Label1"
        Label1.Size = New Size(99, 15)
        Label1.TabIndex = 2
        Label1.Text = "Base Pay Rate ($):"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 63)
        Label2.Name = "Label2"
        Label2.Size = New Size(148, 15)
        Label2.TabIndex = 2
        Label2.Text = "Casual Loading Bonus (%):"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(11, 112)
        Label3.Name = "Label3"
        Label3.Size = New Size(106, 15)
        Label3.TabIndex = 2
        Label3.Text = "Sunday Bonus (%):"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(12, 141)
        Label4.Name = "Label4"
        Label4.Size = New Size(139, 15)
        Label4.TabIndex = 2
        Label4.Text = "Sunday Night Bonus (%):"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(12, 170)
        Label5.Name = "Label5"
        Label5.Size = New Size(113, 15)
        Label5.TabIndex = 2
        Label5.Text = "Saturday Bonus (%):"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(12, 199)
        Label6.Name = "Label6"
        Label6.Size = New Size(146, 15)
        Label6.TabIndex = 2
        Label6.Text = "Saturday Night Bonus (%):"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(11, 228)
        Label7.Name = "Label7"
        Label7.Size = New Size(160, 15)
        Label7.TabIndex = 2
        Label7.Text = "Weekday Evening Bonus (%):"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(11, 257)
        Label8.Name = "Label8"
        Label8.Size = New Size(148, 15)
        Label8.TabIndex = 2
        Label8.Text = "Weekday Night Bonus (%):"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(10, 309)
        Label9.Name = "Label9"
        Label9.Size = New Size(130, 15)
        Label9.TabIndex = 2
        Label9.Text = "Public Holiday Rate ($):"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(10, 389)
        Label10.Name = "Label10"
        Label10.Size = New Size(129, 15)
        Label10.TabIndex = 2
        Label10.Text = "Allowance Per Shift ($):"
        ' 
        ' txtLaundryAllowance
        ' 
        txtLaundryAllowance.Location = New Point(181, 386)
        txtLaundryAllowance.Name = "txtLaundryAllowance"
        txtLaundryAllowance.Size = New Size(100, 23)
        txtLaundryAllowance.TabIndex = 3
        ' 
        ' FormSettings
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(293, 465)
        Controls.Add(txtLaundryAllowance)
        Controls.Add(Label4)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(btnSaveSettings)
        Controls.Add(txtTaxRate)
        Controls.Add(txtHolidayRate)
        Controls.Add(txtWeekdayNight)
        Controls.Add(txtWeekdayEvening)
        Controls.Add(txtSaturdayNight)
        Controls.Add(txtSaturday)
        Controls.Add(txtSundayNight)
        Controls.Add(txtSunday)
        Controls.Add(txtCasualLoading)
        Controls.Add(txtBaseRate)
        Name = "FormSettings"
        Text = "Settings"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtBaseRate As TextBox
    Friend WithEvents txtCasualLoading As TextBox
    Friend WithEvents txtSunday As TextBox
    Friend WithEvents txtSundayNight As TextBox
    Friend WithEvents txtSaturday As TextBox
    Friend WithEvents txtSaturdayNight As TextBox
    Friend WithEvents txtWeekdayEvening As TextBox
    Friend WithEvents txtWeekdayNight As TextBox
    Friend WithEvents txtHolidayRate As TextBox
    Friend WithEvents txtTaxRate As TextBox
    Friend WithEvents btnSaveSettings As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtLaundryAllowance As TextBox
End Class
