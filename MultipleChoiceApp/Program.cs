﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceApp
{
    class Program
    {
        private static Test tempTest;
        private static Teacher tempTeacher;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the multiple choice application\nAre you a:\n(1) Teacher\n(2) Student");
            DisplayUserFunctionality();

            int response = Convert.ToInt32(Console.ReadLine());
            Test testToWrite = new Test();
            Teacher teacherforTest = new Teacher();

            //TODO: Value denoting where in the menu you currently are
            //E.g. 0 = exit
            //But depending on the value, it will tka you to a specfic switch statement which
            //will have a static method to show an interface with methods to return to the previous menu
            //Navigate menu with switch statement wrapped by while loop

            //To not jump ahead in the menu
            bool loggedInStudent = false;
            bool activeTeacher = false;

            
            while(response != 0)
            {
                if(response == 1)//Teacher
                {
                    response = DisplayTeacherLoginInterface();
                    activeTeacher = true;
                }
                else if(response == 2)//Student
                {
                    response = DisplayStudentLoginInterface();
                    loggedInStudent = true;
                }
                else if(response == 3 && loggedInStudent == true)//New Student
                {
                    response = NewStudentMenu();
                }
                else if(response == 4 && loggedInStudent == true)//Existing Student
                {
                    ExisitingStudentMenu();
                }
                else if(response == 5 && loggedInStudent == true)//Student Interface
                {
                    DisplayStudentInterface();
                }
                else if(response == 6 && activeTeacher == true)
                {
                    ViewStudentsMarks();
                }
                else if(response == 7 && activeTeacher == true)
                {
                    teacherforTest = PrepTest();
                    response = 8;
                }
                else if(response == 8 && activeTeacher == true)
                {
                    testToWrite = MakeTest();
                    testToWrite.Author = teacherforTest;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to the multiple choice application\nAre you a:\n(1) Teacher\n(2) Student");
                    DisplayUserFunctionality();

                    response = Convert.ToInt32(Console.ReadLine());
                }
            }

        }

        //---------------------------------
        //TODO: Add restrictions to input range
        //If user inputs 3 with only 1 and 2 as options
        //bring up warning
        //---------------------------------

        //---------------------------------
        //TODO: Use return response to allow user to cancel current functionality
        // and return to previous menu by returning that menu's value
        //---------------------------------

        //Numbering is assigned to method to comply with switch statement
        //All users
        public static void DisplayUserFunctionality()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter an integer value (e.g 1, 2, 3, 4 etc.) to give an answer");
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Teacher
        //1
        public static int DisplayTeacherLoginInterface()
        {
            Console.Clear();
            Console.WriteLine("Would you like to:\n(1) Make a new test?\n(2) Review student's marks?");

            int Response = Convert.ToInt16(Console.ReadLine());
            switch (Response)
            {
                case 1:
                    Response = 7;
                    break;
                case 2:
                    Response = 6;
                    break;
            }

            return Response;
        }
        
        //6
        public static void ViewStudentsMarks()
        {
            //Return student to display
        }

        //7
        public static Teacher PrepTest()
        {
            Console.Clear();

            int makeTest = 8;
            Teacher aTeacher;

            Console.WriteLine("What is your name?:\n");
            string teacher = Console.ReadLine();

            Console.WriteLine("What subject do you teach?:\n");
            string subject = Console.ReadLine();

            tempTeacher = new Teacher(teacher, subject);
            aTeacher = new Teacher(teacher, subject);
            return aTeacher;

        }

        //8
        public static Test MakeTest()
        {
            Console.WriteLine("What is the name of the test?:\n");
            string testName = Console.ReadLine();

            int numOfQuestions;
            Console.WriteLine("How many questions would you like to add to this test?\n");
            numOfQuestions = Convert.ToInt32(Console.ReadLine());

            Test aTest = new Test();
            aTest.SetTestName(testName);
            Question aQuesation;

            string question = "";
            string answerText1 = "";
            string answerText2 = "";
            string answerText3 = "";
            string answerText4 = "";
            int actualAnswer;
            for (int i = 0; i < numOfQuestions; i++)
            {
                //Question
                Console.WriteLine("Please give the question text for question " + (i+1) + "\n");
                question = Console.ReadLine();

                //Answer 1 Text
                Console.WriteLine("Please give the answer text for answer 1\n");
                answerText1 = Console.ReadLine();

                //Answer 2 Text
                Console.WriteLine("Please give the answer text for answer 2\n");
                answerText2 = Console.ReadLine();

                //Answer 3 Text
                Console.WriteLine("Please give the answer text for answer 3\n");
                answerText3 = Console.ReadLine();

                //Answer 4 Text
                Console.WriteLine("Please give the answer text for answer 4\n");
                answerText4 = Console.ReadLine();

                //Answer integer assignment
                Console.WriteLine("Please state the correct answer by typing in either 1, 2, 3 or 4\n");
                actualAnswer = Convert.ToInt16(Console.ReadLine());

                aQuesation = new Question(question, answerText1, answerText2, answerText3, answerText4, actualAnswer);
                aTest.AddQuestion(aQuesation);
            }
            return aTest;
        }
         

        //Student
        //2
        public static int DisplayStudentLoginInterface()
        {
            Console.Clear();
            Console.WriteLine("Are you a(n) :\n(1) New student\n(2) Existing student");
            int Response = Convert.ToInt16(Console.ReadLine());
            switch(Response)
            {
                case 1:
                    Response = 3;
                    break;
                case 2:
                    Response = 4;
                    break;
            }

            return Response;
        }
        //3
        public static int NewStudentMenu()
        {
            int Response = 5;
            Console.Clear();
            Console.WriteLine("Please enter your student number(0 - 8 digits long):\n");
            try
            {
                int studentNumber = Convert.ToInt32(Console.ReadLine());
                string authenticatedStNum = studentNumber + "";
                while(authenticatedStNum.Length > 8)
                {
                    Console.WriteLine("Student Number is not within digit range (0 - 8 digits long)\n Please enter your student number:\n");
                    Console.ReadLine();
                }
                
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
        //4
        public static int ExisitingStudentMenu()
        {
            int passSuccessful = 5;//Pass value back if correct login
            Console.Clear();
            Console.WriteLine("Please enter your student number to login:\n");
            return passSuccessful;
        }
        //5
        public static int DisplayStudentInterface()
        {
            int Response;
            Console.Clear();
            Console.WriteLine("Would you like to :\n(1) Take a test\n(2) View your marks");
            Response = Convert.ToInt16(Console.ReadLine());

            switch(Response)
            {
                case 1:
                    break;
                case 2:
                    break;
            }

            return Response;
        }
    }
}
