using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkhuKB.Util
{
    public class StringUtil
    {
        public static string XSSConverter(string text)
        {

            string result = text;
            result = result.Replace("&lt;p&gt;", "<p>");
            result = result.Replace("&lt;br&gt;", "<br>");
            result = result.Replace("&lt;BR&gt;", "<BR>");
            result = result.Replace("&lt;P&gt;", "<P>");
            result = result.Replace("&lt;/P&gt;", "</P>");
            result = result.Replace("&lt;/p&gt;", "</p>");

            return result;
        }

        public static string getOriginalFileName(string text)
        {
            return text.Substring(0, text.LastIndexOf("_")) + "." + text.Substring(text.LastIndexOf(".") + 1, text.Length - text.LastIndexOf(".") - 1);
        }

        public static string setFileNameByDateTime(string text)
        {
            return text.Substring(0, text.LastIndexOf(".")) + "_" + DateTime.Now.Ticks + "." + text.Substring(text.LastIndexOf(".") + 1, text.Length - text.LastIndexOf(".") - 1);
        }

        public static string getDirectoryInFileName(string text)
        {
            return text.Substring(text.LastIndexOf("\\") + 1, text.Length - text.LastIndexOf("\\") - 1);
        }

        public static string wikiLink(string text)
        {
            string result = "";

            while (text.IndexOf("]]") != -1)
            {
                if (text.IndexOf("[[") != 0)
                {
                    result += text.Substring(0, text.IndexOf("[["));
                    String realText = text.Substring(text.IndexOf("[[") + 2, text.IndexOf("]]") - text.IndexOf("[[") - 2);

                    if (realText.IndexOf("|") != -1)
                    {
                        result += "<a href='DetailView?aIdx=" + realText.Substring(0, realText.IndexOf("|")) + "' title='" + realText + "' class='wikiLink'>" +
                            realText.Substring(realText.IndexOf("|") + 1, realText.Length - realText.IndexOf("|") - 1) + "</a>";
                        text = text.Substring(text.IndexOf("]]") + 2, text.Length - text.IndexOf("]]") - 2);
                    }
                    else
                    {
                        result += "<a href='DetailView?aIdx=" + realText + "' title='" + realText + "' class='wikiLink'>" + realText + "</a>";
                        text = text.Substring(text.IndexOf("]]") + 2, text.Length - text.IndexOf("]]") - 2);
                    }

                }
            }
            result += text;
            return result;

        }
    }
}
