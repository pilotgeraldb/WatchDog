using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;
using WatchDog.Snapshots;

namespace WatchDog.Comparison
{
    public interface IResourceComparator
    {
        IChangeSet Run(IResourceSnapshot snapshotA, IResourceSnapshot snapshotB);
    }
}