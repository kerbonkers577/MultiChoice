﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleChoiceApp
{
    class Student : Person
    {
        private string studentNumber;

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

    }
}