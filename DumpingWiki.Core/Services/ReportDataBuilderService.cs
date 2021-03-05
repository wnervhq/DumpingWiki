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
                var languageDomainWithMaxCount = orderResultLanguajeDomain.Where(x => x.GetPeriod() == currentPeriod).FirstOrDefault();

                filteredLanguageDomainCount.Add(languageDomainWithMaxCount);
            }
            Console.WriteLine($"Building Language Domain Report - End - {filteredLanguageDomainCount.Count()} Rows");

            return filteredLanguageDomainCount;
        }

        public List<LanguajePageMaxCount> BuildPageCountReport(List<CompiledData> compiledData)
        {
            Console.WriteLine("Building Page Count Report - Start");

            var currentPeriods = compiledData.Select(x => x.GetPeriod()).Distinct();

            var languajePageMaxViewCount = (from a in compiledData
                        group a by new { period = a.GetPeriod(), page = a.GetPageTitle() } into grouped
                        select new LanguajePageMaxCount(grouped.Key.period, grouped.Key.page, grouped.Sum(x => x.GetViewCount()))).ToList();

            var orderLanguajePage = languajePageMaxViewCount.OrderBy(x => x.GetPeriod())
                .ThenByDescending(x => x.GetViewCount())
                .ToList();

            var filteredLanguagePage = new List<LanguajePageMaxCount>();

            foreach (var currentPeriod in currentPeriods)
            {
                var languagePageWithMaxCount = orderLanguajePage.Where(x => x.GetPeriod() == currentPeriod).FirstOrDefault();

                filteredLanguagePage.Add(languagePageWithMaxCount);
            }
            Console.WriteLine($"Building Page Count Report - End - {filteredLanguagePage.Count()} Rows");

            return filteredLanguagePage;
        }
    }
}
