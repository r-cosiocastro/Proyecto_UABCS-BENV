using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class ObjectDB : SqliteHelper
    {
        private const String Tag = "Riz: ObjectDb:\t";

        private const String TABLE_NAME = "Objects";
        private const String KEY_ID = "id";
        private const String KEY_TYPE = "type";
        private const String KEY_NAME = "name";
        private const String KEY_DESC = "description";
        private const String KEY_TYPE_ID = "type_id";
        private String[] COLUMNS = new String[] { KEY_ID, KEY_TYPE, KEY_NAME, KEY_DESC, KEY_TYPE_ID };

        public ObjectDB() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " TEXT PRIMARY KEY, " +
                KEY_TYPE + " TEXT, " +
                KEY_NAME + " TEXT, " +
                KEY_DESC + " TEXT, " + 
                KEY_TYPE_ID + " INTEGER )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(ObjectEntity obj)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_ID + ", "
                + KEY_TYPE + ", "
                + KEY_NAME + ", "
                + KEY_DESC + " , "
                + KEY_TYPE_ID + " ) "

                + "VALUES ( '"
                + obj._id + "', '"
                + obj._type + "', '"
                + obj._name + "', '"
                + obj._description + "', '"
                + obj._typeId + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public void addOrReplaceData(ObjectEntity obj)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT OR REPLACE INTO " + TABLE_NAME
                + " ( "
                + KEY_ID + ", "
                + KEY_TYPE + ", "
                + KEY_NAME + ", "
                + KEY_DESC + " , "
                + KEY_TYPE_ID + " ) "

                + "VALUES ( '"
                + obj._id + "', '"
                + obj._type + "', '"
                + obj._name + "', '"
                + obj._description + "', '"
                + obj._typeId + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getDataById(int id)
        {
            return base.getDataById(id);
        }

        public override IDataReader getDataByString(string str)
        {
            Debug.Log(Tag + "Getting Object: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public IDataReader getTrashData(string str)
        {
            Debug.Log(Tag + "Getting Object: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_TYPE + " = '" + str + "'";
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
