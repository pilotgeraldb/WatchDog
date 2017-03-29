using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDog.Changes
{
    public static class ChangeSetExtensions
    {
        public static bool HasChanges(this IChangeSet changeSet)
        {
            return (changeSet != null && changeSet.ChangeItems != null && changeSet.ChangeItems.Any());
        }
    }
}