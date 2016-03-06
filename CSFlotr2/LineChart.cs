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
    public class LineChart : CSFlotr2Core
    {
        public class Data
        {
            public float[] data = null;
            public string label;
            public bool fill = false;
            public bool show = true;
            public bool show_point = false; 
        }

        public string GenerateGraphBody(string id, Data[] data, string xtitle, string ytitle)
        {
            string ret = "";


            ret += "<div id=\"" + id + "\"></div>\n";
            ret += "<script>\n";

            ret += "(function basic_axis(container) {\n"; 

            for(int x=0;x<data.GetLength(0);x++) {
                ret += "var d" + x + " = ["; 
                for(int y=0;y<data[x].data.GetLength(0);y++) {
                    if(y != 0) {
                        ret += ","; 
                    }
                    ret += "[" + y + "," + data[x].data[y] + "]";
                }
                ret += "];"; 
            }

            ret += "graph = Flotr.draw(container,[\n"; 
            for(int x=0;x<data.GetLength(0);x++) {
                ret += "{ data : d" + x + ", label : '" + data[x].label + "' "; 
                if(data[x].fill || data[x].show) {
                    ret += ", lines : { "; 
                    if(data[x].show) {
                        ret += " show : true"; 
                    }
                    if (data[x].fill || data[x].show)
                    {
                        ret += ", ";
                    }
                    if(data[x].fill) {
                        ret += " fill : true"; 
                    }
                    ret += " } "; 
                }
                if(data[x].show_point) {
                    ret += ", points : { show : true}"; 
                }
                ret += "},\n"; 
            }
            ret += "], {\n"; 
            ret += "legend : {\n";
            ret += "backgroundColor : '#D2E8FF', \n";
            ret += "position : 'se'";
            ret += "},\n";

            ret += "xaxis : { title: '" + xtitle + "'},\n";
            ret += "yaxis : { title: '" + ytitle + "'},\n";
            ret += "grid : {\n";
            ret += "verticalLines : false,\n";
            ret += "backgroundColor : {\n";
            ret += "colors : [[0,'#fff'], [1,'#ccc']],\n";
            ret += "start : 'top',\n";
            ret += "end : 'bottom'\n";
            ret += "}\n";

            ret += "}\n";
            ret += "});\n";
            ret += "})(document.getElementById(\"" + id + "\"));\n";

            ret += "</script>\n";
            
            return ret; 

        }

        public void GenerateSimpleHtml(string filename, string title, LineChart.Data[] data, string[] xlabel, string xtitle, string ytitle)
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
            writer.WriteLine(GenerateGraphBody(bc_id, data, xtitle, ytitle));



            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            writer.Close();
        }
    }
}
