using System;
using System.Collections.Generic;

namespace DumpingWiki.Model
{
    public class DumpSource
    {
        public DumpSource(string sourceLink, List<string> fileNames, string directory)
        {
            Id = Guid.NewGuid();
            SourceLink = sourceLink;
            FileNames = fileNames;
            Directory = directory;
        }

        private Guid Id { get; set; }
        private string SourceLink { get; set; }
        private List<string> FileNames { get; set; }
        private string Directory { get; set; }

        public Guid GetID()
        {
            return Id;
        }
        public string GetSourceLink()
        {
            return SourceLink;
        }
        public List<string> GetFileNames()
        {
            return FileNames;
        }
        public string GetDirectory()
        {
            return Directory;
        }
    }
}
