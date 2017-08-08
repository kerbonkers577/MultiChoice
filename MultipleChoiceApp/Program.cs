using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceApp
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the multiple choice application\nAre you a:\n(1) Teacher\n(2) Student");
            DisplayUserFunctionality();

            int response = Convert.ToInt32(Console.ReadLine());

            

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
        public static void PrepTest()
        {
            Console.Clear();
            Console.WriteLine("What is the name of the test?:\n");
            string testName = Console.ReadLine();

            Test newTest = new Test();

        }

        //8
        public static void MakeTest()
        {

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
