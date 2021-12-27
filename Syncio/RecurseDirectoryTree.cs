using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Syncio
{
    
    class RecurseDirectoryTree
    {
        private Node<Model> root;
        List<string> subDirectories = new List<string>();
        Monitor watcher = new Monitor();
        public void WalkDirectoryTree(DirectoryInfo directory)
        {
            root = WalkDirectoryTree(directory, null);
        }

        private Node<Model> WalkDirectoryTree(DirectoryInfo directory, Node<Model> parent)
        {
            var current = CreateDirectoryNode(directory, parent);

            var files = directory.GetFiles("*.*");
            foreach (var file in files)
            {
                // note that files are added under the current directory, not root
                CreateFileNode(file, current);
            }

            var subDirs = directory.GetDirectories();
            foreach (var dirInfo in subDirs)
            {

                // recursion at the current node, not root
                WalkDirectoryTree(dirInfo, current);
            }

            // it's not a void method; it returns the current node so that we can set the root in the public WalkDirectoryTree
            return current;
        }

        private Node<Model> CreateDirectoryNode(DirectoryInfo directory, Node<Model> parent)
        {
            var node = new Node<Model>(new FolderModel(directory.ToString()));
            subDirectories.Add(directory.ToString());

            // ?. notation so that it's skipped if parent is null        
            parent?.AddChild(node);

            return node;
        }

        private Node<Model> CreateFileNode(FileInfo file, Node<Model> parent)
        {
            var node = new Node<Model>(new FileModel(file.FullName));
            
            

            // ?. notation so that it's skipped if parent is null        
            parent?.AddChild(node);

            return node;
        }

        public List<String> GetSubs()
        {
            return subDirectories;
        }

        public void PrintTree()
        {

            root.PrintTree(root, " ");


        }

        public Node<Model> GetTree()
        {
            return root;
        }


    }
}