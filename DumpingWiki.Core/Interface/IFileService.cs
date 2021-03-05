using DumpingWiki.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DumpingWiki.Core.Interface
{
    public interface IFileService
    {
        public void Download(DumpSource source);
        public void DecompressFiles(string pathFiles);
    }
}
