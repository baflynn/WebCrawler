using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Domain.Model;

namespace WebCrawler.Domain.Services
{
    public interface ICrawlWebSites : IDisposable
    {
        SiteMap CrawlWebSite(string url);
    }
}