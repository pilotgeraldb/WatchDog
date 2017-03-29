using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WatchDog.Files;
using WatchDog.Changes;
using WatchDog.Snapshots;

namespace WatchDog.Comparison
{
    public sealed class FileComparator : IResourceComparator
    {
        public FileComparator()
        {
            
        }

        public IChangeSet Run(IResourceSnapshot snapshotA, IResourceSnapshot snapshotB)
        {
            ChangeSet result = new ChangeSet();

            IFileResourceSnapshot A = null;
            IFileResourceSnapshot B = null;

            if (snapshotA != null)
            {
                if (snapshotA is IFileResourceSnapshot)
                {
                    A = (IFileResourceSnapshot)snapshotA;
                }
            }

            if (snapshotB != null)
            {
                if (snapshotB is IFileResourceSnapshot)
                {
                    B = (IFileResourceSnapshot)snapshotB;
                }
            }

            if (A != null && B == null)
            {
                ChangeItem item = new ChangeItem()
                {
                    ResourceType = ResourceType.File,
                    ChangeType = ChangeType.Deleted,
                    FilePath = A.File.FilePath
                };

                result.ChangeItems.Add(item);
            }
            else if (A == null && B != null)
            {
                ChangeItem item = new ChangeItem()
                {
                    ResourceType = ResourceType.File,
                    ChangeType = ChangeType.Created,
                    FilePath = B.File.FilePath
                };

                result.ChangeItems.Add(item);
            }
            else if (A.File.Hash != B.File.Hash)
            {
                ChangeItem item = new ChangeItem()
                {
                    ResourceType = ResourceType.File,
                    ChangeType = ChangeType.Binary,
                    FilePath = B.File.FilePath
                };

                result.ChangeItems.Add(item);
            }

            return result;
        }        
    }
}