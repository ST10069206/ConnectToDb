using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //namespace
using System.Data;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Query;

namespace ConnectToDB.SQLServerDb
{
    public class Tester
    {
        // go to properties, and start up, change to Tester. 
        // manage nuGet packages, entity framework and entityFrameworkCor.SQLServer
        // svm shortcut for: 
        static string[] names = {"Kabelo", "David", "Mike", "Samantha", "Lucy",
                "Thabo", "Jessica", "Rachael", "Donna", "Micheal"};

        static string[] surnames = { "Smith", "Ross", "Jones", "De Byune", "Spectar", "Federer"};

        // connection string: 
        static string strCon = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=studentDB;Integrated Security=True";
        
        static List<Student> studentList = new List<Student>();
        static void Main(string[] args)
        {
            //InsertData();
            //Console.WriteLine("Data added...");

            //GetAllStudents();

            // GetStudents(); //using a datatable

            foreach(Student st in Student.GetStudents())
            {
                Console.WriteLine(st);
            }
            Console.WriteLine("---------------------------------------------------------------");
            // GetStudent("ST81142");
            Console.WriteLine(Student.GetData("ST81142"));
            Console.WriteLine("---------------------------------------------------------------");
            Student.DeleteStudent("ST81142");
            foreach (Student st in Student.GetStudents())
            {
                Console.WriteLine(st);
            }
            Console.WriteLine("---------------------------------------------------------------");
            Console.Read();
        }


        static void InsertData()
        {
            Random rnd = new Random(); //using a class to generate random numbers. 
            for (int i = 1; i <= 15; i++)
            {
                string stNo = $"ST{rnd.Next(10000, 90000)}";
                string name = names[rnd.Next(names.Length-1)]; //range 0 and 9
                string surname = surnames[rnd.Next(surnames.Length-1)]; //Range is 0 and 6
                int age = rnd.Next(18, 25);

                Student st=new Student(stNo, name,surname, age);
                //AddStudent(st);
             }
        }
    }
}
