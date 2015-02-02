using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GetLinks
{
    abstract class FSWorker
    {
        private static String cpath=Directory.GetCurrentDirectory()+"/cache";      //cache

        public static string SaveCache(String source, String fname)     //for testing only
        {
            Directory.CreateDirectory(cpath);
            StreamWriter sw = new StreamWriter(cpath + "/" + fname + ".html", false);
            sw.Write(source);
            sw.Close();
            return cpath + "/" + fname + ".html";
        }

        public static void ClearCache(){
            if (Directory.Exists(cpath))
                Directory.Delete(cpath, true);
        }
    }
}
