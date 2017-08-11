using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceApp
{
    class Program
    {
        private static Teacher tempTeacher;
        private static int activeTest;
        private static Student tempStd = new Student();
        static void Main(string[] args)
        {            
            Test testToWrite = new Test();
            List<Test> allTests = new List<Test>();
            
            Memo memoToAdd = new Memo();

            //To not jump ahead in the menu
            bool loggedInStudent = false;
            bool activeTeacher = false;

            Console.WriteLine("Welcome to the multiple choice application\nAre you a:\n(1) Teacher\n(2) Student\n Or would you like to:\n(0) Exit");
            DisplayUserFunctionality();
            int response = ValidateRange(Console.ReadLine(), 0, 2);

            while (response != 0)
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
                else if(response == 5 && loggedInStudent == true)//Student Interface
                {
                    response = DisplayStudentInterface();
                }
                else if(response == 6 && activeTeacher == true)//Teacher views student's marks
                {
                    ViewStudentsMarks();
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
                    response = PrepTest();
                }
                else if(response == 8 && activeTeacher == true)//Make the multiple choice question
                {
                    testToWrite = MakeTest();
                    testToWrite.Author = tempTeacher;
                    allTests.Add(testToWrite);
                    response = 1;
                }
                else if(response == 9 && loggedInStudent == true)//Look at test menu
                {
                    if(allTests.Count < 1)
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
                        testToWrite = allTests[ViewTests(allTests)];
                        memoToAdd.AddQuestions(testToWrite.ReturnQuestions());
                        response = 11;
                    }
                    
                }
                else if(response == 10 && loggedInStudent == true)//Student views marks
                {
                    ViewOwnMarks();
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
                    tempStd.addMemoForStudent(TakeSelectedTest(testToWrite, memoToAdd));
                    memoToAdd.DisplayMemo();
                    //tempStd.addMemoForStudent(memoToAdd);
                    Console.WriteLine("Press 1 to return to student menu");
                    response =  ValidateRange(Console.ReadLine(),1,1);
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

        //Numbering is assigned to method to comply with if statement
        
            
        //All users
        public static void DisplayUserFunctionality()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter an integer value (e.g 1, 2, 3, 4 etc.) to give an answer\n\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int ValidateInput(string userInput)
        {
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

        //Teacher
        //1
        public static int DisplayTeacherLoginInterface()
        {
            Console.Clear();
            Console.WriteLine("Would you like to:\n(1) Make a new test?\n(2) Review student's marks?\n(3)Return to main menu\n(0) Exit");

            

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
        
        //6
        public static void ViewStudentsMarks()
        {
            Console.Clear();
            tempStd.GetName();
            tempStd.ViewMarks();
        }

        //7
        public static int PrepTest()
        {
            Console.Clear();

            int makeTest = 8;
            

            Console.WriteLine("What is your name?:\n");
            string teacher = Console.ReadLine();

            Console.WriteLine("What subject do you teach?:\n");
            string subject = Console.ReadLine();

            tempTeacher = new Teacher(teacher, subject);
            tempTeacher = new Teacher(teacher, subject);

            return makeTest;
        }

        //8
        public static Test MakeTest()
        {
            Console.WriteLine("What is the name of the test?:\n");
            string testName = Console.ReadLine();

            int numOfQuestions;
            Console.WriteLine("How many questions would you like to add to this test?\n");
            numOfQuestions = ValidateInput(Console.ReadLine());

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
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Please give the question text for question " + (i+1) + "\n");
                Console.ForegroundColor = ConsoleColor.White;
                question = Console.ReadLine();

                //Answer 1 Text
                Console.WriteLine("\nPlease give the answer text for answer 1\n");
                answerText1 = Console.ReadLine();

                //Answer 2 Text
                Console.WriteLine("\nPlease give the answer text for answer 2\n");
                answerText2 = Console.ReadLine();

                //Answer 3 Text
                Console.WriteLine("\nPlease give the answer text for answer 3\n");
                answerText3 = Console.ReadLine();

                //Answer 4 Text
                Console.WriteLine("\nPlease give the answer text for answer 4\n");
                answerText4 = Console.ReadLine();

                //Answer integer assignment
                Console.WriteLine("\nPlease state the correct answer by typing in either 1, 2, 3 or 4\n");
                actualAnswer = ValidateRange(Console.ReadLine(), 1, 4);

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
            Console.WriteLine("Please enter (1) as a new student\n(0) to Exit");
            int Response = ValidateRange(Console.ReadLine(), 0, 1);
            switch(Response)
            {
                case 0:
                    Response = 0;
                    break;
                case 1:
                    Response = 3;
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
                string authenticatedStNum = Console.ReadLine();
                
                while(authenticatedStNum.Length > 8)
                {
                    Console.WriteLine("Student Number is not within digit range (0 - 8 digits long)\n Please enter your student number:\n");
                    Console.ReadLine();
                    authenticatedStNum = Console.ReadLine();
                }


                Console.WriteLine("Please enter your name :\n");
                string name = Console.ReadLine();

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
        //5
        public static int DisplayStudentInterface()
        {
            int Response;
            Console.Clear();
            Console.WriteLine("Would you like to :\n(1) Take a test\n(2) View your marks\n(3)Return to main menu\n(0) Exit");
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

        //9
        public static int ViewTests(List<Test> tests)
        {
            Console.WriteLine("Select a test to write by entering the corresponding number in the brackets");
            for (int i = 0; i < tests.Count; i++)
            {
                Console.WriteLine("(" + i + ") " + tests[i].GetTestName() + "\n");
            }

            activeTest = ValidateRange(Console.ReadLine(), 0, tests.Count);            
            
            return activeTest;
        }
        
        //10
        public static void ViewOwnMarks()
        {
            Console.Clear();
            tempStd.GetName();
            tempStd.ViewMarks();
        }

        //11
        public static Memo TakeSelectedTest(Test activeTest, Memo memoForAdd)
        {
            Console.Clear();

            Console.WriteLine(activeTest.Author.ReturnInfo() + "\n");
            Console.WriteLine("Test Name : " + activeTest.GetTestName() + "\n");

            List<Question> questions = activeTest.ReturnQuestions();
            memoForAdd.AddQuestions(activeTest.ReturnQuestions());

            for (int i = 0; i < questions.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Quesiton - " + questions[i].GetQuestionText());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Answer 1 : " + questions[i].GetAnswer1Text());
                Console.WriteLine("Answer 2 : " + questions[i].GetAnswer2Text());
                Console.WriteLine("Answer 3 : " + questions[i].GetAnswer3Text());
                Console.WriteLine("Answer 4 : " + questions[i].GetAnswer4Text());
                DisplayUserFunctionality();
                memoForAdd.AddStudentsAnswers(ValidateInput(Console.ReadLine()));
            }

            return memoForAdd;
        }
    }
}
