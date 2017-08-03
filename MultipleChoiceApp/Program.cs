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
    }
}
