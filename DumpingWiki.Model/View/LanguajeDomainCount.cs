using System;
using System.Collections.Generic;
using System.Text;

namespace DumpingWiki.Model.View
{
    public class LanguajeDomainCount
    {
        public LanguajeDomainCount
            (string period, string languaje, string domain, int viewCount)
        {
            Period = period;
            Languaje = languaje;
            Domain = domain;
            ViewCount = viewCount;
        }
        private string Period { get; set; }
        private string Languaje { get; set; }
        private string Domain { get; set; }
        private int ViewCount { get; set; }

        public string GetPeriod()
        {
            return Period;
        }
        public string GetLanguaje()
        {
            return Languaje;
        }
        public string GetDomain()
        {
            return Domain;
        }
        public int GetViewCount()
        {
            return ViewCount;
        }
    }
}
