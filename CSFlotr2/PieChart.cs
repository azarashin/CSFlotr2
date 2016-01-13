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
    public class PieChart : CSFlotr2Core
    {
        public class Data
        {
            public float value = 1.0f;
            public string label = "";
            public int explode = 6; 
        }

        public string GenerateGraphBody(string id, Data[] data)
        {
            string ret = "";


            ret += "<div id=\"" + id + "\"></div>\n";
            ret += "<script>\n";

            ret += "(function basic_pie(container) {\n";

            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "var d" + x + " = [";
                ret += "[" + '0' + "," + data[x].value + "]";
                ret += "];";
            }

            ret += "graph = Flotr.draw(container,[\n";
            for (int x = 0; x < data.GetLength(0); x++)
            {
                ret += "{ data : d" + x + ", label : '" + data[x].label + "', ";
                ret += "pie : { explode : " + data[x].explode + "}"; 
                ret += "},\n";
            }
            ret += "], {\n";

            ret += "HtmlText : false,\n";
            ret += "grid : {\n";
            ret += "verticalLines : false,\n";
            ret += "horizontalLines : false\n";
            ret += "},\n";
            ret += "xaxis : { showLabels : false },\n";
            ret += "yaxis : { showLabels : false },\n";
            ret += "pie : {\n";
            ret += "show : true, \n";
            ret += "},\n";
            ret += "mouse : { track : true },\n";
            ret += "legend : {\n";
            ret += "position : 'se',\n";
            ret += "backgroundColor : '#D2E8FF'\n";
            ret += "}\n";

            ret += "});\n";
            ret += "})(document.getElementById(\"" + id + "\"));\n";

            ret += "</script>\n";

            return ret;

        }

        public void GenerateSimpleHtml(string filename, string title, PieChart.Data[] data)
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
