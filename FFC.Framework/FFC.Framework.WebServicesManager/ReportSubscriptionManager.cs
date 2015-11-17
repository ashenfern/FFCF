using FFC.Framework.Common;
using FFC.Framework.Data;
using FFC.Framework.WebServicesManager.RS2010;
using FFC.Framework.WebServicesManager.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;

namespace FFC.Framework.WebServicesManager
{
    public class ReportSubscriptionManager
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

        public ReportSubscriptionManager()
        {
            ReportingService2010 rs = new ReportingService2010();
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
        public bool CreateSubscription(ReportSchedule reportSchedule, string sharedSubscriptionFolderPath)
        {
            //Getting the shared folder path from the config file
            //string shareFolderPath = ConfigurationManager.AppSettings[ReportSubscirptionSharedPath];
            string shareFolderPath = sharedSubscriptionFolderPath;

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

                rs.CreateSubscription(report, extSettings, desc,
                                      eventType, scheduleXml, parameters);

                isSuccessful = true;

                //Console.WriteLine("Report subscription successFully created");
            }

            else
            {
                //Console.WriteLine("Report subscription already exist");
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
        public bool IsSubscriptionExist(ReportSchedule reportSchedule)
        {
            List<Subscription> subscriptions = GetSubscriptions(reportSchedule);
            bool isExist = false;
            string subscriptionFileName = String.Concat(/*reportSchedule.Schedule_ID, */FileNameSplitter, reportSchedule.Report.ReportName, FileNameSplitter, TimestampParameter);
            //Check whether the subscription is exist
            isExist = subscriptions.Select(r => { r.DeliverySettings.ParameterValues.Cast<ParameterValue>().Where(d => d.Name == FileName && d.Value == subscriptionFileName); return r; }).Any();

            return isExist;
        }

        public List<Subscription> GetSubscriptions(ReportSchedule reportSchedule)
        {
            Subscription[] subscriptions = null;
            subscriptions = rs.ListSubscriptions(reportSchedule.Report.ReportPath);

            return subscriptions.ToList();
        }

        /// <summary>
        /// Update the properties of a subscription
        /// </summary>
        /// <param name="reportSchedule">report schedule parmater</param>
        public void UpdateSubscription(ReportSchedule reportSchedule)
        {
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

            string subscriptionFileName = String.Concat(/*reportSchedule.Schedule_ID,*/ FileNameSplitter, reportSchedule.Report.ReportName, FileNameSplitter, TimestampParameter);
            subscriptions = rs.ListSubscriptions(reportSchedule.Report.ReportPath);

            //Get the subscription from the filename
            var subscription = subscriptions.ToList().Select(r => { r.DeliverySettings.ParameterValues.Cast<ParameterValue>().Where(d => d.Name == FileName && d.Value == subscriptionFileName); return r; }).FirstOrDefault();

            //Get Subscription properties
            rs.GetSubscriptionProperties(subscription.SubscriptionID, out extSettings, out desc, out active, out status, out eventType, out matchData, out values);

            //Get  the schedule data
            matchData = new ReportScheduleFormatter().GetMatchData(reportSchedule);
            //Set the Report Name Descriptions
            desc = reportSchedule.Report.ReportName;

            //TODO: Below code is needed because for the password. Giving an error with out the password
            ParameterValue[] extensionParamsTemp = new ParameterValue[extSettings.ParameterValues.Count() + 1];

            extSettings.ParameterValues.CopyTo(extensionParamsTemp, 0);
            extensionParamsTemp[extSettings.ParameterValues.Count()] = new ParameterValue { Name = Password, Value = ConfigurationManager.AppSettings[ReportPassword] };

            extSettings.ParameterValues = extensionParamsTemp;

            rs.SetSubscriptionProperties(subscription.SubscriptionID, extSettings, desc, eventType, matchData, values);

            //Console.WriteLine("Report Succesfully Updated");
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

                        if (parameter.Name == "Market")
                        {
                            //param.Value = reportSchedule.Hierarchy_ID.ToString();
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
