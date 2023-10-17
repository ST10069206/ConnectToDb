using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToDB
{
    public class Student
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        // connection string: 
        static string strCon = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=studentDB;Integrated Security=True";


        public Student(string studentId, string firstName, string lastName, int age)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public void AddStudent()
        {
            // 1. Create connection object: 
            using (SqlConnection conn = new SqlConnection(strCon))
            {
                conn.Open();
                string strInsert = $"INSERT INTO tblStudent(StudentNo,FirstName, LastName, Age)" +
                                    $"VALUES('{StudentId}', '{FirstName}', '{LastName}', {Age})";

                //string strInsert = "INSERT INTO tblStudent" +
                //    "VALUES('ST1000', 'David', 'Smith', 20)";
                //if you insert into all columns, then you dont need the column details. 
                // 2. Select a command from the SqlCommand Class. 

                SqlCommand insertCmd = new SqlCommand(strInsert, conn);

                // 3. Execute command insert, update and delete is non-query. Only select is query

                insertCmd.ExecuteNonQuery();
            }
        }

        public static List<Student> GetStudents()
        {
            SqlConnection con = new SqlConnection(strCon);
            DataTable myTable = new DataTable();
            DataRow myRow;
            string strSelect = "Select * from tblStudent";
            //SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            //SqlDataAdapter myDataAdapter = new SqlDataAdapter(cmdSelect);
            SqlDataAdapter myDataAdapter = new SqlDataAdapter(strSelect, con); //SqlDataAdapter only works with the select statement

            con.Open();
            // convert the results into a datatable, you need the adapter to do that
            myDataAdapter.Fill(myTable);

            List<Student> StudentList = new List<Student>();
            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];
                    string stId = (string)myRow[0]; //need to cast
                    string name = (string)myRow["Firstname"];
                    string surname = (string)myRow[2];
                    int age = Convert.ToInt32(myRow[3]);
                    StudentList.Add(new Student(stId, name, surname, age));
                }
            }
            else
            {
                throw new Exception("No data found");
            }
            return StudentList;
            con.Close();
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
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string stId = reader[0].ToString();
                        string name = reader[1].ToString();
                        string surname = reader[2].ToString();
                        int age = Convert.ToInt32(reader["Age"]); // two ways of reading data:
                                                                  // 1: using index 
                                                                  // 2: using the name of column. 
                        Student st = new Student(stId, name, surname, age);
                        //studentList.Add(st);
                        //Console.WriteLine($"{stId}\t\t{name}\t\t{surname}\t\t{age}");
                    }
                }
            }
        }

        public static Student GetData(string id)
        {
            Student st=null;
            // 1. Create connection object: 
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();

                // 2. Select a command from the SqlCommand Class. 
                string strSelect = $"Select * from tblStudent where StudentNo='{id}'";

                // 3. Execute command insert, update and delete is non-query. Only select is query
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        st=new Student((string)reader[0], (string)reader[1], (string)reader[2], (int)reader[3]);
                    }
                }
            }
            return st;
        }

        public static void DeleteStudent(string id)
        {
            using(SqlConnection con = new SqlConnection(strCon))
            {
                con.Open();
                string strDelete = $"Delete from tblStudent where StudentNo='{id}'";
                SqlCommand cmdDelete = new SqlCommand(strDelete, con);
                cmdDelete.ExecuteNonQuery();
            }
        }
        public override string ToString()
        {
            return $"[{StudentId}]\t{FirstName}\t\t{LastName}\t\t{Age}";
        }
    }
}
