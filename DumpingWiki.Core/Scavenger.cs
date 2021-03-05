using System;
using DumpingWiki.Core.Interface;
using DumpingWiki.Model;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using DumpingWiki.Extensions;


namespace DumpingWiki.Core
{
    public class Scavenger : IReportScavenger
    {
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

        public void GetDownloadedFile(DataFile file)
        {
            WebClient web = new WebClient();

            Console.WriteLine(file.GetDirectory());

            foreach (var fileName in file.GetFileNames() )
            {
                string sourseLink = file.GetSourceLink()+fileName;
                string pathSave = file.GetDirectory()+"\\"+fileName;
                web.DownloadFile(sourseLink, pathSave);
                Console.WriteLine(fileName+" downloaded");
            }

            return ;

        }
        public void GetDataFile(string pathFiles)
        {
            DirectoryInfo directorySelected = new DirectoryInfo(pathFiles);
                        
            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
            {
                Decompress(fileToDecompress);
                File.Delete(fileToDecompress.FullName);
            }
          return;
        }

        public List<CompiledData> GetReportData(string pathFiles)
        {
            Console.WriteLine("Performing File ReadAllLines into array. Process with Parallel.For: ");

            char[] separator = { ' ' };
            string separatorDomainCode = ".";
            string separatorFileName = "-";
            int viewCount = 0;
            int responseSize = 0;
            int found = 0;

            var compiledList = new List<CompiledData>();

            try
            {
                DirectoryInfo directorySelected = new DirectoryInfo(pathFiles);

                foreach (FileInfo fileToRead in directorySelected.GetFiles("*.*"))
                {

                    var period = fileToRead.FullName.Split(separatorFileName);
                    var allLines = File.ReadAllLines(fileToRead.FullName);
                    Parallel.ForEach(allLines, (currentLine) =>
                    {
                        
                        var periodHour = period[2].Substring(0, 2);

                        string[] splitCurrentLine = currentLine.Split(separator);

                        var domainCode = splitCurrentLine[0];
                        var pageTitle = splitCurrentLine[1];
                        viewCount = Convert.ToInt32(splitCurrentLine[2]);
                        responseSize = Convert.ToInt32(splitCurrentLine[3]);
                        string language = null;
                        string domain = null;

                        found = domainCode.IndexOf(separatorDomainCode);
                        if (found < 0)
                        {
                            language = splitCurrentLine[0]; ;
                            domain = "Wikipedia";
                        }
                        else
                        {
                            string[] splitDomainCode = domainCode.Split(separatorDomainCode);
                            language = splitDomainCode.First();
                            domain = splitDomainCode.Last();

                            foreach (DomainData item in Domains.Get().Where(x => (x.GetCode() == domain)))
                            {
                                domain = item.GetName();
                            }
                        }
                        var compiledData = new CompiledData(domainCode, language, domain, pageTitle, viewCount, responseSize,periodHour, currentLine);
                        compiledList.Add(compiledData);
                    });
                }
                
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }
            Console.WriteLine("End readLines");

            return compiledList;
        }

        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }

        public void GetReport (List<string> fileNames, List<CompiledData> compiledData)
        {
            var currentPeriods = new List<string>();

            foreach (var name in fileNames )
            {
                var currentPeriod = name.Split('-');
                currentPeriods.Add(currentPeriod[2].Substring(0,2));
            }
                

            var languageAndDomainCount = (from a in compiledData
                                  group a by new { period = a.GetPeriod(), languaje = a.GetLanguage(), domain = a.GetDomain() } into grouped
                                  select new LanguajeDomainCount (grouped.Key.period, grouped.Key.languaje, grouped.Key.domain, grouped.Sum(x => x.GetViewCount()))
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

            Console.WriteLine("List of Languaje&Domain");

            foreach (var re in filteredLanguageDomainCount)
            {
                Console.WriteLine($"Period: {re.GetPeriod()}, Languaje: {re.GetLanguaje()}, " +
                    $"domain: {re.GetDomain()}, viewCount: {re.GetViewCount()}");
            }

            var page = (from a in compiledData
                        group a by new { period = a.GetPeriod(), page = a.GetPageTitle() } into grouped
                        select new
                        {
                            period = grouped.Key.period,
                            page = grouped.Key.page,
                            viewcount = grouped.Sum(x => x.GetViewCount())
                        }
                ).ToList();

            var orderResultPage = page.OrderBy(x => x.period)
                .ThenByDescending(x => x.viewcount)
                .ToList();

            Console.WriteLine("List of Page");

            foreach (var re in orderResultPage)
            {
                Console.WriteLine($"Period: {re.period}, Page: {re.page}, " +
                    $"viewCount: {re.viewcount}");
            }

            return;
        }
    }
}
