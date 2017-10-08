using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;
using WatchDog.Caching;
using WatchDog.Snapshots;
using WatchDog.Comparison;

namespace WatchDog.Paths
{
    public sealed class PathProcessor : IPathProcessor
    {
        private string Path { get; set; }
        public IResourceComparator Comparator { get; set; }

        public PathProcessor(string path)
        {
            Path = path;
            Comparator = new DirectoryComparator();
        }

        public IChangeSet Run()
        {
            IResourceSnapshot previousSnapshot = SnapshotCache.Retrieve(Path);
            IResourceSnapshot currentSnapshot = ResourceSnapshotFactory.GetSnapshot(Path);

            SnapshotCache.Cache(currentSnapshot);

            if (previousSnapshot == null)
            {
                previousSnapshot = currentSnapshot;
            }

            IChangeSet changeSet = Comparator.Run(currentSnapshot, previousSnapshot);

            return changeSet;
        }
    }
}