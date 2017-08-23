using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceLibrary
{
    public class Test
    {
        //Test class for saving details of test creator and storing collection of questions with it
        private Teacher author;

        public Teacher Author
        {
            set
            {
                author = value;
            }
            get
            {
                return author;
            }
        }

        private string testName;
        List<Question> questions = new List<Question>();

        public void SetTestName(string testName)
        {
            this.testName = testName;
        }

        public string GetTestName()
        {
            return testName;
        }

        public void AddQuestion(Question question)
        {
            questions.Add(question);
        }

        public List<Question> ReturnQuestions()
        {
            return questions;
        }
    }
}
