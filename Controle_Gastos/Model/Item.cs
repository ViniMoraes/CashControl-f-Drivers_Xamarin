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
    public class Item
    {

        private readonly string TABLE_NAME = "item";

        public long id { get; set; }
        public long trip_id { get; set; }
        public long category_id { get; set; }
        public float value { get; set; }
        public string details { get; set; }
        
        public DateTime? registration_date { get; set; }
        public DateTime? lastedit_date { get; set; }
        public long save(Context context)
        {
            DBAdapter db = new DBAdapter(context);
            ContentValues values = new ContentValues();

            values.Put("_tripid", this.trip_id);
            values.Put("_categoryid", this.category_id);
            values.Put("value", this.value);
            values.Put("details", this.details);
            values.Put("registration_date", DateTime.Now.ToString());
            values.Put("lastedit_date", DateTime.Now.ToString());        

            return db.insert(TABLE_NAME, values);
        }

        public int update(Context context)
        {
            DBAdapter db = new DBAdapter(context);
            ContentValues values = new ContentValues();

            values.Put("_trip_id", this.trip_id);
            values.Put("_category_id", this.category_id);
            values.Put("value", this.value);
            values.Put("details", this.details);
            values.Put("lastedit_date", DateTime.Now.ToString());

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

    }
}