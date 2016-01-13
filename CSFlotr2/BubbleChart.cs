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
    public class BubbleChart : CSFlotr2Core
    {
        public class Data
        {
            public class Point
            {
                public float x = 0.0f;
                public float y = 0.0f;
                public float r = 1.0f; 
            }

            public Point[] point = null; 
            public string label = "";
        }

        public string GenerateGraphBody(string id, Data[] data)
        {
            string ret = "";


            ret += "<div id=\"" + id + "\"></div>\n";
            ret += "<script>\n";

            ret += "(function basic_bubble(container) {\n";

            float xmax = data[0].point[0].x;
            float xmin = data[0].point[0].x;
            float ymax = data[0].point[0].y;
            float ymin = data[0].point[0].y;
            float base_r = 5.0f; 

            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "var d" + x + " = [\n";
                for (int y = 0; y < data[x].point.Count(); y++)
                {
                    if (y != 0)
                    {
                        ret += ",\n"; 
                    }
                    ret += "\t[" + data[x].point[y].x + "," + data[x].point[y].y + "," + data[x].point[y].r + "]";

                    if (xmin > data[x].point[y].x - data[x].point[y].r - base_r)
                    {
                        xmin = data[x].point[y].x - data[x].point[y].r - base_r; 
                    }
                    if (xmax < data[x].point[y].x + data[x].point[y].r + base_r)
                    {
                        xmax = data[x].point[y].x + data[x].point[y].r + base_r;
                    }
                    if (ymin > data[x].point[y].y - data[x].point[y].r - base_r)
                    {
                        ymin = data[x].point[y].y - data[x].point[y].r - base_r;
                    }
                    if (ymax < data[x].point[y].y + data[x].point[y].r + base_r)
                    {
                        ymax = data[x].point[y].y + data[x].point[y].r + base_r;
                    }

                }
                ret += "\n];\n";
            }

            ret += "graph = Flotr.draw(container,[\n";
            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "{ data : d" + x + ", label : '" + data[x].label + "' ";
                ret += "},\n";
            }
            ret += "], {\n";

            ret += "bubbles : { show : true, baseRadius : " + base_r + " },\n"; 
            ret += "xaxis   : { min : " + xmin + ", max : " + xmax + " },\n";
            ret += "yaxis   : { min : " + ymin + ", max : " + ymax + " }\n"; 
            ret += "});\n";
            ret += "})(document.getElementById(\"" + id + "\"));\n";

            ret += "</script>\n";

            return ret;

        }

        public void GenerateSimpleHtml(string filename, string title, BubbleChart.Data[] data)
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
            writer.WriteLine(GenerateGraphBody(bc_id, data));



            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            writer.Close();
        }
    }
}
