using Skender.Stock.Indicators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Indicators
{
    internal class RSI
    {
        public struct Result
        {
            public double[] RSI_6;
            public double[] RSI_12;
            public double[] RSI_24;
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
            IEnumerable<RsiResult> a6 = quotes2.GetRsi(lookbackPeriods: 6);
            var a12 = quotes2.GetRsi(lookbackPeriods: 12);
            var a24 = quotes2.GetRsi(lookbackPeriods: 24);

            Result result = new Result();
            result.RSI_6 = new double[a6.Count()];
            result.RSI_12 = new double[a12.Count()];
            result.RSI_24 = new double[a24.Count()];
            for (int i = 0; i < a6.Count(); i++)
            {
                try
                {
                    result.RSI_6[i] = Convert.ToDouble( a6.ElementAt(i).Rsi);
                    result.RSI_12[i] = (double)a12.ElementAt(i).Rsi;
                    result.RSI_24[i] = (double)a24.ElementAt(i).Rsi;
                }
                catch (Exception ex)
                {
                    Auxiliary.insideCatch(ex);
                }
            }
            return result;
        }
    }
}
