using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Syncio
{
    class RecurseDirectoryTree
    {

        List<string> subDirectories = new List<string>();

        private List<Node<Model>> directoryList = new List<Node<Model>>();
        public RecurseDirectoryTree()
        {

        }
        public void WalkDirectoryTree(System.IO.DirectoryInfo directory, Node<Model> root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;


            //a new node is added to the root (root directory) for each sub directory here

             Node<Model> sub = root.addChild(new Node<Model>(new FolderModel(directory.ToString())));
      


            directoryList.Add(root);

            // First, process all the files directly under this folder
            try
            {
                files = directory.GetFiles("*.*");

            }

            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.Message);
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
                    Node<Model> fileNode = sub.addChild(new Node<Model>(new FileModel(file.FullName)));
                }

                // Now find all the subdirectories under this directory.
                subDirs = directory.GetDirectories();

                //here is where the subdirectories are retrieved
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    //Node<Model> sub = root.addChild(new Node<Model>(new FolderModel(directory.ToString())));
                    subDirectories.Add(dirInfo.ToString());

                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo, sub);


                }


            }
        }


        public List<String> GetSubs()
        {
            return subDirectories;
        }

        public void PrintTree()
        {

            //factory.print();
            var rootNode = directoryList[0];
            rootNode.printTree(rootNode, " ");


        }


    }
}