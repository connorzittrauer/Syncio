using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Syncio
{
    class FolderModel : Model
    {
        string directory;

        override
        public string Hash
        {
            get { return null; }
        }
        override
        public string AbsolutePath
        {
            get
            {
                return new DirectoryInfo(directory).FullName;
            }

        }
        override
        public string Name
        {
            get
            {
                return new DirectoryInfo(directory).Name;

            }
        }

        override
        public string CreationTime
        {
            get
            {
                var temp = new FileInfo(directory);
                var cast = (FileSystemInfo)(temp);
                return cast.CreationTime.ToString();
            }
        }

        override
        public string Attributes
        {
            get
            {

                var temp = new FileInfo(directory);
                var cast = (FileSystemInfo)(temp);
                return cast.Attributes.ToString();
            }
        }

        public FolderModel(String directory)
        {
            this.directory = directory;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}