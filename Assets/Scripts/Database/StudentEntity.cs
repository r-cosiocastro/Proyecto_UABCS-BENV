using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Database {
    public struct StudentEntity {
        public int _id { get; set; }
        public string _name { get; set; }
        public string _lastName1 { get; set; }
        public string _lastName2 { get; set; }
        public string _nickname { get; set; }
        public int _listNumber { get; set; }
        public string _classroom { get; set; }
        public int _grade { get; set; }
        public int _stars { get; set; }
        public int _trophies { get; set; }



        public StudentEntity(int id, string name, string lastName1, string lastName2, string nickname, int listNumber, string classroom, int grade, int stars, int trophies) {
            _id = id;
            _name = name;
            _lastName1 = lastName1;
            _lastName2 = lastName2;
            _nickname = nickname;
            _listNumber = listNumber;
            _classroom = classroom;
            _grade = grade;
            _stars = stars;
            _trophies = trophies;
        }

        public StudentEntity(int id, string name, string lastName1, string lastName2, string nickname, int listNumber, string classroom, int grade) {
            _id = id;
            _name = name;
            _lastName1 = lastName1;
            _lastName2 = lastName2;
            _nickname = nickname;
            _listNumber = listNumber;
            _classroom = classroom;
            _grade = grade;
            _stars = 0;
            _trophies = 0;
        }



        public StudentEntity(int id, string name, string lastName1, string lastName2, string nickname, int listNumber, int stars, int trophies) {
            _id = id;
            _name = name;
            _lastName1 = lastName1;
            _lastName2 = lastName2;
            _nickname = nickname;
            _listNumber = listNumber;
            _classroom = "Default";
            _grade = 0;
            _stars = stars;
            _trophies = trophies;
        }

        public StudentEntity(int id, string name, string lastName1, string lastName2, string nickname, int listNumber) {
            _id = id;
            _name = name;
            _lastName1 = lastName1;
            _lastName2 = lastName2;
            _nickname = nickname;
            _listNumber = listNumber;
            _classroom = "Default";
            _grade = 0;
            _stars = 0;
            _trophies = 0;
        }

        public StudentEntity Empty() {
            return new StudentEntity(0, "Default", "Default", "Default", "Default", 0, "Default", 0, 0, 0);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return "id: " + _id
            + ", name: " + _name
            + ", lastName1: " + _lastName1
            + ", lastName2: " + _lastName2
            + ", nickname: " + _nickname
            + ", listNumber: " + _listNumber
            + ", classroom: " + _classroom
            + ", grade: " + _grade
            + ", stars: " + _stars
            + ", trophies: " + _trophies;
        }

        public List<StudentEntity> GetAllStudents() {
            StudentDB studentDB2 = new StudentDB();
            System.Data.IDataReader reader = studentDB2.getAllData();

            int fieldCount = reader.FieldCount;
            List<StudentEntity> studentsList = new List<StudentEntity>();
            while (reader.Read()) {
                StudentEntity entity = new StudentEntity(reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetInt32(5),
                    reader.GetString(6),
                    reader.GetInt32(7),
                    reader.GetInt32(8),
                    reader.GetInt32(9));

                studentsList.Add(entity);
            }
            studentDB2.close();
            return studentsList;
        }

        public void AddStar(int amount) {
            this._stars = +amount;
            StudentDB studentDB = new StudentDB();
            studentDB.addOrReplaceData(this);
        }

        public void UpdateStudent() {
            UpdateData();
        }

        private void UpdateData() {
            System.Data.IDataReader reader = new StudentDB().getDataById(this._id);
            reader.Read();
            this._id = reader.GetInt32(0);
            this._name = reader.GetString(1);
            this._lastName1 = reader.GetString(2);
            this._lastName2 = reader.GetString(3);
            this._nickname = reader.GetString(4);
            this._listNumber = reader.GetInt32(5);
            this._classroom = reader.GetString(6);
            this._grade = reader.GetInt32(7);
            this._stars = reader.GetInt32(8);
            this._trophies = reader.GetInt32(9);
            reader.Close();
        }

        public void AddTrohpy(int amount) {
            StudentDB studentDB = new StudentDB();
            _trophies += amount;
            studentDB.addOrReplaceData(this);
        }

    }
}
