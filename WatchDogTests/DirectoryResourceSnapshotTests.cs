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
    public class DirectoryResourceSnapshotTests
    {
        [Fact]
        public void ShouldInitializeFromDirectory()
        {
            FileEnvironment environment = new FileEnvironment();

            environment.CreateFile();
            environment.CreateFile();
            environment.CreateFile();
           
            var s = new DirectoryResourceSnapshot(environment.Root);

            environment.Destory();

            Assert.NotEmpty(s.Files);
            Assert.Empty(s.Directories);
            Assert.True(s.Files.Count() == 3);
            Assert.True(s.Directories.Count() == 0);
            Assert.Equal(environment.Root, s.Path);
        }

        [Fact]
        public void ShouldInitializeFromDirectoryUsingStaticMethod()
        {
            FileEnvironment environment = new FileEnvironment();

            environment.CreateFile();
            environment.CreateFile();
            environment.CreateFile();

            var s = DirectoryResourceSnapshot.New(environment.Root);

            environment.Destory();

            Assert.NotEmpty(s.Files);
            Assert.Empty(s.Directories);
            Assert.True(s.Files.Count() == 3);
            Assert.True(s.Directories.Count() == 0);
            Assert.Equal(environment.Root, s.Path);
        }
    }
}
