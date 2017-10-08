using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using WatchDogTests.Helpers;
using WatchDog.Comparison;

namespace WatchDogTests
{
    public class ResourceComparatorFactoryTests
    {
        [Fact]
        public void ShouldGetDirectoryComparator()
        {
            FileEnvironment environment = new FileEnvironment();

            IResourceComparator rc = ResourceComparatorFactory.GetComparator(environment.Root);

            environment.Destory();

            Assert.IsType<DirectoryComparator>(rc);
        }

        [Fact]
        public void ShouldGetFileComparator()
        {
            FileEnvironment environment = new FileEnvironment();

            string path = environment.Filename();

            environment.CreateFile(path);

            IResourceComparator rc = ResourceComparatorFactory.GetComparator(path);

            environment.Destory();

            Assert.IsType<FileComparator>(rc);
        }
    }
}
