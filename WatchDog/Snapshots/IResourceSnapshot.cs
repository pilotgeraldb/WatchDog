using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDog.Snapshots
{
    public interface IResourceSnapshot
    {
        string Path { get; }
        DateTime? Created { get; }
    }
}
