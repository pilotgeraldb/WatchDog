using System;
using System.Collections.Generic;
using System.Linq;

namespace WatchDog
{
    public sealed class WatchDogHost
    {
        public IList<IWatcher> Watchers { get; private set; }

        public WatchDogHost()
        {
            Watchers = new List<IWatcher>();
        }

        public WatchDogHost AddWatcher(IWatcher watcher)
        {
            if (watcher != null)
            {
                Watchers.Add(watcher);
            }

            return this;
        }

        public WatchDogHost Start()
        {
            foreach (IWatcher watcher in Watchers)
            {
                watcher.Start();
            }

            return this;
        }

        public WatchDogHost Stop()
        {
            foreach (IWatcher watcher in Watchers)
            {
                watcher.Stop();
            }

            return this;
        }
    }
}
