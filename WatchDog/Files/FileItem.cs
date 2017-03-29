using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WatchDog.Hash;

namespace WatchDog.Files
{
    public class FileItem : IFileItem
    {
        public FileItem(string filePath)
        {
            FilePath = filePath;
            FileInfo = new FileInfo(filePath);

            Hash = ComputeHash();
        }

        public string FilePath { get; private set; }
        public FileInfo FileInfo { get; private set; }
        public string Hash { get; private set; }

        public string ComputeHash()
        {
            string hash = "";

            using (FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                hash = WatchDogHashHelper.Compute(stream);
            }

            return hash;
        }
    }
}
