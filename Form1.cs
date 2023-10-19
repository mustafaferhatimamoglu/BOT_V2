using BOT_V2.Binance.Spot;
using BOT_V2.Binance.Spot.Models;
using BOT_V2.Operations;

namespace BOT_V2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Market market = new Market();
        Market futureMarket = new Market("https://testnet.binancefuture.com");
        string COIN = "BTCUSDT";
        Interval interval = Interval.ONE_MONTH; 
        long Time_2022 = 1640995200000;
        private void button1_Click(object sender, EventArgs e)
        {
            //var RawData = await market.KlineCandlestickData(
            //COIN,
            //            interval,
            //            Time_2022,
            //            null,
            //            1000);
            Test();
        }

        private async void Test()
        {
            var RawData = await market.KlineCandlestickData(
            COIN,
                        interval,
                        Time_2022,
                        null,
                        1000);

            var RawData2 = await futureMarket.Future_KlineCandlestickData(
            COIN,
                        interval,
                        Time_2022,
                        null,
                        1000);
            var serverTime = await market.CheckServerTime();
            var future_serverTime = await futureMarket.Future_CheckServerTime();

            GetDataFromBinance.Create_Coin("BTCUSDT");
        }
    }
}