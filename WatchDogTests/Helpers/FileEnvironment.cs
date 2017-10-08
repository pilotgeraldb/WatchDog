using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WatchDogTests.Helpers
{
    public class FileEnvironment
    {
        public string Root { get; private set; }
        private const string CHANGE_TEXT = "[text_added_by_unit_test]";
        private const string ADD_TEXT = "Hello World!";

        public FileEnvironment(bool create = true)
        {
            Root = $"Data//{Guid.NewGuid().ToString().Replace("-", "")}";

            if (create)
            {
                Directory.CreateDirectory(Root);
            }
        }

        public string Filename()
        {
            return $"{Root}\\File_{GUID()}.txt";
        }

        public void Destory()
        {
            if (!Directory.Exists(Root))
            {
                return;
            }

            string[] fileList = Directory.GetFiles(Root);

            foreach (string file in fileList)
            {
                File.Delete(file);
            }

            string[] dirList = Directory.GetDirectories(Root);
            foreach (string dir in dirList)
            {
                Directory.Delete(dir, true);
            }

            Directory.Delete(Root);
        }

        public void ChangeFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(CHANGE_TEXT);
            }
        }

        public string CreateFile()
        {
            string filename = Filename();

            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine(ADD_TEXT);
            }

            return filename;
        }

        public void RemoveFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private static string GUID()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
