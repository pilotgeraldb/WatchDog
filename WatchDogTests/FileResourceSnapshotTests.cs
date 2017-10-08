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
    public class FileResourceSnapshotTests
    {
        [Fact]
        public void ShouldInitializeFromFile()
        {
            FileEnvironment environment = new FileEnvironment();

            string file = environment.CreateFile();
           
            var s = new FileResourceSnapshot(file);

            environment.Destory();

            Assert.NotNull(s.File);
            Assert.Equal(s.File.FilePath, file);
            Assert.Equal(file, s.Path);
        }

        [Fact]
        public void ShouldInitializeFromFileUsingStaticMethod()
        {
            FileEnvironment environment = new FileEnvironment();

            string file = environment.CreateFile();

            var s = FileResourceSnapshot.New(file);

            environment.Destory();

            Assert.NotNull(s.File);
            Assert.Equal(s.File.FilePath, file);
            Assert.Equal(file, s.Path);
        }
    }
}
