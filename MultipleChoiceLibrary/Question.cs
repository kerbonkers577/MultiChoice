using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceLibrary
{
    public class Question
    {
        //Quesiton holds text and actual int answer for a question for a test
        private string questionText;
        private string answer1;
        private string answer2;
        private string answer3;
        private string answer4;
        private int actualAnswer;

        public Question() { }

        public Question(string questionText, string answer1, string answer2, string answer3, string answer4, int actualAnswer)
        {
            //Create Singular Question for test
            this.questionText = questionText;
            this.answer1 = answer1;
            this.answer2 = answer2;
            this.answer3 = answer3;
            this.answer4 = answer4;
            this.actualAnswer = actualAnswer;
        }

        //Accessors
        public string GetQuestionText()
        {
            return questionText;
        }

        public string GetAnswer1Text()
        {
            return answer1;
        }

        public string GetAnswer2Text()
        {
            return answer2;
        }

        public string GetAnswer3Text()
        {
            return answer3;
        }

        public string GetAnswer4Text()
        {
            return answer4;
        }

        public int GetActualAnswer()
        {
            return actualAnswer;
        }

        //Mutators
        public void SetQuestionText(string questionText)
        {
            this.questionText = questionText;
        }

        public void SetAnswer1Text(string answer1)
        {
            this.answer1 = answer1;
        }

        public void SetAnswer2Text(string answer2)
        {
            this.answer2 = answer2;
        }

        public void SetAnswer3Text(string answer3)
        {
            this.answer3 = answer3;
        }

        public void SetAnswer4Text(string answer4)
        {
            this.answer4 = answer4;
        }
    }
}
