using System;
using System.Collections.Generic;
using System.Text;

namespace DumpingWiki.Model
{
public class CompiledData
    {
        public CompiledData
            (string domainCode, string language, string domain,string pageTitle, int viewCount, int responseSize, string period, string originalLine)
        {
                DomainCode   = domainCode;
                Language     = language;
                Domain       = domain;
                PageTitle    = pageTitle;
                ViewCount    = viewCount;
                ResponseSize = responseSize;
                Period       = period;
                OriginalLine = originalLine;
        }

        private string DomainCode { get; set; }
        private string Language { get; set; }
        private string Domain { get; set; }
        private string PageTitle { get; set; }
        private int ViewCount { get; set; }
        private int ResponseSize { get; set; }
        private string Period { get; set; }
        private string OriginalLine { get; set; }


        public string GetDomainCode ()
        {
            return DomainCode;
        }
        public string GetLanguage()
        {
            return Language;
        }
        public string GetDomain ()
        {
            return Domain;
        }
        public string GetPageTitle ()
        { 
            return PageTitle;
        }
        public int GetViewCount ()
        {
            return ViewCount;
        }
        public int GetResponseSize ()
        {
            return ResponseSize;
        }
        public string GetPeriod()
        {
            return Period;
        }
        public string GetOriginalLine()
        {
            return OriginalLine;
        }
    }
}
