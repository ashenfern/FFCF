using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.Common
{
    public enum Methods
    {
        meanf, //Mean
        rwf,   //Naive
        MovingAverage, //Moving Average
        ets,
        HoltWinters,
        Arima
    }
    public enum DataPeriod
    {
        Hourly,
        Daily,          //Every day
        Day,            //All given day (sunday data)
        MonthDay,       //All Month day (April sunday)
        MonthWeedDay,   //All Month Weed day (April 1st Week monday)
        MonthDate,      //All month date data (April 8th)
    }

    public enum SchedulePeriod
    {
        Daily = 1,
        Monthly,
        Weekdays,
        Weekly
    }

    public enum RenderFormat
    {
        EXCEL,
        XML,
        CSV,
        IMAGE,
        PDF,
        // HTML4.0, 
        // HTML3.2, 
        MHTML
    }

    public enum WriteMode
    {
        // It should be None, Overwrite, or AutoIncrement.
        ////None: The FileName remains the same & Reporting Services doesn't overwrite the existing file should it already exists.
        ////Overwrite: When you want to existing file to be replaces.
        ////AutoIncrement: Use AutoIncrement to append an incrementing number to the filename - this leaves all pervious files on the drive.
        None,
        Overwrite,
        AutoIncrement
    }

    public enum DeliveryTypes
    {
        Email = 1,
        File
    }
}
