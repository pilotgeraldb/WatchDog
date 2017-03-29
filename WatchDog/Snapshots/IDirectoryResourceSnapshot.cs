using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Files;

namespace WatchDog.Snapshots
{
    public interface IDirectoryResourceSnapshot
    {
        string Path { get; }
        IList<IFileItem> Files { get; }
        IList<string> Directories { get; }
        DirectoryInfo DirectoryInfo { get; }
        DateTime? Created { get; }
    }
}
