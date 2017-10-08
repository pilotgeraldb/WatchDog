using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using WatchDogTests.Helpers;
using WatchDog.Comparison;
using WatchDog.Snapshots;
using WatchDog.Changes;

namespace WatchDogTests
{
    public class FileComparatorTests
    {
        [Fact]
        public void ShouldDetectFileChange()
        {
            FileEnvironment environment = new FileEnvironment();

            string path1 = environment.Filename();
            environment.CreateFile(path1);
            environment.ChangeFile(path1);
            var frs1 = new FileResourceSnapshot(path1);

            string path2 = environment.Filename();
            environment.CreateFile(path2);
            var frs2 = new FileResourceSnapshot(path2);

            FileComparator fc = new FileComparator();
            IChangeSet changes = fc.Run(frs1, frs2);

            environment.Destory();

            Assert.NotEmpty(changes.ChangeItems);
            Assert.Equal(ChangeType.Binary, changes.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes.ChangeItems[0].ResourceType);
        }

        [Fact]
        public void ShouldDetectFileDeleted()
        {
            FileEnvironment environment = new FileEnvironment();

            string path1 = environment.Filename();
            environment.CreateFile(path1);
            environment.ChangeFile(path1);
            var frs1 = new FileResourceSnapshot(path1);

            FileResourceSnapshot frs2 = null;

            FileComparator fc = new FileComparator();
            IChangeSet changes = fc.Run(frs1, frs2);

            environment.Destory();

            Assert.NotEmpty(changes.ChangeItems);
            Assert.Equal(ChangeType.Deleted, changes.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes.ChangeItems[0].ResourceType);
        }

        [Fact]
        public void ShouldDetectFileCreated()
        {
            FileEnvironment environment = new FileEnvironment();

            FileResourceSnapshot frs1 = null;

            string path2 = environment.Filename();
            environment.CreateFile(path2);
            var frs2 = new FileResourceSnapshot(path2);

            FileComparator fc = new FileComparator();
            IChangeSet changes = fc.Run(frs1, frs2);

            environment.Destory();

            Assert.NotEmpty(changes.ChangeItems);
            Assert.Equal(ChangeType.Created, changes.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes.ChangeItems[0].ResourceType);
        }
    }
}
