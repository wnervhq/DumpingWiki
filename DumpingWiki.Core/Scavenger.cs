using System;
using DumpingWiki.Core.Interface;
using DumpingWiki.Model;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;


namespace DumpingWiki.Core
{
    public class Scavenger : IReportScavenger
    {
        private readonly IFileService _fileService;
        private readonly IDataService _dataService;
        private readonly IReportService _reportService;
        private readonly IReportDataBuilderService _reportBuilder;

        public Scavenger(IFileService fileService, IDataService dataService, IReportService reportService, IReportDataBuilderService reportBuilder) 
        {
            _fileService = fileService;
            _dataService = dataService;
            _reportService = reportService;
            _reportBuilder = reportBuilder;
        }

        public void Process() 
        {
            var source = BuildDumpSource();

            _fileService.Download(source);

            _fileService.DecompressFiles(source.GetDirectory());
            
            var compiledData = _dataService.Extract(source.GetDirectory());

            var languageReportData = _reportBuilder.BuildLanguageDomainReport(compiledData);

            //var pageReportData = _reportBuilder.BuildPageCountReport(compiledData);

            _reportService.ShowLanguageDomainReport(languageReportData);

            //_reportService.ShowPageCountReport(pageReportData);

            Console.WriteLine("REACHED END OF PROGRAM");

        }

        private DumpSource BuildDumpSource() 
        {
            var sourceLink = GetSourceLink();

            var listNames = GetFileNames(sourceLink);

            var directoryPath = GetTemporaryDirectory();

            return new DumpSource(sourceLink, listNames, directoryPath);
        }

        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        private static string GetSourceLink()
        {
            var sMonth = DateTime.Now.ToString("MM");
            var sYear = DateTime.Now.ToString("yyyy");
            var linkWikiBase = "https://dumps.wikimedia.org/other/pageviews/";
            return $"{linkWikiBase}{sYear}/{sYear}-{sMonth}/";
        }

        public List<string> GetFileNames(string url)
        {
            List<string> webItemsByLine = new List<string>();
            WebClient web = new WebClient();
            String htmlSource = web.DownloadString(url);
            MatchCollection matchLines = Regex.Matches(htmlSource, @"<a href\s*(.+?)\s*gz</a>", RegexOptions.Singleline);

            foreach (Match m in matchLines)
            {
                string line = m.Groups[1].Value;
                int beginStringPosition = line.IndexOf("page");
                int endStringPosition = line.IndexOf(".gz");
                string fileName = line.Substring(beginStringPosition, endStringPosition + 1);
                webItemsByLine.Add(fileName);
            }
            var LstItems = webItemsByLine.Skip(Math.Max(0, webItemsByLine.Count() - 5)).Take(5);

            var fileNames = LstItems.ToList<string>();
            return fileNames;
        }

    }
}
