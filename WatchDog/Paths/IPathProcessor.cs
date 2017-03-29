using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatchDog.Changes;
using WatchDog.Comparison;

namespace WatchDog.Paths
{
    public interface IPathProcessor
    {
        IResourceComparator Comparator { get; }
        IChangeSet Run();
    }
}