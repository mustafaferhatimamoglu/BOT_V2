using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Binance.Common
{
    //
    // Summary:
    //     Binance HMAC signature signing.
    public class BinanceHmac : IBinanceSignatureService
    {
        private byte[] secret;

        public BinanceHmac(string secret)
        {
            this.secret = ((secret != null) ? Encoding.UTF8.GetBytes(secret) : null);
        }

        public string Sign(string payload)
        {
            using HMACSHA256 hMACSHA = new HMACSHA256(secret);
            byte[] bytes = Encoding.UTF8.GetBytes(payload);
            return BitConverter.ToString(hMACSHA.ComputeHash(bytes)).Replace("-", string.Empty).ToLower();
        }
    }
}