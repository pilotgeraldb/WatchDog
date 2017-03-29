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

        //// Set the cache
        //public static void SetCache(string key, object data, DateTime time)
        //{
        //    MemoryCache.Default.Set(key, data, time);
        //}

        //// Get the cache
        //public static object GetCache(string key)
        //{
        //    return MemoryCache.Default.Get(key);
        //}

        public static void Cache(IResourceSnapshot snapshot)
        {
            if (!string.IsNullOrWhiteSpace(snapshot.Path))
            {
                lock (CacheLock)
                {
                    MemoryCache.Default.Set(snapshot.Path, snapshot, DateTimeOffset.MaxValue);
                }
            }
        }

        public static IResourceSnapshot Retrieve(string path, bool remove = false)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                lock (CacheLock)
                {
                    object result = MemoryCache.Default.Get(path);

                    return (result != null) ? (IResourceSnapshot)result : null;
                }
            }

            return null;
        }
    }
}
