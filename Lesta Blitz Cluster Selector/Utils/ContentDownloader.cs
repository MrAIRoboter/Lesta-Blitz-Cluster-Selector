using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lesta_Blitz_Cluster_Selector.Utils
{
    public static class ContentDownloader
    {
        public static string DownloadTextFromUrl(string url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
