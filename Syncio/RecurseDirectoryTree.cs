using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Syncio
{
    class RecurseDirectoryTree
    {

        List<string> subDirectories = new List<string>();
        int count = 0;

        Monitor watcher = new Monitor();
        Node<String> root_node = new Node<string>(null);

        public RecurseDirectoryTree()
        {

        }
        public void WalkDirectoryTree(System.IO.DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            count++;
            
            root_node.addChild(new Node<string>(root.ToString()));

            //if there is a new directory, that need to be a new node, and the files below needed to be added to that node, instead of the just the root
             if (count > 0)
            {

            }
            //Debug.WriteLine(root.ToString()); 

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
                    //Debug.WriteLine("This is the file: " + file.FullName);
                    root_node.addChild(new Node<string>(file.FullName));


                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();




                //here is where the folders are retrieved
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    //watcher.Watch(dirInfo.ToString());
                    
                    //Debug.WriteLine("This is the Folder: " + dirInfo);
                    
                    subDirectories.Add(dirInfo.ToString());


                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo);


                }


            }
        }

        //public List<Object> GetHashSet()
        //{
        //    return hash;
        //}

        public List<String> GetSubs()
        {
            return subDirectories;
        }

        public void PrintTree()
        {
          
            root_node.printTree(root_node, "");
        }

        public Node<String> GetTree()
        {
            return root_node;
        }




    }
}