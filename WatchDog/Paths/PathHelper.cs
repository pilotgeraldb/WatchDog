using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;

namespace WatchDog.Paths
{
    public class PathHelper
    {
        public static ResourceType GetResourceType(string path)
        {
            if (Path.HasExtension(path))
            {
                return ResourceType.File;
            }

            return ResourceType.Directory;
        }
    }
}
