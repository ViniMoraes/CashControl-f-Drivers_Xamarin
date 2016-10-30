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

    public class Resume_Fragment : Android.Support.V4.App.Fragment
    {
        public ExpandableDataAdapter adapter = null;
        private Dictionary<Trip, List<Item>> hashlist = null;
        public void FillHashList()
        {
            hashlist.Clear();
            List<Trip> trip_list = Trip.get_all(this.Activity);
            //Ordena por data de criação
            trip_list = trip_list.OrderBy(a => a.registration_date).Reverse().ToList();
            foreach (var trip in trip_list)
            {
                List<Item> lt = new List<Item>();
                if (trip.get_itens(this.Activity) != null)
                    foreach (var item in trip.get_itens(this.Activity))
                    {
                        if (lt.Find(x => x.category_id == item.category_id) == null)
                            lt.Add(item);
                        else
                            lt.Find(x => x.category_id == item.category_id).value += item.value;
                    }
                hashlist.Add(trip, lt);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            List<Trip> trip_list = Trip.get_all(this.Activity);
            View view;

            if (trip_list== null)
            {
                view = inflater.Inflate(Resource.Layout.no_trips, container, false);
                return view;
            }

            hashlist = new Dictionary<Trip, List<Item>>();
            FillHashList();

            view = inflater.Inflate(Resource.Layout.resume_fragment, container, false);

            //create listview
            var listView = view.FindViewById<ExpandableListView>(Resource.Id.expandableListView);
            adapter = new ExpandableDataAdapter(this.Activity, hashlist);
            listView.SetAdapter(adapter);


            //Calcula o valor total do Resumo
            float sum = 0;
            for (int i = 0; i < adapter.GroupCount; i++)
                sum += adapter.GetGroupSum(i);
            view.FindViewById<TextView>(Resource.Id.textview_total).Text = sum.ToString();

            return view;
        }
    }

    public class ExpandableDataAdapter : BaseExpandableListAdapter
    {

        readonly Activity Context;
        public ExpandableDataAdapter(Activity newContext, System.Object hashlist) : base()
        {
            Context = newContext;
            list = (Dictionary<Trip,List<Item>>) hashlist;
        }
        private Dictionary<Trip, List<Item>> list;

        protected List<DataTest> DataList { get; set; }
        protected List<DataTest_G> DataList_groups { get; set; }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            View header = convertView;
            if (header == null)
            {
                header = Context.LayoutInflater.Inflate(Resource.Layout.list_group, null);
            }
            header.FindViewById<TextView>(Resource.Id.DataHeader).Text = list.Keys.ElementAt(groupPosition).destiny;
            header.FindViewById<TextView>(Resource.Id.textView1).Text = (GetGroupSum(groupPosition) + list.Keys.ElementAt(groupPosition).reward).ToString();

            return header;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = Context.LayoutInflater.Inflate(Resource.Layout.list_item, null);
            }
            //string newId = "", newValue = "";
            //GetChildViewHelper(groupPosition, childPosition, out newId, out newValue);
            //List<DataTest> results = DataList.FindAll((DataTest obj) => obj.trip.Equals(groupPosition+1));
            var item = list[list.Keys.ElementAt(groupPosition)][childPosition];
            row.FindViewById<TextView>(Resource.Id.DataId).Text = Category.get_name(item.category_id,Context);
            row.FindViewById<TextView>(Resource.Id.DataValue).Text = item.value.ToString();

            return row;
            //throw new NotImplementedException ();
        }

        public float GetGroupSum (int groupPosition)
        {
            float sum = 0;
            if (list.Keys.ElementAt(groupPosition).complete_date != null)
                sum += list.Keys.ElementAt(groupPosition).reward;
            if (list[list.Keys.ElementAt(groupPosition)] == null)
                return sum;

            foreach (var item in list[list.Keys.ElementAt(groupPosition)])
                sum += item.value;

            return sum;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            if (list[list.Keys.ElementAt(groupPosition)] == null)
                return 0;

            return list[list.Keys.ElementAt(groupPosition)].Count;
            //char letter = (char)(65 + groupPosition);
            //List<DataTest> results_a = DataList.FindAll((DataTest obj) => obj.categoria[0].Equals(letter));
            //List<DataTest> results = DataList.FindAll((DataTest obj) => obj.trip.Equals(groupPosition+1));
            //return results.Count;
        }

        public override int GroupCount
        {
            get
            {
                return list.Count;
            }            
        }

        #region implemented abstract members of BaseExpandableListAdapter

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return list[list.Keys.ElementAt(groupPosition)][childPosition].id;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetGroupId(int groupPosition)
        {
            return list.Keys.ElementAt(groupPosition).id;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            //throw new NotImplementedException();
            //Toast.MakeText(Context, "group - {groupPosition} child - {chilPosition}", ToastLength.Short);
            return false;
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
            }
        }

        #endregion
    }

}
