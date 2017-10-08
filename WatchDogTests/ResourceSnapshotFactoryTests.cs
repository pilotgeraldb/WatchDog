using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using WatchDogTests.Helpers;
using WatchDog.Snapshots;

namespace WatchDogTests
{
    public class ResourceSnapshotFactoryTests
    {
        [Fact]
        public void ShouldGetFileSnapshot()
        {
            FileEnvironment environment = new FileEnvironment();

            string file = environment.CreateFile();

            IResourceSnapshot s = ResourceSnapshotFactory.GetSnapshot(file);

            environment.Destory();

            Assert.Equal(s.Path, file);
            Assert.IsType<FileResourceSnapshot>(s);
        }
        [Fact]
        public void ShouldGetDirectorySnapshot()
        {
            FileEnvironment environment = new FileEnvironment();

            IResourceSnapshot s = ResourceSnapshotFactory.GetSnapshot(environment.Root);

            environment.Destory();

            Assert.Equal(s.Path, environment.Root);
            Assert.IsType<DirectoryResourceSnapshot>(s);
        }
    }
}
