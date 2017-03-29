using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Hash;

namespace WatchDog.Snapshots
{
    public static class IResourceSnapshotExtensions
    {
        public static string Hash(this IDirectoryResourceSnapshot snapshot)
        {
            if (snapshot != null)
            {
                string files = string.Join("", snapshot.Files.Select(x => x.Hash));

                return WatchDogHashHelper.Compute(files);
            }

            return null;
        }
    }
}
