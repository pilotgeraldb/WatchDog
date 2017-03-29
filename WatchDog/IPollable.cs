using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WatchDog
{
    public interface IPollable
    {
        double PollInterval { get; }
        void Tick(object sender, ElapsedEventArgs e);
        Timer Timer { get; }
    }
}
