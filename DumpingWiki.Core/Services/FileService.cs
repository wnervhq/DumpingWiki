
using DumpingWiki.Core.Interface;
using DumpingWiki.Model;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace DumpingWiki.Core.Services
{
    public class FileService : IFileService
    {
        public void Download(DumpSource source)
        {
            WebClient web = new WebClient();

            Console.WriteLine($"Starting Download From: {source.GetDirectory()}");

            foreach (var fileName in source.GetFileNames())
            {
                string sourseLink = source.GetSourceLink() + fileName;
                string pathSave = source.GetDirectory() + "\\" + fileName;
                web.DownloadFile(sourseLink, pathSave);
                Console.WriteLine(fileName + " downloaded");
            }

            web.Dispose();
        }

        public void DecompressFiles(string pathFiles)
        {
            DirectoryInfo directorySelected = new DirectoryInfo(pathFiles);

            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
            {
                Decompress(fileToDecompress);
                File.Delete(fileToDecompress.FullName);
            }

        }

        private void Decompress(FileInfo fileToDecompress)
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

    }
}
