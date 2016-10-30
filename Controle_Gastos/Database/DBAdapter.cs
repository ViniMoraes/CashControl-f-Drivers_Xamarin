using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;
using Android.Database;

namespace Controle_Gastos.Database
{
    class DBAdapter
    {
        private SQLiteDatabase db;

        public DBAdapter(Context context)
        {
            Database db_aux = new Database(context);
            db = db_aux.WritableDatabase;
        }

        public long insert(string table, ContentValues values)
        {
           return db.Insert(table, null, values);
        }

        public int update(string table, ContentValues values,string whereClause,string[] whereArgs)
        {
            return db.Update(table, values, whereClause, whereArgs);
        }

        public int delete(string table, string whereClause, string[] whereArgs)
        {
           return db.Delete(table, whereClause, whereArgs);
        }

        public ICursor search (string table,string[] columns, string whereClause, string[] whereArgs,string groupBy,string having,string orderBy)
        {
            ICursor cursor = db.Query(table,columns,whereClause,whereArgs,groupBy,having,orderBy);

            if (cursor.Count > 0)
                return cursor;
            else
                return null;
        }


    }
}