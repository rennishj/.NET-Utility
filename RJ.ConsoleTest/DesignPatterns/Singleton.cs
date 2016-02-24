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
   public sealed class Singleton
    {
       private static readonly object padLock = new object();
       private static Singleton _instance = null;
       //This is to make sure that the class cant be initialized outside of this class
       private Singleton()
       { 
        
       }
       public static Singleton Instance
       {
           get 
           {
               lock (padLock)
               {
                   if (_instance == null)
                   {
                       _instance = new Singleton();
                   }
                   return _instance;
               }
           }
        
       }
    }
}
