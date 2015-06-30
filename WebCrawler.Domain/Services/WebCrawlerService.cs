using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Domain.Model;

namespace WebCrawler.Domain.Services
{
    public class WebCrawlerService : ICrawlWebSites
    {
        private IDownloadWebPages _pageDownloader;
        private IParseWebPages _webPageParser;

        public WebCrawlerService()
            : this(new WebClientDownloader(), new SimpleWebPageParser())
        {
        }

        public WebCrawlerService(IDownloadWebPages pageDownloader, IParseWebPages webPageParser)
        {
            _pageDownloader = pageDownloader;
            _webPageParser = webPageParser;
        }

        public SiteMap CrawlWebSite(string url)
        {
            var root = new Page { Url = url };
            var siteDictionary = new SiteDictionary();

            _webPageParser.SetRootUrl(url);

            CrawlWebPage(root, siteDictionary);

            return new SiteMap
            {
                Root = root,
                TotalExternalLinks = siteDictionary.ExternalLinks.Count,
                TotalImages = siteDictionary.Images.Count,
                TotalInternalLinks = siteDictionary.Links.Count
            };
        }

        private void CrawlWebPage(Page page, SiteDictionary siteDictionary)
        {
            Console.WriteLine(string.Format("Processing Page {0}", page.Url));

            FlatPage flatPage = GenerateFlatPage(page.Url);

            MergeFlatPage(page, flatPage, siteDictionary);

            foreach (var link in page.InternalLinks)
            {
                if (!link.Visited)
                {
                    CrawlWebPage(link, siteDictionary);
                }
            }
        }

        private FlatPage GenerateFlatPage(string url)
        {
            string pageContent = _pageDownloader.DownLoad(url);

            if (string.IsNullOrWhiteSpace(pageContent))
            {
                return new FlatPage();
            }

            return _webPageParser.Parse(pageContent, url);
        }

        private void MergeFlatPage(Page page, FlatPage flatPage, SiteDictionary siteDictionary)
        {
            foreach (var link in flatPage.Links)
            {
                string url = link.Value;
                Page existingPage;

                if (!siteDictionary.Links.TryGetValue(url, out existingPage))
                {
                    var newPage = new Page { Url = url };
                    siteDictionary.Links.Add(url, newPage);
                    page.InternalLinks.Add(newPage);
                }
                else
                {
                    page.InternalLinks.Add((Page)existingPage);
                }
            }

            foreach (var image in flatPage.Images)
            {
                string url = image.Value;
                Image existingImage;

                if (!siteDictionary.Images.TryGetValue(url, out existingImage))
                {
                    var newImage = new Image { Url = url };
                    siteDictionary.Images.Add(url, newImage);
                    page.Images.Add(newImage);
                }
                else
                {
                    page.Images.Add((Image)existingImage);
                }
            }

            foreach (var image in flatPage.ExternalLinks)
            {
                string url = image.Value;
                ExternalLink existingLink;

                if (!siteDictionary.ExternalLinks.TryGetValue(url, out existingLink))
                {
                    var newExternalLink = new ExternalLink { Url = url };
                    siteDictionary.ExternalLinks.Add(url, newExternalLink);
                    page.ExternalLinks.Add(newExternalLink);
                }
                else
                {
                    page.ExternalLinks.Add((ExternalLink)existingLink);
                }
            }

            page.Visited = true;
        }

        public void Dispose()
        {
            _pageDownloader.Dispose();
        }

        private class SiteDictionary
        {
            public Dictionary<string, Page> Links { get; set; }

            public Dictionary<string, Image> Images { get; set; }

            public Dictionary<string, ExternalLink> ExternalLinks { get; set; }

            public SiteDictionary()
            {
                Links = new Dictionary<string, Page>();
                Images = new Dictionary<string, Image>();
                ExternalLinks = new Dictionary<string, ExternalLink>();
            }
        }
    }
}