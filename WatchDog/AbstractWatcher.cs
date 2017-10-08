using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;
using WatchDog.Paths;

namespace WatchDog
{
    public abstract class AbstractWatcher : IWatcher
    {
        public string Path { get; set; }

        public bool IsProcessing { get; set; }

        public bool IsRunning { get; set; }

        public Action<IChangeSet> OnChange { get; set; }

        public IPathProcessor Processor { get { return new PathProcessor(Path); } }

        public virtual bool Start()
        {
            IsRunning = true;

            return true;
        }

        public virtual bool Stop()
        {
            IsRunning = false;

            return true;
        }
    }
}
