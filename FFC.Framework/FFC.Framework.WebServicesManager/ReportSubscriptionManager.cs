using FFC.Framework.Common;
using FFC.Framework.Data;
using FFC.Framework.WebServicesManager.RS2010;
using FFC.Framework.WebServicesManager.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;

namespace FFC.Framework.WebServicesManager
{
    public class ReportSubscriptionManager : IReportSubscriptionManager
    {
        #region Constants
        // Report Subsription constants
        private const string TimedSubscription = "TimedSubscription";
        private const string Path = "PATH";
        private const string FileName = "FILENAME";
        private const string FileExtention = "FILEEXTN";
        private const string UserName = "USERNAME";
        private const string Password = "PASSWORD";
        private const string Render_Format = "RENDER_FORMAT";
        private const string FileWriteMode = "WRITEMODE";
        private const string SubscriptionMode = "Report Server FileShare";
        private const string TimestampParameter = "@timestamp";

        //Configuration contstants
        private const string ReportSubscirptionSharedPath = "ReportSubscirptionSharedPath";
        private const string ReportServerURL = "ReportServerURL";
        private const string ReportUsername = "ReportUsername";
        private const string ReportPassword = "ReportPassword";

        private const string FileNameSplitter = "#";

        #endregion

        ReportingService2010 rs;
        private FFCEntities db = new FFCEntities();

        public ReportSubscriptionManager()
        {
            rs = new ReportingService2010();
            rs.Url = ConfigurationManager.AppSettings[ReportServerURL];

            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

            rs.UseDefaultCredentials = true;
        }

        /// <summary>
        /// This method will create required subscription if not exist
        /// </summary>
        /// <param name="reportSchedule">Name of the Report</param>
        /// <param name="parametersDictionary">Report Parameters</param>
        /// <returns></returns>
        public bool CreateFileSubscription(ReportSchedule reportSchedule)
        {
            //Getting the shared folder path from the config file
            string shareFolderPath = ConfigurationManager.AppSettings[ReportSubscirptionSharedPath];
            //string shareFolderPath = sharedSubscriptionFolderPath;

            bool isSuccessful = false;

            //ReportingService2010 rs = new ReportingService2010();
            //rs.Url = ConfigurationManager.AppSettings[ReportServerURL];

            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //rs.UseDefaultCredentials = true;

            //Check whether the subscription already exist
            //bool isExist = IsSubscriptionExist(rs, reportSchedule);
            bool isExist = false;

            if (!isExist)
            {
                string report = reportSchedule.Report.ReportPath;
                string desc = reportSchedule.Report.ReportDescription;
                string eventType = TimedSubscription;

                //Getting the schedule data
                string scheduleXml = new ReportScheduleFormatter().GetMatchData(reportSchedule);

                //Populating the the extension parameters
                ParameterValue[] extensionParams = new ParameterValue[7]
                    {
                        new ParameterValue{Name = Path, Value = shareFolderPath},
                        new ParameterValue{Name = FileName, Value = String.Concat(/*reportSchedule.Schedule_ID,*/ FileNameSplitter ,reportSchedule.Report.ReportName,  FileNameSplitter, TimestampParameter)},
                        new ParameterValue{Name = FileExtention, Value = "TRUE"},
                        new ParameterValue{Name = UserName, Value = ConfigurationManager.AppSettings[ReportUsername]},
                        new ParameterValue{Name = Password, Value = ConfigurationManager.AppSettings[ReportPassword]},
                        new ParameterValue{Name = Render_Format, Value = RenderFormat.EXCEL.ToString()},
                        new ParameterValue{Name = FileWriteMode, Value = WriteMode.Overwrite.ToString()}
                    };

                ExtensionSettings extSettings = new ExtensionSettings();
                extSettings.ParameterValues = extensionParams;
                extSettings.Extension = SubscriptionMode;

                ParameterValue[] parameters = PopulateParamters(reportSchedule);

                var subscriptionId = rs.CreateSubscription(report, extSettings, desc,
                                      eventType, scheduleXml, parameters);

                isSuccessful = true;

                reportSchedule.ReportSubscriptionId = subscriptionId;
                reportSchedule.Report = null;

                db.ReportSchedules.Add(reportSchedule);
                db.SaveChanges();

                //Add the subscription to the Schedule table


                //Console.WriteLine("Report subscription successFully created");
            }

            else
            {
                //Console.WriteLine("Report subscription already exist");
            }

            return isSuccessful;

        }

