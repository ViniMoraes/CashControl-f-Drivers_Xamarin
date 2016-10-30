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
using Controle_Gastos.Database;
using Android.Database;

namespace Controle_Gastos.Model
{
    class Category
    {
        public long id { get; set; }
        public string name { get; set; }
        public int registration_date { get; set; }
        public int lastedit_date { get; set; }

        public static string get_name(long id, Context context)
        {
            DBAdapter db = new DBAdapter(context);
            List<Trip> list = new List<Trip>();

            ICursor cursor = db.search("category", new string[] { "name"}, "_id = ?", new string[] { id.ToString() }, null, null, null);

            if (cursor == null)
                return null;

            cursor.MoveToFirst();

            return cursor.GetString(0);
        }

        public static List<Category> get_all (Context context)
        {
            DBAdapter db = new DBAdapter(context);
            List<Category> list = new List<Category>();

            ICursor cursor = db.search("category", new string[] { "_id", "name", "registration_date", "lastedit_date" }, null, null, null, null, null);
            

            if (cursor == null)
                return null;

            cursor.MoveToFirst();

            do
            {
                Category c = new Category();
                c.id = cursor.GetLong(0);
                c.name = cursor.GetString(1);
                c.registration_date = cursor.GetInt(2);
                c.lastedit_date = cursor.GetInt(3);
                list.Add(c);
            } while (cursor.MoveToNext());
            
            return list;
        }

        public static void category_populate (SQLiteDatabase db)
        {
            string[] categorys = { "Comida", "Combustível", "Pedágio", "Manutenção", "Outros"};
            
            ContentValues category_values = new ContentValues();

            foreach (string s in categorys)
            {
                category_values.Put("registration_date", DateTime.Now.ToString());
                category_values.Put("lastedit_date", DateTime.Now.ToString());
                category_values.Put("name", s);
                db.Insert("category", null, category_values);
                category_values.Clear();
            }

            //TODO

            ContentValues trip_values = new ContentValues();


            trip_values.Put("reward", 1250);
            trip_values.Put("home", "Leme");
            trip_values.Put("destiny", "Araras");
            trip_values.Put("toll_value", 100);
            trip_values.Put("fuell_value", 200);
            trip_values.Put("freight", "Arroz");
            trip_values.Put("registration_date", DateTime.Now.ToString());
            trip_values.Put("lastedit_date", DateTime.Now.ToString());
            trip_values.Put("complete_date", "");
            db.Insert("trip", null, trip_values);
            trip_values.Clear();

            trip_values.Put("reward", 3000);
            trip_values.Put("home", "Campinas");
            trip_values.Put("destiny", "São Paulo");
            trip_values.Put("toll_value", 100);
            trip_values.Put("fuell_value", 200);
            trip_values.Put("freight", "Arroz");
            trip_values.Put("registration_date", DateTime.Now.ToString());
            trip_values.Put("lastedit_date", DateTime.Now.ToString());
            trip_values.Put("complete_date", "");
            db.Insert("trip", null, trip_values);
            trip_values.Clear();

            trip_values.Put("reward", 4000);
            trip_values.Put("home", "Pernambuco");
            trip_values.Put("destiny", "Amapá");
            trip_values.Put("toll_value", 100);
            trip_values.Put("fuell_value", 200);
            trip_values.Put("freight", "Arroz");
            trip_values.Put("registration_date", DateTime.Now.ToString());
            trip_values.Put("lastedit_date", DateTime.Now.ToString());
            trip_values.Put("complete_date", "");
            db.Insert("trip", null, trip_values);
            trip_values.Clear();

            /*
             *            
            values.Put("reward", this.reward);
            values.Put("home", this.home);
            values.Put("destiny", this.destiny);
            values.Put("toll_value", this.toll_value);
            values.Put("fuell_value", this.fuell_value);
            values.Put("freight", this.freight);
            values.Put("registration_date", DateTime.Now.ToString());
            values.Put("lastedit_date", DateTime.Now.ToString());
            values.Put("complete_date", 0);
             */
        }
    }
}
 