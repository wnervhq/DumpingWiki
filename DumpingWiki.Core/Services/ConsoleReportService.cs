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
        string LanguageDomainHeader = "\t Period \t Language \t Domain \t ViewCount";
        string LanguagePageMaxHeader = "\t Period \t Page \t\t ViewCount";
        public void ShowLanguageDomainReport(List<LanguajeDomainCount> list)
        {
            Console.WriteLine("\n Language and Domain Count");
            Console.WriteLine("-------------------------");
            Console.WriteLine(LanguageDomainHeader);
            foreach (var row in list)
            {
                Console.WriteLine($"\t {row.GetPeriod()} \t\t {row.GetLanguaje()} \t " +
                    $"\t {row.GetDomain()} \t {row.GetViewCount()}");
            }
            Console.WriteLine("\n\n");
        }

        public void ShowPageCountReport(List<LanguajePageMaxCount> list)
        {
            Console.WriteLine("Language Max Page Count");
            Console.WriteLine("-------------------------");
            Console.WriteLine(LanguagePageMaxHeader);
            foreach (var row in list)
            {
                Console.WriteLine($"\t {row.GetPeriod()} \t\t {row.GetPage()}, " +
                    $"\t {row.GetViewCount()}");
            }
            Console.WriteLine("\n\n");
        }
    }
}
