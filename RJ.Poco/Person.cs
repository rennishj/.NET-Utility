﻿using RJ.Poco.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Poco
{
    public class Person
    {
        [CsvIgnore]
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        [CsvIgnore]
        public string ZipCode { get; set; }
    }
}