using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;

namespace Database
{
public class ResultDB : SqliteHelper
{
    private const String Tag = "Riz: ResultsDb:\t";

        private const String TABLE_NAME = "Results";
        private const String KEY_ID = "id";
        private const String KEY_STUDENT = "id_student";
        private const String KEY_OBJECT = "id_object";
        private const String KEY_CORRECT = "correct";
        private const String KEY_INCORRECT = "incorrect";
        private String[] COLUMNS = new String[] { KEY_STUDENT, KEY_OBJECT, KEY_CORRECT, KEY_INCORRECT };

        /*
        CREATE TABLE IF NOT EXISTS Results (
  --id  INTEGER PRIMARY KEY, 
  id_student integer, 
  id_object TEXT,
  correct INTEGER NOT NULL DEFAULT 0, 
  incorrect INTEGER NOT NULL DEFAULT 0,
  
  FOREIGN KEY(id_student) 
  REFERENCES Students (id)
  ON DELETE CASCADE, 
  FOREIGN KEY(id_object) 
  REFERENCES Objects(id) 
  ON DELETE CASCADE
  );
  
  CREATE UNIQUE INDEX idx_results_student_object ON Results (id_student, id_object);
  */

        public ResultDB() : base()
        {
            StudentDB stdb = new StudentDB();
            ObjectDB obdb = new ObjectDB();
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                //KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_STUDENT + " INTEGER, " +
                KEY_OBJECT + " TEXT, " + 
                KEY_CORRECT + " INTEGER NOT NULL DEFAULT 0, " + 
                KEY_INCORRECT + " INTEGER NOT NULL DEFAULT 0, " +
                "FOREIGN KEY(id_student) REFERENCES Students (id) ON DELETE CASCADE, " +
                "FOREIGN KEY(id_object) REFERENCES Objects(id) ON DELETE CASCADE) ";
            dbcmd.ExecuteNonQuery();
        }

        /*
        INSERT OR REPLACE INTO Results (id_student, id_object, correct, incorrect) 
VALUES (1, 
        '1',
        (SELECT COALESCE(SUM(correct),0) FROM Results WHERE id_student = 1 AND id_object = '1') + 1,
        (SELECT incorrect FROM Results WHERE id_student = 1 AND id_object = '1')
        );
        */
        
        public void addCorrectResult(ResultEntity obj)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT OR REPLACE INTO " + TABLE_NAME
                + " ( "
                + KEY_STUDENT + ", "
                + KEY_OBJECT + " , "
                + KEY_CORRECT + " , "
                + KEY_INCORRECT + " ) "

                + "VALUES ( '"
                + obj._idStudent + "', '"
                + obj._idObject + "', "
                + "(SELECT COALESCE(SUM("+KEY_CORRECT+"),0) FROM " +TABLE_NAME+ " WHERE "+KEY_STUDENT+" = "+obj._idStudent+" AND "+KEY_OBJECT+" = '"+ obj._idObject +"') + 1, "
                + "(SELECT "+KEY_INCORRECT+" FROM "+TABLE_NAME+" WHERE "+KEY_STUDENT+" = "+obj._idStudent+" AND "+KEY_OBJECT+" = '"+obj._idObject+"')"
                + " )";
            dbcmd.ExecuteNonQuery();
        }

        public void addCorrectResult(int _idStudent, string _idObject)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT OR REPLACE INTO " + TABLE_NAME
                + " ( "
                + KEY_STUDENT + ", "
                + KEY_OBJECT + " , "
                + KEY_CORRECT + " , "
                + KEY_INCORRECT + " ) "

