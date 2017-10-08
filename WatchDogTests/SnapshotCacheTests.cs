using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WatchDog.Caching;
using WatchDog.Snapshots;
using System.IO;
using WatchDogTests.Helpers;

namespace WatchDogTests
{
    public class SnapshotCacheTests
    {
        [Fact]
        public void ShouldCacheSnapshot()
        {
            FileEnvironment environment = new FileEnvironment();

            string testpath = environment.CreateFile();

            SnapshotCache.Cache(new FileResourceSnapshot(testpath));

            IResourceSnapshot snapshot = SnapshotCache.Retrieve(testpath);

            environment.Destory();

            Assert.NotNull(snapshot.Created);
            Assert.Equal(testpath, snapshot.Path);
        }
    }
}
