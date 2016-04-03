/**
 * Copyright (c) 2015-2016  Shinsuke.Ogata
 **/ 

using CSFlotr2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            RunBarChart();
            RunLineChart();
            RunPieChart();
            RunRadarChart();
            RunBubbleChart(); 
        }

        static void RunBarChart() {
            int category_max = 3; 
            int data_max = 5;
            BarChart.Data[] data = new BarChart.Data[category_max];
            string[] xlabel = new string[data_max];
            Random r = new Random();

            for (int x = 0; x < data_max; x++)
            {
                xlabel[x] = "x" + x; 
            }

            for (int x = 0; x < category_max; x++)
            {
                data[x] = new BarChart.Data(); 
                data[x].data = new float[data_max];
                data[x].label = "label" + x; 
                for (int y = 0; y < data_max; y++)
                {
                    data[x].data [y]= r.Next(10000) / 10.0f;
                }
            }


            StackedBarChart sb = new StackedBarChart(); 
            sb.SetHorizontal(true);

            BasicBarChart bc = new BasicBarChart();
            bc.SetHorizontal(false);

            HistgramChart hc = new HistgramChart();
            hc.SetHorizontal(false);

            bc.GenerateSimpleHtml("simple_bar_chart.html", "sample", data, xlabel, "x-label", "y-label");
            sb.GenerateSimpleHtml("stacked_bar_chart.html", "sample", data, xlabel, "x-label", "y-label");
            hc.GenerateSimpleHtml("histgram_chart.html", "sample", data, xlabel, "x-label", "y-label");

            return; 


        }

        static void RunLineChart()
        {
            int category_max = 3;
            int data_max = 200;
            LineChart.Data[] data = new LineChart.Data[category_max];
            string[] xlabel = new string[data_max];

            for (int x = 0; x < data_max; x++)
            {
                xlabel[x] = "x" + x;
            }

            for (int x = 0; x < category_max; x++)
            {
                data[x] = new LineChart.Data();
                data[x].data = new float[data_max];
                data[x].label = "label" + x; 
                for (int y = 0; y < data_max; y++)
                {
                    data[x].data[y] = (float)Math.Sin(y * Math.PI * 2.0f / 200.0f) * (x + 1);
                }
            }
            data[1].show_point = true;
            data[2].fill = true; 

            LineChart lc = new LineChart();

            lc.GenerateSimpleHtml("line_chart.html", "sample", data, xlabel, "x-label", "y-label"); 


        }

        static void RunPieChart()
        {
            int category_max = 3;
            PieChart.Data[] data = new PieChart.Data[category_max];
            string[] xlabel = new string[category_max];

            for (int x = 0; x < category_max; x++)
            {
                xlabel[x] = "x" + x;
            }

            for (int x = 0; x < category_max; x++)
            {
                data[x] = new PieChart.Data();
                data[x].value = x + 1; 
                data[x].label = "label" + x;
            }

            PieChart lc = new PieChart();
            lc.GenerateSimpleHtml("pie_chart.html", "sample", data); 
        }

        static void RunRadarChart()
        {
            int category_max = 3;
            int data_max = 7;
            RadarChart.Data[] data = new RadarChart.Data[category_max];
            string[] ticks_label = new string[data_max];

            for (int i = 0; i < ticks_label.Length; i++)
            {
                ticks_label[i] = "tick_" + i; 
            }

            Random rand = new Random(); 

            for (int x = 0; x < category_max; x++)
            {
                data[x] = new RadarChart.Data();
                data[x].data = new float[data_max];
                data[x].label = "label" + x;
                for (int y = 0; y < data_max; y++)
                {
                    data[x].data[y] = rand.Next(5) + 1;
                }
            }

            RadarChart lc = new RadarChart();
            lc.SetMax(6.0f);
            lc.GenerateSimpleHtml("radar_chart.html", "sample", data, ticks_label); 

        }

        static void RunBubbleChart()
        {
            int category_max = 3;
            int data_max = 7;
            BubbleChart.Data[] data = new BubbleChart.Data[category_max];

            Random rand = new Random();

            for (int x = 0; x < category_max; x++)
            {
                data[x] = new BubbleChart.Data();
                data[x].point = new BubbleChart.Data.Point[data_max];
                data[x].label = "label" + x;
                for (int y = 0; y < data_max; y++)
                {
                    data[x].point[y] = new BubbleChart.Data.Point(); 
                    data[x].point[y].x = rand.Next(100);
                    data[x].point[y].y = rand.Next(100);
                    data[x].point[y].r = rand.Next(6);
                }
            }

            BubbleChart lc = new BubbleChart();
            lc.GenerateSimpleHtml("bubble_chart.html", "sample", data, "x-label", "y-label"); 

        }
    
    }
}
