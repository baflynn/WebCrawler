using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Domain.Model
{
    public class Page : Resource
    {
        public Page()
        {
            InternalLinks = new List<Page>();
            Images = new List<Image>();
            ExternalLinks = new List<ExternalLink>();
        }

        public List<Page> InternalLinks { get; set; }

        public List<Image> Images { get; set; }

        public List<ExternalLink> ExternalLinks { get; set; }

        public bool Visited { get; set; }
    }
}