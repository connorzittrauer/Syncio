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

            public Node<T> AddChild(Node<T> child)
            {
                child.SetParent(this); //was originally passing "this" keyword
                this.children.Add(child);
                return child;
            }

            public void AddChildren(List<Node<T>> children)
            {
                children.ForEach(each => each.SetParent(this)); //was originally passing "this" keyword
                this.children.AddRange(children);

            }

            public List<Node<T>> GetChildren()
            {
                return children;
            }

            public T GetData()
            {

                return data;
            }

            public void SetData(T data)
            {
                this.data = data;
            }

            private void SetParent(Node<T> parent)
            {
                this.parent = parent;
            }

            public Node<T> GetParent()
            {
                return parent;
            }

        public Node<T> Find(Node<T> root, string data)
        {
            //base case
            if (root == null || data.Equals(root.GetData()))
            {
                return root;
            }

            Node<T>[] children = root.GetChildren().ToArray();
            Node<T> target = null;
            for (int i = 0; target == null && i < children.Length; i++)
            {
                target = Find(children[i], data);
            }
            return target;
        }

        public void DeleteNode()
            {
                if (parent != null)
                {
                    int index = this.parent.GetChildren().IndexOf(this);
                    this.parent.GetChildren().Remove(this);
                    foreach (Node<T> node in GetChildren())
                    {
                        node.SetParent(this.parent);
                    }
                    //***original in Java this.parent.getChildren().addAll(index, this.getChildren());
                    this.parent.GetChildren().InsertRange(index, this.GetChildren());
                }
                else
                {
                    DeleteRootNode();
                }
                this.GetChildren().Clear();
            }
            public Node<T> DeleteRootNode()
            {
                if (parent != null)
                {
                    throw new Exception("Illegal state, delete root node not called on root");
                }
                Node<T> newParent = null;
                //*** original Java if (!getChildren().isEmpty()) 
                if (GetChildren().Any())
                {
                    //*** original Java newParent = getChildren().get(0);
                    newParent = GetChildren().ElementAt(0);
                    newParent.SetParent(null);

                    ////*** original Java getChildren().remove(0);
                    GetChildren().RemoveAt(0);

                    foreach (Node<T> node in GetChildren())
                    {
                        node.SetParent(newParent);
                    }

                    ////*** original Java newParent.getChildren().addAll(getChildren());
                    newParent.GetChildren().AddRange(GetChildren());

                }
                this.GetChildren().Clear();
                return newParent;

            }


        public void PrintTree(Node<T> node, String appender)
            {
                Debug.WriteLine(appender + node.GetData());

                node.GetChildren().ForEach(child => PrintTree(child, appender + appender));


            }

      


    }
 }




