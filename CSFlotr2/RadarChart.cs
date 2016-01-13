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
    public class RadarChart : CSFlotr2Core
    {
        private float m_min = 0.0f;
        private float m_max = 10.0f;

        public class Data
        {
            public float[] data = null;
            public string label;
        }

        public string GenerateGraphBody(string id, Data[] data, string[] tick_label)
        {
            string ret = "";


            ret += "<div id=\"" + id + "\"></div>\n";
            ret += "<script>\n";

            ret += "(function basic_radar(container) {\n";

            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "var s" + x + " = { label : '" + data[x].label + "', data : [";
                for (int y = 0; y < data[x].data.GetLength(0); y++)
                {
                    if (y != 0)
                    {
                        ret += ",";
                    }
                    ret += "[" + y + "," + data[x].data[y] + "]";
                }
                ret += "]};";
            }

            ret += "var ticks = [\n";
            for (int i = 0; i < tick_label.Length; i++)
            {
                if (i != 0)
                {
                    ret += ","; 
                }
                ret += "[" + i + ", \"" + tick_label[i] + "\"]\n"; 
            }
            ret += "]\n"; 

            ret += "graph = Flotr.draw(container,[";
            for (int x = 0; x < data.GetLength(0); x++)
            {
                if (x != 0)
                {
                    ret += ","; 
                }
                ret += "s" + x; 
            }
            ret += "], {\n";

            ret += "radar : { show : true}, \n"; 
            ret += "grid  : { circular : true, minorHorizontalLines : true}, \n"; 
            ret += "yaxis : { min : " + m_min + ", max : " + m_max + ", minorTickFreq : 2}, \n";
            ret += "xaxis : { ticks : ticks},\n"; 
            ret += "mouse : { track : true}\n"; 

            ret += "});\n";
            ret += "})(document.getElementById(\"" + id + "\"));\n";

            ret += "</script>\n";

            return ret;

        }

        public void SetMax(float max)
        {
            m_max = max; 
        }

        public void SetMin(float min)
        {
            m_min = min;
        }

        public void GenerateSimpleHtml(string filename, string title, RadarChart.Data[] data, string[] xlabel)
        {
            string bc_id = "bc_id";

            CSFlotr2Core gbc = new CSFlotr2Core();


            StreamWriter writer =
                new StreamWriter(filename, false, UTF8Encoding.UTF8);


            writer.WriteLine("<html lang='ja' dir='ltr'>");
            writer.WriteLine("<head>");
            writer.WriteLine("<meta charset='utf-8'>");

            writer.WriteLine(gbc.Include());

            writer.WriteLine(gbc.GenerateGraphHeader(bc_id, null));



            writer.WriteLine("</head>");
            writer.WriteLine("<body>");


            writer.WriteLine(gbc.GenerateGraphTitle(bc_id, title));
            writer.WriteLine(GenerateGraphBody(bc_id, data, xlabel));



            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            writer.Close();
        }    


    }
}
