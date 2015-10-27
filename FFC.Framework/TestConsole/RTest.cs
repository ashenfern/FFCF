using FFC.Framework.Common;
using RDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    public class RTest
    {
        public static void HelloR()
        {
            REngine.SetEnvironmentVariables();
            // There are several options to initialize the engine, but by default the following suffice:
            REngine engine = REngine.GetInstance();

            // .NET Framework array to R vector.
            NumericVector group1 = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99 });
            engine.SetSymbol("group1", group1);
            // Direct parsing from R script.
            NumericVector group2 = engine.Evaluate("group2 <- c(29.89, 29.93, 29.72, 29.98, 30.02, 29.98)").AsNumeric();

            // Test difference of mean and get the P-value.
            GenericVector testResult = engine.Evaluate("t.test(group1, group2)").AsList();
            double p = testResult["p.value"].AsNumeric().First();

            Console.WriteLine("Group1: [{0}]", string.Join(", ", group1));
            Console.WriteLine("Group2: [{0}]", string.Join(", ", group2));
            Console.WriteLine("P-value = {0:0.000}", p);

            // you should always dispose of the REngine properly.
            // After disposing of the engine, you cannot reinitialize nor reuse it
            engine.Dispose();
        }

        public static void TestMethod()
        {
            REngine.SetEnvironmentVariables();
            // There are several options to initialize the engine, but by default the following suffice:
            REngine engine = REngine.GetInstance();

            // .NET Framework array to R vector.
            NumericVector testTs = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99 });

            engine.SetSymbol("testTs", testTs);

            // Direct parsing from R script.
            //NumericVector group2 = engine.Evaluate("group2 <- c(29.89, 29.93, 29.72, 29.98, 30.02, 29.98)").AsNumeric();

            //auto arima for monthly
            engine.Evaluate("tsValue <- ts(testTs, frequency=1, start=c(2010, 1, 1))");
            engine.Evaluate("library(forecast)");
            engine.Evaluate("arimaFit <- auto.arima(tsValue)");
            engine.Evaluate("fcast <- forecast(tsValue, h=5)");

            var a = engine.Evaluate("fcast <- forecast(tsValue, h=5)").AsCharacter();
            NumericVector forecasts = engine.Evaluate("fcast$mean").AsNumeric();

            foreach (var item in forecasts)
            {
                Console.WriteLine(item);
            }

            engine.Dispose();
        }

        public static void TestMethod2()
        {
            FFCEntities db = new FFCEntities();
            var list = db.sp_Forecast_GetProductCountYearDayByProductId(1).ToList();

            List<double> values = list.Select(r => Double.Parse(r.Count.ToString())).ToList();

            REngine.SetEnvironmentVariables();
            // There are several options to initialize the engine, but by default the following suffice:
            REngine engine = REngine.GetInstance();

            // .NET Framework array to R vector.
            NumericVector testTs = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99, 1000 });
            //NumericVector testTs = engine.CreateNumericVector(new double[] { 1,2,3,4,5,6});
            //NumericVector testTs = engine.CreateNumericVector(values);

            //DataFrame datafrate = engine.CreateDataFrame(new List<string> { },);

            engine.SetSymbol("testTs", testTs);

            // Direct parsing from R script.
            //NumericVector group2 = engine.Evaluate("group2 <- c(29.89, 29.93, 29.72, 29.98, 30.02, 29.98)").AsNumeric();

            //auto arima for monthly
            engine.Evaluate("tsValue <- ts(testTs, frequency=1, start=c(2010, 1, 1))");
            engine.Evaluate("library(forecast)");
            engine.Evaluate("arimaFit <- Arima(tsValue)");
            engine.Evaluate("fcast <- forecast(tsValue, h=5)");

            var a = engine.Evaluate("fcast <- forecast(tsValue, h=5)").AsCharacter();
            NumericVector forecasts = engine.Evaluate("fcast$mean").AsNumeric();

            foreach (var item in forecasts)
            {
                Console.WriteLine(item);
            }

            engine.Dispose();
        }

        public static void Test()
        {
            int productId = 1;
            Methods method = Methods.rwf; //meanf(YYMMDD,MMDD,MMWWDD,DD), rtw, rtw(with Drift), Moving AVG,ets, Arima, HoltWinters, msts
            DataPeriod dataType = DataPeriod.Daily;
            int periods = 50;

            var values = GetCorrespondingData(dataType, productId);
            //FFCEntities db = new FFCEntities();
            //var list = db.sp_Forecast_GetProductCountYearDayByProductId(productId).ToList();
            //List<double> values = list.Select(r => Double.Parse(r.Count.ToString())).ToList();

            REngine.SetEnvironmentVariables();

            // There are several options to initialize the engine, but by default the following suffice:
            REngine engine = REngine.GetInstance();

            // .NET Framework array to R vector.
            //NumericVector testTs = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99, 1000 });
            //NumericVector testTs = engine.CreateNumericVector(new double[] { 6, 5, 6, 5, 6, 5 });
            NumericVector testTs = engine.CreateNumericVector(values);

            engine.SetSymbol("testTs", testTs);

            //auto arima for monthly
            engine.Evaluate("tsValue <- ts(testTs, frequency=1, start=c(2010, 1, 1))");
            engine.Evaluate("library(forecast)");
            //engine.Evaluate(String.Format("Fit <- {0}(tsValue)", method)); // Fit <- Arima(tsValue)
            MethodManipulation(engine, method);
            engine.Evaluate(String.Format("fcast <- forecast(Fit, h={0})", periods));

            Plot(engine);

            //var a = engine.Evaluate("fcast <- forecast(tsValue, h=5)").AsCharacter();
            NumericVector forecasts = engine.Evaluate("fcast$mean").AsNumeric();

            foreach (var item in forecasts)
            {
                Console.WriteLine(item);
            }

            engine.Dispose();
        }

        public static void Plot(REngine engine)
        {
            engine.Evaluate(@"png(filename='C:\\Users\\ashfernando\\Documents\\RFiles\\Images\\Test2.png')");
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

    }
}

