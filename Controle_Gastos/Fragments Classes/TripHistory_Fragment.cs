using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Controle_Gastos.Model;

namespace Controle_Gastos.Fragments_Classes
{
    public class TripHistory_Fragment : Android.Support.V4.App.Fragment
    {
        View view;
        TripListAdapter adapter;
        ListView listView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            List<Trip> trip_list = Trip.get_all(this.Activity);
            adapter = new TripListAdapter(this.Activity,trip_list);
            if (trip_list == null)
                view = inflater.Inflate(Resource.Layout.tripHistory_fragment, container, false);
            else
            {
                view = inflater.Inflate(Resource.Layout.trip_list, container, false);
                listView = view.FindViewById<ListView>(Resource.Id.trip_list);
                updateHistoryFragment();
            }
            return view;
        }

        public void updateHistoryFragment()
        {   if (listView == null)
            {
                listView = view.FindViewById<ListView>(Resource.Id.trip_list);
                this.Activity.RegisterForContextMenu(listView);
            }
            List<Trip> trip_list = Trip.get_all(this.Activity);
            trip_list = trip_list.OrderBy(a => a.registration_date).Reverse().ToList();
            adapter.trip_list = trip_list;
            adapter.NotifyDataSetChanged();
            if (trip_list != null)
                listView.Adapter = adapter;
        }
    }


    public class TripListAdapter : BaseAdapter
    {
        readonly Activity Context;
        public List<Trip> trip_list { get; set; }

        public TripListAdapter(Activity context,List<Trip> list)
        {
            Context = context;
            trip_list = list;
        }

        public override int Count
        {
            get
            {
                //List<Trip> t = Trip.get_all();
                //return t.Count;
                if (trip_list == null)
                    return 0;
                else
                    return trip_list.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Trip t = trip_list[position];
            View view = convertView;
            if (view == null)
            {
                view = Context.LayoutInflater.Inflate(Resource.Layout.trip,null);
            }
            view.FindViewById<TextView>(Resource.Id.txt_reward).Text = t.reward.ToString();
            view.FindViewById<TextView>(Resource.Id.txt_home).Text = t.home;
            view.FindViewById<TextView>(Resource.Id.txt_destiny).Text = t.destiny;

            return view;
        }
    }
}