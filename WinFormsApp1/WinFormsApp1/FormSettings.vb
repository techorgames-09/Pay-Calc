Public Class FormSettings
    Private Sub FormSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load all live calculations state directly into your layout textboxes
        txtBaseRate.Text = Form1.BaseRate.ToString("F2")
        txtCasualLoading.Text = Form1.CasualLoading.ToString("F2")
        txtSunday.Text = Form1.SundayPenalty.ToString("F2")
        txtSundayNight.Text = Form1.SundayNightPenalty.ToString("F2")
        txtSaturday.Text = Form1.SaturdayPenalty.ToString("F2")
        txtSaturdayNight.Text = Form1.SaturdayNightPenalty.ToString("F2")
        txtWeekdayEvening.Text = Form1.WeekdayEveningPenalty.ToString("F2")
        txtWeekdayNight.Text = Form1.WeekdayNightPenalty.ToString("F2")
        txtHolidayRate.Text = Form1.PublicHolidayRate.ToString("F2")
        txtTaxRate.Text = Form1.TaxPercentage.ToString("F2")
        txtLaundryAllowance.Text = Form1.LaundryAllowancePerShift.ToString("F2") ' Sync allowance entry field
    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        ' Parse changes safely out from inputs back to global Form1 metrics variables
        Decimal.TryParse(txtBaseRate.Text, Form1.BaseRate)
        Decimal.TryParse(txtCasualLoading.Text, Form1.CasualLoading)
        Decimal.TryParse(txtSunday.Text, Form1.SundayPenalty)
        Decimal.TryParse(txtSundayNight.Text, Form1.SundayNightPenalty)
        Decimal.TryParse(txtSaturday.Text, Form1.SaturdayPenalty)
        Decimal.TryParse(txtSaturdayNight.Text, Form1.SaturdayNightPenalty)
        Decimal.TryParse(txtWeekdayEvening.Text, Form1.WeekdayEveningPenalty)
        Decimal.TryParse(txtWeekdayNight.Text, Form1.WeekdayNightPenalty)
        Decimal.TryParse(txtHolidayRate.Text, Form1.PublicHolidayRate)
        Decimal.TryParse(txtTaxRate.Text, Form1.TaxPercentage)
        Decimal.TryParse(txtLaundryAllowance.Text, Form1.LaundryAllowancePerShift)

        ' Trigger configuration file rewrite and re-calculate active totals
        Form1.SaveConfiguration()
        Form1.UpdateTotals()

        MessageBox.Show("Settings saved and updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
    End Sub
End Class
