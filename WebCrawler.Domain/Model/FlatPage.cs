using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Domain.Model
{
    public class FlatPage : Resource
    {
        public Dictionary<string, string> Links { get; set; }

        public Dictionary<string, string> Images { get; set; }

        public Dictionary<string, string> ExternalLinks { get; set; }

        public FlatPage()
        {
            Links = new Dictionary<string, string>();
            Images = new Dictionary<string, string>();
            ExternalLinks = new Dictionary<string, string>();
        }
    }
}