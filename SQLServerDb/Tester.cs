using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //namespace

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

            GetAllStudents();
            foreach(Student st in studentList)
            {
                Console.WriteLine(st);
            }
            Console.WriteLine("---------------------------------------------------------------");
            GetStudent("ST81142");
            Console.Read();
        }

        static void AddStudent(Student st)
        {
            // 1. Create connection object: 
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                string strInsert = $"INSERT INTO tblStudent(StudentNo,FirstName, LastName, Age)" +
                                    $"VALUES('{st.StudentId}', '{st.FirstName}', '{st.LastName}', {st.Age})";

                //string strInsert = "INSERT INTO tblStudent" +
                //    "VALUES('ST1000', 'David', 'Smith', 20)";
                //if you insert into all columns, then you dont need the column details. 
                // 2. Select a command from the SqlCommand Class. 

                SqlCommand insertCmd = new SqlCommand(strInsert, conn);

                    // 3. Execute command insert, update and delete is non-query. Only select is query

                insertCmd.ExecuteNonQuery();
            }
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
                AddStudent(st);
             }
        }

        static void GetAllStudents()
        {
            // 1. Create connection object: 
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                // 2. Select a command from the SqlCommand Class. 
                string strSelect = "Select * from tblStudent";

                // 3. Execute command insert, update and delete is non-query. Only select is query
                SqlCommand cmdSelect= new SqlCommand(strSelect, con);

                using(SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string stId = reader[0].ToString();
                        string name = reader[1].ToString();
                        string surname = reader[2].ToString();
                        int age =Convert.ToInt32(reader["Age"]); // two ways of reading data:
                                                                 // 1: using index 
                                                                 // 2: using the name of column. 
                        Student st=new Student(stId, name, surname, age);
                        studentList.Add(st);
                        //Console.WriteLine($"{stId}\t\t{name}\t\t{surname}\t\t{age}");
                    }
                }
            }
        }

        static void GetStudent(string stNo)
        {
            // 1. Create connection object: 
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                // 2. Select a command from the SqlCommand Class. 
                string strSelect = $"Select * from tblStudent where StudentNo='{stNo}'";

                // 3. Execute command insert, update and delete is non-query. Only select is query
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetString(0)}\t\t{reader.GetString(1)}" +
                            $"\t\t{reader[2]}\t\t{reader.GetInt32(3)}");
                    }
                }
            }
        }
    }
}
