using System;
using System.Collections.Generic;
using System.Text;

namespace NantBuildBlocks
{
    class CDataValue : BasicProperty
    {
        public CDataValue()
        {
            base.InherentType = this.GetType().ToString();
        }

        public new string Name
        {
            get
            {
                return (Value);
            }
            set
            {
                Value = value;
            }
        }

        public new string Value
        {
            get;
            set;
        }

        public override string ToString()
        {
            string ret = @"<!CDATA[[";
            ret = ret + Value;
            ret = ret + @"]]>";
            return (ret);
        }
    }
}
