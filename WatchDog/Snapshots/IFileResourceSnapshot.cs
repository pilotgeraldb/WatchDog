using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Files;

namespace WatchDog.Snapshots
{
    public interface IFileResourceSnapshot
    {
        string Path { get; }
        IFileItem File { get; }
        DateTime? Created { get; }
    }
}
