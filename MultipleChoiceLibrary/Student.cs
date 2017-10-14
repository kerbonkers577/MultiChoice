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
        private string id;
        private string studentNumber;
        private string password;
        private List<Memo> completedTests = new List<Memo>();

        public Student() { }

        public Student(string id, string name, string password, string studentNumber) : base(name)
        {
            this.id = id;
            this.studentNumber = studentNumber;
        }

        public override string ReturnInfo()
        {
            string info;
            info = String.Format("Name : {0}\nStudent Number : {1}", name, studentNumber);
            return info;
        }

        public void SetID(string id)
        {
            this.id = id;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetStudentNumber(string studentNumber)
        {
            this.studentNumber = studentNumber;
        }

        public void SetPassword(string password)
        {
            this.password = password;
        }

        public string GetID()
        {
            return id;
        }

        public string GetPassword()
        {
            return password;
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
                Console.WriteLine(memo.GetTestName());
                Console.WriteLine(memo.GetMark());
            }
        }

        ~Student() { }
    }
}
