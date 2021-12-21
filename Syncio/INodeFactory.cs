using System;
using System.Collections.Generic;
using System.Text;

namespace Syncio
{
    abstract class INodeFactory
    {
        public abstract Node<Model> CreateNode(int type, string path); 
    }
}
