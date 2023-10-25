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
    internal class OBV
    {
        public struct Result
        {
            public double[] OBV;
        };
        public static Result Calculate(DataTable gelen)
        {
            Collection<Quote> quotes2 = new Collection<Quote>();
            for (int i = 0; i < gelen.Rows.Count; i++)
            {
                quotes2.Add(new Quote
                {
                    Date = Auxiliary.BinanceTimeStampToUtcDateTime((long)gelen.Rows[i]["Kline_close_time"]),
                    Open = Convert.ToDecimal(gelen.Rows[i]["Open_price"]),
                    Close = Convert.ToDecimal(gelen.Rows[i]["Close_price"]),
                    Low = Convert.ToDecimal(gelen.Rows[i]["Low_price"]),
                    High = Convert.ToDecimal(gelen.Rows[i]["High_price"]),
                    Volume = Convert.ToDecimal(gelen.Rows[i]["Volume"]),
                });
            }
            var a1 = quotes2.GetObv();

            Result result = new Result();
            result.OBV = new double[a1.Count()];
            int counter = 0;
            foreach (var item in a1)
            {
                try
                {
                    //result.TIME[counter] = (double)item.Date;
                    result.OBV[counter] = (double)item.Obv;
                }
                catch (Exception ex)
                {

                }

                counter++;
            }
            return result;
        }
    }
}
