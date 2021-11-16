using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database {
    public struct ResultEntity {
        public int _idStudent { get; set; }
        public string _idObject { get; set; }
        public int _correct { get; set; }
        public int _incorrect { get; set; }
        public int _groupCorrect { get; set; }
        public int _groupIncorrect { get; set; }

        public ResultEntity(int idStudent, string idObject) {
            _idStudent = idStudent;
            _idObject = idObject;
            _correct = 0;
            _incorrect = 0;
            _groupCorrect = 0;
            _groupIncorrect = 0;
        }

        public ResultEntity(int idStudent, string idObject, int correct, int incorrect) {
            _idStudent = idStudent;
            _idObject = idObject;
            _correct = correct;
            _incorrect = incorrect;
            _groupCorrect = 0;
            _groupIncorrect = 0;
        }

        public ResultEntity(int idStudent, string idObject, int correct, int incorrect, int groupCorrect, int groupIncorrect) {
            _idStudent = idStudent;
            _idObject = idObject;
            _correct = correct;
            _incorrect = incorrect;
            _groupCorrect = groupCorrect;
            _groupIncorrect = groupIncorrect;
        }

        public ResultEntity GetResults(int idStudent, string idObject) {
            ResultDB resultDB = new ResultDB();
            System.Data.IDataReader reader = resultDB.getDataFromResults(idStudent, idObject);
            reader.Read();
            ResultEntity entity = new ResultEntity(reader.GetInt32(0),
                reader.GetString(1),
                reader.GetInt32(2),
                reader.GetInt32(3),
                reader.GetInt32(4),
                reader.GetInt32(5));

            resultDB.close();
            return entity;
        }

        public ResultEntity GetResults(StudentEntity studentEntity, ObjectEntity objectEntity) {
            ResultDB resultDB = new ResultDB();
            ResultEntity entity;
            System.Data.IDataReader reader1 = resultDB.getNumOfRows(studentEntity, objectEntity);

            reader1.Read(); int numRows = reader1.GetInt32(0);

            if (numRows > 0) {
                System.Data.IDataReader reader = resultDB.getDataFromResults(studentEntity, objectEntity);
                reader.Read();
                entity = new ResultEntity(studentEntity._id,
                    objectEntity._id,
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3));

                Debug.Log("Result: " + studentEntity._nickname + " has " + entity._correct + " correct answers in " + objectEntity._name + "; Group has " + entity._groupCorrect);
            } else {
                entity = new ResultEntity(studentEntity._id, objectEntity._id);
            }
            resultDB.close();
            return entity;
        }

        public void AddCorrectAnswer() {
            ResultDB resultDB = new ResultDB();
            resultDB.addCorrectResult(this);
            _correct++;
            _groupCorrect++;
        }

        public void AddIncorrectAnswer() {
            ResultDB resultDB = new ResultDB();
            resultDB.addIncorrectResult(this);
            _incorrect++;
            _groupIncorrect++;
        }
    }


}
