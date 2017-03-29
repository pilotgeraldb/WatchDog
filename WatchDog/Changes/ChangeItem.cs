using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDog.Changes
{
    public sealed class ChangeItem
    {
        public ChangeItem()
        {
            UniqueID = Guid.NewGuid().ToString();
        }

        public string UniqueID { get; private set; }
        public ResourceType ResourceType { get; set; }
        public ChangeType ChangeType { get; set; }
        public string FilePath { get; set; }
        public string Hash { get; set; }
    }
}
