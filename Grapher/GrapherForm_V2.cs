using BOT_V2.Binance.Spot.Models;
using BOT_V2.Indicators;
using BOT_V2.Operations;
using ScottPlot;
using ScottPlot.Plottable;
using ScottPlot.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOT_V2.Grapher
{
    public partial class GrapherForm_V2 : Form
    {
        FormsPlot[] FormsPlots;
        //SignalPlotXY coinPrice_High = new();
        //SignalPlotXY coinPrice_Low = new(); 
        SignalPlotXY[] coinPrice_High = new SignalPlotXY[13];
        SignalPlotXY[] coinPrice_Low = new SignalPlotXY[13];

        SignalPlotXY[] Indicator_RSI_6 = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_RSI_12 = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_RSI_24 = new SignalPlotXY[13];

        SignalPlotXY[] Indicator_KDJ_K = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_KDJ_D = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_KDJ_J = new SignalPlotXY[13];

        SignalPlotXY[] Indicator_OBV_6 = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_OBV_12 = new SignalPlotXY[13];
        SignalPlotXY[] Indicator_OBV_24 = new SignalPlotXY[13];
        //double[][][] d_coinPrice = new double[3][][];
        double[,][] d_coinPrice;
        double[,][] d_indicator_RSI;
        double[,][] d_indicator_KDJ;
        double[,][] d_indicator_OBV;
        string[] intervals = {
            "1m", "5m", "15m", "30m", "1h",
            "2h", "4h", "6h", "8h", "12h",
            "1d", "3d", "1w" };

        string coinName;
        public GrapherForm_V2(string coinName)
        {
            InitializeComponent();
            this.coinName = coinName;

            FormsPlots = new FormsPlot[] { FP_CoinPrice, FP_RSI, FP_KDJ, FP_OBV };
            foreach (var fp in FormsPlots)
            {
                fp.AxesChanged += FP_CoinPrice_AxesChanged;
                fp.Plot.Style(Style.Black);
                fp.Plot.XAxis.DateTimeFormat(true);
            }
            FP_CoinPrice.Load += FP_CoinPrice_Load;
            FP_CoinPrice.MouseEnter += FP_CoinPrice_MouseEnter;
            FP_CoinPrice.MouseLeave += FP_CoinPrice_MouseLeave;
            FP_CoinPrice.MouseMove += FP_CoinPrice_MouseMove;
            //B_NEXT.Click += B_NEXT_Click;
            Create_CoinPrice_Buttons();
        }
        void Create_CoinPrice_Buttons()
        {
            for (int i = 0; i < intervals.Count(); i++)
            {
                TLP_Buttons.Controls.Add(new Button
                {
                    Name = "Button_CoinPrice_" + i.ToString(),
                    Text = intervals[i],

                });
                ((Button)TLP_Buttons.Controls.Find("Button_CoinPrice_" + i.ToString(), true).First()).Click += Button_CoinPrice_Clicked;
            }
            {
                TLP_Buttons.Controls.Add(new Button
                {
                    Name = "Button_Back",
                    Text = "Back",

                });
                ((Button)TLP_Buttons.Controls.Find("Button_Back", true).First()).Click += Button_Back_Clicked;
            }
            for (int i = 0; i < intervals.Count(); i++)
            {
                TLP_Buttons.Controls.Add(new Button
                {
                    Name = "Button_CoinIndicator_" + i.ToString(),
                    Text = intervals[i],

                });
                ((Button)TLP_Buttons.Controls.Find("Button_CoinIndicator_" + i.ToString(), true).First()).Click += Button_CoinIndicator_Clicked;
            }
            {
                TLP_Buttons.Controls.Add(new Button
                {
                    Name = "Button_Next",
                    Text = "Next",

                });
                ((Button)TLP_Buttons.Controls.Find("Button_Next", true).First()).Click += Button_Next_Clicked;
            }



        }

        
        void DrawVerticalLine_All(double gelen)
        {
            foreach (var item in FormsPlots)
            {
                item.Plot.AddVerticalLine(gelen);
            }
        }

        private void Button_Next_Clicked(object? sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void Button_Back_Clicked(object? sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void Button_CoinIndicator_Clicked(object? sender, EventArgs e)
        {
            Button rx = (Button)sender;
            int rxi = Convert.ToInt32(rx.Name.Split('_')[2]);
            Indicator_RSI_6[rxi].IsVisible = !Indicator_RSI_6[rxi].IsVisible;
            Indicator_RSI_12[rxi].IsVisible = !Indicator_RSI_12[rxi].IsVisible;
            Indicator_RSI_24[rxi].IsVisible = !Indicator_RSI_24[rxi].IsVisible;

            Indicator_KDJ_K[rxi].IsVisible = !Indicator_KDJ_K[rxi].IsVisible;
            Indicator_KDJ_D[rxi].IsVisible = !Indicator_KDJ_D[rxi].IsVisible;
            Indicator_KDJ_J[rxi].IsVisible = !Indicator_KDJ_J[rxi].IsVisible;

            Indicator_OBV_6[rxi].IsVisible = !Indicator_OBV_6[rxi].IsVisible;
            Indicator_OBV_12[rxi].IsVisible = !Indicator_OBV_12[rxi].IsVisible;
            Indicator_OBV_24[rxi].IsVisible = !Indicator_OBV_24[rxi].IsVisible;
            if (Indicator_RSI_6[rxi].IsVisible)
            {
                rx.BackColor = Color.Green;
            }
            else
            {
                rx.BackColor = Color.Empty;
            }
        }


        void Button_CoinPrice_Clicked(object? sender, EventArgs e)
        {
            Button rx = (Button)sender;
            int rxi = Convert.ToInt32(rx.Name.Split('_')[2]);
            coinPrice_High[rxi].IsVisible = !coinPrice_High[rxi].IsVisible;
            coinPrice_Low[rxi].IsVisible = !coinPrice_Low[rxi].IsVisible;
            if (coinPrice_High[rxi].IsVisible)
            {
                rx.BackColor = Color.Green;
            }
            else
            {
                rx.BackColor = Color.Empty;
            }
        }
        private void FP_CoinPrice_MouseMove(object? sender, MouseEventArgs e)
        {
            (double coordinateX, double coordinateY) = FP_CoinPrice.GetMouseCoordinates();
            Crosshair.X = coordinateX;
            Crosshair.Y = coordinateY;

            foreach (var item in Crosshair_Indicators)
            {
                item.X = coordinateX;
            }
            for (int i = 1; i < FormsPlots.Count(); i++)
            {
                FormsPlots[i].Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
            }
            FP_CoinPrice.Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
        }

        private void FP_CoinPrice_MouseLeave(object? sender, EventArgs e)
        {
            Crosshair.IsVisible = false;
            foreach (var item in Crosshair_Indicators)
            {
                item.IsVisible = false;
            }
            for (int i = 1; i < FormsPlots.Count(); i++)
            {
                FormsPlots[i].Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
            }
            FP_CoinPrice.Refresh(lowQuality: true, skipIfCurrentlyRendering: true);
        }

        private Crosshair Crosshair;
        private Crosshair[] Crosshair_Indicators;//= new Crosshair[3];
        private void FP_CoinPrice_MouseEnter(object? sender, EventArgs e)
        {
            Crosshair.IsVisible = true;
            foreach (var item in Crosshair_Indicators)
            {
                item.IsVisible = true;
            }
        }

        private void FP_CoinPrice_Load(object? sender, EventArgs e)
        {
            Crosshair = FP_CoinPrice.Plot.AddCrosshair(0, 0);
            Crosshair_Indicators = new Crosshair[FormsPlots.Count() - 1];
            for (int i = 0; i < FormsPlots.Count(); i++)
            {
                if (FormsPlots[i] == FP_CoinPrice)
                {
                    continue;
                }
                else
                {
                    Crosshair_Indicators[i - 1] = FormsPlots[i].Plot.AddCrosshair(0, 0);
                }
            }
            FP_CoinPrice_MouseLeave(null, null);
            Invoke(new Action(delegate ()
            {
                //------------------------------------------------------------------------
                d_coinPrice = new double[intervals.Count(), 3][];
                //interval
                //time-high-low
                //counter
                for (int count_interval = 0; count_interval < intervals.Count(); count_interval++)
                {
                    string sqlQuery = "" +
                        "select * from " + coinName + "_" + intervals[count_interval] + " \r\n" +
                        "order by Kline_open_time ";
                    var klinePriceData = Operations.KlineData.GetKlineData(sqlQuery);
                    d_coinPrice[count_interval, 0] = new double[klinePriceData.Count];
                    d_coinPrice[count_interval, 1] = new double[klinePriceData.Count];
                    d_coinPrice[count_interval, 2] = new double[klinePriceData.Count];
                    //d_coinPrice[ count_interval, count_interval][5] = 0;

                    for (int counter_klinePriceData = 0; counter_klinePriceData < klinePriceData.Count; counter_klinePriceData++)
                    {
                        d_coinPrice[count_interval, 0][counter_klinePriceData] = Indicators.Auxiliary.BTS_DateTime(klinePriceData[counter_klinePriceData].Kline_close_time).ToOADate();
                        d_coinPrice[count_interval, 1][counter_klinePriceData] = klinePriceData[counter_klinePriceData].High_price;
                        d_coinPrice[count_interval, 2][counter_klinePriceData] = klinePriceData[counter_klinePriceData].Low_price;
                    }
                    coinPrice_High[count_interval] = FP_CoinPrice.Plot.AddSignalXY(d_coinPrice[count_interval, 0], d_coinPrice[count_interval, 1], color: Color.Green);
                    coinPrice_Low[count_interval] = FP_CoinPrice.Plot.AddSignalXY(d_coinPrice[count_interval, 0], d_coinPrice[count_interval, 2], color: Color.Red);
                    coinPrice_High[count_interval].LineWidth = 1 + count_interval;
                    coinPrice_Low[count_interval].LineWidth = 1 + count_interval;
                    coinPrice_High[count_interval].IsVisible = false;
                    coinPrice_Low[count_interval].IsVisible = false;
                }
                //------------------------------------------------------------------------
                d_indicator_RSI = new double[intervals.Count(), 4][];
                //interval
                //time-6-12-24
                //counter
                for (int count_interval = 0; count_interval < intervals.Count(); count_interval++)
                {
                    string sqlQuery = "" +
                        "select * from " + coinName + "_" + intervals[count_interval] + "_RSI \r\n" +
                        "order by time ";
                    var klineIndicatorData = Operations.KlineData.GetIndicatorData_RSI(sqlQuery);
                    d_indicator_RSI[count_interval, 0] = new double[klineIndicatorData.Count];
                    d_indicator_RSI[count_interval, 1] = new double[klineIndicatorData.Count];
                    d_indicator_RSI[count_interval, 2] = new double[klineIndicatorData.Count];
                    d_indicator_RSI[count_interval, 3] = new double[klineIndicatorData.Count];
                    //d_indicator_RSI[ count_interval, count_interval][5] = 0;

                    for (int counter_klineIndicatorData = 0; counter_klineIndicatorData < klineIndicatorData.Count; counter_klineIndicatorData++)
                    {
                        d_indicator_RSI[count_interval, 0][counter_klineIndicatorData] = Indicators.Auxiliary.BTS_DateTime(klineIndicatorData[counter_klineIndicatorData].TIME).ToOADate();
                        d_indicator_RSI[count_interval, 1][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].RSI_6;
                        d_indicator_RSI[count_interval, 2][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].RSI_12;
                        d_indicator_RSI[count_interval, 3][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].RSI_24;
                    }
                    Indicator_RSI_6[count_interval] = FP_RSI.Plot.AddSignalXY(d_indicator_RSI[count_interval, 0], d_indicator_RSI[count_interval, 1], color: Color.Yellow);
                    Indicator_RSI_12[count_interval] = FP_RSI.Plot.AddSignalXY(d_indicator_RSI[count_interval, 0], d_indicator_RSI[count_interval, 2], color: Color.Red);
                    Indicator_RSI_24[count_interval] = FP_RSI.Plot.AddSignalXY(d_indicator_RSI[count_interval, 0], d_indicator_RSI[count_interval, 3], color: Color.Purple);

                    Indicator_RSI_6[count_interval].LineWidth = 1 + count_interval;
                    Indicator_RSI_12[count_interval].LineWidth = 1 + count_interval;
                    Indicator_RSI_24[count_interval].LineWidth = 1 + count_interval;

                    Indicator_RSI_6[count_interval].IsVisible = false;
                    Indicator_RSI_12[count_interval].IsVisible = false;
                    Indicator_RSI_24[count_interval].IsVisible = false;
                }
                //------------------------------------------------------------------------
                d_indicator_KDJ = new double[intervals.Count(), 4][];
                for (int count_interval = 0; count_interval < intervals.Count(); count_interval++)
                {
                    string sqlQuery = "" +
                        "select * from " + coinName + "_" + intervals[count_interval] + "_KDJ \r\n" +
                        "order by time ";
                    var klineIndicatorData = Operations.KlineData.GetIndicatorData_KDJ(sqlQuery);
                    d_indicator_KDJ[count_interval, 0] = new double[klineIndicatorData.Count];
                    d_indicator_KDJ[count_interval, 1] = new double[klineIndicatorData.Count];
                    d_indicator_KDJ[count_interval, 2] = new double[klineIndicatorData.Count];
                    d_indicator_KDJ[count_interval, 3] = new double[klineIndicatorData.Count];
                    //d_indicator_KDJ[ count_interval, count_interval][5] = 0;

                    for (int counter_klineIndicatorData = 0; counter_klineIndicatorData < klineIndicatorData.Count; counter_klineIndicatorData++)
                    {
                        d_indicator_KDJ[count_interval, 0][counter_klineIndicatorData] = Indicators.Auxiliary.BTS_DateTime(klineIndicatorData[counter_klineIndicatorData].TIME).ToOADate();
                        d_indicator_KDJ[count_interval, 1][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].KDJ_K;
                        d_indicator_KDJ[count_interval, 2][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].KDJ_D;
                        d_indicator_KDJ[count_interval, 3][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].KDJ_J;
                    }
                    Indicator_KDJ_K[count_interval] = FP_KDJ.Plot.AddSignalXY(d_indicator_KDJ[count_interval, 0], d_indicator_KDJ[count_interval, 1], color: Color.Yellow);
                    Indicator_KDJ_D[count_interval] = FP_KDJ.Plot.AddSignalXY(d_indicator_KDJ[count_interval, 0], d_indicator_KDJ[count_interval, 2], color: Color.Red);
                    Indicator_KDJ_J[count_interval] = FP_KDJ.Plot.AddSignalXY(d_indicator_KDJ[count_interval, 0], d_indicator_KDJ[count_interval, 3], color: Color.Purple);

                    Indicator_KDJ_K[count_interval].LineWidth = 1 + count_interval;
                    Indicator_KDJ_D[count_interval].LineWidth = 1 + count_interval;
                    Indicator_KDJ_J[count_interval].LineWidth = 1 + count_interval;

                    Indicator_KDJ_K[count_interval].IsVisible = false;
                    Indicator_KDJ_D[count_interval].IsVisible = false;
                    Indicator_KDJ_J[count_interval].IsVisible = false;
                }
                //------------------------------------------------------------------------
                d_indicator_OBV = new double[intervals.Count(), 4][];
                for (int count_interval = 0; count_interval < intervals.Count(); count_interval++)
                {
                    string sqlQuery = "" +
                        "select * from " + coinName + "_" + intervals[count_interval] + "_OBV \r\n" +
                        "order by time ";
                    var klineIndicatorData = Operations.KlineData.GetIndicatorData_OBV(sqlQuery);
                    d_indicator_OBV[count_interval, 0] = new double[klineIndicatorData.Count];
                    d_indicator_OBV[count_interval, 1] = new double[klineIndicatorData.Count];
                    d_indicator_OBV[count_interval, 2] = new double[klineIndicatorData.Count];
                    d_indicator_OBV[count_interval, 3] = new double[klineIndicatorData.Count];
                    //d_indicator_OBV[ count_interval, count_interval][5] = 0;

                    for (int counter_klineIndicatorData = 0; counter_klineIndicatorData < klineIndicatorData.Count; counter_klineIndicatorData++)
                    {
                        d_indicator_OBV[count_interval, 0][counter_klineIndicatorData] = Indicators.Auxiliary.BTS_DateTime(klineIndicatorData[counter_klineIndicatorData].TIME).ToOADate();
                        d_indicator_OBV[count_interval, 1][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].OBV_6;
                        d_indicator_OBV[count_interval, 2][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].OBV_12;
                        d_indicator_OBV[count_interval, 3][counter_klineIndicatorData] = klineIndicatorData[counter_klineIndicatorData].OBV_24;
                    }
                    Indicator_OBV_6[count_interval] = FP_OBV.Plot.AddSignalXY(d_indicator_OBV[count_interval, 0], d_indicator_OBV[count_interval, 1], color: Color.Yellow);
                    Indicator_OBV_12[count_interval] = FP_OBV.Plot.AddSignalXY(d_indicator_OBV[count_interval, 0], d_indicator_OBV[count_interval, 2], color: Color.Red);
                    Indicator_OBV_24[count_interval] = FP_OBV.Plot.AddSignalXY(d_indicator_OBV[count_interval, 0], d_indicator_OBV[count_interval, 3], color: Color.Purple);

                    Indicator_OBV_6[count_interval].LineWidth = 1 + count_interval;
                    Indicator_OBV_12[count_interval].LineWidth = 1 + count_interval;
                    Indicator_OBV_24[count_interval].LineWidth = 1 + count_interval;

                    Indicator_OBV_6[count_interval].IsVisible = false;
                    Indicator_OBV_12[count_interval].IsVisible = false;
                    Indicator_OBV_24[count_interval].IsVisible = false;
                }


                CreateSignalPoints();
                UseSignalPoints();


            }
            ));
        }
        int interval_position = 4; //1 hour
        List<double> HotSpots_X = new();
        List<double> HotSpots_Y = new();
        List<double> HotSpots_X_end = new();
        List<double> HotSpots_Y_end = new();
        List<string> HotSpots_end_status = new();
        int SuccessCount = 0;
        int FailCount = 0;
        void CreateSignalPoints()
        {
            for (int i = 50; i < d_indicator_RSI[4, 0].Count(); i++)
            {
                if (
                    d_indicator_RSI[4, 1][i - 1] < 30 &&
                    d_indicator_RSI[4, 1][i] > 30  &&

                    d_indicator_KDJ[4, 2][i]> d_indicator_KDJ[4, 1][i] &&
                    d_indicator_KDJ[4, 1][i] > d_indicator_KDJ[4, 3][i]
                    )
                {
                    int t1 = Array.IndexOf(d_coinPrice[0, 0], d_coinPrice[4, 0][i]);
                    HotSpots_X.Add(d_coinPrice[0, 0][t1]);

                    DrawVerticalLine_All(d_coinPrice[0, 0][t1]);
                    HotSpots_Y.Add(d_coinPrice[0, 2][t1]);
                }
            }
        }
        void UseSignalPoints()
        {
            double pnl = 0;
            double getProfit = 1.01;
            double stopLoss = 0.99;
            for (int i = 0; i < HotSpots_X.Count; i++)
            {
                int t1 = Array.IndexOf(d_coinPrice[0, 0], HotSpots_X[i]);
                for (int j = t1; j < d_coinPrice[0, 0].Count(); j++)
                {
                    if (HotSpots_Y[i] * stopLoss >= d_coinPrice[0, 2][j])
                    {
                        HotSpots_X_end.Add(d_coinPrice[0, 0][j]);
                        HotSpots_Y_end.Add(d_coinPrice[0, 2][j]);
                        pnl -= 1 - stopLoss;
                        HotSpots_end_status.Add("Loss");
                        FailCount++;
                        break;
                    }
                    else if (HotSpots_Y[i] * getProfit <= d_coinPrice[0, 1][j])
                    {
                        HotSpots_X_end.Add(d_coinPrice[0, 0][j]);
                        HotSpots_Y_end.Add(d_coinPrice[0, 1][j]);
                        pnl += getProfit - 1;
                        HotSpots_end_status.Add("Profit");
                        SuccessCount++;
                        break;
                    }
                }

            }


            for (int i = 0; i < HotSpots_end_status.Count; i++)
            {
                var rp = FP_CoinPrice.Plot.AddRectangle(
                    xMin: HotSpots_X[i], xMax: HotSpots_X_end[i], yMin: HotSpots_Y[i], yMax: HotSpots_Y_end[i]);
                //rp.BorderColor = Color.Blue;
                //rp.BorderLineWidth = 3;
                //rp.BorderLineStyle = LineStyle.Dot;
                if (HotSpots_end_status[i] == "Profit")
                {
                    rp.Color = Color.FromArgb(100, Color.Green);
                }
                else
                {
                    rp.Color = Color.FromArgb(100, Color.Red);
                }

            }
        }


        private void FP_CoinPrice_AxesChanged(object? sender, EventArgs e)
        {
            foreach (var fp in FormsPlots)
            {
                if (fp == FP_CoinPrice)
                    continue;
                if (fp == FP_RSI)
                {
                    var newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits();
                    var x_newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits(0, 1);
                    x_newAxisLimits = x_newAxisLimits.WithY(0, 100);
                    // disable events briefly to avoid an infinite loop
                    fp.Configuration.AxesChangedEventEnabled = false;
                    fp.Plot.SetAxisLimits(x_newAxisLimits);
                    fp.Render();
                    fp.Configuration.AxesChangedEventEnabled = true;
                }
                if (fp == FP_KDJ)
                {
                    var newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits();
                    var x_newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits(0, 1);
                    x_newAxisLimits = x_newAxisLimits.WithY(0, 100);
                    // disable events briefly to avoid an infinite loop
                    fp.Configuration.AxesChangedEventEnabled = false;
                    fp.Plot.SetAxisLimits(x_newAxisLimits);
                    fp.Render();
                    fp.Configuration.AxesChangedEventEnabled = true;
                }
                if (fp == FP_OBV)
                {
                    var newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits();
                    var x_newAxisLimits = FP_CoinPrice.Plot.GetAxisLimits(0, 1);

                    //x_newAxisLimits = x_newAxisLimits.WithY(-1000000, 1000000);
                    // disable events briefly to avoid an infinite loop
                    fp.Configuration.AxesChangedEventEnabled = false;
                    //fp.Plot.SetAxisLimits(x_newAxisLimits);
                    //fp.Plot.SetAxisLimitsX(x_newAxisLimits);
                    fp.Plot.SetAxisLimitsX(x_newAxisLimits.XMin, x_newAxisLimits.XMax);
                    fp.Render();
                    fp.Configuration.AxesChangedEventEnabled = true;
                }
                //if (fp == formsPlot2)
                //{
                //    var newAxisLimits = formsPlot1.Plot.GetAxisLimits();
                //    var x_newAxisLimits = formsPlot1.Plot.GetAxisLimits(0, 1);
                //    x_newAxisLimits = x_newAxisLimits.WithY(0, 100);
                //    // disable events briefly to avoid an infinite loop
                //    fp.Configuration.AxesChangedEventEnabled = false;
                //    fp.Plot.SetAxisLimits(x_newAxisLimits);
                //    fp.Render();
                //    fp.Configuration.AxesChangedEventEnabled = true;
                //}
                //if (fp == formsPlot3)
                //{
                //    var newAxisLimits = formsPlot1.Plot.GetAxisLimits();
                //    var x_newAxisLimits = formsPlot1.Plot.GetAxisLimits(0, 1);
                //    x_newAxisLimits = x_newAxisLimits.WithY(-20, 120);
                //    // disable events briefly to avoid an infinite loop
                //    fp.Configuration.AxesChangedEventEnabled = false;
                //    fp.Plot.SetAxisLimits(x_newAxisLimits);
                //    fp.Render();
                //    fp.Configuration.AxesChangedEventEnabled = true;
                //}
            }
        }
    }
}
