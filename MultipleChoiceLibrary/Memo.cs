using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MultipleChoiceLibrary
{
    public class Memo
    {
        //Hold information regarding test as well as student's answers
        //Holds logic for marking a test based on Question objects
        enum options{correct = '\u2713' , incorrect = 'X'};

        private DataAccess data = new DataAccess();
        private List<int> studentAnswers = new List<int>();
        char option;
        int mark;
        string testName;

        

        public void AddStudentsAnswers(int answer)
        {
            studentAnswers.Add(answer);
        }

        public void DisplayMemo(SqlConnection dbConn, int studentID, int testID)
        {
            Console.Clear();

            DataSet QA = data.GetStudentQuestionAnswers(dbConn, studentID);
            DataSet Questions = data.GetSpecificTestQuestions(dbConn, testID);

            int count;
            object[] QARow;
            object[] QRow;

            //Banner
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("                     M E M O                     ");
            Console.WriteLine("------------------------------------------------");

            List<int> StudentsAnswers = new List<int>();
            Question tempQuestion = new Question();

            count = Questions.Tables[0].Rows.Count;

            for (int i = 0; i < count; i++)
            {
                QRow = Questions.Tables[0].Rows[i].ItemArray;
                QARow = QA.Tables[0].Rows[i].ItemArray;

                //Quesitons list population
                //tempQuestion = new Question((QRow[2] + ""), (QRow[3] + ""),
                //        (QRow[4] + ""), (QRow[5] + ""), (QRow[6] + ""), Convert.ToInt16(QRow[7]));


                tempQuestion.SetQuestionText(QRow[2] + "");
                tempQuestion.SetAnswer1Text(QRow[3] + "");
                tempQuestion.SetAnswer2Text(QRow[4] + "");
                tempQuestion.SetAnswer3Text(QRow[5] + "");
                tempQuestion.SetAnswer4Text(QRow[6] + "");

                //StudentAnswers list population
                StudentsAnswers = StudentsAnswers = new List<int>();
                StudentsAnswers.Add(Convert.ToInt16(QARow[1]));
                StudentsAnswers.Add(Convert.ToInt16(QARow[3]));
                


                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Quesiton - " + tempQuestion.GetQuestionText());
                Console.ForegroundColor = ConsoleColor.White;

                //Loops through displaying the question, answer texts, correct answer and the student's answer weighed up against it.

                Console.WriteLine(string.Format("Answer 1: {0}\nAnswer 2: {1}\nAnswer 3: {2}\nAnswer 4: {3}\n",
                    tempQuestion.GetAnswer1Text(), tempQuestion.GetAnswer2Text(), tempQuestion.GetAnswer3Text(), tempQuestion.GetAnswer4Text()));


                switch (StudentsAnswers[1])
                {
                    case 1:
                        Console.WriteLine("Correct Answer : " + tempQuestion.GetAnswer1Text());
                        break;
                    case 2:
                        Console.WriteLine("Correct Answer : " + tempQuestion.GetAnswer2Text());
                        break;
                    case 3:
                        Console.WriteLine("Correct Answer : " + tempQuestion.GetAnswer3Text());
                        break;
                    case 4:
                        Console.WriteLine("Correct Answer : " + tempQuestion.GetAnswer4Text());
                        break;
                }

                switch (StudentsAnswers[0])
                {
                    case 1:
                        Console.WriteLine("Student's Answer : " + tempQuestion.GetAnswer1Text());
                        break;
                    case 2:
                        Console.WriteLine("Student's Answer : " + tempQuestion.GetAnswer2Text());
                        break;
                    case 3:
                        Console.WriteLine("Student's Answer : " + tempQuestion.GetAnswer3Text());
                        break;
                    case 4:
                        Console.WriteLine("Student's Answer : " + tempQuestion.GetAnswer4Text());
                        break;
                }

                if (StudentsAnswers[0] == StudentsAnswers[1])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine((option = (char)options.correct) + "\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (StudentsAnswers[0] != StudentsAnswers[1])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine((option = (char)options.incorrect) + "\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }

            int mark = data.CalculateStudentsMark(dbConn, studentID);
            data.InsertMark(dbConn, studentID, testID, mark);
            Console.WriteLine(string.Format("Mark : {0} / {1}", mark, count));

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
