using RJ.Poco.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Poco
{
    public class Person
    {
        //[CsvIgnore]
        [ExcelColumnOrder(1)]
        public int PersonId { get; set; }
        [ExcelColumnOrder(3)]
        public string FirstName { get; set; }
        [ExcelColumnOrder(2)]
        public string LastName { get; set; }
        [ExcelColumnOrder(4)]
        public string Email { get; set; }
        [ExcelColumnOrder(5)]
        public string ZipCode { get; set; }
    }
}
