using RJ.Poco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RJ.DAL.DAL
{
    /// <summary>
    /// This is an example of a simple ADO.NET transaction
    /// https://msdn.microsoft.com/en-us/library/2k2hy99x(v=vs.110).aspx
    /// </summary>
   public class StudentAccess
    {
       public static  string ConnectionString
       {
           get { return ConfigurationManager.ConnectionStrings["NetUtility"].ConnectionString; }
       }

       public static void CreateStudent(Student stud)
       {
           try
           {
               using (var con = new SqlConnection(ConnectionString))
               {
                   con.Open();
                   var tran = con.BeginTransaction();
                   using (var cmd = new SqlCommand("dbo.InsertStudent", con, tran))
                   {
                       int studentId = 0;                       
                       cmd.CommandType = System.Data.CommandType.StoredProcedure;
                       cmd.CommandTimeout = 30;
                       cmd.Transaction = tran;
                       cmd.Parameters.AddWithValue("@Name", stud.Name);
                       cmd.Parameters.AddWithValue("@Email", stud.Email);
                       cmd.Parameters.AddWithValue("@StudentId", 0).Direction = System.Data.ParameterDirection.Output;                       
                       cmd.ExecuteNonQuery();
                       if (cmd.Parameters["@StudentId"].Value != null)
                       {
                           studentId = Convert.ToInt32(cmd.Parameters["@StudentId"].Value);
                       }                       
                       if (stud.Courses != null && stud.Courses.Count > 0)
                       {
                           foreach (var course in stud.Courses)
                           {
                               CreateStudentCourse(course, studentId,con,cmd);
                           }
                       }
                       tran.Commit();
                   }
               }
           }
           catch (Exception ex)
           {
               //exception will automatically rollback the transaction
               throw;
           }
           
       }

       private static void CreateStudentCourse(Course sc,int studentId,SqlConnection con,SqlCommand cmd)
       {   
           cmd.Parameters.Clear();
           cmd.CommandText = "dbo.InsertStudentCourse";
           cmd.CommandType = System.Data.CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@StudentId", studentId);
           cmd.Parameters.AddWithValue("@CourseId", sc.CourseId);
           cmd.ExecuteNonQuery();
       }
    }
}
