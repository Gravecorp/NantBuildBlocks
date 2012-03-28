using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;

namespace NantBuildBlocks
{
    class Constants
    {
        public static string DEFAULTSAVEPATH = Path.Combine(Utility.AssemblyDirectory, "StoredBlocks");
        public static string SAVEDBLOCKSSEARCHPATTERN = "*.block";
    }
}
