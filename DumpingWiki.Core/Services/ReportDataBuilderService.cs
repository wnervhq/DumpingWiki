using DumpingWiki.Core.Interface;
using DumpingWiki.Model;
using DumpingWiki.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DumpingWiki.Core.Services
{
    public class ReportDataBuilderService : IReportDataBuilderService
    {
        public List<LanguajeDomainCount> BuildLanguageDomainReport(List<CompiledData> compiledData)
        {
            Console.WriteLine("Building Language Domain Report - Start");

            var currentPeriods = compiledData.Select(x => x.GetPeriod()).Distinct();

            var languageAndDomainCount = (from a in compiledData
                                          group a by new { period = a.GetPeriod(), languaje = a.GetLanguage(), domain = a.GetDomain() } into grouped
                                          select new LanguajeDomainCount(grouped.Key.period, grouped.Key.languaje, grouped.Key.domain, grouped.Sum(x => x.GetViewCount()))
                ).ToList();

            var orderResultLanguajeDomain = languageAndDomainCount.OrderBy(x => x.GetPeriod())
                .ThenByDescending(x => x.GetViewCount())
                .ToList();

            var filteredLanguageDomainCount = new List<LanguajeDomainCount>();

            foreach (var currentPeriod in currentPeriods)
            {
                var languageDomainWithMaxCount = languageAndDomainCount.Where(x => x.GetPeriod() == currentPeriod).FirstOrDefault();

                filteredLanguageDomainCount.Add(languageDomainWithMaxCount);
            }
            Console.WriteLine($"Building Language Domain Report - End - {filteredLanguageDomainCount.Count()} Rows");

            return filteredLanguageDomainCount;
        }

        public List<LanguajePageMaxViewCount> BuildPageCountReport(List<CompiledData> compiledData)
        {
            Console.WriteLine("Building Page Count Report - Start");

            var page = (from a in compiledData
                        group a by new { period = a.GetPeriod(), page = a.GetPageTitle() } into grouped
                        select new LanguajePageMaxViewCount(grouped.Key.period, grouped.Key.page, grouped.Sum(x => x.GetViewCount()))).ToList();

            var result = page.OrderBy(x => x.GetPeriod())
                .ThenByDescending(x => x.GetViewCount())
                .ToList();

            Console.WriteLine($"Building Page Count Report - End - {result.Count()} Rows");

            return result;
        }
    }
}
