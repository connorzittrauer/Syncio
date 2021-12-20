using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Syncio
{
        public class Tree<T>
        {
            private T data = default(T);
            private List<Tree<T>> children = new List<Tree<T>>();

            private Tree<T> parent = null;

            public Tree(T data)
            {
                this.data = data;
            
            }

            public Tree<T> addChild(Tree<T> child)
            {
                child.setParent(child); //was originally passing "this" keyword
                this.children.Add(child);
                return child;
            }

            public void addChildren(List<Tree<T>> children)
            {
                children.ForEach(each => each.setParent(parent)); //was originally passing "this" keyword
                this.children.AddRange(children);

            }

            public List<Tree<T>> getChildren()
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

            private void setParent(Tree<T> parent)
            {
                this.parent = parent;
            }

            public Tree<T> getParent()
            {
                return parent;
            }



            public void printTree(Tree<T> node, String appender)
            {
                Debug.WriteLine(appender + node.getData());
                node.getChildren().ForEach(each => printTree(each, appender + appender));

            }
        }
 }


