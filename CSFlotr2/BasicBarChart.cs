/**
 * Copyright (c) 2015-2016  Shinsuke.Ogata
 **/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFlotr2
{
    public class BasicBarChart : BarChart
    {
        public BasicBarChart()
        {

        }

        public override string GenerateGraphBody(string id, Data[] data, string[] xlabel)
        {
            string ret = "";
            string horizontal;
            float bar_width = 0.8f / data.GetLength(0); 

            if (m_horizontal)
            {
                horizontal = "true";
            }
            else
            {
                horizontal = "false";
            }

            ret += "<div id=\"" + id + "\"></div>\n";
            ret += "<script>\n";

            ret += "(function basic_bars(container, horizontal) {\n";

            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "var d" + x + " = [\n";
                for (int y = 0; y < data[x].data.GetLength(0); y++)
                {
                    if (y != 0)
                    {
                        ret += ",";
                    }
                    if (m_horizontal)
                    {
                        ret += "[" + data[x].data[y] + "," + (y + bar_width * x) + "]\n";
                    }
                    else
                    {
                        ret += "[" + (y + bar_width * x) + "," + data[x].data[y] + "]\n";
                    }
                }
                ret += "];\n";
            }

            ret += "var ticks = [\n";

            for (int i = 0; i < xlabel.Count(); i++)
            {
                if (i != 0)
                {
                    ret += ","; 
                }
                ret += "[" + i + ", '" + xlabel[i] +"']\n"; 

            }
            ret += "]; \n"; 

            ret += "graph = Flotr.draw(container,[\n";
            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "{ data : d" + x + ", label : '" + data[x].label + "' },\n";
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

            if (m_horizontal)
            {
                ret += "yaxis : { ticks : ticks},\n";
            }
            else
            {
                ret += "xaxis : { ticks : ticks},\n"; 
            }

            ret += "mouse : {\n";
            ret += "track : true,\n";
            ret += "relative : true\n";
            ret += "},\n";


            ret += "grid : {\n";
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
