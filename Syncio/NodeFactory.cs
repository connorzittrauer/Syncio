using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Syncio
{
    class NodeFactory : INodeFactory
    {
        private List<Node<Model>> factory = new List<Node<Model>>();
        private Node<Model> root = null;
        private Node<Model> file = null;

        //private Node<Model> node = new Node<Model>(null);
        public override Node<Model> CreateNode(int type, string path)
        {
            switch (type)
            {
                case 1:
                    root = new Node<Model>(new FolderModel(path));
                    factory.Add(root);
                    break;
                case 2:
                    file = root.addChild(new Node<Model>(new FileModel(path)));
                    factory.Add(file);
                    break;


            }
            return root;

        }

        public void print()
        {
            var tempRoot = factory[0];
            Debug.WriteLine(tempRoot);
            Debug.WriteLine(factory.Count);
            
            root.printTree(tempRoot, " ");


        }
    }
}
