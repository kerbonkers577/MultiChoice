using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MultipleChoiceLibrary;

namespace MultipleChoiceApp
{
    class Program
    {
        private static Teacher tempTeacher;
        private static int activeTest;
        private static Student tempStd = new Student();
        private static DataAccess data = new DataAccess();
        

        static void Main(string[] args)
        {            
            Test testToWrite = new Test();
            List<Test> allTests = new List<Test>();
            
            Memo memoToAdd = new Memo();

            //To not jump ahead in the menu
            bool loggedInStudent = false;
            bool activeTeacher = false;

            //TestIDTemp for test selects
            int testSelect = 0;

            SqlConnection dbConn = new SqlConnection();
            string connectionString = Properties.Settings.Default.ConnectionString;

            dbConn = DataAccessTest(connectionString, dbConn);

            Console.WriteLine("Welcome to the multiple choice application\nAre you a:\n(1) Teacher\n(2) Student\n Or would you like to:\n(0) Exit");
            DisplayUserFunctionality();

            //Response acts as LCV
            int response = ValidateRange(Console.ReadLine(), 0, 2);

            while (response != 0)
            {
                if(response == 1)//Teacher
                {
                    response = Convert.ToInt16(DisplayTeacherInterfaceAsync());
                    activeTeacher = true;
                }
                else if(response == 2)//Student
                {
                    Console.WriteLine(String.Format(
                        "{0}\n{1}\n{2}\n", "----------------------", "Student", "----------------------\n"));
                    response = Convert.ToInt16(DisplayStudentLoginInterface());
                    loggedInStudent = true;
                }
                else if(response == 3 && loggedInStudent == true)//New Student
                {
                    Console.WriteLine(String.Format(
                        "{0}\n{1}\n{2}\n", "----------------------", "Student", "----------------------\n"));
                    response = NewStudentMenu(dbConn);
                }
                else if(response == 5 && loggedInStudent == true)//Student Interface
                {
                    Console.WriteLine(String.Format(
                        "{0}\n{1}\n{2}\n", "----------------------", "Student", "----------------------\n"));
                    response = DisplayStudentInterface();
                }
                else if(response == 6 && activeTeacher == true)//Teacher views student's marks
                {
                    ViewStudentsMarks(dbConn);

                    Console.WriteLine("(1) To return");
                    int intialresponse = ValidateRange(Console.ReadLine(), 1, 1);

                    switch (intialresponse)
                    {
                        case 1:
                            response = 1;
                            break;
                    }
                }
                else if(response == 7 && activeTeacher == true)//Prep Test (Author, subject)
                {
                    tempTeacher = PrepTest(dbConn);
                    response = 8;
                    
                }
                else if(response == 8 && activeTeacher == true)//Make the multiple choice question
                {
                    MakeTest(dbConn, tempTeacher);
                    response = 1;
                }
                else if(response == 9 && loggedInStudent == true)//Look at test menu
                { 
                    if(ViewTests(dbConn) == null)
                    {
                        Console.WriteLine("No tests currently available\nPress (1) to return");
                        int intialresponse = ValidateRange(Console.ReadLine(),1,1);

                        switch(intialresponse)
                        {
                            case 1:
                                response = 5;
                                break;
                        }
                    }
                    else
                    {
                        List<string> testNames = new List<string>();
                        DataSet tempTests = ViewTests(dbConn);
                        int maxCount = 1;

                        Console.WriteLine(String.Format(
                        "{0}\n{1}\n{2}\n", "----------------------", "Tests", "----------------------"));

                        object[] row;

                        for (int i = 0; i < tempTests.Tables[0].Rows.Count; i++)
                        {
                            //Loops through arrays as rows so it resets index
                            row = tempTests.Tables[0].Rows[i].ItemArray;
                            testNames.Add("" + row[1]);
                            maxCount++;
                        }

                        for (int i = 0; i < tempTests.Tables[0].Rows.Count; i++)
                        {
                            Console.WriteLine(string.Format("({0}) {1}", i + 1, testNames[i]));
                        }
                        

                        Console.WriteLine("\nEnter Number by entering corresponding number in brackets");
                        testSelect = ValidateRange(Console.ReadLine(), 1, (maxCount));
                        dbConn.Close();
                        response = 11;
                    }
                    
                }
                else if(response == 10 && loggedInStudent == true)//Student views marks
                {
                    //Student obj passed through to view own marks
                    ViewOwnMarks(tempStd, dbConn);
                    Console.WriteLine("(1) To return");
                    int intialresponse = ValidateRange(Console.ReadLine(), 1, 1);

                    switch (intialresponse)
                    {
                        case 1:
                            response = 5;
                            break;
                    }
                }
                else if(response == 11 && loggedInStudent == true)//Student takes selected test
                {
                    //Change for ADO
                    //Push through to database
                    int testID = TakeSelectedTest(dbConn, testSelect);

                    //TODO:
                    //DisplayMemo
                    memoToAdd.DisplayMemo(dbConn, Convert.ToInt16(tempStd.GetID()), testID);

                    Console.WriteLine("Press 1 to return to student menu");
                    response =  ValidateRange(Console.ReadLine(),1,1);

                    switch(response)
                    {
                        case 1:
                            response = 5;
                            break;
                    }
                }
                else if (response == 4)
                {
                    Console.Clear();
                    loggedInStudent = false;
                    activeTeacher = false;
                    Console.WriteLine("Welcome to the multiple choice application\nAre you a:\n(1) Teacher\n(2) Student\n Or would you like to: \n(0) Exit");
                    DisplayUserFunctionality();
                    response = ValidateRange(Console.ReadLine(), 0, 2);
                }
                else if(response == 12)//Student Login Added in assignment 3, hence weird number allocation
                {
                    int exitResponse = 0;

                    Console.Clear();
                    Console.WriteLine("Please enter your student number : ");
                    string num = CheckStringNotNull(Console.ReadLine());
                    Console.WriteLine("Please enter your password : ");
                    string password = CheckStringNotNull(Console.ReadLine());

                    bool validResponse = ExistingStudentMenu(dbConn, num, password);

                    while (exitResponse != 1 && validResponse == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Credentials");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Would you like to return to the main menu or try again?\n(1) To return to main menu\n(0) To Retry");
                        //Short circuts and exits
                        exitResponse = ValidateRange(Console.ReadLine(),0,1);

                        if (exitResponse == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter your student number : ");
                            num = CheckStringNotNull(Console.ReadLine());
                            Console.WriteLine("Please enter your password : ");
                            password = CheckStringNotNull(Console.ReadLine());
                            validResponse = ExistingStudentMenu(dbConn, num, password);
                        }
                    }

                    DataSet loggedStudent;

                    if(exitResponse == 0)
                    {
                        response = 5;
                        //TODO: Set the static student obj as the returned results
                        loggedStudent = data.GetSpecificStudent(dbConn, num);

                        //Loops through arrays as rows so it resets index
                        object[] row = loggedStudent.Tables[0].Rows[0].ItemArray;

                        for (int j = 0; j < row.Length; j++)
                        {
                            tempStd.SetID("" + row[0]);
                            tempStd.SetName("" + row[1]);
                            tempStd.SetStudentNumber("" + row[2]);
                            tempStd.SetPassword("" + row[3]);
                        }

                        


                    }
                    else
                    {
                        response = 4;
                    }
                }
                
            }

        }


        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        ////-------------------------------------------------------------------------------
        
