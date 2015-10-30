using FFC.Framework.Common;
using FFC.Framework.Data;
using RDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.WebServicesManager
{
    public class ForecastManager
    {
        FFCEntities context = new FFCEntities();

        #region SP_Calls
        public List<sp_Forecast_GetDailyAvereageProductTransactions_Result> GetDailyAvereageProductTransactions()
        {
            var result = context.sp_Forecast_GetDailyAvereageProductTransactions();
            return result.ToList();
        }

        public List<sp_Forecast_GetDailyTimeSpecificAvereageProductTransactions_Result> GetDailyTimeSpecificAvereageProductTransactions()
        {
            var result = context.sp_Forecast_GetDailyTimeSpecificAvereageProductTransactions();
            return result.ToList();
        }

        public List<sp_Forecast_GetWeeklyAverageTransactions_Result> GetWeeklyAverageTransactions()
        {
            var result = context.sp_Forecast_GetWeeklyAverageTransactions();
            return result.ToList();
        }

        public List<sp_Forecast_GetWeeklyAvereageProductTransactions_Result> GetWeeklyAvereageProductTransactions()
        {
            var result = context.sp_Forecast_GetWeeklyAvereageProductTransactions();
            return result.ToList();
        }

        public List<sp_Forecast_GetProductCountYearDayByProductId_Result> GetProductCountYearDayByProductId(int productId)
        {
            var result = context.sp_Forecast_GetProductCountYearDayByProductId(productId);
            return result.ToList();
        }

        public List<sp_Forecast_GetProductCountMonthDayByProductId_Result> GetProductCountMonthDayByProductId(int productId)
        {
            var result = context.sp_Forecast_GetProductCountMonthDayByProductId(productId);
            return result.ToList();
        }

        public List<sp_Forecast_GetProductCountDayByProductId_Result> GetProductCountDayByProductId(int productId)
        {
            var result = context.sp_Forecast_GetProductCountDayByProductId(productId);
            return result.ToList();
        } 
        #endregion

        #region R_Related
        public static ForecastResult Fcast1()
        {
            ForecastResult fcastResult = new ForecastResult();

            int productId = 1;
            Methods method = Methods.rwf; //meanf(YYMMDD,MMDD,MMWWDD,DD), rtw, rtw(with Drift), Moving AVG,ets, Arima, HoltWinters, msts
            DataPeriod dataType = DataPeriod.Daily;
            int periods = 50;

            var values = GetCorrespondingData(dataType, productId);
            //FFCEntities db = new FFCEntities();
            //var list = db.sp_Forecast_GetProductCountYearDayByProductId(productId).ToList();
            //List<double> values = list.Select(r => Double.Parse(r.Count.ToString())).ToList();
            //REngine.SetEnvironmentVariables(@"C:\Program Files\R\R-2.13.1\bin\i386");

            //SetupPath();
            //Log();

           
            REngine.SetEnvironmentVariables();

            // There are several options to initialize the engine, but by default the following suffice:
            REngine engine = REngine.GetInstance();
           
            //engine.Initialize();

            // .NET Framework array to R vector.
            //NumericVector testTs = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99, 1000 });
            NumericVector testTs = engine.CreateNumericVector(new double[] { 6, 5, 6, 5, 6, 5 });
            //NumericVector testTs = engine.CreateNumericVector(values);

            engine.SetSymbol("testTs", testTs);
            engine.Evaluate("n <- c(1,2,3)");
            //auto arima for monthly
            engine.Evaluate("tsValue <- ts(testTs, frequency=1, start=c(2010, 1, 1))");
            engine.Evaluate("library(forecast)");
            //engine.Evaluate(String.Format("Fit <- {0}(tsValue)", method)); // Fit <- Arima(tsValue)
            MethodManipulation(engine, method);
            engine.Evaluate(String.Format("fcast <- forecast(Fit, h={0})", periods));

            Plot(engine);

            //var a = engine.Evaluate("fcast <- forecast(tsValue, h=5)").AsCharacter();
            NumericVector forecasts = engine.Evaluate("fcast$mean").AsNumeric();

            fcastResult.results = forecasts.ToList();

            //foreach (var item in forecasts)
            //{
            //    Console.WriteLine(item);
            //}

            //engine.Dispose();

            return fcastResult;
        }

        public static ForecastResult fcast2()
        {
            ForecastResult forecastResult = new ForecastResult() { Method = Methods.Arima, results = new List<double>() { 1.0 } };
           // model.ForecastResult = forecastResult;
            return forecastResult;
        }

        public static void SetupPath(string Rversion = "R-3.2.2")
        {
            var oldPath = System.Environment.GetEnvironmentVariable("PATH");
            var rPath = System.Environment.Is64BitProcess ?
                                   string.Format(@"C:\Program Files\R\{0}\bin\x64", Rversion) :
                                   string.Format(@"C:\Program Files\R\{0}\bin\i386", Rversion);

            if (!Directory.Exists(rPath))
                throw new DirectoryNotFoundException(
                  string.Format(" R.dll not found in : {0}", rPath));
            var newPath = string.Format("{0}{1}{2}", rPath,
                                         System.IO.Path.PathSeparator, oldPath);
            System.Environment.SetEnvironmentVariable("PATH", newPath);
        }

        public static void Plot(REngine engine)
        {
            engine.Evaluate(@"png(filename='C:\\Users\\ashfernando\\Desktop\\Test\\Test2.png')");
            engine.Evaluate("plot(fcast)");
            engine.Evaluate("dev.off()");
        }

        public static void MethodManipulation(REngine engine, Methods method)
        {
            engine.Evaluate(String.Format("Fit <- {0}(tsValue)", method.ToString())); // Fit <- Arima(tsValue)
        }

        public static List<double> GetCorrespondingData(DataPeriod data, int productId)
        {
            FFCEntities db = new FFCEntities();
            List<double> values = new List<double>();

            if (data == DataPeriod.Daily)
            {
                var list = db.sp_Forecast_GetProductCountYearDayByProductId(productId).ToList();
                values = list.Select(r => Double.Parse(r.Count.ToString())).ToList();
            }
            else if (data == DataPeriod.Day)
            {
                var list = db.sp_Forecast_GetProductCountDayByProductId(productId).ToList();
                values = list.Select(r => Double.Parse(r.Count.ToString())).ToList();
            }

            return values;
        } 
        #endregion

    }
}

