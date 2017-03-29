using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WatchDog.Files;
using WatchDog.Changes;
using WatchDog.Snapshots;
using WatchDog.Paths;

namespace WatchDog.Comparison
{
    public sealed class DirectoryComparator : IResourceComparator
    {
        public DirectoryComparator()
        {
            
        }

        public IChangeSet Run(IResourceSnapshot snapshotA, IResourceSnapshot snapshotB)
        {
            ChangeSet result = new ChangeSet();

            IDirectoryResourceSnapshot A = null;
            IDirectoryResourceSnapshot B = null;

            if (snapshotA != null)
            {
                if (snapshotA is IDirectoryResourceSnapshot)
                {
                    A = (IDirectoryResourceSnapshot)snapshotA;
                }
            }

            if (snapshotB != null)
            {
                if (snapshotA is IDirectoryResourceSnapshot)
                {
                    B = (IDirectoryResourceSnapshot)snapshotB;
                }
            }

            if (snapshotA == null && snapshotB != null)
            {
                ChangeItem item = new ChangeItem()
                {
                    ResourceType = ResourceType.Directory,
                    ChangeType = ChangeType.Deleted,
                    FilePath = B.Path,
                    Hash = B.Hash()
                };

                result.ChangeItems.Add(item);

                return result;
            }
            else if (snapshotA != null && snapshotB == null)
            {
                ChangeItem item = new ChangeItem()
                {
                    ResourceType = ResourceType.Directory,
                    ChangeType = ChangeType.Created,
                    FilePath = A.Path,
                    Hash = A.Hash()
                };

                result.ChangeItems.Add(item);

                return result;
            }

            IEnumerable<IFileItem> ma = FilesMissingFromA(A, B);
            IEnumerable<IFileItem> mb = FilesMissingFromB(A, B);
            IEnumerable<string> mda = DirectoriesMissingFromA(A, B);
            IEnumerable<string> mdb = DirectoriesMissingFromB(A, B);
            IEnumerable<IFileItem> changedFiles = FilesChanged(A, B);

            foreach (IFileItem f in ma)
            {
                ChangeItem item = new ChangeItem()
                {
                    ResourceType = ResourceType.File,
                    ChangeType = ChangeType.Deleted,
                    FilePath = f.FilePath,
                    Hash = f.Hash
                };
                result.ChangeItems.Add(item);
            }

            foreach (IFileItem f in mb)
            {
                ChangeItem item = new ChangeItem()
                {
                    ResourceType = ResourceType.File,
                    ChangeType = ChangeType.Created,
                    FilePath = f.FilePath,
                    Hash = f.Hash
                };
                result.ChangeItems.Add(item);
            }

            foreach (string d in mda)
            {
                ChangeItem item = new ChangeItem()
                {
                    ResourceType = ResourceType.Directory,
                    ChangeType = ChangeType.Deleted,
                    FilePath = d,
                    Hash = null
                };
                result.ChangeItems.Add(item);
            }

            foreach (string d in mdb)
            {
                ChangeItem item = new ChangeItem()
                {
                    ResourceType = ResourceType.Directory,
                    ChangeType = ChangeType.Created,
                    FilePath = d,
                    Hash = null
                };
                result.ChangeItems.Add(item);
            }

            foreach (IFileItem f in changedFiles)
            {
                bool addedOrRemoved = result.ChangeItems
                    .Where(x => x.FilePath == f.FilePath)
                    .Any();

                if (!addedOrRemoved)
                {
                    ChangeItem item = new ChangeItem()
                    {
                        ResourceType = ResourceType.File,
                        ChangeType = ChangeType.Binary,
                        FilePath = f.FilePath,
                        Hash = f.Hash
                    };
                    result.ChangeItems.Add(item);
                }
            }

            result = CheckForRenaming(result);

            return result;
        }

        private ChangeSet CheckForRenaming(ChangeSet set)
        {
            //rename = same directory, same hash, different name
            IList<ChangeItem> changes = new List<ChangeItem>();

            foreach (ChangeItem c in set.ChangeItems)
            {
                string dir = Path.GetDirectoryName(c.FilePath);

                bool created = set.ChangeItems
                    .Where(x => x.Hash == c.Hash && Path.GetDirectoryName(x.FilePath) == dir)
                    .Where(x => x.ChangeType == ChangeType.Created)
                    .Any();

                bool deleted = set.ChangeItems
                    .Where(x => x.Hash == c.Hash && Path.GetDirectoryName(x.FilePath) == dir)
                    .Where(x => x.ChangeType == ChangeType.Deleted)
                    .Any();

                bool renamed = created && deleted;

                if (renamed)
                {
                    bool renameDetected = changes
                    .Where(x => x.Hash == c.Hash && x.ChangeType == ChangeType.Renamed)
                    .Any();

                    if (!renameDetected)
                    {
                        ResourceType type = PathHelper.GetResourceType(c.FilePath);

                        changes.Add(new ChangeItem()
                        {
                            ChangeType = ChangeType.Renamed,
                            FilePath = c.FilePath,
                            Hash = c.Hash,
                            ResourceType = type
                        });
                    }
                }
                else
                {
                    changes.Add(c);
                }

            }

            return new ChangeSet() { ChangeItems = changes };
        }

        private IEnumerable<IFileItem> FilesChanged(IDirectoryResourceSnapshot A, IDirectoryResourceSnapshot B)
        {
            List<IFileItem> filesChanged = new List<IFileItem>();

            IEnumerable<string> AFileHashes = A.Files.Select(x => x.Hash);

            foreach (IFileItem f in B.Files)
            {
                if (!AFileHashes.Contains(f.Hash))
                {
                    filesChanged.Add(f);
                }
            }

            return filesChanged;
        }

        private IEnumerable<IFileItem> FilesMissingFromA(IDirectoryResourceSnapshot A, IDirectoryResourceSnapshot B)
        {
            List<IFileItem> filesMissingFromA = new List<IFileItem>();

            IEnumerable<string> AFileNames = A.Files.Select(x => x.FilePath);

            foreach (IFileItem f in B.Files)
            {
                if (!AFileNames.Contains(f.FilePath))
                {
                    filesMissingFromA.Add(f);
                }
            }

            return filesMissingFromA;
        }

        private IEnumerable<IFileItem> FilesMissingFromB(IDirectoryResourceSnapshot A, IDirectoryResourceSnapshot B)
        {
            List<IFileItem> filesMissingFromB = new List<IFileItem>();

            IEnumerable<string> BFileNames = B.Files.Select(x => x.FilePath);

            foreach (IFileItem f in A.Files)
            {
                if (!BFileNames.Contains(f.FilePath))
                {
                    filesMissingFromB.Add(f);
                }
            }

            return filesMissingFromB;
        }

        private IEnumerable<string> DirectoriesMissingFromA(IDirectoryResourceSnapshot A, IDirectoryResourceSnapshot B)
        {
            List<string> directoriesMissingFromA = new List<string>();

            foreach (string d in B.Directories)
            {
                if (!A.Directories.Contains(d))
                {
                    directoriesMissingFromA.Add(d);
                }
            }

            return directoriesMissingFromA;
        }

        private IEnumerable<string> DirectoriesMissingFromB(IDirectoryResourceSnapshot A, IDirectoryResourceSnapshot B)
        {
            List<string> directoriesMissingFromB = new List<string>();

            foreach (string d in A.Directories)
            {
                if (!B.Directories.Contains(d))
                {
                    directoriesMissingFromB.Add(d);
                }
            }

            return directoriesMissingFromB;
        }
    }
}