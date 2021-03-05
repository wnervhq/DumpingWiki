using System;
using DumpingWiki.Core;
using DumpingWiki.Core.Services;

namespace DumpingWiki
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START DUMPING VIEWS FROM WIKIMEDIA - Version 1.0.0");
            Console.WriteLine("--------------------------------------------------");

            var fileService = new FileService();
            var dataService = new DataService();
            var consoleReportService = new ConsoleReportService();
            var reportBuilderService = new ReportDataBuilderService();

            var scavenger = new Scavenger(fileService, dataService, consoleReportService, reportBuilderService);
            
            scavenger.Process();

            Console.ReadLine();
        
        }
    }
}
