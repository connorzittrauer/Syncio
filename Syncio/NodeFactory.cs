using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Syncio
{
    class NodeFactory
    {
        private List<Node<Model>> directoryList = new List<Node<Model>>();
        private Node<Model> root = null;
        private Node<Model> file = null;


        //public Node<Model> CreateNode(int type, string path)
        //{
        //    switch (type)
        //    {
        //        case 1:
        //            root = new Node<Model>(new FolderModel(path));
        //            directoryList.Add(root);
        //            break;
        //        case 2:
        //            file = root.addChild(new Node<Model>(new FileModel(path)));
        //            directoryList.Add(file);
        //            break;

        //    }
        //    return root;

        //}

        public Node<Model> GetFileNode(String path)
        {
            //Node<Model> file = new Node<Model>(new FileModel(path));
            return new Node<Model>(new FileModel(path)); 
        }
        public Node<Model> GetFolderNode(String path)
        {
            //Node<Model> folder = new Node<Model>(new FolderModel(path));
            return new Node<Model>(new FolderModel(path));
        }


        //public void print()
        //{
        //    Debug.WriteLine(directoryList.Count);

        //    var rootNode = directoryList[0];


        //    root.printTree(rootNode, " ");


        //}

    }
}
