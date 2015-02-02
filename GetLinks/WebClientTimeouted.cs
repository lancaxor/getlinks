using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GetLinks
{
    public class WebClientTimeouted:WebClient
    {
        /// <summary>
        /// Timeout in seconds.
        /// </summary>
        public int Timeout { get; set; }
        //public var enc = Encoding.UTF8;
        //public bool CancelRequest { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest wr = base.GetWebRequest(address);
            wr.Timeout = this.Timeout * 1000;
            return wr;
        }
    }
}
