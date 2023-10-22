using BOT_V2.Binance.Spot;
using BOT_V2.Binance.Spot.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Operations
{
    internal class GetDataFromBinance
    {
        static Market market = new Market();
        //Market futureMarket = new Market("https://testnet.binancefuture.com");
        static Market futureMarket = new Market("https://fapi.binance.com");

        private int Time_Second = 1000;
        private int Time_Minute = 60 * 1000;
        private int Time_15_Minute = 15 * 60 * 1000;
        private int Time_1_Hour = 60 * 60 * 1000;
        private int Time_2_Hour = 2 * 60 * 60 * 1000;
        private int Time_4_Hour = 4 * 60 * 60 * 1000;
        private int Time_6_Hour = 6 * 60 * 60 * 1000;
        private int Time_8_Hour = 8 * 60 * 60 * 1000;
        private int Time_12_Hour = 12 * 60 * 60 * 1000;
        private int Time_Day = 24 * 60 * 60 * 1000;
        private int Time_Week = 7 * 24 * 60 * 60 * 1000;

        long serverTime;
        long coin_StartTime;
        public async void Create_Coin(string COIN)
        {
            var string_serverTime = await futureMarket.Future_CheckServerTime();
            serverTime = Convert.ToInt64(string_serverTime.Split(":")[1].Split("}")[0]);

            var klineData = await futureMarket.Future_KlineCandlestickData(
                COIN,
                Interval.ONE_MONTH,
                null,//serverTime,
                null,
                999);
            var KlineData_Array = Data2Array(klineData);
            coin_StartTime = Convert.ToInt64(KlineData_Array[0].Split(",")[0]);

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

        private async void Create_Update_SQL_private(string COIN, Interval interval)
        {
            #region Setup
            string identifier = COIN + "_" + interval.ToString();
            string stringInterval = interval.ToString();
            int splitIndex = stringInterval.Length;
            int interval_multiplier = Convert.ToInt32(stringInterval.Substring(0, splitIndex - 1));
            int interval_type = 0;
            if (stringInterval.Contains("m")) { interval_type = Time_Minute; }
            if (stringInterval.Contains("h")) { interval_type = Time_1_Hour; }
            if (stringInterval.Contains("d")) { interval_type = Time_Day; }
            if (stringInterval.Contains("w")) { interval_type = Time_Week; }

            if (interval_type == 0 || coin_StartTime == 0)
            {
                MessageBox.Show("Error on Create_Update_SQL_private_" + COIN + "_" + interval.ToString() + "_" + coin_StartTime);
                throw new Exception();
            }
            long intervalInUse = interval_type * interval_multiplier;
            #endregion
            CreateTable(identifier);

            long time_InUse;
            do
            {
                time_InUse = GetLastTime_PlusOne(identifier);
                var klineData = await futureMarket.Future_KlineCandlestickData(
                        COIN,
                        interval,
                        time_InUse,
                        null,
                        999);
                if (klineData == "[]")
                {
                    MessageBox.Show("Veri Boş geldi bi bok var");
                }
                var KlineData_Array = Data2Array(klineData);
                foreach (var line in KlineData_Array)
                {
                    var column = line.Split(",");
                    string sqlQuery = "INSERT INTO [dbo]." + identifier + "\r\n" +
                        "           ([Kline_open_time]\r\n" +
                        "           ,[Open_price]\r\n" +
                        "           ,[High_price]\r\n" +
                        "           ,[Low_price]\r\n" +
                        "           ,[Close_price]\r\n" +
                        "           ,[Volume]\r\n" +
                        "           ,[Kline_close_time]\r\n" +
                        "           ,[Quote_asset_volume]\r\n" +
                        "           ,[Number_of_trades]\r\n" +
                        "           ,[Taker_buy_base_asset_volume]\r\n" +
                        "           ,[Taker_buy_quote_asset_volume])\r\n" +
                        "     VALUES\r\n" +
                        "(" + column[0] + "\r\n" +
                        "," + column[1] + "\r\n" +
                        "," + column[2] + "\r\n" +
                        "," + column[3] + "\r\n" +
                        "," + column[4] + "\r\n" +
                        "," + column[5] + "\r\n" +
                        "," + column[6] + "\r\n" +
                        "," + column[7] + "\r\n" +
                        "," + column[8] + "\r\n" +
                        "," + column[9] + "\r\n" +
                        "," + column[10] + ")\r\n";
                    Database.SQL_query(sqlQuery);
                }
            } while (time_InUse < serverTime);

            int counter = 0;
            string strategyName = "_";
            string sqlQueryCreate;

            #region GetKlineData_ALL
            DataTable sqlKlineData_ALL = Database.SQL_query("select * from " + identifier);
            #endregion

            #region RSI
            strategyName = "RSI";
            sqlQueryCreate = "SET ANSI_NULLS ON\r\n" +
                    "\r\n\r\n" + "SET QUOTED_IDENTIFIER ON\r\n" + "\r\n\r\n" +
                    "CREATE TABLE " + identifier + "_" + strategyName + "(\r\n\t" +
                    "[Time] [bigint] NOT NULL,\r\n\t" +
                    "[RSI_6] [decimal](25, 10) NOT NULL,\r\n\t" +
                    "[RSI_12] [decimal](25, 10) NOT NULL,\r\n\t" +
                    "[RSI_24] [decimal](25, 10) NOT NULL,\r\n\t" +
                    "CONSTRAINT [PK_" + identifier + "_" + strategyName + "] PRIMARY KEY CLUSTERED \r\n(\r\n\t" +
                    "[Time] ASC\r\n" +
                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n" +
                    ") ON [PRIMARY]\r\n";
            Database.SQL_query(sqlQueryCreate);

            counter = 0;
            try
            {
                Database.SQL_query("delete  from " + identifier + "_" + strategyName + " where [Time] IN (\r\n" +
                    "select TOP 1 [Time] from " + identifier + "_" + strategyName + " \r\n" +
                    "order by [Time] desc )");
                var a1 = Database.SQL_query("select COUNT(*) from " + identifier + "_" + strategyName);
                counter = System.Convert.ToInt32(a1.Rows[0][0]); // a1.Rows[0][0]
            }
            catch (Exception) { }

            var a2 = Indicators.RSI.Calculate(sqlKlineData_ALL);
            for (; counter < sqlKlineData_ALL.Rows.Count; counter++)
            {
                var sqlQuery3 = "" +
                    "INSERT INTO " + identifier + "_" + strategyName + " ([Time],[RSI_6],[RSI_12],[RSI_24])VALUES\r\n" +
                    "('" + sqlKlineData_ALL.Rows[counter]["Kline_close_time"] + "','"
                    + a2.RSI_6[counter].ToString("0." + new string('#', 339)) + "','"
                    + a2.RSI_12[counter].ToString("0." + new string('#', 339)) + "','"
                    + a2.RSI_24[counter].ToString("0." + new string('#', 339)) + "')";
                Database.SQL_query(sqlQuery3);
            }
            #endregion
            #region KDJ
            strategyName = "KDJ";
            sqlQueryCreate = "SET ANSI_NULLS ON\r\n" +
                "\r\n\r\n" +
                "SET QUOTED_IDENTIFIER ON\r\n" +
                "\r\n\r\n" +
                "CREATE TABLE " + identifier + "_" + strategyName + "(\r\n\t" +
                "[Time] [bigint] NOT NULL,\r\n\t" +
                "[K] [decimal](25, 10) NOT NULL,\r\n\t" +
                "[D] [decimal](25, 10) NOT NULL,\r\n\t" +
                "[J] [decimal](25, 10) NOT NULL,\r\n\t" +
                "CONSTRAINT [PK_" + identifier + "_" + strategyName + "] PRIMARY KEY CLUSTERED \r\n(\r\n\t" +
                "[Time] ASC\r\n" +
                ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n" +
                ") ON [PRIMARY]\r\n";
            Database.SQL_query(sqlQueryCreate);

            counter = 0;
            try
            {
                Database.SQL_query("delete  from " + identifier + "_" + strategyName + " where [Time] IN (\r\n" +
                    "select TOP 1 [Time] from " + identifier + "_" + strategyName + " \r\n" +
                    "order by [Time] desc )");
                var a1 = Database.SQL_query("select COUNT(*) from " + identifier + "_" + strategyName);
                counter = System.Convert.ToInt32(a1.Rows[0][0]); // a1.Rows[0][0]
            }
            catch (Exception) { }

            var a3 = Indicators.KDJ.Calculate(sqlKlineData_ALL);
            for (; counter < sqlKlineData_ALL.Rows.Count; counter++)
            {
                var sqlQuery3 = "" +
                    "INSERT INTO " + identifier + "_" + strategyName + " ([Time],[K],[D],[J])VALUES\r\n" +
                    "('" + sqlKlineData_ALL.Rows[counter]["Kline_close_time"] + "','" + a3.K[counter].ToString("0." + new string('#', 339)) + "','" + a3.D[counter].ToString("0." + new string('#', 339)) + "','" + a3.J[counter].ToString("0." + new string('#', 339)) + "')";
                Database.SQL_query(sqlQuery3);
            }
            #endregion

        }
        void CreateTable(string identifier)
        {
            string sql_query = "SET ANSI_NULLS ON\r\n" +
                    "-- GO " +
                    "\r\n\r\nSET QUOTED_IDENTIFIER ON\r\n" +
                    "-- GO " +
                    "\r\n\r\n" +
                    "CREATE TABLE " + identifier + "(\r\n" +
                    "\t[Kline_open_time] [bigint] NOT NULL,\r\n" +
                    "\t[Open_price] [decimal](30, 15) NOT NULL,\r\n" +
                    "\t[High_price] [decimal](30, 15) NOT NULL,\r\n" +
                    "\t[Low_price] [decimal](30, 15) NOT NULL,\r\n" +
                    "\t[Close_price] [decimal](30, 15) NOT NULL,\r\n" +
                    "\t[Volume] [decimal](30, 15) NOT NULL,\r\n" +
                    "\t[Kline_close_time] [bigint] NOT NULL,\r\n" +
                    "\t[Quote_asset_volume] [decimal](30, 15) NOT NULL,\r\n" +
                    "\t[Number_of_trades] [int] NOT NULL,\r\n" +
                    "\t[Taker_buy_base_asset_volume] [decimal](30, 15) NOT NULL,\r\n" +
                    "\t[Taker_buy_quote_asset_volume] [decimal](30, 15) NOT NULL" +
                    ",\r\n" +
                    " CONSTRAINT [PK_" + identifier + "] PRIMARY KEY CLUSTERED \r\n" +
                    "(\r\n" +
                    "\t[Kline_open_time] ASC\r\n" +
                    ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n" +
                    ") ON [PRIMARY]\r\n" +
                    "-- GO ";
            Database.SQL_query(sql_query);
            Database.SQL_query("delete  from " + identifier + " where Kline_open_time IN (\r\n" +
                    "select TOP 1 Kline_open_time from " + identifier + " \r\n" +
                    "order by Kline_open_time desc )");
        }

        long GetLastTime_PlusOne(string identifier)
        {
            try
            {
                //Database.SQL_query("delete  from " + identifier + " where Kline_open_time IN (\r\n" +
                //    "select TOP 1 Kline_open_time from " + identifier + " \r\n" +
                //    "order by Kline_open_time desc )");
                return (long)Database.SQL_query("\r\nselect top 1 Kline_close_time from " + identifier + " order by Kline_open_time desc").Rows[0][0] + 1;
            }
            catch (Exception ex)
            {
                return coin_StartTime;
            }
        }
        String[] Data2Array(string R)
        {
            return R.Replace("],[", "\n").
                Replace("[[", "").
                Replace("]]", "").
                Replace("\"", "").
                Split('\n');
        }
    }
}
