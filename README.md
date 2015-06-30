## Synopsis

A simple C# web crawler

## Code Example

To use
ICrawlWebSites crawler = new WebCrawlerService();
SiteMap sitemap = crawler. CrawlWebSite(“SomeUrl”);

## Tests

### Integration Tests 
Test the crawler against a real web-site.

### Unit Test
Test behaviour using stubbed web-sites. Obviously needs a lot more tests.
