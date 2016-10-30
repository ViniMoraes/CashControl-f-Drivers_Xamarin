using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Controle_Gastos.Model;
using Android.Util;
using Controle_Gastos.Fragments_Classes;

namespace Controle_Gastos
{
    [Activity(Label = "@string/New_Trip", Theme = "@style/MyTheme")]
    public class New_Trip_Activity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_trip);

            var toolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

           // DisplayMetrics dm = new DisplayMetrics();
          //  WindowManager.DefaultDisplay.GetMetrics(dm);

          //  Window.SetLayout(dm.WidthPixels * 80 / 100, dm.HeightPixels * 60 / 100);

            SetSupportActionBar(toolBar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            ViewPager viewpager = (ViewPager)FindViewById(Resource.Id.viewPager);

            var add_button = FindViewById<Button>(Resource.Id.button_add_trip);

            add_button.Click += delegate 
            {
                var txtReward = FindViewById<TextView>(Resource.Id.addtrip_txtReward).Text;
                var txtHome = FindViewById<TextView>(Resource.Id.addtrip_txtHome).Text;
                var txtDestiny = FindViewById<TextView>(Resource.Id.addtrip_txtDestiny).Text;
                var txtTollValue = FindViewById<TextView>(Resource.Id.addtrip_txtToll).Text;
                var txtFuelValue = FindViewById<TextView>(Resource.Id.addtrip_txtFuel).Text;
                var txtFreight = FindViewById<TextView>(Resource.Id.addtrip_txtFreight).Text;



                Trip t = new Trip();
                t.reward = txtReward == "" ? float.Parse("0.0") : float.Parse(txtReward);
                t.home = txtHome == "" ? " -- " : txtHome;
                t.destiny = txtDestiny == "" ? " -- " : txtDestiny;
                t.toll_value = txtTollValue == "" ? float.Parse("0.0") : float.Parse(txtTollValue);
                t.fuell_value = txtFuelValue == "" ? float.Parse("0.0") : float.Parse(txtFuelValue);
                t.freight = txtFreight == "" ? "" : txtFreight;
                t.save(this);

                Resume_Fragment resumeFragment = MyFragmentAdapter.getLastResumeFragment();
                if (resumeFragment != null)
                {
                    resumeFragment.FillHashList();
                    resumeFragment.adapter.NotifyDataSetChanged();
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