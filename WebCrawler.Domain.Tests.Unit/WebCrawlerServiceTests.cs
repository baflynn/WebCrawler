using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebCrawler.Domain.Model;
using WebCrawler.Domain.Services;
using WebCrawler.Domain.Tests.Unit.Stubs;

namespace WebCrawler.Domain.Tests.Unit
{
    public class WebCrawlerServiceTests
    {
        private WebCrawlerService _sut;
        private SiteMap _siteMap;

        [SetUp]
        public void SetUp()
        {
            _sut = new WebCrawlerService(new TestWebPageDownloader(), new SimpleWebPageParser());

            _siteMap = _sut.CrawlWebSite("http://www.testwebsite.com/");
        }

        public class When_crawling : WebCrawlerServiceTests
        {
            [Test]
            public void It_should_populate_root()
            {
                Assert.IsNotNull(_siteMap.Root);
            }

            [Test]
            public void It_should_set_TotalExternalLinks()
            {
                Assert.AreEqual(1, _siteMap.TotalExternalLinks);
            }

            [Test]
            public void It_should_set_TotalImages()
            {
                Assert.AreEqual(1, _siteMap.TotalImages);
            }

            [Test]
            public void It_should_set_TotalInternalLinks()
            {
                Assert.AreEqual(1, _siteMap.TotalInternalLinks);
            }
        }
    }
}