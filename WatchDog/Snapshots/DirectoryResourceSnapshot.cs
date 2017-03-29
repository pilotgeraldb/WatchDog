using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WatchDog.Files;
using WatchDog.Hash;

namespace WatchDog.Snapshots
{
    public sealed class DirectoryResourceSnapshot : IResourceSnapshot, IDirectoryResourceSnapshot
    {
        public DirectoryResourceSnapshot(string path)
        {
            Initialize(path);
        }

        public string Path { get; private set; }
        public IList<IFileItem> Files { get; private set; }
        public IList<string> Directories { get; private set; }
        public DirectoryInfo DirectoryInfo { get; private set; }
        public DateTime? Created { get; private set; }

        public static DirectoryResourceSnapshot New(string path)
        {
            return new DirectoryResourceSnapshot(path);
        }

        private void Initialize(string path)
        {
            Path = path;
            Created = DateTime.Now;
            Files = new List<IFileItem>();
            Directories = new List<string>();

            GetFiles();
            GetDirectories();
        }

        private void GetFiles()
        {
            if (!string.IsNullOrWhiteSpace(Path))
            {
                DirectoryInfo = new DirectoryInfo(Path);

                if (Directory.Exists(Path))
                {
                    List<string> filePaths = Directory.GetFiles(Path).ToList();

                    foreach (string path in filePaths)
                    {
                        FileItem item = new FileItem(path);
                        Files.Add(item);
                    }
                }
            }
        }

        private void GetDirectories()
        {
            if (!string.IsNullOrWhiteSpace(Path))
            {
                if (Directory.Exists(Path))
                {
                    Directories = Directory.GetDirectories(Path).ToList();
                }
            }
        }
    }
}
