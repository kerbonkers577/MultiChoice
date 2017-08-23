using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceLibrary
{
    public class Student : Person
    {
        //Stores info regarding to test
        private string studentNumber;
        private List<Memo> completedTests = new List<Memo>();

        public Student() { }

        public Student(string name, string studentNumber) : base(name)
        {
            this.studentNumber = studentNumber;
        }

        public override string ReturnInfo()
        {
            string info;
            info = String.Format("Name : {0}\nStudent Number : {1}", name, studentNumber);
            return info;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetStudentNumber(string studentNumber)
        {
            this.studentNumber = studentNumber;
        }

        public string GetName()
        {
            return name;
        }

        public string GetStudentNumber()
        {
            return studentNumber;
        }


        //Student can hold multiple memos from completed tests
        public void addMemoForStudent(Memo memo)
        {
            completedTests.Add(memo);
        }

        public void ViewMarks()
        {
            foreach(Memo memo in completedTests)
            {
                memo.GetTestName();
                memo.GetMark();
            }
        }
    }
}
