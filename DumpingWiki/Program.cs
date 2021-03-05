using System;
using DumpingWiki.Core;
using DumpingWiki.Model;
using System.IO;


namespace DumpingWiki
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dumping pageviews from Wikipedia");

            var scavenger = new Scavenger();
            string sMonth = DateTime.Now.ToString("MM");
            string sYear = DateTime.Now.ToString("yyyy");
            string linkWikiBase = "https://dumps.wikimedia.org/other/pageviews/";
            string sourceLink = linkWikiBase+sYear+"/"+sYear+"-"+sMonth+"/";

            var listNames = scavenger.GetFileNames(sourceLink);

            string directoryPath = GetTemporaryDirectory();
            var dataFiles = new DataFile(sourceLink, listNames, directoryPath);
            scavenger.GetDownloadedFile(dataFiles);
            scavenger.GetDataFile(directoryPath);

            var compiledData = scavenger.GetReportData(directoryPath);

            scavenger.GetReport(dataFiles.GetFileNames(), compiledData);

            Console.ReadLine();
    }
        
        public static string GetTemporaryDirectory() 
        { 
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()); 
            Directory.CreateDirectory(tempDirectory); 
            return tempDirectory; 
        }

    }
}
