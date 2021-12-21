using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Syncio
{
    class FileManager
    {

        //This class could make use of the events triggered from the Monitor class, when a change is a detected in the source directory a certain method is invoked 

        public FileManager()
        {

        }



        public void InitialCopy(string sourceDirName, string destDirname, bool copySubDirs)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(sourceDirName);

            if (!sourceDir.Exists)
            {
                System.Windows.MessageBox.Show("Please select a valid directory");
            }

            DirectoryInfo[] sourceDirs = sourceDir.GetDirectories();

            //if the destination directory doesn't exist, create it
            Directory.CreateDirectory(destDirname);

            FileInfo[] files = sourceDir.GetFiles();


            foreach (FileInfo file in files)
            {
                string tempPath = System.IO.Path.Combine(destDirname.ToString(), file.Name);
                file.CopyTo(tempPath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in sourceDirs)
                {
                    string tempPath = System.IO.Path.Combine(destDirname.ToString(), subdir.Name);
                    InitialCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }

        }

        public void Create()
        {
           
        }

        public void Change()
        {

        }

        public void Rename()
        {

        }
        public void Delete()
        {

        }






    }
}
