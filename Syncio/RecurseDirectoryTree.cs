using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Syncio
{
    class RecurseDirectoryTree
    {

        List<string> subDirectories = new List<string>();
        Node<Model> node = new Node<Model>(null);
        NodeFactory factory = new NodeFactory();




        public RecurseDirectoryTree()
        {
            //node.addChild(new Node<Model>(new FolderModel("C:\\Users\\Connor\\3D Objects\\Test")));
        }
        public void WalkDirectoryTree(System.IO.DirectoryInfo directory)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;



            //on the first pass, this is the root of the directory tree 

            //this needs to be a new node every pass, node1, node2, and so on
            //node.addChild(new Node<Model>(new FolderModel(directory.ToString())));


            //if there is a new directory, that needs to be a new node, and the files below needed to be added to that node, instead of the just the root


            //node = factory.CreateNode(1, directory.ToString());
            factory.CreateNode(1, directory.ToString());

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
                    //node.addChild(new Node<Model>(new FileModel(file.FullName)));
                    
                    //node.addChild(factory.CreateNode(2, file.FullName));
                    factory.CreateNode(2, file.FullName);
                }

                // Now find all the subdirectories under this directory.
                subDirs = directory.GetDirectories();

                //here is where the subdirectories are retrieved
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {

                    subDirectories.Add(dirInfo.ToString());

                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo);


                }


            }
        }


        public List<String> GetSubs()
        {
            return subDirectories;
        }

        public void PrintTree()
        {

           factory.print();

           //node.printTree(node, " ");


        }


        public Node<Model> GetDirectoryTree()
        {
            return node;
        }



    }
}