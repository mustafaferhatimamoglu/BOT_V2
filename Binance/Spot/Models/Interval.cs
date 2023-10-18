using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Binance.Spot.Models
{
    public struct Interval
    {
        public static Interval ONE_SECOND => new Interval("1s");

        public static Interval ONE_MINUTE => new Interval("1m");

        public static Interval THREE_MINUTE => new Interval("3m");

        public static Interval FIVE_MINUTE => new Interval("5m");

        public static Interval FIFTEEN_MINUTE => new Interval("15m");

        public static Interval THIRTY_MINUTE => new Interval("30m");

        public static Interval ONE_HOUR => new Interval("1h");

        public static Interval TWO_HOUR => new Interval("2h");

        public static Interval FOUR_HOUR => new Interval("4h");

        public static Interval SIX_HOUR => new Interval("6h");

        public static Interval EIGTH_HOUR => new Interval("8h");

        public static Interval TWELVE_HOUR => new Interval("12h");

        public static Interval ONE_DAY => new Interval("1d");

        public static Interval THREE_DAY => new Interval("3d");

        public static Interval ONE_WEEK => new Interval("1w");

        public static Interval ONE_MONTH => new Interval("1M");

        public string Value { get; private set; }

        private Interval(string value)
        {
            Value = value;
        }

        public static implicit operator string(Interval enm)
        {
            return enm.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}