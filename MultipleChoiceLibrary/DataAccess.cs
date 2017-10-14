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
                if (arr[i].Equals('\''))
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

            if (hasApo) // if true
            {
                List<char> changedArr = arr.ToList();

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
        public SqlConnection TestConnection(string connectionString, SqlConnection dbConn)
        {
            //string message = "";
            try
            {
                dbConn = new SqlConnection(connectionString);
                dbConn.Open();
                dbConn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("SQL connection failed " + e.Message);
            }

            return dbConn;
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
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve students table as dataset: " + e.Message);
            }

            return students;
        }

        public DataSet GetSpecificStudent(SqlConnection dbconn, string studentNum)
        {

            InjectionCheckWrap(studentNum);

            string sql = @"select *
                            from Student
                            where student_Number = '"+ studentNum +"'";
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

        public bool CheckSpecificStudentLogin(SqlConnection dbconn, string studentNum, string studentLogin)
        {
            bool credentialsExist = false;

            InjectionCheckWrap(studentNum);
            InjectionCheckWrap(studentLogin);

            string sql = @"select * 
                            from Student
                            where student_login = '" + studentLogin +"'" +
                            " and student_Number = '" + studentNum + "'";
            DataSet studentLoginCheck = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(studentLoginCheck);

                if (studentLoginCheck.Tables[0].Rows.Count != 0)
                {
                    credentialsExist = true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve students table as dataset: " + e.Message);
            }

            return credentialsExist;
        }

        public void InsertNewStudent(SqlConnection dbConn, string studentName, string studentNum, string studentPassword)
        {
            string sql = @"insert into Student(student_Name, student_Number, student_login)
                            values('" + studentName + "', '" + studentNum + "', '" + studentPassword + "')";

            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, dbConn);

                dbConn.Open();
                sqlCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not insert new student table as dataset: " + e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not insert new student table as dataset: " + e.Message);
            }
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
                        where student_ID = " + studentID + " and question_ID = " + questionID;
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

        public void InsertStudentAnswer(SqlConnection dbConn, int studentID, int questionID, int answer)
        {

            string sql = @"insert into StudentAnswer(student_ID, question_ID, studentAnswer)
                            values(" + studentID + "," + questionID + "," + answer + ")";

            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, dbConn);

                dbConn.Open();
                sqlCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Teacher table as dataset: " + e.Message);
            }
        }

        //Question table access

        public DataSet GetQuestionTable(SqlConnection dbconn)
        {
            string sql = @"select *
                        from Question";
            DataSet question = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(question);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Question table as dataset: " + e.Message);
            }

            return question;
        }

        public void InsertQuestion(SqlConnection dbconn, int testID, string questionText, string answer1Text, string answer2Text, string answer3Text, string answer4Text, string actualAnswer)
        {
            
            testID = Convert.ToInt16(QuestionApostropheCheck(Convert.ToString(testID)));
            questionText = QuestionApostropheCheck(questionText);
            answer1Text = QuestionApostropheCheck(answer1Text);
            answer2Text = QuestionApostropheCheck(answer2Text);
            answer3Text = QuestionApostropheCheck(answer3Text);
            answer4Text = QuestionApostropheCheck(answer4Text);

            string sql = @"insert into Question(test_ID, question_text, answer1_text, answer2_text, answer3_text, answer4_text, actualAnswer)
                            values (" + testID + ",'" + questionText + "', '" + answer1Text + "', '" + answer2Text + "', '" + answer3Text + "', '" + answer4Text + "','" + actualAnswer + "')";

            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, dbconn);

                dbconn.Open();
                sqlCmd.ExecuteNonQuery();
                dbconn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not insert Question : " + e.Message);
            }
            dbconn.Close();
        }

        public DataSet GetSpecificTestQuestions(SqlConnection dbConn, int testID)
        {
            string sql = @"select *
                            from Question
                            where test_ID = " + testID;

            DataSet questions = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbConn);
                adapter.Fill(questions);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Test table " + e.Message);
            }

            return questions;
        }

        //Teacher table access

        public DataSet GetTeacherTable(SqlConnection dbconn)
        {
            string sql = @"select *
                        from Teacher";
            DataSet teachers = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(teachers);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Teacher table as dataset: " + e.Message);
            }

            return teachers;
        }

        public DataSet GetSpecificTeacher(SqlConnection dbconn, int teacherID)
        {
            string sql = @"select *
                        from Teacher
                        where teacher_ID = " + teacherID;
            DataSet teacher = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(teacher);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Teacher table as dataset: " + e.Message);
            }

            return teacher;
        }

        //Test

        public int InsertIntoTests(SqlConnection dbConn, string teacherID, string testName, int testTotal)
        {
            string sql = @"insert into Test(test_Name, teacher_ID, testTotal)
                            values('" + testName + "', " + teacherID + "," + testTotal + ")";

            int testID = 0;
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, dbConn);

                dbConn.Open();
                sqlCmd.ExecuteNonQuery();
                dbConn.Close();

                //Get newly added Teacher details
                sql = @"select *
                        from Test t
                        where test_Name = '" + testName + "' and teacher_ID = " + teacherID + " and testTotal = " + testTotal;

                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbConn);
                DataSet testSpecs = new DataSet();

                adapter.Fill(testSpecs);


                object[] obj = testSpecs.Tables[0].Rows[0].ItemArray;

                testID = Convert.ToInt16(obj[0]);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not insert into Test table " + e.Message);
            }

            dbConn.Close();
            return testID;
        }

        public DataSet PrepTest(SqlConnection dbConn, string teacherName, string subject)
        {
            string sql = @"insert into Teacher(teacherName, taughtSubject)
                            values('" + teacherName + "', '"+ subject +"')";

            DataSet teacherSpecs = new DataSet();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, dbConn);

                dbConn.Open();
                sqlCmd.ExecuteNonQuery();
                dbConn.Close();

                
                //Get newly added Teacher details
                sql = @"select *
                        from Teacher
                        where teacherName = '" + teacherName + "' and taughtSubject = '" + subject + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbConn);

                adapter.Fill(teacherSpecs);

                
            }
            catch(SqlException e)
            {
                Console.WriteLine("Could not insert into Tests " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not insert into Tests " + e.Message);
            }

            return teacherSpecs;

        }

        public DataSet GetTestTable(SqlConnection dbconn)
        {
            string sql = @"select *
                        from Test";

            DataSet tests = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(tests);
            }
            catch(SqlException e)
            {
                Console.WriteLine("Could not retrieve Test table " + e.Message);
            }

            return tests;
        }

        public DataSet GetSpecificTest(SqlConnection dbConn, int testID)
        {
            string sql = @"select *
                            from Test
                            where test_ID = " + testID;

            DataSet tests = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbConn);
                adapter.Fill(tests);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Test table " + e.Message);
            }

            return tests;
        }


        //StudentMark table Access

        public DataSet GetStudentMark(SqlConnection dbconn, string studentNum)
        {
            InjectionCheckWrap(studentNum);

            string sql = @"select s.student_Name,t.test_Name, m.mark
                            from StudentMark m
                            join Student s on m.student_ID = s.student_ID
                            join Test t on m.test_ID = t.test_ID
                            where s.student_Number = " + studentNum;
            DataSet marks = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(marks);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve StudentMark table as dataset: " + e.Message);
            }

            return marks;
        }

        public DataSet GetStudentMarkTable(SqlConnection dbconn)
        {

            string sql = @"select s.student_Name, t.test_Name, mark
                            from StudentMark m
                            join Student s on m.student_ID = s.student_ID
                            join Test t on m.test_ID = t.test_ID";

            DataSet marks = new DataSet();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(marks);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve StudentMark table as dataset: " + e.Message);
            }

            return marks;
        }

        public void InsertMark(SqlConnection dbConn, int studentID, int testID, int mark)
        {
            string sql = @"insert into StudentMark(student_ID, test_ID, mark)
                            values(" + studentID + ", " + testID + "," + mark + ")";
            
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, dbConn);

                dbConn.Open();
                sqlCmd.ExecuteNonQuery();
                dbConn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not insert into Tests " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not insert into Tests " + e.Message);
            }
            

        }

        //Memo - Blend of questions and student Answers
        public DataSet GetStudentQuestionAnswers(SqlConnection dbconn, int studentID)
        {
            DataSet studentQA = new DataSet();

            string sql = @"select a.studentAnswer_ID, a.studentAnswer, q.question_ID, q.actualAnswer
                            from StudentAnswer a
                            join Question q on a.question_ID = q.question_ID
                            where a.student_ID = " + studentID;

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(studentQA);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve StudentQA table as dataset: " + e.Message);
            }

            return studentQA;
        }

        public int CalculateStudentsMark(SqlConnection dbconn, int studentID)
        {
            int mark = 0;
            DataSet studentMark = new DataSet();

            string sql = @"select count(studentAnswer)
                            from StudentAnswer a
                            join Question q on a.question_ID = q.question_ID
                            where a.student_ID = " + studentID + " and a.studentAnswer = actualAnswer";

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, dbconn);
                adapter.Fill(studentMark);

                object[] markCount = studentMark.Tables[0].Rows[0].ItemArray;

                mark = Convert.ToInt16(markCount[0]);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Could not retrieve Student's marks table as dataset: " + e.Message);
            }

            return mark;
        }
    }
}
