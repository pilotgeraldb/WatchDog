using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using WatchDog.Changes;

namespace WatchDog
{
    public class PollingWatcher : AbstractWatcher, IPollable
    {
        public Timer Timer { get; set; }
        public double PollInterval { get; set; }
        
        public PollingWatcher(string path)
        {
            Path = path;
            PollInterval = 100;
            Timer = new Timer(PollInterval);
        }

        public PollingWatcher(string path, double interval)
        {
            Path = path;
            PollInterval = interval;
            Timer = new Timer(interval);
        }

        public void Tick(object sender, ElapsedEventArgs e)
        {
            if (!IsProcessing)
            {
                IsProcessing = true;

                IChangeSet changeSet = Processor.Run();

                if (OnChange != null && changeSet.HasChanges())
                {
                    OnChange.Invoke(changeSet);
                }

                IsProcessing = false;
            }
        }

        public override bool Start()
        {
            Timer = new Timer(PollInterval);
            Timer.Elapsed += Tick;
            Timer.Start();

            IsRunning = true;

            return true;
        }

        public override bool Stop()
        {
            Timer.Stop();
            Timer.Elapsed -= Tick;

            IsRunning = false;

            return true;
        }
    }
}
