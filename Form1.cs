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

            var getDataFromBinance = new GetDataFromBinance();
            //await GetDataFromBinance.Create_Coin("BTCUSDT");
            getDataFromBinance.Create_Coin("BTCUSDT");
        }

        private void B_BTCUSDT_Click(object sender, EventArgs e)
        {
            var f = new Grapher.GrapherForm_V3("BTCUSDT");
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string coinName = TB_coinName.Text;
            int interval_position = LB_interval.SelectedIndex;
            double RSI_limit = (double)NUD_RSI.Value;
            double getProfit = (double)NUD_getProfit.Value;
            double stopLoss = (double)NUD_stopLoss.Value;
            var f = new Grapher.GrapherForm_V4(coinName, interval_position, RSI_limit, getProfit, stopLoss);
            f.Show();
        }

        private void Button_Setup_Click(object sender, EventArgs e)
        {
            var f = new Grapher.GrapherForm_V4("BTCUSDT", true);
            f.Show();
        }

        private void Button_ML_Click(object sender, EventArgs e)
        {
            var f = new MachineLearning.ML_V1();
            f.Show();
        }
    }
}