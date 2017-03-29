using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;

namespace WatchDog.Changes
{
    public class ChangeSet : IChangeSet
    {
        public ChangeSet()
        {
            ChangeItems = new List<ChangeItem>();
        }

        public IList<ChangeItem> ChangeItems { get; set; }

        public bool HasChanges
        {
            get
            {
                return (ChangeItems != null && ChangeItems.Count() > 0);
            }
        }
    }
}
