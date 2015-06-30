using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Domain.Services
{
    public interface IDownloadWebPages : IDisposable
    {
        string DownLoad(string pageUrl);
    }
}