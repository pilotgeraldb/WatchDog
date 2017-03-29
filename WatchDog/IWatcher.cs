using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WatchDog.Changes;
using WatchDog.Paths;

namespace WatchDog
{
    public interface IWatcher
    {
        string Path { get; }
        bool IsRunning { get; }
        bool IsProcessing { get; }
        Action<IChangeSet> OnChange { get; set; }
        IPathProcessor Processor { get; }
        bool Start();
        bool Stop(); 
    }
}