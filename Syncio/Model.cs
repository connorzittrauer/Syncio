using System;
using System.Collections.Generic;
using System.Text;

namespace Syncio
{
    abstract class Model
    {
        public abstract string Hash
        {
            get;
        }
        public abstract string AbsolutePath
        {
            get;
        }
        public abstract string Name
        {
            get;
        }
        public abstract string CreationTime
        {
            get;
        }
        public abstract string Attributes
        {
            get;
        }


    }
}
