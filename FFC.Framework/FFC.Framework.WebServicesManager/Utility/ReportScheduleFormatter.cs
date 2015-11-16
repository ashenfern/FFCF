using FFC.Framework.Common;
using FFC.Framework.Data;
using FFC.Framework.WebServicesManager.RS2010;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FFC.Framework.WebServicesManager.Utility
{
    /// <summary>
    /// This class provides method to format subscription schedules
    /// </summary>
    public class ReportScheduleFormatter
    {
        /// <summary>
        /// Returns corresponding reporty subscription schedule data 
        /// </summary>
        /// <param name="reportSchedule"></param>
        /// <returns>Schedule xml</returns>
        public string GetMatchData(ReportSchedule reportSchedule)
        {
            ScheduleDefinition schedule = new ScheduleDefinition();

            //Make start date time as the begining of the current day
            schedule.StartDateTime = DateTime.Today.Add(reportSchedule.Time_to_Execute_the_Report);

            if (reportSchedule.Expiration_Date_for_the_Schedule != null)
            {
                if (schedule.StartDateTime < schedule.EndDate)
                {
                    schedule.EndDateSpecified = true;
                    schedule.EndDate = schedule.EndDate;
                }
                else
                {
                    schedule.EndDate = schedule.StartDateTime.AddMinutes(20);
                    schedule.EndDateSpecified = true;
                }
            }
            else
            {
                schedule.EndDateSpecified = false;
            }
            schedule.Item = GetPattern(reportSchedule.Report_Schedule_Type_Code, reportSchedule.Report_Schedule_Day);

            XmlDocument xmlDoc = GetScheduleAsXml(schedule);
            return xmlDoc.OuterXml;
        }

        /// <summary>
        /// Create the recurrence pattern according to the schedule Type
        /// </summary>
        /// <param name="scheduleTypeParam"></param>
        /// <returns></returns>
        private RecurrencePattern GetPattern(string scheduleTypeParam, int scheduleDay)
        {
            ScheduleType scheduleType = (ScheduleType)Enum.Parse(typeof(ScheduleType), scheduleTypeParam, true);

            switch (scheduleType)
            {
                case ScheduleType.Daily:
                    DailyRecurrence dailyPattern = new DailyRecurrence();
                    dailyPattern.DaysInterval = 1;
                    return dailyPattern;
                case ScheduleType.Monthly:
                    MonthlyDOWRecurrence monthlyPattern = new MonthlyDOWRecurrence();
                    monthlyPattern.WhichWeekSpecified = true;
                    monthlyPattern.WhichWeek = WeekNumberEnum.LastWeek;

                    MonthsOfYearSelector months = new MonthsOfYearSelector();
                    months.January = true;
                    months.February = true;
                    months.March = true;
                    months.April = true;
                    months.May = true;
                    months.June = true;
                    months.July = true;
                    months.August = true;
                    months.September = true;
                    months.October = true;
                    months.November = true;
                    months.December = true;
                    monthlyPattern.MonthsOfYear = months;

                    DaysOfWeekSelector days = GetExecutionDay(scheduleDay);
                    monthlyPattern.DaysOfWeek = days;

                    return monthlyPattern;
                case ScheduleType.Weekdays:
                    WeeklyRecurrence weekdaysRecurrence = new WeeklyRecurrence();

                    DaysOfWeekSelector Weekdays = new DaysOfWeekSelector();
                    Weekdays.Monday = true;
                    Weekdays.Tuesday = true;
                    Weekdays.Wednesday = true;
                    Weekdays.Thursday = true;
                    Weekdays.Friday = true;

                    weekdaysRecurrence.DaysOfWeek = Weekdays;

                    weekdaysRecurrence.WeeksIntervalSpecified = true;
                    weekdaysRecurrence.WeeksInterval = 1;

                    return weekdaysRecurrence;
                case ScheduleType.Weekly:
                    WeeklyRecurrence weeklyRecurrence = new WeeklyRecurrence();

                    DaysOfWeekSelector weeklydays = GetExecutionDay(scheduleDay);
                    weeklyRecurrence.DaysOfWeek = weeklydays;

                    weeklyRecurrence.WeeksIntervalSpecified = true;
                    weeklyRecurrence.WeeksInterval = 1;

                    return weeklyRecurrence;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Generate XML Document from the schedule definition
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private XmlDocument GetScheduleAsXml(ScheduleDefinition schedule)
        {
            MemoryStream buffer = new MemoryStream();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScheduleDefinition));
            xmlSerializer.Serialize(buffer, schedule);
            buffer.Seek(0, SeekOrigin.Begin);

            XmlDocument doc = new XmlDocument();
            doc.Load(buffer);
            // patch up WhichWeek
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("rs",
                    "http://schemas.microsoft.com/sqlserver/2003/12/reporting/reportingservices");

            XmlNode node =
                doc.SelectSingleNode(
                     "/ScheduleDefinition/rs:MonthlyDOWRecurrence/rs:WhichWeek", ns
                );
            if (node != null)
            {
                switch (node.InnerXml)
                {
                    case "FirstWeek":
                        node.InnerXml = "FIRST_WEEK"; break;
                    case "SecondWeek":
                        node.InnerXml = "SECOND_WEEK"; break;
                    case "ThirdWeek":
                        node.InnerXml = "THIRD_WEEK"; break;
                    case "FourthWeek":
                        node.InnerXml = "FOURTH_WEEK"; break;
                    case "LastWeek":
                        node.InnerXml = "LAST_WEEK"; break;
                }
            }

            return doc;
        }

        /// <summary>
        /// Get the execution day
        /// </summary>
        /// <param name="scheduleDay">schedule day</param>
        /// <returns>DaysOfWeekSelector</returns>
        private DaysOfWeekSelector GetExecutionDay(int scheduleDay)
        {
            DaysOfWeekSelector days = new DaysOfWeekSelector();
            switch (scheduleDay)
            {
                case 1:
                    days.Sunday = true;
                    break;
                case 2:
                    days.Monday = true;
                    break;
                case 3:
                    days.Tuesday = true;
                    break;
                case 4:
                    days.Wednesday = true;
                    break;
                case 5:
                    days.Thursday = true;
                    break;
                case 6:
                    days.Friday = true;
                    break;
                case 7:
                    days.Saturday = true;
                    break;
                default:
                    break;
            }

            return days;
        }
    }
}

