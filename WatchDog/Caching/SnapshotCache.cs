using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Snapshots;

namespace WatchDog.Caching
{
    public sealed class SnapshotCache
    {
        private static readonly object CacheLock = new object();
        private static MemoryCache cache = new MemoryCache("SnapshotCache");

        public static void Cache(IResourceSnapshot snapshot)
        {
            if (!string.IsNullOrWhiteSpace(snapshot.Path))
            {
                lock (CacheLock)
                {
                    cache.Set(snapshot.Path, snapshot, DateTimeOffset.MaxValue);
                }
            }
        }

        public static IResourceSnapshot Retrieve(string path, bool remove = false)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                lock (CacheLock)
                {
                    object result = cache.Get(path);

                    return (result != null) ? (IResourceSnapshot)result : null;
                }
            }

            return null;
        }
    }
}
