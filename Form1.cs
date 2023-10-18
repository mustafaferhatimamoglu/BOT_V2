using BOT_V2.Binance.Spot;
using BOT_V2.Binance.Spot.Models;

namespace BOT_V2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Market market = new Market();
        string COIN;
        Interval interval; 
        long Time_2022 = 1640995200000;
        private async Task button1_ClickAsync(object sender, EventArgs e)
        {
            var RawData = await market.KlineCandlestickData(
            COIN,
                        interval,
                        Time_2022,
                        null,
                        1000);
        }
    }
}