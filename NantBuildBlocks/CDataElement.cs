using System;
using System.Collections.Generic;
using System.Text;

namespace NantBuildBlocks
{
    class CDataElement : BasicElement
    {
        public CDataElement()
        {
            base.InherentType = this.GetType().ToString();
        }

        public new string ElementName
        {
            get
            {
                return ("CData");
            }
            set
            {

            }
        }


        public new List<BasicElement> ChildElements
        {
            get
            {
                base.ChildElements.Clear();
                return (base.ChildElements);
            }
        }

        public override string ToString()
        {
            
            return (ElementName);
        }
    }
}
