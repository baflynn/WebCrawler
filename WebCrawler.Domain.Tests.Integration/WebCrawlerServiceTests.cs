using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebCrawler.Domain.Model;
using WebCrawler.Domain.Services;

namespace WebCrawler.Domain.Tests.Integration
{
    [TestFixture]
    public class WebCrawlerServiceTests
    {
        private WebCrawlerService _sut;
        private SiteMap _siteMap;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _sut = new WebCrawlerService();

            _siteMap = _sut.CrawlWebSite("http://wiprodigital.com/");
        }

        public class When_crawling_wiprodigital : WebCrawlerServiceTests
        {
            [Test]
            public void It_should_populate_root()
            {
                Assert.IsNotNull(_siteMap.Root);
            }

            [Test]
            public void It_should_set_TotalExternalLinks()
            {
                Assert.AreNotEqual(0, _siteMap.TotalExternalLinks);
            }

            [Test]
            public void It_should_set_TotalImages()
            {
                Assert.AreNotEqual(0, _siteMap.TotalImages);
            }

            [Test]
            public void It_should_set_TotalInternalLinks()
            {
                Assert.AreNotEqual(0, _siteMap.TotalInternalLinks);
            }
        }
    }
}