using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class IntExtensions
    {
        public static bool IsBetween(this int i, int left, int right)
        {
            return left <= i && i <= right;
        }
    }

    public static class ConsoleKeyInfoExtensions
    {
        public static bool IsKey(this ConsoleKeyInfo pressed, char key)
        {
            return pressed.KeyChar == key;
        }
    }
    
}
