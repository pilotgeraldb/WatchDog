using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;

namespace WatchDog
{
    public interface IWatcher
    {
        string Path { get; }
        Action<IChangeSet> OnChange { get; }
        bool Start();
        bool Stop(); 
    }
}