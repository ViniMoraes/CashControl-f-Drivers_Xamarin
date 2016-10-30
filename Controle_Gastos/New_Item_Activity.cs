using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Controle_Gastos.Model;
using Controle_Gastos.Fragments_Classes;

namespace Controle_Gastos
{
    [Activity(Label = "@string/New_Item", Theme = "@style/MyTheme")]
    class New_Item_Activity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.nItem_fragment);

           // DisplayMetrics dm = new DisplayMetrics();
           // WindowManager.DefaultDisplay.GetMetrics(dm);

           // Window.SetLayout(dm.WidthPixels * 80 / 100, dm.HeightPixels * 60 / 100);
            //Window.SetGravity(GravityFlags.Top);

            //ColorDrawable dw = new ColorDrawable(Android.Graphics.Color.Black);
            //Window.SetBackgroundDrawable(dw);
            // this.setBackgroundDrawable(dw);

            var toolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolBar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            ViewPager viewpager = (ViewPager)FindViewById(Resource.Id.viewPager);
            
            //Trip Spiner
            Spinner spinner_trip = FindViewById<Spinner>(Resource.Id.additem_trip_spinner);

            var trip_list = Trip.search(this, "complete_date = ?", new string[] { "" }, "registration_date ASC");
            List<string> trip_list_array = new List<string>();

            if (trip_list == null)
                trip_list_array.Add("");
            else
                foreach (Trip t in trip_list)             
                    trip_list_array.Add(t.destiny);
                

            var adapter_trip = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerDropDownItem,trip_list_array);

            spinner_trip.Adapter = adapter_trip;
            

            //Category Spiner
            Spinner spinner_category = FindViewById<Spinner>(Resource.Id.additem_category_spinner);
            var category_list = Category.get_all(this);
            List<string> category_names = new List<string>();
            foreach (Category c in category_list)
                category_names.Add(c.name);
            
            var adapter_category = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, category_names.ToArray());
            spinner_category.Adapter = adapter_category;


            Button add_item = FindViewById<Button>(Resource.Id.additem_buttonAdd);
            add_item.Click += delegate 
            {

                var txtValue = FindViewById<TextView>(Resource.Id.additem_txtValue).Text;
                var txtDetails = FindViewById<TextView>(Resource.Id.additem_txtDetails).Text;
                var txtCategory = spinner_category.SelectedItem.ToString();
                var txtTrip = spinner_trip.SelectedItem.ToString();
                
                Item i = new Item();
                i.value = txtValue == "" ? float.Parse("0.0") : float.Parse(txtValue);
                i.value *= -1;
                i.details = txtDetails == "" ? " -- " : txtDetails;
                i.trip_id = trip_list.Find(x => x.destiny == spinner_trip.SelectedItem.ToString()).id;
                i.category_id = category_list.Find(x => x.name == spinner_category.SelectedItem.ToString()).id;
                
                i.save(this);

                Resume_Fragment resumeFragment = MyFragmentAdapter.getLastResumeFragment();
                if (resumeFragment != null)
                {
                    resumeFragment.FillHashList();
                    resumeFragment.adapter.NotifyDataSetChanged();
                }

                TripCurrent_Fragment tripcFragment = MyFragmentAdapter.getlastTripCurrentFragment();
                if (tripcFragment != null)
                {
                    tripcFragment.updateCurrentFragment();
                }

                this.Finish();
            };
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}