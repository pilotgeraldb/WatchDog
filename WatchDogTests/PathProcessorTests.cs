using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;
using WatchDog.Paths;
using Xunit;
using WatchDogTests.Helpers;

namespace WatchDogTests
{
    public class PathProcessorTests
    {
        public PathProcessorTests()
        {

        }

        [Fact]
        public void ShouldDetectFileChange()
        {
            FileEnvironment environment = new FileEnvironment();

            string path = environment.Filename();

            PathProcessor PathProcessor = new PathProcessor(environment.Root);

            if (!File.Exists(path))
            {
                environment.CreateFile(path);
            }

            IChangeSet changes = PathProcessor.Run();

            environment.ChangeFile(path);

            IChangeSet changes2 = PathProcessor.Run();

            environment.Destory();

            Assert.NotEmpty(changes2.ChangeItems);
            Assert.Equal(ChangeType.Binary, changes2.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes2.ChangeItems[0].ResourceType);
        }

        [Fact]
        public void ShouldDetectFileAdd()
        {
            FileEnvironment environment = new FileEnvironment();

            string path = environment.Filename();

            PathProcessor PathProcessor = new PathProcessor(environment.Root);

            IChangeSet changes = PathProcessor.Run();

            environment.CreateFile(path);

            IChangeSet changes2 = PathProcessor.Run();

            environment.Destory();

            Assert.NotEmpty(changes2.ChangeItems);
            Assert.Equal(ChangeType.Created, changes2.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes2.ChangeItems[0].ResourceType);
        }

        [Fact]
        public void ShouldDetectFileRemove()
        {
            FileEnvironment environment = new FileEnvironment();

            string pathToAdd = environment.Filename();
            string pathToRemove = pathToAdd;

            PathProcessor PathProcessor = new PathProcessor(environment.Root);

            PathProcessor.Run();

            environment.CreateFile(pathToAdd);

            PathProcessor.Run();

            environment.RemoveFile(pathToRemove);

            IChangeSet changes2 = PathProcessor.Run();

            environment.Destory();

            Assert.NotEmpty(changes2.ChangeItems);
            Assert.Equal(ChangeType.Deleted, changes2.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes2.ChangeItems[0].ResourceType);
        }

        [Fact]
        public void ShouldDetectFileRename()
        {
            FileEnvironment environment = new FileEnvironment();

            string pathToAdd = environment.Filename();
            string renameTo = environment.Filename();

            PathProcessor PathProcessor = new PathProcessor(environment.Root);

            environment.CreateFile(pathToAdd);

            PathProcessor.Run();

            File.Move(pathToAdd, renameTo);

            IChangeSet changes2 = PathProcessor.Run();

            environment.Destory();

            Assert.NotEmpty(changes2.ChangeItems);
            Assert.Equal(ChangeType.Renamed, changes2.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes2.ChangeItems[0].ResourceType);
        }
    }
}