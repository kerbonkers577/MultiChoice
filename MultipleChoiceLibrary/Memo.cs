using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceLibrary
{
    public class Memo
    {
        //Hold information regarding test as well as student's answers
        //Holds logic for marking a test based on Question objects
        enum options{correct = '\u2713' , incorrect = 'X'};

        List<Question> questions = new List<Question>();
        List<int> studentAnswers = new List<int>();
        char option;
        int mark;
        string testName;

        public void AddQuestions(List<Question> testQuestions)
        {
            questions = testQuestions;
        }

        public void AddStudentsAnswers(int answer)
        {
            studentAnswers.Add(answer);
        }

        public void DisplayMemo()
        {
            Console.Clear();
            //Banner
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("                     M E M O                     ");
            Console.WriteLine("------------------------------------------------");

            for (int i = 0; i < questions.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Quesiton - " + questions[i].GetQuestionText());
                Console.ForegroundColor = ConsoleColor.White;

                

                Console.WriteLine(string.Format("Answer 1: {0}\nAnswer 2: {0}\nAnswer 3: {0}\nAnswer 4: {0}\n", 
                    questions[i].GetAnswer1Text(), questions[i].GetAnswer2Text(), questions[i].GetAnswer3Text(), questions[i].GetAnswer4Text()));


                switch (questions[i].GetActualAnswer())
                {
                    case 1:
                        Console.WriteLine("Correct Answer : " + questions[i].GetAnswer1Text());
                        break;
                    case 2:
                        Console.WriteLine("Correct Answer : " + questions[i].GetAnswer2Text());
                        break;
                    case 3:
                        Console.WriteLine("Correct Answer : " + questions[i].GetAnswer3Text());
                        break;
                    case 4:
                        Console.WriteLine("Correct Answer : " + questions[i].GetAnswer4Text());
                        break;
                }

                switch (studentAnswers[i])
                {
                    case 1:
                        Console.WriteLine("Student's Answer : " + questions[i].GetAnswer1Text());
                        break;
                    case 2:
                        Console.WriteLine("Student's Answer : " + questions[i].GetAnswer2Text());
                        break;
                    case 3:
                        Console.WriteLine("Student's Answer : " + questions[i].GetAnswer3Text());
                        break;
                    case 4:
                        Console.WriteLine("Student's Answer : " + questions[i].GetAnswer4Text());
                        break;
                }

                if (questions[i].GetActualAnswer() == studentAnswers[i])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine((option = (char)options.correct) + "\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    mark++;
                }

                if (questions[i].GetActualAnswer() != studentAnswers[i])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine((option = (char)options.incorrect) + "\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            Console.WriteLine(string.Format("Mark : {0} / {0}", mark, (questions.Count + 1)));

        }


        public string GetTestName()
        {
            return testName;
        }

        public void SetTestName(string testName)
        {
            this.testName = testName;
        }

        public int GetMark()
        {
            return mark;
        }
    }
}
