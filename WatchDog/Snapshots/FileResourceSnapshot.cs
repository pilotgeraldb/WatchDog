using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WatchDog.Files;

namespace WatchDog.Snapshots
{
    public sealed class FileResourceSnapshot : IResourceSnapshot, IFileResourceSnapshot
    {
        public FileResourceSnapshot(string path)
        {
            Initialize(path);
        }

        public string Path { get; private set; }
        public IFileItem File { get; private set; }
        public DateTime? Created { get; private set; }

        public static FileResourceSnapshot New(string path)
        {
            return new FileResourceSnapshot(path);
        }

        private void Initialize(string path)
        {
            Path = path;
            Created = DateTime.Now;
            File = new FileItem(path);
        }

        public string Hash
        {
            get
            {
                return File.Hash;
            }
        }
    }
}
