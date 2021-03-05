using DumpingWiki.Core.Interface;
using DumpingWiki.Model;
using DumpingWiki.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DumpingWiki.Core.Services
{
    public class ConsoleReportService : IReportService
    {

        public void ShowLanguageDomainReport(List<LanguajeDomainCount> list)
        {
            Console.WriteLine("Language and Domain Count");
            Console.WriteLine("-------------------------");
            foreach (var row in list)
            {
                Console.WriteLine($"Period: {row.GetPeriod()}, Languaje: {row.GetLanguaje()}, " +
                    $"domain: {row.GetDomain()}, viewCount: {row.GetViewCount()}");
            }
        }

        public void ShowPageCountReport(List<LanguajePageMaxViewCount> list)
        {
            Console.WriteLine("Language Max Page Count");
            Console.WriteLine("-------------------------");
            foreach (var row in list)
            {
                Console.WriteLine($"Period: {row.GetPeriod()}, Page: {row.GetPage()}, " +
                    $"viewCount: {row.GetViewCount()}");
            }
        }
    }
}
