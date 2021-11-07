using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database
{
    public struct StudentEntity
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public string _lastName1 { get; set; }
        public string _lastName2 { get; set; }
        public string _nickname { get; set; }
        public int _listNumber { get; set; }
        public string _classroom { get; set; }
        public int _grade { get; set; }

        public StudentEntity(int id, string name, string lastName1, string lastName2, string nickname, int listNumber, string classroom, int grade)
        {
            _id = id;
            _name = name;
            _lastName1 = lastName1;
            _lastName2 = lastName2;
            _nickname = nickname;
            _listNumber = listNumber;
            _classroom = classroom;
            _grade = grade;
        }

        public StudentEntity(int id, string name, string lastName1, string lastName2, string nickname)
        {
            _id = id;
            _name = name;
            _lastName1 = lastName1;
            _lastName2 = lastName2;
            _nickname = nickname;
            _listNumber = 0;
            _classroom = "Default";
            _grade = 0;
        }

        public StudentEntity(int id, string name, string lastName1, string lastName2, string nickname, int listNumber)
        {
            _id = id;
            _name = name;
            _lastName1 = lastName1;
            _lastName2 = lastName2;
            _nickname = nickname;
            _listNumber = listNumber;
            _classroom = "Default";
            _grade = 0;
        }

        public StudentEntity(string name, string lastName1, string lastName2, string nickname, int listNumber)
        {
            _id = 0;
            _name = name;
            _lastName1 = lastName1;
            _lastName2 = lastName2;
            _nickname = nickname;
            _listNumber = listNumber;
            _classroom = "Default";
            _grade = 0;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
