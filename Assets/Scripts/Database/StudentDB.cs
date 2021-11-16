using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database {
    public class StudentDB : SqliteHelper {
        private const String Tag = "StudentsDb:\t";

        private const String TABLE_NAME = "Students";
        private const String KEY_ID = "id";
        private const String KEY_NAME = "name";
        private const String KEY_LASTNAME1 = "lastname1";
        private const String KEY_LASTNAME2 = "lastname2";
        private const String KEY_NICKNAME = "nickname";
        private const String KEY_LISTNUMBER = "listnumber";
        private const String KEY_CLASSROOM = "classroom";
        private const String KEY_GRADE = "grade";
        private const String KEY_STARS = "stars";
        private const String KEY_TROPHIES = "trophies";
        private String[] COLUMNS = new String[] { KEY_ID, KEY_NAME, KEY_LASTNAME1, KEY_LASTNAME2, KEY_NICKNAME, KEY_LISTNUMBER, KEY_CLASSROOM, KEY_GRADE, KEY_STARS, KEY_TROPHIES };

        public StudentDB() : base() {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_NAME + " TEXT NOT NULL, " +
                KEY_LASTNAME1 + " TEXT, " +
                KEY_LASTNAME2 + " TEXT, " +
                KEY_NICKNAME + " TEXT NOT NULL, " +
                KEY_LISTNUMBER + " INTEGER, " +
                KEY_CLASSROOM + " TEXT DEFAULT 'A', " +
                KEY_GRADE + " INTEGER DEFAULT 0, " +
                KEY_STARS + " INTEGER DEFAULT 0, " +
                KEY_TROPHIES + " INTEGER DEFAULT 0 )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(StudentEntity obj) {
            if (obj._id != 0) {
                addOrReplaceData(obj);
                return;
            }

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_NAME + ", "
                + KEY_LASTNAME1 + " , "
                + KEY_LASTNAME2 + " , "
                + KEY_NICKNAME + " , "
                + KEY_LISTNUMBER + " , "
                + KEY_CLASSROOM + " , "
                + KEY_GRADE + " , "
                + KEY_STARS + " , "
                + KEY_TROPHIES + " ) "

                + "VALUES ( '"
                + obj._name + "', '"
                + obj._lastName1 + "', '"
                + obj._lastName2 + "', '"
                + obj._nickname + "', '"
                + obj._listNumber + "', '"
                + obj._classroom + "', '"
                + obj._grade + "', '"
                + obj._stars + "', '"
                + obj._trophies + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void addOrReplaceData(StudentEntity obj) {
            if (obj._id == 0) {
                addData(obj);
                return;
            }

            Debug.Log("Adding Student: " + obj.ToString());

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT OR REPLACE INTO " + TABLE_NAME
                + " ( "
                + KEY_ID + ", "
                + KEY_NAME + ", "
                + KEY_LASTNAME1 + " , "
                + KEY_LASTNAME2 + " , "
                + KEY_NICKNAME + " , "
                + KEY_LISTNUMBER + " , "
                + KEY_CLASSROOM + " , "
                + KEY_GRADE + " , "
                + KEY_STARS + " , "
                + KEY_TROPHIES + " ) "

                + "VALUES ( '"
                + obj._id + "', '"
                + obj._name + "', '"
                + obj._lastName1 + "', '"
                + obj._lastName2 + "', '"
                + obj._nickname + "', '"
                + obj._listNumber + "', '"
                + obj._classroom + "', '"
                + obj._grade + "', '"
                + obj._stars + "', '"
                + obj._trophies + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getDataById(int id) {
            Debug.Log(Tag + "Getting Object: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = " + id;
            return dbcmd.ExecuteReader();
        }

        public override IDataReader getDataByString(string str) {
            Debug.Log(Tag + "Getting Object: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public override void deleteDataByString(string id) {
            Debug.Log(Tag + "Deleting Object: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void deleteDataById(int id) {
            base.deleteDataById(id);
        }

        public override void deleteAllData() {
            Debug.Log(Tag + "Deleting Table");

            base.deleteAllData(TABLE_NAME);
        }

        public override IDataReader getAllData() {
            return base.getAllData(TABLE_NAME);
        }

        /* EXAMPLE FUNCTIONS

        public IDataReader getNearestLocation(LocationInfo loc)
        {
            Debug.Log(Tag + "Getting nearest centoid from: "
                + loc.latitude + ", " + loc.longitude);
            IDbCommand dbcmd = getDbCommand();

            string query =
                "SELECT * FROM "
                + TABLE_NAME
                + " ORDER BY ABS(" + KEY_NAME + " - " + loc.latitude
                + ") + ABS(" + KEY_DESC + " - " + loc.longitude + ") ASC LIMIT 1";

            dbcmd.CommandText = query;
            return dbcmd.ExecuteReader();
        }

        public IDataReader getLatestTimeStamp()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " ORDER BY " + KEY_DATE + " DESC LIMIT 1";
            return dbcmd.ExecuteReader();
        }
        */
    }
}
