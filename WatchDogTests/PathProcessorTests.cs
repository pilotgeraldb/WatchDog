using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Changes;
using WatchDog.Paths;
using Xunit;

namespace WatchDogTests
{
    public class PathProcessorTests
    {
        private const string CHANGE_TEXT = "[This is a change]";
        private const string NEW_TEXT = "Hello World!";
        private const string ROOT = @"Data";
        
        public PathProcessorTests()
        {

        }

        private void SetupEnvironment()
        {
            if (!Directory.Exists(ROOT))
            {
                Directory.CreateDirectory(ROOT);
            }
            else
            {
                string[] fileList = Directory.GetFiles(ROOT);

                foreach (string file in fileList)
                {
                    File.Delete(file);
                }

                string[] dirList = Directory.GetDirectories(ROOT);
                foreach (string dir in dirList)
                {
                    Directory.Delete(dir, true);
                }
            }
        }

        [Fact]
        public void ShouldDetectFileChange()
        {
            SetupEnvironment();

            string path = $"{ROOT}\\File1.txt";

            PathProcessor PathProcessor = new PathProcessor(@"Data");

            if (!File.Exists(path))
            {
                AddFile(path);
            }

            IChangeSet changes = PathProcessor.Run();

            ChangeFile(path);

            IChangeSet changes2 = PathProcessor.Run();

            RevertChangeFile(path);

            Assert.NotEmpty(changes2.ChangeItems);
            Assert.Equal(ChangeType.Binary, changes2.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes2.ChangeItems[0].ResourceType);
        }

        [Fact]
        public void ShouldDetectFileAdd()
        {
            SetupEnvironment();

            string path = $"{ROOT}\\File2.txt";

            PathProcessor PathProcessor = new PathProcessor(@"Data");

            IChangeSet changes = PathProcessor.Run();

            AddFile(path);

            IChangeSet changes2 = PathProcessor.Run();

            Assert.NotEmpty(changes2.ChangeItems);
            Assert.Equal(ChangeType.Created, changes2.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes2.ChangeItems[0].ResourceType);
        }

        [Fact]
        public void ShouldDetectFileRemove()
        {
            SetupEnvironment();

            string pathToAdd = $"{ROOT}\\File3.txt";
            string pathToRemove = $"{ROOT}\\File3.txt";

            PathProcessor PathProcessor = new PathProcessor(@"Data");

            var pp = new PathProcessor(@"Data");

            PathProcessor.Run();

            AddFile(pathToAdd);

            PathProcessor.Run();

            RemoveFile(pathToRemove);

            IChangeSet changes2 = PathProcessor.Run();

            Assert.NotEmpty(changes2.ChangeItems);
            Assert.Equal(ChangeType.Deleted, changes2.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes2.ChangeItems[0].ResourceType);
        }

        [Fact]
        public void ShouldDetectFileRename()
        {
            SetupEnvironment();

            string pathToAdd = $"{ROOT}\\File1.txt";
            string renameTo = $"{ROOT}\\File2.txt";

            PathProcessor PathProcessor = new PathProcessor(@"Data");

            var pp = new PathProcessor(@"Data");

            AddFile(pathToAdd);

            PathProcessor.Run();

            File.Move(pathToAdd, renameTo);

            IChangeSet changes2 = PathProcessor.Run();

            Assert.NotEmpty(changes2.ChangeItems);
            Assert.Equal(ChangeType.Renamed, changes2.ChangeItems[0].ChangeType);
            Assert.Equal(ResourceType.File, changes2.ChangeItems[0].ResourceType);
        }

        private void ChangeFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(CHANGE_TEXT);
            }
        }

        private void AddFile(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(NEW_TEXT);
            }
        }

        private void RemoveFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void RevertChangeFile(string path)
        {
            string contents = null;

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    contents = sr.ReadToEnd();
                }
            }

            contents = contents.Replace(CHANGE_TEXT, "");

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(contents);
            }
        }
    }
}
