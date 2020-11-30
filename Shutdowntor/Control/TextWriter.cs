using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutdowntor.Control
{
    public class TextWriter
    {
        private DirectoryInfo defaultFolder = new DirectoryInfo("./TextWriterBuildFolder");
        private FileInfo defaultfile = new FileInfo("defaultfile");
        private FileInfo fileInfo;
        private DirectoryInfo directoryInfo;
        private string fileName;
        private string extension;
        private static Object locker = new object();
        public bool IfExistsReplace = false;
        public FileInfo FileInfo { get { return fileInfo; } }
        public TextWriter(DirectoryInfo folder, FileInfo fileName, string extensions)
        {
            init(fileName, folder, extensions);
        }
        public TextWriter(DirectoryInfo folder, FileInfo fileName)
        {
            init(fileName, folder);
        }
        public TextWriter(FileInfo fileName)
        {
            init(fileName);
        }
        public TextWriter()
        {
            init(defaultfile);
        }
        public TextWriter(string fileName, string extensions=null)
        {
            init(new FileInfo($"{fileName}{(string.IsNullOrEmpty(extensions) ? "" : extensions)}"));
        }

        private FileInfo checkExist(FileInfo fileInfo)
        {
            if (IfExistsReplace && fileInfo.Exists)
            {
                fileInfo.Delete();
                return fileInfo;
            }
            FileInfo newOne = fileInfo;
            int i = 1;
            while (newOne.Exists)
            {
                newOne = new FileInfo(newOne.DirectoryName + Path.DirectorySeparatorChar +$"({i})" + fileInfo.Name);
                i++;
            }
            return newOne;
        }

        private void init(FileInfo fileInfo, DirectoryInfo folder=null, string extension=null)
        {
            
            folder = folder == null ? defaultFolder : folder;

            if (!folder.Exists)
            {
                Directory.CreateDirectory(folder.FullName).Create();
            }

            string fileNameWithExten = fileInfo.Name;
            string fileName = fileInfo.Name;
            int lastindex = fileInfo.Name.LastIndexOf('.');
            if (lastindex > -1)
            {
                fileName = fileInfo.Name.Substring(0, lastindex);
            }
            if (extension != null)
            {
                fileNameWithExten = fileName + extension;
            }
            fileInfo = new FileInfo(folder.FullName + Path.DirectorySeparatorChar + fileNameWithExten);

            this.fileInfo = fileInfo;
            this.fileName = fileName;
            this.directoryInfo = folder;
            this.extension = fileInfo.Extension;
        }

        public void WriteLine(string line)
        {
            fileInfo = checkExist(fileInfo);
            lock (locker)
            {
                using (StreamWriter sw = File.AppendText(fileInfo.FullName))
                {
                    sw.WriteLine(line);
                    sw.Close();
                }
            }
        }
        
        public void WriteAllLine(string [] lines)
        {
            fileInfo = checkExist(fileInfo);
            lock (locker)
            {
                using (StreamWriter sw = File.AppendText(fileInfo.FullName))
                {
                    foreach(string line in lines)
                    {
                        sw.WriteLine(line);
                        sw.Close();
                    }

                }
            }
        }
    }
}
