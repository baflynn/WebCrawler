using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Domain.Services;

namespace WebCrawler.Domain.Tests.Unit.Stubs
{
    public class TestWebPageDownloader : IDownloadWebPages
    {
        public string DownLoad(string pageUrl)
        {
            if (pageUrl == "http://www.testwebsite.com/")
            {
                return @"<!DOCTYPE html>
                         <html>
                         <head></head>
                         <body>
                            <a href=""http://www.testwebsite.com/page1""/>
                            <a href=""http://someotherwebsite.com/page1""/>
                            <img src=""http://www.testwebsite.com/image1""/>
                         </body>
                         </html>";
            }
            else
            {
                return @"<!DOCTYPE html>
                         <html>
                         <head></head>
                         <body>
                         </body>
                         </html>";
            }
        }

        public void Dispose()
        {
        }
    }
}