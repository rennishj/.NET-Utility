using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.ConsoleTest.PrepClass
{
    //http://stackoverflow.com/questions/4506990/what-is-the-use-of-static-constructors.This explains what a static constructor is
    public class ScopeMonitor
    {

        private static string firstPart = "http://www.asp.net";
        public static string fullUrl = null;
        private static string secondPart = "foo/bar";
        static ScopeMonitor()
        {
            fullUrl = firstPart + secondPart;
        }
    }
}
