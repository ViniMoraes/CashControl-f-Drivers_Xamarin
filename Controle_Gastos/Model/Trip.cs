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
using Controle_Gastos.Database;
using Android.Database;

namespace Controle_Gastos.Model
{
   public class Trip
    {
        private readonly string TABLE_NAME = "trip";
        public long id { get; set; }
        public float reward { get; set; }
        public string home { get; set; }
        public string destiny { get; set; }
        public float toll_value { get; set; }
        public float fuell_value { get; set; }
        public string freight { get; set; }
        public DateTime? registration_date { get; set; }
        public DateTime? lastedit_date { get; set; }
        public DateTime? complete_date { get; set; }

        public long save(Context context)
        {
            DBAdapter db = new DBAdapter(context);
            ContentValues values = new ContentValues();

            values.Put("reward", this.reward);
            values.Put("home", this.home);
            values.Put("destiny", this.destiny);
            values.Put("toll_value", this.toll_value);
            values.Put("fuell_value", this.fuell_value);
            values.Put("freight", this.freight);
            values.Put("registration_date", DateTime.Now.ToString());
            values.Put("lastedit_date", DateTime.Now.ToString());
            values.Put("complete_date", "");

            return db.insert(TABLE_NAME, values);
        }

        public int update(Context context)
        {
            DBAdapter db = new DBAdapter(context);
            ContentValues values = new ContentValues();

            values.Put("reward", this.reward);
            values.Put("home", this.home);
            values.Put("destiny", this.destiny);
            values.Put("toll_value", this.toll_value);
            values.Put("fuell_value", this.fuell_value);
            values.Put("freight", this.freight);
            values.Put("lastedit_date", DateTime.Now.ToString());

            return db.update(TABLE_NAME, values, "_id = ?", new string[] { this.id.ToString() });
        }

        public int complete(Context context)
        {
            DBAdapter db = new DBAdapter(context);
            ContentValues values = new ContentValues();

            values.Put("complete_date", DateTime.Now.ToString());

            return db.update(TABLE_NAME, values, "_id = ?", new string[] { this.id.ToString() });
        }

        public bool delete(Context context)
        {
            DBAdapter db = new DBAdapter(context);
            if (db.delete(TABLE_NAME, "_id = ?", new string[] { this.id.ToString() }) > 0)
                return true;
            else
                return false;
        }

        public static List<Trip> get_all(Context context)
        {
            DBAdapter db = new DBAdapter(context);
            List<Trip> list = new List<Trip>();
            //TODO
            ICursor cursor = db.search("trip", new string[] { "*" }, null, null, null, null, null);

            if (cursor == null)
                return null;

            cursor.MoveToFirst();

            do
            {
                Trip t = new Trip();

                t.id = cursor.GetLong(0);
                t.reward = cursor.GetFloat(1);
                t.home = cursor.GetString(1);
                t.destiny = cursor.GetString(3);
                t.toll_value = cursor.GetFloat(4);
                t.fuell_value = cursor.GetFloat(5);
                t.freight = cursor.GetString(6);
                t.registration_date = DateTime.Parse(cursor.GetString(7));
                t.lastedit_date = DateTime.Parse(cursor.GetString(8));
                string tmp = cursor.GetString(9);
                if (tmp == "" || tmp == null)
                    t.complete_date = null;
                else
                    t.complete_date =  DateTime.Parse(tmp);

                list.Add(t);

            } while (cursor.MoveToNext());

            return list;
        }

        public static List<Trip> search(Context context, string where, string[] whereargs, string orderby)
        {
            DBAdapter db = new DBAdapter(context);
            List<Trip> list = new List<Trip>();

            ICursor cursor = db.search("trip", new string[] { "*" }, where, whereargs, null, null, orderby);

            if (cursor == null)
                return null;

            cursor.MoveToFirst();

            do
            {
                Trip t = new Trip();

                t.id = cursor.GetLong(0);
                t.reward = cursor.GetFloat(1);
                t.home = cursor.GetString(1);
                t.destiny = cursor.GetString(3);
                t.toll_value = cursor.GetFloat(4);
                t.fuell_value = cursor.GetFloat(5);
                t.freight = cursor.GetString(6);
                t.registration_date = DateTime.Parse(cursor.GetString(7));
                t.lastedit_date = DateTime.Parse(cursor.GetString(8));
                string tmp = cursor.GetString(9);
                if (tmp == "" || tmp == null)
                    t.complete_date = null;
                else
                    t.complete_date = DateTime.Parse(tmp);
                list.Add(t);

            } while (cursor.MoveToNext());

            return list;
        }

        public List<Item> get_itens (Context context)
        {
            DBAdapter db = new DBAdapter(context);
            List<Item> list = new List<Item>();
            ICursor cursor = db.search("item", new string[] { "*" }, null,null, null, null, "registration_date ASC");
            cursor = db.search("item", new string[] { "*" }, "_tripid = ?", new string[] { id.ToString() }, null, null, "registration_date ASC");

            if (cursor == null)
                return null;

            cursor.MoveToFirst();

            do
            {
                Item i = new Item();

                i.id = cursor.GetLong(0);
                i.trip_id = cursor.GetLong(1);
                i.category_id = cursor.GetLong(2);
                i.value = cursor.GetFloat(3);
                i.details = cursor.GetString(4);
                i.registration_date = DateTime.Parse(cursor.GetString(5));
                i.lastedit_date = DateTime.Parse(cursor.GetString(6));

                list.Add(i);

            } while (cursor.MoveToNext());

            return list;
        }

    }
}