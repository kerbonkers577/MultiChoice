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
            Console.WriteLine("Welcome to the multiple choice application\nAre you a:\n(1) Student\n(2) Teacher");
            DisplayUserFunctionality();

            int response = Convert.ToInt16(Console.ReadKey());

            //TODO: Value denoting where in the menu you currently are
            //E.g. 0 = exit
            //But depending on the value, it will tka you to a specfic switch statement which
            //will have a static method to show an interface with methods to return to the previous menu

            if(response == 1)
            {

            }
            else if(response == 2)
            {

            }

            Console.ReadKey();
        }

        public static void DisplayUserFunctionality()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Enter an integer value (e.g 1, 2, 3, 4 etc.) to give an answer");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DispalyStudentLoginInterface()
        {
            Console.WriteLine("Are you a(n) :\n(1) New student\n(2) Existing student");
            int Response = Convert.ToInt16(Console.ReadKey());

            if(Response == 1)
            {

            }
        }

        public static void DisplayStudentInterface()
        {
            string interfaceOptions = "";
        }
    }
}