        //// Menu Functionality
         
        ////-------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------


        //Numbering is assigned to method to comply with if statement    
        //All users
        //// VALIDATION
        public static void DisplayUserFunctionality()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter an integer value (e.g 1, 2 etc.) to interact with the program\n\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int ValidateInput(string userInput)
        {
            //Validates input through use of out keyword
            //Allows for checking if the value is actually a number
            int input;
            while (int.TryParse(userInput, out input) == false)
            {
                Console.WriteLine("Please enter an integer value (e.g 1, 2)");
                userInput = Console.ReadLine();
            }

            return input;
        }

        public static int ValidateRange(string input, int min, int max)
        {
            //Takes validated number and puts it through range check
            int validiatedInput;
            validiatedInput = ValidateInput(input);
            while(validiatedInput > max || validiatedInput < min)
            {
                Console.WriteLine("Input out of range");
                Console.WriteLine("Please enter a number that is within range \n(If in a menu, the numbers in the brackets)\n(If creating a test, the number of the answer text(Either 1,2,3 or 4))");
                validiatedInput = ValidateInput(Console.ReadLine());
            }
            return validiatedInput;
        }

        public static string CheckStringNotNull(dynamic toValidate)
        {
            toValidate = Convert.ToString(toValidate);
            while(String.IsNullOrEmpty(toValidate) == true)
            {
                Console.WriteLine("Input cannot be empty");
                Console.WriteLine("Please input something");
                toValidate = Console.ReadLine();
            }

            return toValidate;

        }

