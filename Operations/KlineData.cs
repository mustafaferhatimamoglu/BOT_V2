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
                        Taker_buy_quote_asset_volume = Convert.ToSingle(reader["Taker_buy_quote_asset_volume"])
                    };
                    klineDataList.Add(stockData);
                }
                reader.Close();
            }
            return klineDataList;
        }
    }
}
