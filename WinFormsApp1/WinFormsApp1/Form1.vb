Imports System.IO
Imports System.Globalization
Imports System.Text

Public Structure RosterShift
    Public Day As String
    Public StartTime As String
    Public EndTime As String
    Public Pay As Decimal
    Public TotalHours As Decimal
    Public IsPublicHoliday As Boolean
End Structure

Public Class Form1

    ' Profile rates made Public so FormSettings can easily access them
    Public BaseRate As Decimal = 13.49D
    Public CasualLoading As Decimal = 1.25D
    Public SundayPenalty As Decimal = 1.75D
    Public SundayNightPenalty As Decimal = 2.25D
    Public SaturdayPenalty As Decimal = 1.5D
    Public SaturdayNightPenalty As Decimal = 1.75D
    Public WeekdayEveningPenalty As Decimal = 1.5D
    Public WeekdayNightPenalty As Decimal = 1.75D
    Public PublicHolidayRate As Decimal = 2.5D
    Public TaxPercentage As Decimal = 15D
    Public LaundryAllowancePerShift As Decimal = 1.25D ' Dynamic setting property

    Private WeeklyShifts As New List(Of RosterShift)()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConfiguration()
        UpdateTotals()
    End Sub

    Public Sub LoadConfiguration()
        Dim configFile As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt")
        If File.Exists(configFile) Then
            Try
                Dim lines() As String = File.ReadAllLines(configFile)
                For Each line As String In lines
                    If line.Contains("=") Then
                        Dim parts() As String = line.Split("="c)
                        Dim key As String = parts(0).Trim().ToUpper()
                        Dim val As Decimal = 0.0D

                        If Decimal.TryParse(parts(1).Trim(), val) Then
                            Select Case key
                                Case "BASE_RATE" : BaseRate = val
                                Case "CASUAL_LOADING" : CasualLoading = val
                                Case "SUNDAY_PENALTY" : SundayPenalty = val
                                Case "SUNDAY_NIGHT_PENALTY" : SundayNightPenalty = val
                                Case "SATURDAY_PENALTY" : SaturdayPenalty = val
                                Case "SATURDAY_NIGHT_PENALTY" : SaturdayNightPenalty = val
                                Case "WEEKDAY_EVENING_PENALTY" : WeekdayEveningPenalty = val
                                Case "WEEKDAY_NIGHT_PENALTY" : WeekdayNightPenalty = val
                                Case "PUBLIC_HOLIDAY_RATE" : PublicHolidayRate = val
                                Case "TAX_PERCENTAGE" : TaxPercentage = val
                                Case "LAUNDRY_ALLOWANCE_PER_SHIFT" : LaundryAllowancePerShift = val
                            End Select
                        End If
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show("Could not fully parse config.txt. Using default rules.", "Configuration Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
    End Sub

    Public Sub SaveConfiguration()
        Dim configFile As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt")
        Try
            Dim sb As New StringBuilder()
            sb.AppendLine("BASE_RATE=" & BaseRate.ToString("F2"))
            sb.AppendLine("CASUAL_LOADING=" & CasualLoading.ToString("F2"))
            sb.AppendLine("SUNDAY_PENALTY=" & SundayPenalty.ToString("F2"))
            sb.AppendLine("SUNDAY_NIGHT_PENALTY=" & SundayNightPenalty.ToString("F2"))
            sb.AppendLine("SATURDAY_PENALTY=" & SaturdayPenalty.ToString("F2"))
            sb.AppendLine("SATURDAY_NIGHT_PENALTY=" & SaturdayNightPenalty.ToString("F2"))
            sb.AppendLine("WEEKDAY_EVENING_PENALTY=" & WeekdayEveningPenalty.ToString("F2"))
            sb.AppendLine("WEEKDAY_NIGHT_PENALTY=" & WeekdayNightPenalty.ToString("F2"))
            sb.AppendLine("PUBLIC_HOLIDAY_RATE=" & PublicHolidayRate.ToString("F2"))
            sb.AppendLine("TAX_PERCENTAGE=" & TaxPercentage.ToString("F2"))
            sb.AppendLine("LAUNDRY_ALLOWANCE_PER_SHIFT=" & LaundryAllowancePerShift.ToString("F2"))
            File.WriteAllText(configFile, sb.ToString())
        Catch ex As Exception
            MessageBox.Show("Could not write out settings file modifications: " & ex.Message, "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GetShiftPay(dayName As String, startTime As DateTime, endTime As DateTime, isHoliday As Boolean, ByRef calculatedHours As Decimal) As Decimal
        Dim baseDate As DateTime = DateTime.Today
        Dim start As DateTime = baseDate.Add(startTime.TimeOfDay)
        Dim [end] As DateTime = baseDate.Add(endTime.TimeOfDay)

        If [end] <= start Then
            [end] = [end].AddDays(1)
        End If

        Dim currentTime As DateTime = start
        Dim totalEarnings As Decimal = 0.0D
        Dim hoursPerStep As Decimal = 0.25D
        calculatedHours = 0.0D

        While currentTime < [end]
            Dim currentDayFlag As String = dayName.ToLower()
            Dim span As TimeSpan = currentTime.Subtract(start)

            If span.Days > 0 Then
                Dim daysOfWeek As New List(Of String) From {"monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"}
                Dim nextIndex As Integer = (daysOfWeek.IndexOf(currentDayFlag) + 1) Mod 7
                currentDayFlag = daysOfWeek(nextIndex)
            End If

            Dim currentHour As Decimal = Convert.ToDecimal(currentTime.Hour) + Convert.ToDecimal(currentTime.Minute / 60.0)
            Dim multiplier As Decimal = 1D

            If isHoliday Then
                multiplier = PublicHolidayRate
            ElseIf currentDayFlag = "sunday" Then
                If currentHour >= 9D AndAlso currentHour < 23D Then multiplier = SundayPenalty Else multiplier = SundayNightPenalty
            ElseIf currentDayFlag = "saturday" Then
                If currentHour >= 7D AndAlso currentHour < 23D Then multiplier = SaturdayPenalty Else multiplier = SaturdayNightPenalty
            Else
                If currentHour >= 7D AndAlso currentHour < 18D Then
                    multiplier = CasualLoading
                ElseIf currentHour >= 18D AndAlso currentHour < 23D Then
                    multiplier = WeekdayEveningPenalty
                Else
                    multiplier = WeekdayNightPenalty
                End If
            End If

            totalEarnings += (BaseRate * multiplier) * hoursPerStep
            calculatedHours += hoursPerStep
            currentTime = currentTime.AddMinutes(15)
        End While

        Return totalEarnings
    End Function

    Public Sub UpdateTotals()
        lstShifts.Items.Clear()
        Dim totalHoursPay As Decimal = 0.0D
        Dim totalHoursCount As Decimal = 0.0D
        Dim totalShiftsCount As Integer = WeeklyShifts.Count

        For Each shift As RosterShift In WeeklyShifts
            totalHoursPay += shift.Pay
            totalHoursCount += shift.TotalHours
            Dim holidayLabel As String = If(shift.IsPublicHoliday, " [PH]", "")
            lstShifts.Items.Add(String.Format("{0}{1} ({2} - {3}) -> {4:C}", shift.Day, holidayLabel, shift.StartTime, shift.EndTime, shift.Pay))
        Next

        Dim laundryAllowance As Decimal = totalShiftsCount * LaundryAllowancePerShift
        Dim finalGross As Decimal = totalHoursPay + laundryAllowance

        Dim calculatedTax As Decimal = 0.0D
        If chkApplyTax.Checked Then
            calculatedTax = finalGross * (TaxPercentage / 100D)
        End If
        Dim finalNet As Decimal = finalGross - calculatedTax

        lblLogs.Text = String.Format("Shifts Logged: {0}", totalShiftsCount)
        lblHoursCount.Text = String.Format("Hours Logged: {0:F2} hrs", totalHoursCount)
        lblLaundry.Text = String.Format("Laundry Allowance: {0:C}", laundryAllowance)
        lblOutput.Text = String.Format("GROSS PAY: {0:C}", finalGross)
        lblTaxDeduction.Text = String.Format("Tax Deduction ({0}%): {1:C}", TaxPercentage, calculatedTax)
        lblNetPay.Text = String.Format("ESTIMATED NET PAY: {0:C}", finalNet)
    End Sub

    Private Sub btnAddShift_Click(sender As Object, e As EventArgs) Handles btnAddShift.Click
        Dim day As String = cmbDay.Text.Trim()

        If String.IsNullOrEmpty(day) Then
            MessageBox.Show("Please select a day before adding.", "Validation Check", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim shiftHours As Decimal = 0.0D
        Dim isHoliday As Boolean = chkPublicHoliday.Checked
        Dim calculatedPay As Decimal = GetShiftPay(day, dtpStart.Value, dtpEnd.Value, isHoliday, shiftHours)

        Dim newShift As New RosterShift With {
            .Day = day,
            .StartTime = dtpStart.Value.ToString("HH:mm"),
            .EndTime = dtpEnd.Value.ToString("HH:mm"),
            .Pay = calculatedPay,
            .TotalHours = shiftHours,
            .IsPublicHoliday = isHoliday
        }

        WeeklyShifts.Add(newShift)
        UpdateTotals()
        chkPublicHoliday.Checked = False
    End Sub

    Private Sub chkApplyTax_CheckedChanged(sender As Object, e As EventArgs) Handles chkApplyTax.CheckedChanged
        UpdateTotals()
    End Sub

    Private Sub btnOpenSettings_Click(sender As Object, e As EventArgs) Handles btnOpenSettings.Click
        Dim settingsWindow As New FormSettings()
        settingsWindow.ShowDialog()
    End Sub

    Private Sub btnClearRoster_Click(sender As Object, e As EventArgs) Handles btnClearRoster.Click
        WeeklyShifts.Clear()
        UpdateTotals()
    End Sub

    Private Sub btnSaveRoster_Click(sender As Object, e As EventArgs) Handles btnSaveRoster.Click
        If WeeklyShifts.Count = 0 Then
            MessageBox.Show("No shifts loaded to save.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim sfd As New SaveFileDialog()
        sfd.Filter = "CSV Files (*.csv)|*.csv"
        sfd.Title = "Save Weekly Roster Data"

        If sfd.ShowDialog() = DialogResult.OK Then
            Try
                Dim sb As New StringBuilder()
                For Each shift As RosterShift In WeeklyShifts
                    sb.AppendLine(String.Format("{0},{1},{2},{3}", shift.Day, shift.StartTime, shift.EndTime, shift.IsPublicHoliday))
                Next
                File.WriteAllText(sfd.FileName, sb.ToString())
                MessageBox.Show("Roster data exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Failed to save data: " & ex.Message, "Disk Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnLoadRoster_Click(sender As Object, e As EventArgs) Handles btnLoadRoster.Click
        Dim ofd As New OpenFileDialog() ' FIXED: Added 'As' connection keyword cleanly
        ofd.Filter = "CSV Files (*.csv)|*.csv"
        ofd.Title = "Open Roster Data File"

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim lines() As String = File.ReadAllLines(ofd.FileName)
                WeeklyShifts.Clear()

                For Each line As String In lines
                    If String.IsNullOrWhiteSpace(line) Then Continue For
                    Dim parts() As String = line.Split(","c)

                    If parts.Length >= 3 Then
                        Dim day As String = parts(0).Trim()
                        Dim startStr As String = parts(1).Trim()
                        Dim endStr As String = parts(2).Trim()

                        Dim isHoliday As Boolean = False
                        If parts.Length >= 4 Then
                            Boolean.TryParse(parts(3).Trim(), isHoliday)
                        End If

                        Dim startDT As DateTime = DateTime.ParseExact(startStr, "HH:mm", CultureInfo.InvariantCulture)
                        Dim endDT As DateTime = DateTime.ParseExact(endStr, "HH:mm", CultureInfo.InvariantCulture)

                        Dim shiftHours As Decimal = 0.0D
                        Dim calculatedPay As Decimal = GetShiftPay(day, startDT, endDT, isHoliday, shiftHours)

                        Dim loadedShift As New RosterShift With {
                            .Day = day,
                            .StartTime = startStr,
                            .EndTime = endStr,
                            .Pay = calculatedPay,
                            .TotalHours = shiftHours,
                            .IsPublicHoliday = isHoliday
                        }
                        WeeklyShifts.Add(loadedShift)
                    End If
                Next

                UpdateTotals()
                MessageBox.Show("Roster imported cleanly!", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Failed to load roster data: " & ex.Message, "Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class
