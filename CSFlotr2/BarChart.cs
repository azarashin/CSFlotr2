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
    public abstract class BarChart : CSFlotr2Core
    {
        public class Data
        {
            public float[] data = null;
            public string label; 
        }

        public abstract string GenerateGraphBody(string id, Data[] data, string[] xlabel, string xtitle, string ytitle);

        protected bool m_horizontal = false;

        public void SetHorizontal(bool flag)
        {
            m_horizontal = flag;
        }


        public void GenerateSimpleHtml(string filename, string title, BarChart.Data[] data, string[] xlabel, string xtitle, string ytitle)
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
            writer.WriteLine(GenerateGraphBody(bc_id, data, xlabel, xtitle, ytitle));



            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            writer.Close();
        }
    }
}