        //Teacher
        //1

        public static async Task<int> DisplayTeacherInterfaceAsync()
        {
            return await Task.Run(() => DisplayTeacherLoginInterface());
        }

        public static int DisplayTeacherLoginInterface()
        {
            //Allows navigation for teachers
            Console.Clear();
            Console.WriteLine(String.Format(
                "{0}\n{1}\n{2}", "----------------------", "Teacher's Options", "----------------------"));
            Console.WriteLine("Would you like to:\n(1) Make a new test?\n(2) Review student's marks?\n(3) Return to main menu\n(0) Exit");

            int Response;

            Response = ValidateRange(Console.ReadLine(), 0, 3);

            switch (Response)
            {
                case 0:
                    Response = 0;
                    break;
                case 1:
                    Response = 7;
                    break;
                case 2:
                    Response = 6;
                    break;
                case 3:
                    Response = 4;
                    break;
                    
            }

            return Response;
        }

        //6 Display Marks for teacher
        public static DataSet ViewStudentsMarks(SqlConnection dbConn)
        {
            DataSet marks;
            //Display all marks through db
            marks = data.GetStudentMarkTable(dbConn);

            Console.Clear();

            List<string> studentName = new List<string>();
            List<string> testName = new List<string>();
            List<string> studentMark = new List<string>();

            Console.WriteLine(String.Format(
                "{0}\n{1}\n{2}", "----------------------", "Student's Marks", "----------------------"));

            for (int i = 0; i < marks.Tables[0].Rows.Count; i++)
            {
                //Loops through arrays as rows so it resets index
                object[] row = marks.Tables[0].Rows[i].ItemArray;

                studentName.Add("" + row[0]);
                testName.Add("" + row[1]);
                studentMark.Add("" + row[2]);

                Console.WriteLine(string.Format("Student Name: {0}\nStudent Number{1,5}, \nStudent's Mark:{2,5}", studentName[i],
                                testName[i], studentMark[i]));
            }
            Console.WriteLine();
            return marks;
        }

        //7
        public static Teacher PrepTest(SqlConnection dbConn)
        {
            Console.Clear();

            Console.WriteLine("What is your name?:\n");
            string teacher = CheckStringNotNull(Console.ReadLine());

            Console.WriteLine("What subject do you teach?:\n");
            string subject = CheckStringNotNull(Console.ReadLine());

            DataSet teacherForTest = data.PrepTest(dbConn, teacher, subject);
            Teacher testTeacher = new Teacher();

            for (int i = 0; i < teacherForTest.Tables[0].Rows.Count; i++)
            {
                object[] row = teacherForTest.Tables[0].Rows[i].ItemArray;
                testTeacher.SetID(row[0] + "");
                testTeacher.SetName(row[1] + "");
                testTeacher.SetSubject(row[2] + "");   
            }

            return testTeacher;
        }

