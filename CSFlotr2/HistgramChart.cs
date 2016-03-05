using CSFlotr2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFlotr2
{
    public class HistgramChart : BarChart
    {
        public HistgramChart()
        {

        }

        public override string GenerateGraphBody(string id, Data[] data, string[] xlabel)
        {
            string ret = "";
            string horizontal;
            float bar_width = 0.8f / data.GetLength(0);

            horizontal = "false";

            ret += "<div id=\"" + id + "\"></div>\n";
            ret += "<script>\n";

            ret += "(function basic_bars(container, horizontal) {\n";


            // for bar
            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "var d" + x + " = [\n";
                for (int y = 0; y < data[x].data.GetLength(0); y++)
                {
                    if (y != 0)
                    {
                        ret += ",";
                    }
                    ret += "[" + (y + bar_width * x) + "," + data[x].data[y] + "]\n";
                }
                ret += "];\n";
            }


            // for line
            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "var dl" + x + " = [\n";
                float sum = 0.0f;
                for (int y = 0; y < data[x].data.GetLength(0); y++)
                {
                    sum += data[x].data[y]; 
                }

                float sum2 = 0.0f;

                ret += "[" + (-1 + bar_width * x) + "," + 0 + "]\n";
                for (int y = 0; y < data[x].data.GetLength(0); y++)
                {
                    ret += ",";
                    sum2 += data[x].data[y];
                    float rt = sum2 / sum * 100.0f; 
                    ret += "[" + (y + bar_width * x) + "," + rt + "]\n";
                }
                ret += ", [" + (data[x].data.GetLength(0) + bar_width * x) + "," + 100 + "]\n";

                ret += "];\n";
            }


            // for marker
            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "var m" + x + " = [\n";
                for (int y = 0; y < data[x].data.GetLength(0); y++)
                {
                    if (y != 0)
                    {
                        ret += ",";
                    }
                    ret += "[" + (y + bar_width * x) + "," + data[x].data[y] + "]\n";
                }
                ret += "];\n";
            }


            // for ticks
            ret += "var ticks = [\n";

            for (int i = 0; i < xlabel.Count(); i++)
            {
                if (i != 0)
                {
                    ret += ",";
                }
                ret += "[" + i + ", '" + xlabel[i] + "']\n";

            }
            ret += "]; \n";

            ret += "graph = Flotr.draw(container,[\n";
            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "{ data : dl" + x + ", bars: {show: false}, lines: {show: true }, yaxis : 2 },\n";
                ret += "{ data : d" + x + ",  bars: {show: true, start: 'top', end: 'bottom'}, fillOpacity: 1.0, label : '" + data[x].label + "' },\n";
                ret += "{ data : m" + x + ", bars: {show: false}, markers: { show: true, position: 'ct'}},\n";
            }
            ret += "], {\n";
            ret += "legend : {\n";
            ret += "backgroundColor : '#D2E8FF', \n";
            ret += "position : 'se'";
            ret += "},\n";

            ret += "bars : {\n";
            ret += "show : true,\n";
            ret += "stacked : false,\n";
            ret += "horizontal : horizontal,\n";
            ret += "barWidth : " + bar_width + ",\n";
            ret += "lineWidth : 1,\n";
            ret += "shadowSize : 0\n";
            ret += "},\n";

//            ret += "yaxis : { ticks : ticks},\n";
            ret += "xaxis : { ticks : ticks},\n";
            ret += "y2axis : { title: '%'},\n";

            ret += "mouse : {\n";
            ret += "track : true,\n";
            ret += "relative : true\n";
            ret += "},\n";


            ret += "grid : {\n";
            ret += "minorVerticalLines: true,\n";
            ret += "verticalLines : horizontal,\n";
            ret += "horizontalLines : !horizontal\n";
            ret += "}\n";
            ret += "});\n";
            ret += "})(document.getElementById(\"" + id + "\"), " + horizontal + ");\n";

            ret += "</script>\n";

            return ret;
        }


    }
}
