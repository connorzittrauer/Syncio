using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Syncio
{
        public class Node<T>
        {
            private T data = default(T);
 
            private List<Node<T>> children = new List<Node<T>>();

            private Node<T> parent = null;
           

            public Node(T data)
            {
                this.data = data;
            
            }

            public Node<T> addChild(Node<T> child)
            {
                child.setParent(this); //was originally passing "this" keyword
                this.children.Add(child);
                return child;
            }

            public void addChildren(List<Node<T>> children)
            {
                children.ForEach(each => each.setParent(this)); //was originally passing "this" keyword
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

            public void deleteNode()
            {
                if (parent != null)
                {
                    int index = this.parent.getChildren().IndexOf(this);
                    this.parent.getChildren().Remove(this);
                    foreach (Node<T> node in getChildren())
                    {
                        node.setParent(this.parent);
                    }
                    //***original in Java this.parent.getChildren().addAll(index, this.getChildren());
                    this.parent.getChildren().InsertRange(index, this.getChildren());
                }
                else
                {
                    deleteRootNode();
                }
                this.getChildren().Clear();
            }
            public Node<T> deleteRootNode()
            {
                if (parent != null)
                {
                    throw new Exception("Illegal state, delete root node not called on root");
                }
                Node<T> newParent = null;
                //*** original Java if (!getChildren().isEmpty()) 
                if (getChildren().Any())
                {
                    //*** original Java newParent = getChildren().get(0);
                    newParent = getChildren().ElementAt(0);
                    newParent.setParent(null);

                    ////*** original Java getChildren().remove(0);
                    getChildren().RemoveAt(0);

                    foreach (Node<T> node in getChildren())
                    {
                        node.setParent(newParent);
                    }

                    ////*** original Java newParent.getChildren().addAll(getChildren());
                    newParent.getChildren().AddRange(getChildren());

                }
                this.getChildren().Clear();
                return newParent;

            }


        public void printTree(Node<T> node, String appender)
            {
                Debug.WriteLine(appender + node.getData());

                node.getChildren().ForEach(child => printTree(child, appender + appender));


            }



    }
 }




