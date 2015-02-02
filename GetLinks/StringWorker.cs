using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GetLinks
{
    abstract class StringWorker
    {
        private static bool bHttp = false;
        private static int http = 0, https = 0;

        public static string GetTitle(String source)
        {
            int start, end;
            int titStart = 0;
            if (!(source.ToLower().Contains("<title")&& source.ToLower().Contains("</title>")))
                return "[Unknown Title]";
            titStart = source.ToLower().IndexOf("<title");      // for ex, <title id=... >
            start = source.ToLower().IndexOf('>', titStart) + 1;
            end = source.ToLower().IndexOf("</title>");
            return source.Substring(start, end - start).Trim();     //just for this string i used ToLower instead of new var...
        }

        public static string[] GetLinks(String source)
        {
            List<String> res = new List<string>();
            String cropped = source.ToLower(), link = string.Empty;
            int start, end = 1;     //link format: <a .... href="http://lalala" .. >...            
            while (end > 0)
            {
                http = cropped.IndexOf("http://");
                https = cropped.IndexOf("https://");

                if (http < 0)
                {
                    start = https;
                    bHttp = false;
                }
                else if (https < 0)
                {
                    start = http;
                    bHttp = true;
                }
                else if (http < https)
                {
                    start = http;
                    bHttp = true;
                }
                else
                {
                    start = https;
                    bHttp = false;
                }

                if (start < 0) break;
                end = cropped.IndexOfAny(new char[] { '/', '?', '\"', '\'', '&' }, start + (bHttp ? 7 : 8));
                if (end < 0) break;
                link = cropped.Substring(start, end - start);

                if (start > 0 && end > 0 && (from r in res where r.Equals(link) select r).Count() == 0 && link.Contains('.'))       //no doubles
                    res.Add(link);
                cropped = cropped.Substring(end);
            }

            return res.ToArray<string>();
        }

        //public static Encoding GetEncoding(String EncodedSource)
        //{
        //    String source = EncodedSource.ToLower();
        //    int utf8I = Math.Max(source.IndexOf("charset=utf-8"), source.IndexOf("charset=\"utf-8\""));
        //    int win1251I = Math.Max(source.IndexOf("charset=windows-1251"), source.IndexOf("\"charset=windows-1251\""));
        //    //int koi8I = Math.Max(source.IndexOf("charset=KOI8-R"), source.IndexOf("\"charset=KOI8-R\""));
        //    int koi8I = GetMinPoz(new int[]{
        //        source.IndexOf("charset=KOI8-R"),
        //        source.IndexOf("\"charset=KOI8-R\""),
        //        source.IndexOf("charset=KOI8-U"),
        //        source.IndexOf("\"charset=KOI8-U\"")
        //    });

        //    int tmp = GetMinPoz(new int[] { utf8I, win1251I, koi8I });
        //    if (tmp == win1251I)
        //        return Encoding.GetEncoding(1251);
        //    else if (tmp == koi8I)
        //        return Encoding.GetEncoding("KOI8-R");
        //    else
        //        return Encoding.UTF8;
        //}

        //public static String Encode(String source, Encoding StartEncoding)
        //{
        //    byte[] oldS = StartEncoding.GetBytes(source);
        //    Encoding e = GetEncoding(source);
        //    if (e == StartEncoding) //no sence to do next steps
        //        return source;
        //    byte[] newS = Encoding.Convert(StartEncoding, e, oldS);
        //    return e.GetString(newS);
        //}

        private static int GetMinPoz(int[] arg)
        {
            int min = -1;
            for (int i = 0; i < arg.Length; i++)
                if (min < arg[i] && arg[i] > 0)
                    min = arg[i];
            return min;
        }
    }
}
