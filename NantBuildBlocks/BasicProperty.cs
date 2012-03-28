using System;
using System.Collections.Generic;
using System.Text;

namespace NantBuildBlocks
{
    public class BasicProperty
    {
        public BasicProperty()
        {
            InherentType = this.GetType().ToString();
        }

        public BasicProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public string InherentType
        {
            get;
            set;
        }

        public override string ToString()
        {
            return (" " + Name + "=\"" + Value + "\"");
        }
    }
}
