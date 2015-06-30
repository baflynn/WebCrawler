using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Domain.Services
{
    public class WebClientDownloader : IDownloadWebPages
    {
        private WebClient _client;

        public WebClientDownloader()
        {
            _client = new WebClient();
        }

        public string DownLoad(string pageUrl)
        {
            try
            {
                return _client.DownloadString(pageUrl);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}