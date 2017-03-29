using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;
using WatchDog.Paths;

namespace WatchDog.Snapshots
{
    public sealed class ResourceSnapshotFactory
    {
        public static IResourceSnapshot GetSnapshot(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (PathHelper.GetResourceType(path) == ResourceType.Directory)
                {
                    return DirectoryResourceSnapshot.New(path);
                }
                else
                {
                    return FileResourceSnapshot.New(path);
                }
            }

            return null;
        }
    }
}
