using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;
using WatchDog.Paths;

namespace WatchDog.Comparison
{
    public sealed class ResourceComparatorFactory
    {
        public static IResourceComparator GetComparator(string path)
        {
            if (PathHelper.GetResourceType(path) == ResourceType.Directory)
            {
                return new DirectoryComparator();
            }
            else
            {
                return new FileComparator();
            }
        }
    }
}