using BOT_V2.Binance.Spot;
using BOT_V2.Binance.Spot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Operations
{
    internal class GetDataFromBinance
    {
        static int Time_Second = 1000;
        static int Time_Minute = 60 * 1000;
        static int Time_15_Minute = 15 * 60 * 1000;
        static int Time_1_Hour = 60 * 60 * 1000;
        static int Time_2_Hour = 2 * 60 * 60 * 1000;
        static int Time_4_Hour = 4 * 60 * 60 * 1000;
        static int Time_6_Hour = 6 * 60 * 60 * 1000;
        static int Time_8_Hour = 8 * 60 * 60 * 1000;
        static int Time_12_Hour = 12 * 60 * 60 * 1000;
        static int Time_Day = 24 * 60 * 60 * 1000;
        static int Time_Week = 7 * 24 * 60 * 60 * 1000;

        public static async void Create_Coin(string COIN)
        {
            //var string_serverTime = Convert.ToInt64(await futureMarket.Future_CheckServerTime());
            var string_serverTime = await futureMarket.Future_CheckServerTime();
            long serverTime = Convert.ToInt64(string_serverTime.Split(":")[1].Split("}")[0]);
            var klineData = await futureMarket.Future_KlineCandlestickData(
                COIN,
                Interval.ONE_MONTH,
                null,//serverTime,
                null,
                999);
            var KlineData_Array = Data2Array(klineData);
            long kline_StartTime = Convert.ToInt64(KlineData_Array[0].Split(",")[0]);

            Create_Update_SQL_private(COIN, Interval.ONE_MINUTE);
            Create_Update_SQL_private(COIN, Interval.FIVE_MINUTE);
            Create_Update_SQL_private(COIN, Interval.FIFTEEN_MINUTE);
            Create_Update_SQL_private(COIN, Interval.THIRTY_MINUTE);
            Create_Update_SQL_private(COIN, Interval.ONE_HOUR);
            Create_Update_SQL_private(COIN, Interval.TWO_HOUR);
            Create_Update_SQL_private(COIN, Interval.FOUR_HOUR);
            Create_Update_SQL_private(COIN, Interval.SIX_HOUR);
            Create_Update_SQL_private(COIN, Interval.EIGTH_HOUR);
            Create_Update_SQL_private(COIN, Interval.TWELVE_HOUR);
            Create_Update_SQL_private(COIN, Interval.ONE_DAY);
            Create_Update_SQL_private(COIN, Interval.THREE_DAY);
            Create_Update_SQL_private(COIN, Interval.ONE_WEEK);
        }

        static Market market = new Market();
        //Market futureMarket = new Market("https://testnet.binancefuture.com");
        static Market futureMarket = new Market("https://fapi.binance.com");
        private async static void Create_Update_SQL_private(string COIN, Interval interval)
        {
            #region Setup
            string stringInterval = interval.ToString();
            int splitIndex = stringInterval.Length;
            int interval_multiplier = Convert.ToInt32(stringInterval.Substring(0, splitIndex - 1));
            int interval_type = 0;
            if (stringInterval.Contains("m")) { interval_type = Time_Minute; }
            if (stringInterval.Contains("h")) { interval_type = Time_1_Hour; }
            if (stringInterval.Contains("d")) { interval_type = Time_Day; }
            if (stringInterval.Contains("w")) { interval_type = Time_Week; }

            if (interval_type == 0)
            {
                MessageBox.Show("Error on Create_Update_SQL_private_" + COIN + "_" + interval.ToString());
                throw new Exception();
            }
            long intervalInUse = interval_type * interval_multiplier;
            #endregion


        }

        static String[] Data2Array(string R)
        {
            return R.Replace("],[", "\n").
                Replace("[[", "").
                Replace("]]", "").
                Replace("\"", "").
                Split('\n');
        }
    }
}
