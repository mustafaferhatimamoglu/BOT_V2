using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Indicators
{
    internal class Auxiliary
    {
        public static DateTime BinanceTimeStampToUtcDateTime(double binanceTimeStamp)
        {
            // Binance timestamp is milliseconds past epoch
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            //epoch = epoch.AddHours(3);
            return epoch.AddMilliseconds(binanceTimeStamp);
        }
        public static DateTime BTS_DateTime(double binanceTimeStamp)
        {
            // Binance timestamp is milliseconds past epoch
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            epoch = epoch.AddHours(3);
            return epoch.AddMilliseconds(binanceTimeStamp);
        }
    }
}
