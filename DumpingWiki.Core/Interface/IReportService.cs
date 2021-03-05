using DumpingWiki.Model.View;
using System.Collections.Generic;

namespace DumpingWiki.Core.Interface
{
    public interface IReportService
    {
        void ShowLanguageDomainReport(List<LanguajeDomainCount> list);
        void ShowPageCountReport(List<LanguajePageMaxCount> list);
    }
}
