using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using WatchDogTests.Helpers;
using WatchDog.Paths;
using WatchDog.Changes;

namespace WatchDogTests
{
    public class PathHelperTests
    {
        [Fact]
        public void ShouldGetFileResource()
        {
            FileEnvironment environment = new FileEnvironment(false);

            ResourceType rc = PathHelper.GetResourceType(environment.Filename());

            Assert.Equal(ResourceType.File, rc);
        }

        [Fact]
        public void ShouldGetDirectoryResource()
        {
            FileEnvironment environment = new FileEnvironment(false);

            ResourceType rc = PathHelper.GetResourceType(environment.Root);

            Assert.Equal(ResourceType.Directory, rc);
        }
    }
}
