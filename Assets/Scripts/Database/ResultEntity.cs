using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database{
    public struct ResultEntity
    {
        public int _idStudent { get; set; }
        public string _idObject { get; set; }
        public int _correct { get; set; }
        public int _incorrect { get; set; }
        public int _groupCorrect { get; set; }
        public int _groupIncorrect { get; set; }

        public ResultEntity(int idStudent, string idObject){
            _idStudent = idStudent;
            _idObject = idObject;
            _correct = 0;
            _incorrect = 0;
            _groupCorrect = 0;
            _groupIncorrect = 0;
        }

        public ResultEntity(int idStudent, string idObject, int correct, int incorrect){
            _idStudent = idStudent;
            _idObject = idObject;
            _correct = correct;
            _incorrect = incorrect;
            _groupCorrect = 0;
            _groupIncorrect = 0;
        }

        public ResultEntity(int idStudent, string idObject, int correct, int incorrect, int groupCorrect, int groupIncorrect){
            _idStudent = idStudent;
            _idObject = idObject;
            _correct = correct;
            _incorrect = incorrect;
            _groupCorrect = groupCorrect;
            _groupIncorrect = groupIncorrect;
        }
    }

    
}
