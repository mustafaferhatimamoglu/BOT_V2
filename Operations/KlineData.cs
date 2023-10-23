using System;
using System.Collections.Generic;
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
    }
}