        public bool CreateEmailSubscription(ReportSchedule reportSchedule)
        {
            string report = reportSchedule.Report.ReportPath;
            string desc = reportSchedule.Report.ReportDescription;
            string eventType = TimedSubscription;
            bool isSuccessful = false;

            //Getting the schedule data
            string scheduleXml = new ReportScheduleFormatter().GetMatchData(reportSchedule);
            //string scheduleXml = @"<ScheduleDefinition>";
            //scheduleXml += @"<StartDateTime>2003-02-24T09:00:00-08:00</StartDateTime><WeeklyRecurrence><WeeksInterval>1</WeeksInterval>";
            //scheduleXml += @"<DaysOfWeek><Monday>True</Monday></DaysOfWeek>";
            //scheduleXml += @"</WeeklyRecurrence></ScheduleDefinition>";

            ParameterValue[] extensionParams = new ParameterValue[8];

            extensionParams[0] = new ParameterValue();
            extensionParams[0].Name = "TO";
            extensionParams[0].Value = reportSchedule.EmailTo;//"dank@adventure-works.com";

            extensionParams[1] = new ParameterValue();
            extensionParams[1].Name = "ReplyTo";
            extensionParams[1].Value = "reporting@adventure-works.com";

            extensionParams[2] = new ParameterValue();
            extensionParams[2].Name = "IncludeReport";
            extensionParams[2].Value = "True";

            extensionParams[3] = new ParameterValue();
            extensionParams[3].Name = "RenderFormat";
            extensionParams[3].Value = "MHTML";

            extensionParams[4] = new ParameterValue();
            extensionParams[4].Name = "Subject";
            extensionParams[4].Value = "@ReportName was executed at @ExecutionTime";

            extensionParams[5] = new ParameterValue();
            extensionParams[5].Name = "Comment";
            extensionParams[5].Value = reportSchedule.EmailComment;//"Here is your daily sales report for Michael.";

            extensionParams[6] = new ParameterValue();
            extensionParams[6].Name = "IncludeLink";
            extensionParams[6].Value = "True";

            extensionParams[7] = new ParameterValue();
            extensionParams[7].Name = "Priority";
            extensionParams[7].Value = "NORMAL";

            ParameterValue parameter = new ParameterValue();
            parameter.Name = "BranchID";
            parameter.Value = "1";

            ParameterValue[] parameters = new ParameterValue[1];
            parameters[0] = parameter;

            string matchData = scheduleXml;
            ExtensionSettings extSettings = new ExtensionSettings();
            extSettings.ParameterValues = extensionParams;
            extSettings.Extension = "Report Server Email";

            try
            {
                var subscriptionId = rs.CreateSubscription(report, extSettings, desc, eventType, matchData, parameters);

                isSuccessful = true;

                reportSchedule.ReportSubscriptionId = subscriptionId;
                reportSchedule.Report = null;

                db.ReportSchedules.Add(reportSchedule);
                db.SaveChanges();
            }

            catch (SoapException e)
            {
                Console.WriteLine(e.Detail.InnerXml.ToString());
            }
            return isSuccessful;
        }

        /// <summary>
        /// This methods will check whether the subscriptions is all ready exist
        /// </summary>
        /// <param name="rs">Reporting service object</param>
        /// <param name="reportPath">path of the report</param>
        /// <param name="description">Description of the report</param>
        /// <returns>bool</returns>
        public bool IsSubscriptionExist(int reportId)
        {
            List<ReportSchedule> subscriptions = GetSubscriptions(reportId);
            bool isExist = false;
            //string subscriptionFileName = String.Concat(/*reportSchedule.Schedule_ID, */FileNameSplitter, reportSchedule.Report.ReportName, FileNameSplitter, TimestampParameter);
            //Check whether the subscription is exist
            //isExist = subscriptions.Select(r => { r.DeliverySettings.ParameterValues.Cast<ParameterValue>().Where(d => d.Name == FileName && d.Value == subscriptionFileName); return r; }).Any();

            return isExist;
        }

        public List<ReportSchedule> GetSubscriptions(int reportId)
        {

            List<ReportSchedule> schdeuleList = new List<ReportSchedule>();
            //string reportPath = @"/Report_Sample_Subscription/ReportStudentWithParmas";

            //Subscription[] subscriptions = null;
            //subscriptions = rs.ListSubscriptions(reportPath);

            //foreach (var subscription in subscriptions)
            //{
            //    ReportSchedule reportSchedule = new ReportSchedule();
            //    //var isExist = ;


            //}
            //int reportId = 1;

            schdeuleList = db.ReportSchedules.Where(r => r.ReportId == reportId).ToList();

            return schdeuleList;
        }