        //8
        public static void MakeTest(SqlConnection dbConn, Teacher teacher)
        {
            //Adds details for test author
            Console.Clear();
            Console.WriteLine("What is the name of the test?:\n");
            string testName = CheckStringNotNull(Console.ReadLine());

            int numOfQuestions;
            Console.WriteLine("How many questions would you like to add to this test?\n");
            numOfQuestions = ValidateInput(Console.ReadLine());

            int testID = data.InsertIntoTests(dbConn, teacher.GetTeacherID(), testName, numOfQuestions);

            

            //Cycles through question to add them to the test
            string question = "";
            string answerText1 = "";
            string answerText2 = "";
            string answerText3 = "";
            string answerText4 = "";
            string actualAnswer = "";

            int i = 0;
            for (i = 0; i < numOfQuestions; i++)
            {
                //Question
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Please give the question text for question " + (i + 1) + "\n");
                Console.ForegroundColor = ConsoleColor.White;
                question = CheckStringNotNull(Console.ReadLine());

                //Answer 1 Text
                Console.WriteLine("\nPlease give the answer text for answer 1\n");
                answerText1 = CheckStringNotNull(Console.ReadLine());

                //Answer 2 Text
                Console.WriteLine("\nPlease give the answer text for answer 2\n");
                answerText2 = CheckStringNotNull(Console.ReadLine());

                //Answer 3 Text
                Console.WriteLine("\nPlease give the answer text for answer 3\n");
                answerText3 = CheckStringNotNull(Console.ReadLine());

                //Answer 4 Text
                Console.WriteLine("\nPlease give the answer text for answer 4\n");
                answerText4 = CheckStringNotNull(Console.ReadLine());

                //Answer integer assignment
                Console.WriteLine("\nPlease state the correct answer by typing in either 1, 2, 3 or 4\n");
                actualAnswer = Convert.ToString(ValidateRange(CheckStringNotNull(Console.ReadLine()), 1, 4));

                data.InsertQuestion(dbConn, testID, question, answerText1, answerText2, answerText3, answerText4, actualAnswer);
            }
        }

            
         

        //Student
        //2
        public static async Task<int> DisplayStudentLoginInterfaceASync()
        {
            return await Task.Run(() => DisplayStudentLoginInterface());
        }

