using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDog.Files
{
    public interface IFileItem
    {
        string FilePath { get; }
        string Hash { get; }
        FileInfo FileInfo { get; }

        string ComputeHash();
    }
}
