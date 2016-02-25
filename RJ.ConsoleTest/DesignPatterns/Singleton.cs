using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.ConsoleTest.DesignPatterns
{
    /// <summary>
    ///// http://csharpindepth.com/Articles/General/Singleton.aspx.This is an excellent atricle
    /// </summary>
   //public sealed class Singleton
   // {
   //    private static readonly object padLock = new object();
   //    private static Singleton _instance = null;
   //    //This is to make sure that the class cant be initialized outside of this class
   //    private Singleton()
   //    { 
        
   //    }
   //    public static Singleton Instance
   //    {
   //        get 
   //        {
   //            lock (padLock)
   //            {
   //                if (_instance == null)
   //                {
   //                    _instance = new Singleton();
   //                }
   //                return _instance;
   //            }
   //        }
        
   //    }
   // }

    public sealed class Singleton
    {
        private Singleton()
        {

        }
        public static Singleton Instance
        {
            get { return Nested.instance; }
        }

        private class Nested
        {
            internal static readonly Singleton instance = null;
            //static contructor gets invoked only once per AppDomain,so you will have only one instance in memory
            static Nested()
            {
                instance = new Singleton();
            }
            //http://stackoverflow.com/questions/15250077/why-do-we-need-static-constructors for more info on static constructors
            
        }
    }
}
