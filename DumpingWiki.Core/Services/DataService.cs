using DumpingWiki.Core.Interface;
using DumpingWiki.Extensions;
using DumpingWiki.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DumpingWiki.Core.Services
{
    public class DataService : IDataService
    {
        public List<CompiledData> Extract(string pathFiles)
        {
            Console.WriteLine("Extracting Data ");

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
                    Console.WriteLine($"Working on File: {fileToRead.Name}");

                    var period = fileToRead.FullName.Split(separatorFileName);
                    var allLines = File.ReadAllLines(fileToRead.FullName);
                    foreach (var currentLine in allLines)
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
                            var smallDomain = splitDomainCode.Last();
                            domain = Domains.Get().Where(x => x.GetCode() == smallDomain).FirstOrDefault().GetName();

                        }
                        var compiledData = new CompiledData(domainCode, language, domain, pageTitle, viewCount, responseSize, periodHour, currentLine);
                        compiledList.Add(compiledData);
                    };
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }

            return compiledList;
        }
    }
}
