using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Domain.Model;

namespace WebCrawler.Domain.Services
{
    public interface IParseWebPages : IDisposable
    {
        FlatPage Parse(string pageContent, string pageUrl);

        void SetRootUrl(string url);
    }
}