        /// <summary>
        /// Update the properties of a subscription
        /// </summary>
        /// <param name="reportSchedule">report schedule parmater</param>
        public void UpdateSubscription(ReportSchedule reportSchedule)
        {
            reportSchedule.ReportSubscriptionId = db.ReportSchedules.Where(r => r.ReportId == reportSchedule.ReportId).Select(r => r.ReportSubscriptionId).FirstOrDefault();
            reportSchedule.Report = db.Reports.Where(r => r.ReportId == reportSchedule.ReportId).FirstOrDefault();

            ReportingService2010 rs = new ReportingService2010();
            rs.Url = ConfigurationManager.AppSettings[ReportServerURL];

            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.UseDefaultCredentials = true;

            ActiveState active = null;
            ExtensionSettings extSettings = null;
            ParameterValue[] values;
            string desc = string.Empty;
            string eventType = string.Empty;
            string matchData = string.Empty;
            string status;

            Subscription[] subscriptions = null;

            //string subscriptionFileName = String.Concat(/*reportSchedule.Schedule_ID,*/ FileNameSplitter, reportSchedule.Report.ReportName, FileNameSplitter, TimestampParameter);
            //subscriptions = rs.ListSubscriptions(reportSchedule.Report.ReportPath);

            ////Get the subscription from the filename
            //var subscription = subscriptions.ToList().Select(r => { r.DeliverySettings.ParameterValues.Cast<ParameterValue>().Where(d => d.Name == FileName && d.Value == subscriptionFileName); return r; }).FirstOrDefault();

            //Get Subscription properties
            rs.GetSubscriptionProperties(reportSchedule.ReportSubscriptionId, out extSettings, out desc, out active, out status, out eventType, out matchData, out values);

            //Get  the schedule data
            matchData = new ReportScheduleFormatter().GetMatchData(reportSchedule);

            //Set the Report Name Descriptions
            desc = reportSchedule.Report.ReportName;

            //TODO: Below code is needed because for the password. Giving an error with out the password
            ParameterValue[] extensionParamsTemp = new ParameterValue[extSettings.ParameterValues.Count() + 1];

            //extSettings.ParameterValues.CopyTo(extensionParamsTemp, 0);
            //extensionParamsTemp[extSettings.ParameterValues.Count()] = new ParameterValue { Name = Password, Value = ConfigurationManager.AppSettings[ReportPassword] };

            //extSettings.ParameterValues = extensionParamsTemp;

            rs.SetSubscriptionProperties(reportSchedule.ReportSubscriptionId, extSettings, desc, eventType, matchData, values);

            db.Entry(reportSchedule).State = EntityState.Modified;
            db.SaveChanges();

            //Console.WriteLine("Report Succesfully Updated");
        }


        public void DeleteSubscription(int id)
        {
            ReportSchedule reportschedule = db.ReportSchedules.Find(id);

            rs.DeleteSubscription(reportschedule.ReportSubscriptionId);

            db.ReportSchedules.Remove(reportschedule);
            db.SaveChanges();

        }

        /// <summary>
        /// Pouplate parmeter values needed for the report subscription
        /// </summary>
        /// <param name="reportSchedule">report schedule parmate</param>
        /// <returns></returns>
        public ParameterValue[] PopulateParamters(ReportSchedule reportSchedule)
        {
            List<ParameterValue> parameterValueList = new List<ParameterValue>();

            ReportingService2010 rs = new ReportingService2010();
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //string report = "/Report_Sample_Subscription/Release_Details_Report_RUM";
            string report = reportSchedule.Report.ReportPath;
            bool forRendering = false;
            string historyID = null;
            ParameterValue[] values = null;
            DataSourceCredentials[] credentials = null;
            ItemParameter[] parameters = null;

            try
            {
                parameters = rs.GetItemParameters(report, historyID, forRendering, values, credentials);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        ParameterValue param = new ParameterValue();
                        param.Name = parameter.Name;

                        if (parameter.Name == "BranchID")
                        {
                            param.Value = "1";
                        }
                        else if (parameter.Name == "Releases")
                        {
                            //param.Value = reportSchedule.Release_ID.ToString();
                            //param.Value = "3013";
                        }
                        //else if (parameter.Name == "StoreStatus")
                        //{
                        //    //param.Value = reportSchedule.Release_ID.ToString();
                        //    param.Value = "Temp_Closed";
                        //}
                        //else if (parameter.Name == "RegionDivision")
                        //{
                        //    //param.Value = reportSchedule.Release_ID.ToString();
                        //    param.Value = "0";
                        //}
                        //else if (parameter.Name == "DeploymentDates")
                        //{
                        //    //param.Value = reportSchedule.Release_ID.ToString();
                        //    param.Value = "0";
                        //}
                        //else if (parameter.ParameterTypeName == "Boolean")
                        //{
                        //    param.Value = "False";
                        //}
                        //else if (parameter.MultiValue)
                        //{
                        //    param.Value = "1";
                        //}
                        //else if (parameter.ParameterTypeName == "String")
                        //{
                        //    param.Value = parameter.DefaultValues.ToString();
                        //}
                        //else if (parameter.ParameterTypeName == "Int")
                        //{
                        //    param.Value = "1";
                        //    param.Value = parameter.DefaultValues.ToString();

                        //}
                        else
                        {
                            //Console.WriteLine("Name: {0}", parameter.Name);
                            param.Value = parameter.DefaultValues[0].ToString();
                        }

                        parameterValueList.Add(param);

                    }
                }
            }

            catch (SoapException e)
            {
                Console.WriteLine(e.Detail.InnerXml.ToString());
            }

            return parameterValueList.ToArray();
        }
    }
}

