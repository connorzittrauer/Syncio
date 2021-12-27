using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Syncio
{
    class FileModel : Model
    {
        string filePath;
        string hash;
        Hasher hasher = new Hasher();
        override
        public string Hash
        {
            get
            { return hash; }

        }
        override
        public string AbsolutePath
        {
            get
            {
                var temp = new FileInfo(filePath);
                var cast = (FileSystemInfo)(temp);
                return cast.FullName.ToString();
            }

        }
        override
        public string Name
        {
            get
            {
                var temp = new FileInfo(filePath);
                var cast = (FileSystemInfo)(temp);
                return cast.Name.ToString();
            }
        }

        override
        public string CreationTime
        {
            get
            {
                var temp = new FileInfo(filePath);
                var cast = (FileSystemInfo)(temp);
                return cast.CreationTime.ToString();
            }
        }

        override
        public string Attributes
        {
            get
            {

                var temp = new FileInfo(filePath);
                var cast = (FileSystemInfo)(temp);
                return cast.Attributes.ToString();
            }
        }

        public FileModel(String filePath)
        {
            this.filePath = filePath;
            hash = hasher.GenerateHash(filePath);
        }
        
        public override string ToString()
        {
            return AbsolutePath + " (" + Attributes + ")";
        }
    }
}
