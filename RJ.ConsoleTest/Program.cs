using RJ.BLL;
using RJ.ConsoleTest.DesignPatterns;
using RJ.ConsoleTest.PrepClass;
using RJ.DAL.DAL;
using RJ.Poco;
using RJ.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //CsvHelper();
                
                //PalindromeTest();
                //TestRecusrion();
                //TestSelectMany();
                //InvokeFibonacciNumbers();       

                //RJ.ConsoleTest.DerivedC.NestedC c1 = new DerivedC.NestedC();
                //BaseC.NestedC c2 = new BaseC.NestedC();
                //Console.WriteLine(c1.x);
                //Console.WriteLine(c2.x);
                //SpliAndPrintCharacters("Rennish Joseph|Nathan John Joseph|Anupama Joseph|Nora Ann Joseph|Neha Rennish Joseph|");
                //Console.WriteLine("Aray in the normal order :" + "1,2,3,4,5,6");
                //PrintAnArrayInReverse(new int[] { 1, 2, 3, 4, 5, 6 });
                //var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9,9,1,1,5,8,6 };
                //Console.WriteLine(HasDuplicates(list)); 
                //Console.WriteLine("{0}",ScopeMonitor.fullUrl);

                CreateStudentCourse();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Hit enter to Reprocess ");
                var result = Console.ReadLine();
                if (result == "1")
                {
                    TestRecusrion();
                }
            }
            //Console.WriteLine("Finished wrting to the file");
            Console.ReadLine();

        }

        private static void FirstNFibonacciNumbers()
        {
            Console.WriteLine("Please enter a number");
            int number = Convert.ToInt32(Console.ReadLine());
            var fib = FirstNFibonacciNumbers(number);
            foreach (var item in fib)
            {
                Console.WriteLine(item);
            }

        }

        private static void PrintAnArrayInReverse(int[] a)
        {
            
            for (int i = a.Length-1; i >=0 ; i--)
            {

                Console.WriteLine(a[i]);
            }
        }
        public static List<int> FirstNFibonacciNumbers(int number)
        {
            //List<int> result = new List<int>();
            //if (number == 0)
            //{
            //    result.Add(0);
            //    return result;
            //}
            //else if (number == 1)
            //{
            //    result.Add(0);
            //    return result;
            //}
            //else if (number == 2)
            //{
            //    result.AddRange(new List<int>() { 0, 1 });
            //    return result;
            //}
            //else
            //{
            //    //if we got thus far,we should have f1,f2 and f3 as fibonacci numbers
            //    int f1 = 0,
            //        f2 = 1;

            //    result.AddRange(new List<int>() { f1, f2 });
            //    for (int i = 2; i < number; i++)
            //    {
            //        result.Add(result[i - 1] + result[i - 2]);
            //    }
            //}
            //return result;

            List<int> result = new List<int>();
            if(number == 0)
                return result;
            if (number == 1)
            {
                result.Add(0);
                return result;
            }            
            if (number == 2)
            {
                result.AddRange(new List<int> { 0, 1 });
                return result;
            }
            int f1 = 0,
                f2 = 1;
            result.AddRange(new List<int> { f1, f2 });
            for (int i = 2; i < number; i++)
            {
                result.Add(result[i - 1] + result[i - 2]);
            }
            return result;

        }

        private static bool HasDuplicates(List<int> sodokuNumbers)
        {
            //http://stackoverflow.com/questions/18547354/c-sharp-linq-find-duplicates-in-list
            //The below will give you how many times an element is repeated
            var result = sodokuNumbers.GroupBy(x => x)
                                       .Where(x => x.Count() > 1)
                                       .Select(e => new{Element = e.Key,Counter = e.Count()})
                                       .ToList();
            return result.Any(a => a.Counter > 0);                  
            
        }

        private void TestTuples()
        {
            var t1 = new Tuple<string, int, Address>("Rennish", 36, new Address { Id = 1,Address1 = "123 Mainstreet"});
            string name = t1.Item1;
            int age = t1.Item2;
            Address address = t1.Item3;
        }

        private static void TestSelectMany()
        {

            var orders = new List<Order>
            {
              new Order{OrderId = 1,Packages = new List<Package>()
              {
                new Package{PackageId = 1,OrderId = 1,ShipDate = new DateTime(2015,12,31),
                            Items = new List<PackageItems>(){new PackageItems{PackageItemId = 1,PackageId = 1,Qty = 10,Size = "SM"},
                                                             new PackageItems{PackageItemId = 2,PackageId = 2,Qty = 10,Size = "XL"}}},
                new Package{PackageId = 2,OrderId = 1,ShipDate = new DateTime(2015,12,25),
                            Items = new List<PackageItems>(){new PackageItems{PackageItemId = 3,PackageId = 1,Qty = 10,Size = "SM"},
                                                             new PackageItems{PackageItemId = 4,PackageId = 2,Qty = 10,Size = "XL"}}},
                new Package{PackageId = 3,OrderId = 1,ShipDate = new DateTime(2016,02,10),
                            Items = new List<PackageItems>(){new PackageItems{PackageItemId = 5,PackageId = 1,Qty = 10,Size = "SM"},
                                                             new PackageItems{PackageItemId = 6,PackageId = 2,Qty = 10,Size = "XL"}}}
              }}
            };

            var packageItemIds = orders.SelectMany(o => o.Packages)
                                        .SelectMany(p => p.Items)
                                        .Where(pi => pi.Qty > 0 && pi.Size.ToLower().Equals("xl"))
                                        .Select(pki => pki.PackageItemId)
                                        .Distinct()
                                        .ToList();
            foreach (var item in packageItemIds)
            {
                Console.WriteLine(item);
            }
        }

        private static void CsvHelperMethod()
        {
            CsvHelper.WriteCsv<Person>(GetPersons(), @"C:\Test\Person.csv", "|");
            CsvHelper.ReportBurstFiles(@"C:\Test\Reports.csv", "|");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(DateTime.Now.ToString("ddMMyyyyhhmmsstt"));
            }
        }

        private static void TestRecusrion()
        {
            var meItems = new MenuItemsService().MenuItemReadAll().Result;
            foreach (var item in meItems)
            {
                Console.WriteLine("{0} {1} {2}", item.ParentId, item.DisplayName, item.Url);
            }
        }

        private static void PalindromeTest()
        {
            Console.WriteLine("Enter the word");
            string val = Console.ReadLine();
            Console.WriteLine(IsPalindrome(val));
        }

        public static  bool IsPalindrome(string word)
        {
            //first reverse the string
            string reversedString = new string(word.Reverse().ToArray());
            return string.Compare(word, reversedString) == 0 ? true : false;
        }

        private static string ReverseString(string str)
        {
            char[] original = str.ToCharArray();
            Array.Reverse(original);
            return new string(original);
        }
        private static void TestPolymorphism()
        {
            var shapes = new List<Shape>();
            shapes.Add(new Circle());
            shapes.Add(new Triangle());
            foreach (var shape in shapes)
            {
                shape.Draw();
            }
        }

        private static bool CompareDates(DateTime date1 ,DateTime date2)
        {
            return DateTime.Compare(date1, date2) == 0 ? true : false;
        }

        private static void SpliAndPrintCharacters(string input)
        {
            string[] sArray = input.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var str in sArray)
            {
                Console.WriteLine(str);
            }
            
        }

        private static void PrintHeader()
        {
            string[] columns = new string[] { "PersonId", "FirstName", "LastName" };
            string header = string.Join("|", columns);
            Console.WriteLine(header);
        }

        private static void CreateStudentCourse()
        {
            Student student = new Student
            {
                Name = "Rennish Joseph",
                Email = "rennishj@gmail.com",
                Courses = new List<Course>()
                {
                    new Course(){CourseId = 1},
                    new Course(){CourseId = 2},
                    new Course(){CourseId = 3}
                }
            };

            StudentAccess.CreateStudent(student);
        }

        private static List<Person> GetPersons()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                persons.Add(new Person
                {
                    FirstName = "Rennish_" + i.ToString(),
                    LastName = "Joseph_" + i.ToString(),
                    PersonId = i,
                    ZipCode = "32225_" + i.ToString()
                });
            }
            return persons;
        }
    }

    public enum ConsoleActions
    { 
        PrintHeader = 1,
    }

    #region C# Interview Prep
    public class Shape
    {
        public virtual void Draw()
        {
            Console.WriteLine("Base classes Draw");
        }
    }
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
    }
    public class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Circles draw gets called");
            base.Draw();
        }
    }

    public class Triangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Drawing a Triangle");
            base.Draw();
        }
    }

    public class BaseC
    {
        public class NestedC
        {
            public int x = 200;
            public int y = 0;
        }
    }

    public class DerivedC : BaseC
    {
        new public class NestedC
        {
            public int x = 1000;
            public int y = 200;
            public int z = 200;
        }
    
    }

    
    #endregion
    
}
