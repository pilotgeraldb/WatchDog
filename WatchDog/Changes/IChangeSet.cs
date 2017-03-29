using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDog.Changes
{
    public interface IChangeSet
    {
        IList<ChangeItem> ChangeItems { get; }
    }
}
