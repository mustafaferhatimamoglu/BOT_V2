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
    internal class KDJ
    {
        public struct Result
        {
            public double[] K;
            public double[] D;
            public double[] J;
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
            var a1 = quotes2.GetStoch();

            Result result = new Result();
            result.K = new double[a1.Count()];
            result.D = new double[a1.Count()];
            result.J = new double[a1.Count()];
            int counter = 0;
            foreach (var item in a1)
            {
                try
                {
                    //result.TIME[counter] = (double)item.Date;
                    result.K[counter] = (double)item.K;
                    result.D[counter] = (double)item.D;
                    result.J[counter] = (double)item.J;
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
