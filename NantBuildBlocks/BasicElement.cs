using System;
using System.Collections.Generic;
using System.Text;

namespace NantBuildBlocks
{
    public class BasicElement
    {
        private List<BasicProperty> properties;
        private List<BasicElement> childElements;

        public BasicElement()
        {

        }

        public BasicElement(string elementName)
        {
            ElementName = elementName;
        }

        public string ElementName
        {
            get;
            set;
        }


        public List<BasicProperty> Properties
        {
            get
            {
                if (properties == null)
                {
                    properties = new List<BasicProperty>();
                }
                return (properties);
            }
        }

        public List<BasicElement> ChildElements
        {
            get
            {
                if (childElements == null)
                {
                    childElements = new List<BasicElement>();
                }
                return (childElements);
            }
        }

        public override string ToString()
        {
            string ret = "<" + ElementName;
            foreach (BasicProperty prop in Properties)
            {
                ret = ret + " " + prop.ToString();
            }
            ret = ret + " />";
            return (ret);
        }
    }
}
