using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Syncio
{
        public class Node<Model>
        {
            private Model data = default(Model);
 
            private List<Node<Model>> children = new List<Node<Model>>();

            private Node<Model> parent = null;

            List<Model> nodelist;

            public Node(Model data)
            {
                this.data = data;
            
            }

            public Node<Model> addChild(Node<Model> child)
            {
                child.setParent(child); //was originally passing "this" keyword
                this.children.Add(child);
                return child;
            }

            public void addChildren(List<Node<Model>> children)
            {
                children.ForEach(each => each.setParent(parent)); //was originally passing "this" keyword
                this.children.AddRange(children);

            }

            public List<Node<Model>> getChildren()
            {
                return children;
            }

            public Model getData()
            {

                return data;
            }

            public void setData(Model data)
            {
                this.data = data;
            }

            private void setParent(Node<Model> parent)
            {
                this.parent = parent;
            }

            public Node<Model> getParent()
            {
                return parent;
            }



            public void printTree(Node<Model> node, String appender)
            {
                Debug.WriteLine(appender + node.getData());
                

                
                node.getChildren().ForEach(each => printTree(each, appender + appender));

            }



        }
 }


