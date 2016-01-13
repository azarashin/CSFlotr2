/**
 * Copyright (c) 2015-2016  Shinsuke.Ogata
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFlotr2
{
    public class GraphAttribute
    {
        public int width_px = 600; // 画素幅
        public int height_px = 400; // 画素高さ
        public int margin = 20; // マージン（ピクセル）
        public string margin_type = "auto";

        public int title_font_size = 16; // フォントのサイズ
        public string title_font_weight = "bold";
        public string title_text_align = "center";
        public int title_margin = 50; 
    }


    public class CSFlotr2Core
    {
        public CSFlotr2Core()
        { 
        }

        public string Include()
        {
            return "<script type='text/javascript' src='js/flotr2/flotr2.min.js'></script>"; 
        }

        public string GenerateGraphHeader(string id, GraphAttribute attr)
        {
            if (attr == null)
            {
                attr = new GraphAttribute(); 
            }

            string ret = "";
            ret += "<style type='text/css'>";
            ret += "#" + id + " {\n";
            ret += "width : " + attr.width_px + "px;\n"; 
            ret += "height: " + attr.height_px + "px;\n"; 
            ret += "margin: " + attr.margin + " " + attr.margin_type + ";\n"; 
            ret += "}\n"; 
            ret += "." + id + "-title {\n"; 
            ret += "font-size:" + attr.title_font_size + "px;\n"; 
            ret += "font-weight:" + attr.title_font_weight + ";\n"; 
            ret += "text-align:" + attr.title_text_align + ";\n"; 
            ret += "margin:" + attr.title_margin + "px 0 0;\n"; 
            ret += "}\n";
            ret += "</style>\n"; 

            return ret; 
        }

        public string GenerateGraphTitle(string id, string title)
        {
            string ret = ""; 
            ret += "<p class='" + id + "-title'>" + title + "</p>\n";
            return ret; 
        }
    }

}
