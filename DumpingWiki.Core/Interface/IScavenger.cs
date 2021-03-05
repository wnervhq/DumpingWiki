using DumpingWiki.Model;
using System.Collections.Generic;

namespace DumpingWiki.Core.Interface
{
    interface IReportScavenger
    {
        public List<string> GetFileNames(string url);
        public void GetDownloadedFile(DataFile file);
        public void GetDataFile(string pathFiles);
        public List<CompiledData> GetReportData(string pathFiles);
        public void GetReport(List<string> fileNames, List<CompiledData> compiledData );

    }
}
