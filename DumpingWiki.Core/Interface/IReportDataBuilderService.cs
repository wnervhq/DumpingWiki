using DumpingWiki.Model;
using DumpingWiki.Model.View;
using System.Collections.Generic;

namespace DumpingWiki.Core.Interface
{
    public interface IReportDataBuilderService
    {
        List<LanguajeDomainCount> BuildLanguageDomainReport(List<CompiledData> compiledData);
        List<LanguajePageMaxViewCount> BuildPageCountReport(List<CompiledData> compiledData);
    }
}
