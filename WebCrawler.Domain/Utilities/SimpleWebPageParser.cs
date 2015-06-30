using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebCrawler.Domain.Model;

namespace WebCrawler.Domain.Services
{
    public class SimpleWebPageParser : IParseWebPages
    {
        private const string AnchorRegex = @"(<a.*?>)";
        private const string HrefRegex = @"href=\""(.*?)\""";
        private const string ImageRegex = @"(<img.*?>)";
        private const string SrcRegex = @"src=\""(.*?)\""";
        private const string DomainRegex = @".*([^\.]+)(com)$";
        private const string DotCom = ".com";

        private string _rootUrl;
        private string _domain;

        public void SetRootUrl(string rootUrl)
        {
            _rootUrl = rootUrl.ToLower();

            Uri uri = new Uri(rootUrl);
            _domain = uri.Host.ToLower();

            if (_domain.Contains(DotCom))
            {
                _domain = _domain.Replace(DotCom, string.Empty);
            }
        }

        public FlatPage Parse(string pageContent, string pageUrl)
        {
            if (string.IsNullOrWhiteSpace(_domain))
            {
                throw new ArgumentException("Please invoke SetRootUrl before parsing");
            }

            var newPage = new FlatPage { Url = pageUrl };

            ParseLinks(pageContent, newPage);

            ParseImages(pageContent, newPage);

            return newPage;
        }

        private void ParseLinks(string pageContent, FlatPage page)
        {
            MatchCollection anchors = Regex.Matches(pageContent, AnchorRegex, RegexOptions.Singleline);

            foreach (Match anchor in anchors)
            {
                Match href = Regex.Match(anchor.Groups[1].Value, HrefRegex, RegexOptions.Singleline);

                if (href.Success)
                {
                    ParseHref(page, href);
                }
            }
        }

        private void ParseHref(FlatPage page, Match href)
        {
            string url = href.Groups[1].Value;

            if (url.ToLower() == _rootUrl)
            {
                return;
            }

            if (url.StartsWith("#") || url.StartsWith("/#"))
            {
                return;
            }

            if (url.Contains(_domain) || url.StartsWith("/"))
            {
                if (!page.Links.ContainsKey(url))
                {
                    page.Links.Add(url, url);
                }
            }
            else
            {
                if (!page.ExternalLinks.ContainsKey(url))
                {
                    page.ExternalLinks.Add(url, url);
                }
            }
        }

        private void ParseImages(string pageContent, FlatPage page)
        {
            MatchCollection images = Regex.Matches(pageContent, ImageRegex, RegexOptions.Singleline);

            foreach (Match image in images)
            {
                Match href = Regex.Match(image.Groups[1].Value, SrcRegex, RegexOptions.Singleline);

                if (href.Success)
                {
                    string url = href.Groups[1].Value;

                    if (!page.Images.ContainsKey(url))
                    {
                        page.Images.Add(url, url);
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}