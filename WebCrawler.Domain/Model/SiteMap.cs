using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Domain.Model
{
    public class SiteMap
    {
        public Page Root { get; set; }

        public int TotalInternalLinks { get; set; }

        public int TotalImages { get; set; }

        public int TotalExternalLinks { get; set; }
    }
}