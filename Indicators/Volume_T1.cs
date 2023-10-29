using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Indicators
{
    internal class Volume_T1
    {        public struct Result
        {
            public double[] OBV_6;
            public double[] OBV_12;
            public double[] OBV_24;
        };
        //public static Result Calculate(DataTable gelen)
        //{
        //    Collection<Quote> quotes2 = new Collection<Quote>();
        //    for (int i = 0; i < gelen.Rows.Count; i++)
        //    {
        //        quotes2.Add(new Quote
        //        {
        //            Date = Auxiliary.BinanceTimeStampToUtcDateTime((long)gelen.Rows[i]["Kline_close_time"]),
        //            Open = Convert.ToDecimal(gelen.Rows[i]["Open_price"]),
        //            Close = Convert.ToDecimal(gelen.Rows[i]["Close_price"]),
        //            Low = Convert.ToDecimal(gelen.Rows[i]["Low_price"]),
        //            High = Convert.ToDecimal(gelen.Rows[i]["High_price"]),
        //            Volume = Convert.ToDecimal(gelen.Rows[i]["Volume"]),
        //        });
        //    }
        //    var a6 = quotes2.GetVolatilityStop();
        //    var a12 = quotes2.GetAdl();
        //    var a24 = quotes2.GetAdx();

        //    Result result = new Result();
        //    result.OBV_6 = new double[a6.Count()];
        //    result.OBV_12 = new double[a6.Count()];
        //    result.OBV_24 = new double[a6.Count()];
        //    int counter = 0;
        //    //foreach (var item in a1)
        //    //{
        //    //    try
        //    //    {
        //    //        //result.TIME[counter] = (double)item.Date;
        //    //        result.OBV[counter] = (double)item.Obv;
        //    //    }
        //    //    catch (Exception ex)
        //    //    {

        //    //    }

        //    //    counter++;
        //    //}
        //    for (int i = 0; i < a6.Count(); i++)
        //    {
        //        try
        //        {
        //            result.OBV_6[i] = (double)a6.ElementAt(i).ObvSma;
        //            result.OBV_12[i] = (double)a12.ElementAt(i).ObvSma;
        //            result.OBV_24[i] = (double)a24.ElementAt(i).ObvSma;
        //        }
        //        catch (Exception ex)
        //        {
        //            Auxiliary.insideCatch(ex);
        //        }
        //    }
        //    return result;
        //}
    }
}