        public static int DisplayStudentLoginInterface()
        {
            //Allows navigation for a student
            Console.Clear();
            Console.WriteLine(String.Format(
                        "{0}\n{1}\n{2}\n", "----------------------", "Student", "----------------------"));
            Console.WriteLine("Please enter (1) as a new student\nPlease enter (2) as existing student\n(0) to Exit");
            int Response = ValidateRange(Console.ReadLine(), 0, 2);
            switch(Response)
            {
                case 0:
                    Response = 0;
                    break;
                case 1:
                    Response = 3;
                    break;
                case 2:
                    Response = 12;
                    break;
            }

            return Response;
        }
        //3 New Students
        public static int NewStudentMenu(SqlConnection dbConn)
        {
            //Brings up an interface for a student to enter details
            int Response = 5;
            Console.Clear();
            Console.WriteLine("Please enter your student number(1 - 4 digits long):\n");
            try
            {
                string authenticatedStNum = Console.ReadLine();

                ValidateRange(authenticatedStNum, 1, 9999);


                Console.WriteLine("Please enter your name :\n");
                string name = CheckStringNotNull(Console.ReadLine());

                Console.WriteLine("Please enter a password :\n");
                string password = CheckStringNotNull(Console.ReadLine());

                //TODO:
                //Add new student insert

                data.InsertNewStudent(dbConn, name, authenticatedStNum, password);

                tempStd.SetStudentNumber(authenticatedStNum);
                tempStd.SetName(name);
                
            }
            catch(OverflowException e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Entry");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to confirm\n");
                Console.ReadKey();
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Entry");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press any key to confirm\n");
                Console.ReadKey();
            }
            return Response;
        }

        public static bool ExistingStudentMenu(SqlConnection dbconn, string studentNum, string studentPassword)
        {
            bool correct = false;

            if (data.CheckSpecificStudentLogin(dbconn, studentNum, studentPassword) == true)
            {
                correct = true;
            }

            return correct;
        }
        //5
        public static int DisplayStudentInterface()
        {
            //Allows student navigation pass credentials screen
            int Response;
            Console.Clear();
            Console.WriteLine(String.Format(
                "{0}\n{1}\n{2}", "----------------------", "Student's Options", "----------------------"));

            Console.WriteLine("Would you like to :\n(1) Take a test\n(2) View your marks\n(3) Return to main menu\n(0) Exit");
            Response = ValidateRange(Console.ReadLine(), 0, 3);

            switch(Response)
            {
                case 0:
                    Response = 0;
                    break;
                case 1:
                    Response = 9;
                    break;
                case 2:
                    Response = 10;
                    break;
                case 3:
                    Response = 4;
                    break;
            }

            return Response;
        }

        //9 Student Views Marks
        public static DataSet ViewTests(SqlConnection dbconn)
        {
            DataSet marks;
            //Display all marks through db
            marks = data.GetTestTable(dbconn);
            return marks;
            
        }
        
        //10
        public static void ViewOwnMarks(Student st, SqlConnection dbConn)
        {
            DataSet marks;
            //Display all marks through db
            marks = data.GetStudentMark(dbConn, st.GetStudentNumber());
            Console.Clear();


            List<string> studentName = new List<string>();
            List<string> testName = new List<string>();
            List<string> studentMark = new List<string>();

            Console.WriteLine(String.Format(
                "{0}\n{1}\n{2}", "----------------------", "Student's Marks", "----------------------"));

            for (int i = 0; i < marks.Tables[0].Rows.Count; i++)
            {
                //Loops through arrays as rows so it resets index
                object[] row = marks.Tables[0].Rows[i].ItemArray;

                for (int j = 0; j < row.Length; j++)
                {
                    studentName.Add("" + row[0]);
                    testName.Add("" + row[1]);
                    studentMark.Add("" + row[2]);
                }
                Console.WriteLine(string.Format("Student Name: {0}\nStudent Number{1,5}, \nStudent's Mark:{2,5}", studentName[i],
                                testName[i], studentMark[i]));
            }
            Console.WriteLine();
        }

        //11
        public static int TakeSelectedTest(SqlConnection dbConn, int testSelect)
        {
            Console.Clear();
            //Test Details
            DataSet activeTest = new DataSet();
            
            activeTest = data.GetSpecificTest(dbConn, testSelect);
            List<int> testID = new List<int>();
            List<string> testNames = new List<string>();
            List<int> testCount = new List<int>();
            List<int> teacherID = new List<int>();
            object[] row;

            for (int i = 0; i < activeTest.Tables[0].Rows.Count; i++)
            {
                //Loops through arrays as rows so it resets index
                row = activeTest.Tables[0].Rows[i].ItemArray;
                testID.Add(Convert.ToInt16(row[0]));
                testNames.Add("" + row[1]);
                teacherID.Add(Convert.ToInt16(row[2]));
                testCount.Add(Convert.ToInt16(row[3]));
            }

            DataSet activeTeacher = new DataSet();

            activeTeacher = data.GetSpecificTeacher(dbConn, teacherID[0]);
            List<string> teacherNames = new List<string>();
            List<string> teacherSubjects = new List<string>();

            for (int i = 0; i < activeTeacher.Tables[0].Rows.Count; i++)
            {
                //Loops through arrays as rows so it resets index
                row = activeTeacher.Tables[0].Rows[i].ItemArray;
                teacherNames.Add("" + row[1]);
                teacherSubjects.Add("" + row[2]);
            }

            //Question details
            Console.WriteLine("Teacher Name: " + teacherNames + "\n");
            Console.WriteLine("Teacher Subject: " + teacherSubjects + "\n");
            Console.WriteLine("Test Name : " + testNames[0] + "\n");

            int totalQuestions = testCount[0];

            DataSet questions = new DataSet();
            questions = data.GetSpecificTestQuestions(dbConn, testID[0]);
            List<string> question = new List<string>();

            for (int i = 0; i < totalQuestions; i++)
            {
                row = questions.Tables[0].Rows[i].ItemArray;

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Quesiton - " + row[2]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Answer 1 : " + row[3]);
                Console.WriteLine("Answer 2 : " + row[4]);
                Console.WriteLine("Answer 3 : " + row[5]);
                Console.WriteLine("Answer 4 : " + row[6]);
                DisplayUserFunctionality();

                int answer = ValidateRange(Console.ReadLine(),1,4);

                Student temp = tempStd;
                data.InsertStudentAnswer(dbConn, Convert.ToInt16(tempStd.GetID()), Convert.ToInt16(row[0]), answer);
            }

            return testID[0];
        }

        public static SqlConnection DataAccessTest(string connectionString, SqlConnection dbConn)
        {
            dbConn = data.TestConnection(connectionString, dbConn);
            return dbConn;
        }
    }
}
