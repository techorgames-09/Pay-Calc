Imports System.IO
Imports System.Globalization
Imports System.Text

Public Structure RosterShift
    Public Day As String
    Public StartTime As String
    Public EndTime As String
    Public Pay As Decimal
    Public TotalHours As Decimal
End Structure

Public Class Form1

    ' Profile rates with standard defaults if config is missing
    Private BaseRate As Decimal = 13.49D
    Private CasualLoading As Decimal = 1.25D
    Private SundayPenalty As Decimal = 1.75D
    Private SundayNightPenalty As Decimal = 2.25D
    Private SaturdayPenalty As Decimal = 1.5D
    Private SaturdayNightPenalty As Decimal = 1.75D
    Private WeekdayEveningPenalty As Decimal = 1.5D
    Private WeekdayNightPenalty As Decimal = 1.75D

    Private WeeklyShifts As New List(Of RosterShift)()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConfiguration()
        UpdateTotals()
    End Sub

    Private Sub LoadConfiguration()
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
                            End Select
                        End If
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show("Could not fully parse config.txt. Using default rules.", "Configuration Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
    End Sub

    Private Function GetShiftPay(dayName As String, startTime As DateTime, endTime As DateTime, ByRef calculatedHours As Decimal) As Decimal
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

            If currentDayFlag = "sunday" Then
                If currentHour >= 9D AndAlso currentHour < 23D Then
                    multiplier = SundayPenalty
                Else
                    multiplier = SundayNightPenalty
                End If
            ElseIf currentDayFlag = "saturday" Then
                If currentHour >= 7D AndAlso currentHour < 23D Then
                    multiplier = SaturdayPenalty
                Else
                    multiplier = SaturdayNightPenalty
                End If
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

    Private Sub UpdateTotals()
        lstShifts.Items.Clear()
        Dim totalHoursPay As Decimal = 0.0D
        Dim totalHoursCount As Decimal = 0.0D
        Dim totalShiftsCount As Integer = WeeklyShifts.Count

        For Each shift As RosterShift In WeeklyShifts
            totalHoursPay += shift.Pay
            totalHoursCount += shift.TotalHours
            lstShifts.Items.Add(String.Format("{0} ({1} - {2}) -> {3:C}", shift.Day, shift.StartTime, shift.EndTime, shift.Pay))
        Next

        Dim laundryAllowance As Decimal = totalShiftsCount * 1.25D
        Dim finalGross As Decimal = totalHoursPay + laundryAllowance

        lblLogs.Text = String.Format("Shifts Logged: {0}", totalShiftsCount)
        lblHoursCount.Text = String.Format("Hours Logged: {0:F2} hrs", totalHoursCount)
        lblLaundry.Text = String.Format("Laundry Allowance: {0:C}", laundryAllowance)
        lblOutput.Text = String.Format("TOTAL PAY: {0:C}", finalGross)
    End Sub

    Private Sub btnAddShift_Click(sender As Object, e As EventArgs) Handles btnAddShift.Click
        Dim day As String = cmbDay.Text.Trim()

        If String.IsNullOrEmpty(day) Then
            MessageBox.Show("Please select a day before adding.", "Validation Check", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim shiftHours As Decimal = 0.0D
        Dim calculatedPay As Decimal = GetShiftPay(day, dtpStart.Value, dtpEnd.Value, shiftHours)

        Dim newShift As New RosterShift With {
            .Day = day,
            .StartTime = dtpStart.Value.ToString("HH:mm"),
            .EndTime = dtpEnd.Value.ToString("HH:mm"),
            .Pay = calculatedPay,
            .TotalHours = shiftHours
        }

        WeeklyShifts.Add(newShift)
        UpdateTotals()
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
                    sb.AppendLine(String.Format("{0},{1},{2}", shift.Day, shift.StartTime, shift.EndTime))
                Next
                File.WriteAllText(sfd.FileName, sb.ToString())
                MessageBox.Show("Roster data exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Failed to save data: " & ex.Message, "Disk Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnLoadRoster_Click(sender As Object, e As EventArgs) Handles btnLoadRoster.Click
        Dim ofd As New OpenFileDialog()
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

                        Dim startDT As DateTime = DateTime.ParseExact(startStr, "HH:mm", CultureInfo.InvariantCulture)
                        Dim endDT As DateTime = DateTime.ParseExact(endStr, "HH:mm", CultureInfo.InvariantCulture)

                        Dim shiftHours As Decimal = 0.0D
                        Dim calculatedPay As Decimal = GetShiftPay(day, startDT, endDT, shiftHours)

                        Dim loadedShift As New RosterShift With {
                            .Day = day,
                            .StartTime = startStr,
                            .EndTime = endStr,
                            .Pay = calculatedPay,
                            .TotalHours = shiftHours
                        }
                        WeeklyShifts.Add(loadedShift)
                    End If
                Next

                UpdateTotals()
                MessageBox.Show("Roster imported cleanly!", "Import Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Failed to load roster data: " & ex.Message, "Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class
