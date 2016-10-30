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
using Controle_Gastos.Model;

namespace Controle_Gastos.Database
{
    class Database : SQLiteOpenHelper
    {
        private static readonly string DB_NAME = "Ap_db";
        private static readonly int DB_VERSION = 1;

        public Database(Context context) : base(context, DB_NAME, null, DB_VERSION) {}
        
        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL("CREATE TABLE trip(_id integer primary key autoincrement," +
                " reward real," +
                " home text," +
                " destiny text," +
                " toll_value real, " +
                " fuell_value," +
                " freight text," +
                " registration_date string," +
                " lastedit_date string," +
                " complete_date string)");

            db.ExecSQL("CREATE TABLE category(_id integer primary key autoincrement," +
                " name text, " +
                " registration_date string," +
                " lastedit_date string)");

            db.ExecSQL("CREATE TABLE item(_id integer primary key autoincrement," +
                " _tripid integer, " +
                " _categoryid integer, " +
                " value real not null," +
                " details text, " +
                " registration_date string," +
                " lastedit_date string," +
                " FOREIGN KEY(_tripid) REFERENCES trip(_id), " +
                " FOREIGN KEY(_categoryid) REFERENCES category(_id))");

                Category.category_populate(db);

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("drop table trip");
            db.ExecSQL("drop table category");
            db.ExecSQL("drop table item");
            OnCreate(db);
        }
    }
}