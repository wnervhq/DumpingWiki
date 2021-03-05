using System;
using System.Collections.Generic;
using System.Text;

namespace DumpingWiki.Model.View
{
    public class LanguajePageMaxViewCount
    {
        public LanguajePageMaxViewCount
            (string period, string page, string viewCount)
        {
            Period = period;
            Page = page;
            ViewCount = viewCount;
        }
        private string Period { get; set; }
        private string Page { get; set; }
        private string ViewCount { get; set; }

        public string GetPeriod()
        {
            return Period;
        }
        public string GetPage()
        {
            return Page;
        }
        public string GetViewCount()
        {
            return ViewCount;
        }
    }
}
