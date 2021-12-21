using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Syncio
{
        public class Node<T>
        {
            private T data = default(T);
 
            private List<Node<T>> children = new List<Node<T>>();

            private Node<T> parent = null;

            List<T> nodelist;

            public Node(T data)
            {
                this.data = data;
            
            }

            public Node<T> addChild(Node<T> child)
            {
                child.setParent(child); //was originally passing "this" keyword
                this.children.Add(child);
                return child;
            }

            public void addChildren(List<Node<T>> children)
            {
                children.ForEach(each => each.setParent(parent)); //was originally passing "this" keyword
                this.children.AddRange(children);

            }

            public List<Node<T>> getChildren()
            {
                return children;
            }

            public T getData()
            {

                return data;
            }

            public void setData(T data)
            {
                this.data = data;
            }

            private void setParent(Node<T> parent)
            {
                this.parent = parent;
            }

            public Node<T> getParent()
            {
                return parent;
            }



        public void printTree(Node<T> node, String appender)
        {
            Debug.WriteLine(appender + node.getData());
            node.getChildren().ForEach(each => printTree(each, appender + appender));


        }



    }
 }




