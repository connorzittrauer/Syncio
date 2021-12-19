using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Syncio
{
    class RecurseTree
    {
        List<Object> hash = new List<Object>();
        List<string> subDirectories = new List<string>();

        Monitor watcher = new Monitor();

        public RecurseTree()
        {

        }
        public void WalkDirectoryTree(System.IO.DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;


            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                //log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Debug.WriteLine(e.Message);
            }


            //here is where the files are retrieved
            if (files != null)
            {
                foreach (System.IO.FileInfo file in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    Debug.WriteLine("This is the file: " + file.FullName);

                    hash.Add(file.FullName);


                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();




                //here is where the folders are retrieved
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    Debug.WriteLine("This is the Folder: " + dirInfo);


                    //here could  pass each directory to the monitor
                    //watcher.InitializeWatcher(dirInfo.ToString());
                    subDirectories.Add(dirInfo.ToString());


                    hash.Add(dirInfo);

                    WalkDirectoryTree(dirInfo);


                }


            }
        }

        public List<Object> GetHashSet()
        {
            return hash;
        }

        public List<String> GetSubs()
        {
            return subDirectories;
        }




    }
}