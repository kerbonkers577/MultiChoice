using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MultipleChoiceLibrary
{
    public class DataAccess
    {

        public bool InjectionCheck(string stringToCheck)
        {
            //Checks for ' character and flags it for database rules
            bool containsApo = false;
            char[] arr;
            arr = stringToCheck.ToCharArray();

            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i].Equals('\''))
                {
                    containsApo = true;
                }
            }

            return containsApo;
        }

        public string InjectionCheckWrap(string someString)
        {
            //based on flagged check, it will alert the user about inseting the ' character
            //Mainly for IDs

            while (InjectionCheck(someString))
            {
                Console.WriteLine("Cannot input \' characters in this input");
                someString = Console.ReadLine();
            }

            return someString;
        }

        public string QuestionApostropheCheck(string someString)
        {
            //Changes to a ' in a varchar for strings like questions
            //for the database
            bool hasApo = false; 
            char[] arr;
            arr = someString.ToCharArray();

            hasApo = InjectionCheck(someString);

            if(hasApo) // if true
            {
                List <char> changedArr = arr.ToList();

                for (int i = 0; i < changedArr.Count; i++)
                {
                    if (changedArr[i].Equals('\''))
                    {
                        changedArr.Insert(i - 1, '\'');
                    }
                }

                someString = Convert.ToString(changedArr);

            }

            return someString;
        }

        //Database connection testing method
        public string TestConnection(string connectionString, SqlConnection dbConn)
        {
            string message = "";
            try
            {
                dbConn = new SqlConnection(connectionString);
                dbConn.Open();
                message = "Database connection established";
                dbConn.Close();
            }
            catch (SqlException e)
            {
                message = "SQL connection failed " + e.Message;
            }

            return message;
        }


        //Student table access

        public DataSet GetStudentTable(SqlConnection dbconn)
        {
            string sql = @"select *
                        from Student";
            DataSet students = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(students);
            }
            catch(SqlException e)
            {
                Console.WriteLine("Could not retrieve students table as dataset: " + e.Message);
            }

            return students;
        }

        public DataSet GetSpecificStudent(SqlConnection dbconn, string studentID)
        {

            InjectionCheckWrap(studentID);

            string sql = @"select * 
                        from Student
                        where student_ID = " + studentID;
            DataSet students = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(students);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve students table as dataset: " + e.Message);
            }

            return students;
        }

        //StudentAnswer table access

        public DataSet GetStudentAnswerTable(SqlConnection dbconn)
        {
            string sql = @"select *
                        from StudentAnswer";
            DataSet students = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(students);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve studentAnswer table as dataset: " + e.Message);
            }

            return students;
        }

        public DataSet GetSpecificStudentAnswerTable(SqlConnection dbconn, string studentID, string questionID)
        {
            InjectionCheckWrap(studentID);
            InjectionCheckWrap(questionID);

            string sql = @"select *
                        from StudentAnswer
                        where";
            DataSet students = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(students);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve studentAnswer table as dataset: " + e.Message);
            }

            return students;
        }

        //Question table access

        public DataSet GetQuestionTable(SqlConnection dbconn)
        {
            string sql = @"select *
                        from Question";
            DataSet students = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(students);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Question table as dataset: " + e.Message);
            }

            return students;
        }

        public DataSet InsertQuestion(SqlConnection dbconn, string question)
        {
            question = QuestionApostropheCheck(question);

            string sql = @"select *
                        from Question";
            DataSet students = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(students);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Question table as dataset: " + e.Message);
            }

            return students;
        }

        //Teacher table access

        public DataSet GetTeacherTable(SqlConnection dbconn)
        {
            string sql = @"select *
                        from Teacher";
            DataSet students = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(students);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Teacher table as dataset: " + e.Message);
            }

            return students;
        }

        //StudentMark table Access

        public DataSet GetStudentMarkTable(SqlConnection dbconn)
        {
            string sql = @"select *
                        from Teacher";
            DataSet students = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(students);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Teacher table as dataset: " + e.Message);
            }

            return students;
        }
    }
}
