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
using Controle_Gastos.Model;
using Java.Lang;
using Controle_Gastos;
using Android.Graphics;
using Android.Support.V4.Content;

namespace Controle_Gastos.Fragments_Classes
{
    public class TripCurrent_Fragment : Android.Support.V4.App.Fragment
    {
        private LvAdapter adapter;
        Trip trip_current;
        List<Item> itens;
        View view;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.tripHistory_fragment, container, false);

            TextView message = view.FindViewById<TextView>(Resource.Id.txt_mensagem);

            //TODO - colocar string como flexível
            message.Text = "Você não selecionou nenhuma viagem!";

            var trip_list = Trip.get_all(Activity);
            if (trip_list == null)
                return view;

            view = inflater.Inflate(Resource.Layout.tripCurrent_fragment, container, false);

            trip_list.OrderBy(x => x.complete_date);
            trip_current = trip_list.First();

            adapter = new LvAdapter(this.Activity,itens);

            updateCurrentFragment();

          //  Spinner teste = (Spinner)view.FindViewById(Resource.Id.spinner1);

         //   ArrayAdapter<String> adapter = new ArrayAdapter<String>(view.Context, Resource.Layout.support_simple_spinner_dropdown_item, new String[] { "1", "2" });

         //   teste.Adapter = adapter;


            return view;
        }

        public void updateCurrentFragment()
        {
            TextView txt_destiny = view.FindViewById<TextView>(Resource.Id.text_destiny);
            TextView txt_reward = view.FindViewById<TextView>(Resource.Id.text_reward);
            TextView txt_spent = view.FindViewById<TextView>(Resource.Id.text_spent);
            TextView txt_total = view.FindViewById<TextView>(Resource.Id.text_total);
            ListView lv_itens = view.FindViewById<ListView>(Resource.Id.lv_itens);

            txt_destiny.Text = trip_current.destiny;
            txt_reward.Text = trip_current.reward.ToString();

            itens = trip_current.get_itens(this.Activity);

            string total_spent = "0";
            if (itens != null)
                total_spent = itens.Sum(a => a.value).ToString();

            txt_spent.Text = total_spent;

            txt_total.Text = (trip_current.reward + Convert.ToDouble(total_spent)).ToString();
            if (Convert.ToDouble(txt_total.Text) >= 0)
                txt_total.SetTextColor(new Color(ContextCompat.GetColor(Activity, Android.Resource.Color.HoloGreenLight)));
            else
                txt_total.SetTextColor(new Color(ContextCompat.GetColor(Activity, Android.Resource.Color.HoloRedLight)));

            itens = trip_current.get_itens(this.Activity);
            adapter.setListItem(itens);                    
            adapter.NotifyDataSetChanged();
            if (itens != null)
                lv_itens.Adapter = adapter;
        }

    }
}

public class LvAdapter : BaseAdapter
{
    private readonly Activity Context;
    private List<Item> list_item;
    public LvAdapter(Activity context, List<Item> list) : base()
    {
        Context = context;
        list_item = list;
    }

    public void setListItem(List<Item> list)
    {
        list_item = list;
    }

    public override int Count
    {
        get
        {
            return list_item.Count;
        }
    }

    public override Java.Lang.Object GetItem(int position)
    {
        throw new NotImplementedException();
    }

    public override long GetItemId(int position)
    {
        return list_item[position].id;
    }

    public override View GetView(int position, View convertView, ViewGroup parent)
    {
        View row = convertView;
        if (row == null)
        {
            row = Context.LayoutInflater.Inflate(Resource.Layout.list_item_current, null);
        }
        var item = list_item[position];
        row.FindViewById<TextView>(Resource.Id.lic_tv_category).Text = Category.get_name(item.category_id, Context);
        row.FindViewById<TextView>(Resource.Id.lic_tv_value).Text = item.value.ToString();
        if (item.value >= 0)
        {
            row.FindViewById<TextView>(Resource.Id.lic_tv_value).SetTextColor(new Color(ContextCompat.GetColor(Context, Android.Resource.Color.HoloGreenLight)));
        }
        else
        {
            row.FindViewById<TextView>(Resource.Id.lic_tv_value).SetTextColor(new Color(ContextCompat.GetColor(Context, Android.Resource.Color.HoloRedLight)));
        }
        return row;
    }
}
