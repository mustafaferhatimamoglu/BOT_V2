using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Operations
{
    internal class KlineData
    {
        public long Kline_open_time { get; set; }
        public double Open_price { get; set; }
        public double High_price { get; set; }
        public double Low_price { get; set; }
        public double Close_price { get; set; }
        public long Kline_close_time { get; set; }
        public double Volume { get; set; }
        public double Quote_asset_volume { get; set; }
        public double Number_of_trades { get; set; }
        public double Taker_buy_base_asset_volume { get; set; }
        public double Taker_buy_quote_asset_volume { get; set; }

        public long TIME { get; set; }
        public double RSI_6 { get; set; }
        public double RSI_12 { get; set; }
        public double RSI_24 { get; set; }
        public double KDJ_K { get; set; }
        public double KDJ_D { get; set; }
        public double KDJ_J { get; set; }
        public double OBV_6 { get; set; }
        public double OBV_12 { get; set; }
        public double OBV_24 { get; set; }




        public static List<KlineData> GetKlineData_All(string commandString)
        {
            List<KlineData> klineDataList = new List<KlineData>();

            using (SqlConnection connect = new SqlConnection(Database.SQLCon + ";Connect Timeout=60;Persist Security Info=True;MultipleActiveResultSets=true;"))
            {
                connect.Open();

                SqlCommand command = new SqlCommand(commandString, connect);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    KlineData stockData = new KlineData
                    {
                        Kline_open_time = (long)Convert.ToDouble(reader["Kline_open_time"]),
                        Open_price = Convert.ToSingle(reader["Open_price"]),
                        High_price = Convert.ToSingle(reader["High_price"]),
                        Low_price = Convert.ToSingle(reader["Low_price"]),
                        Close_price = Convert.ToSingle(reader["Close_price"]),
                        Volume = Convert.ToSingle(reader["Volume"]),
                        Kline_close_time = (long)Convert.ToDouble(reader["Kline_close_time"]),
                        Quote_asset_volume = Convert.ToSingle(reader["Quote_asset_volume"]),
                        Number_of_trades = Convert.ToSingle(reader["Number_of_trades"]),
                        Taker_buy_base_asset_volume = Convert.ToSingle(reader["Taker_buy_base_asset_volume"]),
                        Taker_buy_quote_asset_volume = Convert.ToSingle(reader["Taker_buy_quote_asset_volume"]),
                        RSI_6 = Convert.ToSingle(reader["RSI_6"]),
                        RSI_12 = Convert.ToSingle(reader["RSI_12"]),
                        RSI_24 = Convert.ToSingle(reader["RSI_24"]),
                    };
                    klineDataList.Add(stockData);
                }
                reader.Close();
            }
            return klineDataList;
        }
        public static List<KlineData> GetKlineData(string commandString)
        {
            List<KlineData> klineDataList = new List<KlineData>();

            using (SqlConnection connect = new SqlConnection(Database.SQLCon + ";Connect Timeout=60;Persist Security Info=True;MultipleActiveResultSets=true;"))
            {
                connect.Open();

                SqlCommand command = new SqlCommand(commandString, connect);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    KlineData stockData = new KlineData
                    {
                        Kline_open_time = (long)Convert.ToDouble(reader["Kline_open_time"]),
                        Open_price = Convert.ToSingle(reader["Open_price"]),
                        High_price = Convert.ToSingle(reader["High_price"]),
                        Low_price = Convert.ToSingle(reader["Low_price"]),
                        Close_price = Convert.ToSingle(reader["Close_price"]),
                        Volume = Convert.ToSingle(reader["Volume"]),
                        Kline_close_time = (long)Convert.ToDouble(reader["Kline_close_time"]),
                        Quote_asset_volume = Convert.ToSingle(reader["Quote_asset_volume"]),
                        Number_of_trades = Convert.ToSingle(reader["Number_of_trades"]),
                        Taker_buy_base_asset_volume = Convert.ToSingle(reader["Taker_buy_base_asset_volume"]),
                        Taker_buy_quote_asset_volume = Convert.ToSingle(reader["Taker_buy_quote_asset_volume"]),
                        KDJ_K = Convert.ToSingle(reader["K"]),
                        KDJ_D = Convert.ToSingle(reader["D"]),
                        KDJ_J = Convert.ToSingle(reader["J"]),
                    };
                    klineDataList.Add(stockData);
                }
                reader.Close();
            }
            return klineDataList;
        }
        public static List<KlineData> GetIndicatorData_RSI(string commandString)
        {
            List<KlineData> klineDataList = new List<KlineData>();

            using (SqlConnection connect = new SqlConnection(Database.SQLCon + ";Connect Timeout=60;Persist Security Info=True;MultipleActiveResultSets=true;"))
            {
                connect.Open();

                SqlCommand command = new SqlCommand(commandString, connect);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    KlineData stockData = new KlineData
                    {
                        TIME = (long)Convert.ToDouble(reader["Time"]),
                        RSI_6 = Convert.ToSingle(reader["RSI_6"]),
                        RSI_12 = Convert.ToSingle(reader["RSI_12"]),
                        RSI_24 = Convert.ToSingle(reader["RSI_24"]),
                    };
                    klineDataList.Add(stockData);
                }
                reader.Close();
            }
            return klineDataList;
        }
        public static List<KlineData> GetIndicatorData_KDJ(string commandString)
        {
            List<KlineData> klineDataList = new List<KlineData>();

            using (SqlConnection connect = new SqlConnection(Database.SQLCon + ";Connect Timeout=60;Persist Security Info=True;MultipleActiveResultSets=true;"))
            {
                connect.Open();

                SqlCommand command = new SqlCommand(commandString, connect);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    KlineData stockData = new KlineData
                    {
                        TIME = (long)Convert.ToDouble(reader["Time"]),
                        KDJ_K = Convert.ToSingle(reader["K"]),
                        KDJ_D = Convert.ToSingle(reader["D"]),
                        KDJ_J = Convert.ToSingle(reader["J"]),
                    };
                    klineDataList.Add(stockData);
                }
                reader.Close();
            }
            return klineDataList;
        }
        public static List<KlineData> GetIndicatorData_OBV(string commandString)
        {
            List<KlineData> klineDataList = new List<KlineData>();

            using (SqlConnection connect = new SqlConnection(Database.SQLCon + ";Connect Timeout=60;Persist Security Info=True;MultipleActiveResultSets=true;"))
            {
                connect.Open();

                SqlCommand command = new SqlCommand(commandString, connect);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    KlineData stockData = new KlineData
                    {
                        TIME = (long)Convert.ToDouble(reader["Time"]),
                        OBV_6 = Convert.ToSingle(reader["OBV_6"]),
                        OBV_12 = Convert.ToSingle(reader["OBV_12"]),
                        OBV_24 = Convert.ToSingle(reader["OBV_24"]),
                    };
                    klineDataList.Add(stockData);
                }
                reader.Close();
            }
            return klineDataList;
        }
    }
}
