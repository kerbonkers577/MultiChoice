using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceApp
{
    class Memo
    {
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
            for (int i = 0; i < questions.Count; i++)
            {

                Console.WriteLine("Quesiton - " + questions[i].GetQuestionText());
                switch (questions[i].GetActualAnswer())
                {
                    case 1:
                        Console.WriteLine("Answer 1 : " + questions[i].GetAnswer1Text());
                        break;
                    case 2:
                        Console.WriteLine("Answer 2 : " + questions[i].GetAnswer2Text());
                        break;
                    case 3:
                        Console.WriteLine("Answer 2 : " + questions[i].GetAnswer3Text());
                        break;
                    case 4:
                        Console.WriteLine("Answer 4 : " + questions[i].GetAnswer4Text());
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
            Console.WriteLine("Mark : " + mark);

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