                + "VALUES ( '"
                + _idStudent + "', '"
                + _idObject + "', "
                + "(SELECT COALESCE(SUM("+KEY_CORRECT+"),0) FROM " +TABLE_NAME+ " WHERE "+KEY_STUDENT+" = "+_idStudent+" AND "+KEY_OBJECT+" = '"+ _idObject +"') + 1, "
                + "(SELECT "+KEY_INCORRECT+" FROM "+TABLE_NAME+" WHERE "+KEY_STUDENT+" = "+_idStudent+" AND "+KEY_OBJECT+" = '"+_idObject+"')"
                + " )";
            dbcmd.ExecuteNonQuery();
        }

        /*
        INSERT OR REPLACE INTO Results (id_student, id_object, correct, incorrect) 
VALUES (1, 
        '1',
        (SELECT correct FROM Results WHERE id_student = 1 AND id_object = '1'),
        (SELECT COALESCE(SUM(incorrect),0) FROM Results WHERE id_student = 1 AND id_object = '1') + 1);
        */

        public void addIncorrectResult(ResultEntity obj)
        {

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT OR REPLACE INTO " + TABLE_NAME
                + " ( "
                + KEY_STUDENT + ", "
                + KEY_OBJECT + ", "
                + KEY_CORRECT + " , "
                + KEY_INCORRECT + " ) "

                + "VALUES ( '"
                + obj._idStudent + "', '"
                + obj._idObject + "', "
                + "(SELECT "+KEY_CORRECT+" FROM "+TABLE_NAME+" WHERE "+KEY_STUDENT+" = "+obj._idStudent+" AND "+KEY_OBJECT+" = '"+obj._idObject+"'),"
                + "(SELECT COALESCE(SUM("+KEY_INCORRECT+"),0) FROM "+TABLE_NAME+" WHERE "+KEY_STUDENT+" = "+obj._idStudent+" AND "+KEY_OBJECT+" = '"+obj._idObject+"') + 1);";
            dbcmd.ExecuteNonQuery();
        }

        public void addIncorrectResult(int _idStudent, string _idObject)
        {

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT OR REPLACE INTO " + TABLE_NAME
                + " ( "
                + KEY_STUDENT + ", "
                + KEY_OBJECT + ", "
                + KEY_CORRECT + " , "
                + KEY_INCORRECT + " ) "

                + "VALUES ( '"
                + _idStudent + "', '"
                + _idObject + "', "
                + "(SELECT "+KEY_CORRECT+" FROM "+TABLE_NAME+" WHERE "+KEY_STUDENT+" = "+_idStudent+" AND "+KEY_OBJECT+" = '"+_idObject+"'),"
                + "(SELECT COALESCE(SUM("+KEY_INCORRECT+"),0) FROM "+TABLE_NAME+" WHERE "+KEY_STUDENT+" = "+_idStudent+" AND "+KEY_OBJECT+" = '"+_idObject+"') + 1);";
            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getDataById(int id)
        {
            return base.getDataById(id);
        }

        /*
         SELECT
   (SELECT COALESCE(SUM(correct), 0) FROM Results WHERE id_student = 1 AND id_object = '1') as 'Aciertos usuario',
   (SELECT COALESCE(SUM(incorrect), 0) FROM Results WHERE id_student = 1 AND id_object = '1') as 'Errores usuario',
 	SUM(correct) as 'Aciertos grupo', 
 	SUM(incorrect) as 'Errores grupo'
FROM Results; 
        */

        public IDataReader getDataFromCurrentObject(int _idStudent, string _idObject)
        {
            Debug.Log(Tag + "Getting Results: " + _idStudent);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT"
                + "(SELECT COALESCE(SUM("+KEY_CORRECT+"), 0) FROM "+TABLE_NAME+" WHERE "+KEY_STUDENT+" = "+_idStudent+" AND "+KEY_OBJECT+" = '"+_idObject+"') as 'Aciertos usuario',"
                + "(SELECT COALESCE(SUM("+KEY_INCORRECT+"), 0) FROM "+TABLE_NAME+" WHERE id_student = "+_idStudent+" AND id_object = '"+_idObject+"') as 'Errores usuario',"
                    + "SUM("+KEY_CORRECT+") as 'Aciertos grupo', "
                    + "SUM("+KEY_INCORRECT+") as 'Errores grupo'"
                    +" FROM "+TABLE_NAME;
            return dbcmd.ExecuteReader();
        }

        //TODO: SELECT Students.name, Objects.name, correct, incorrect FROM Results INNER JOIN Students ON Results.id_student = Students.id INNER JOIN Objects ON Results.id_object = Objects.id;

        public override IDataReader getDataByString(string str)
        {
            Debug.Log(Tag + "Getting Object: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public override void deleteDataByString(string id)
        {
            Debug.Log(Tag + "Deleting Object: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void deleteDataById(int id)
        {
            base.deleteDataById(id);
        }

        public override void deleteAllData()
        {
            Debug.Log(Tag + "Deleting Table");

            base.deleteAllData(TABLE_NAME);
        }

        public override IDataReader getAllData()
        {
            return base.getAllData(TABLE_NAME);
        }
 }
}