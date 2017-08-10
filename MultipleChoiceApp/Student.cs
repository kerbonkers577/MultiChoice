using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceApp
{
    class Student : Person
    {
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

        public string GetName()
        {
            return name;
        }

        public string GetStudentNumber()
        {
            return studentNumber;
        }

        public void addMemoForStudent(Memo memo)
        {
            completedTests.Add(memo);
        }

    }
}
