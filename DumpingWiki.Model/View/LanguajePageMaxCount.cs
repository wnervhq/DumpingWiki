using System;
using System.Collections.Generic;
using System.Text;

namespace DumpingWiki.Model.View
{
    public class LanguajePageMaxCount
    {
        public LanguajePageMaxCount
            (string period, string page, int viewCount)
        {
            Period = period;
            Page = page;
            ViewCount = viewCount;
        }
        private string Period { get; set; }
        private string Page { get; set; }
        private int ViewCount { get; set; }

        public string GetPeriod()
        {
            return Period;
        }
        public string GetPage()
        {
            return Page;
        }
        public int GetViewCount()
        {
            return ViewCount;
        }
    }
}
