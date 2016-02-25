using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RJ.Poco
{
   public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Course> Courses { get; set; }
    }

   public class Course
   {
       public int CourseId { get; set; }
       public string Name { get; set; }
   }
   public class StudentCourse
   {
       public int StudentId { get; set; }
       public int CourseId { get; set; }
       public DateTime EnrolledDate { get; set; }
   }
}
