using Android.App;
using Android.Views;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Controle_Gastos.Fragments_Classes;
using static Android.Support.Design.Widget.TabLayout;
using Android.Support.Design.Widget;
using Android.Widget;
using Clans.Fab;
using Android.Content;
using System;

namespace Controle_Gastos
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);           

            var toolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //toolBar.Elevation = 20;
            //toolBar.Menu.Add("teste");
            //toolBar.NavigationIcon;
            //= (Android.Graphics.Drawables.Drawable)Resource.Drawable.abc_ic_ab_back_mtrl_am_alpha;

            SetSupportActionBar(toolBar);


            //toolBar.NavigationIcon = Resource.Drawable.abc_ic_go_search_api_mtrl_alpha;

            TabLayout tab = (TabLayout)FindViewById(Resource.Id.TabLayout);

            tab.SetSelectedTabIndicatorColor(Resource.Color.branco_azulado);
            tab.SetSelectedTabIndicatorHeight(5);   
            tab.AddTab(tab.NewTab().SetText(Resource.String.Resume));
            tab.AddTab(tab.NewTab().SetText(Resource.String.Current_Trip));
            tab.AddTab(tab.NewTab().SetText(Resource.String.History));

            ViewPager viewpager = (ViewPager)FindViewById(Resource.Id.viewPager);
            viewpager.Adapter = new MyFragmentAdapter(SupportFragmentManager) ;


            //Sincroniza a viewpager com as tabs
            viewpager.AddOnPageChangeListener(new TabLayoutOnPageChangeListener(tab));
            tab.SetOnTabSelectedListener(new ViewPagerOnTabSelectedListener(viewpager));

            //Inicia na segunda tab
            viewpager.SetCurrentItem(1, false);

            //Floating Action Menu 

            Clans.Fab.FloatingActionButton fab_newtrip = FindViewById<Clans.Fab.FloatingActionButton>(Resource.Id.fab_newtrip);
            Clans.Fab.FloatingActionButton fab_newitem = FindViewById<Clans.Fab.FloatingActionButton>(Resource.Id.fab_newitem);
            Clans.Fab.FloatingActionMenu fa_menu = FindViewById<Clans.Fab.FloatingActionMenu>(Resource.Id.fa_menu);

            fab_newtrip.Click += delegate 
            {
                fa_menu.Close(false);
                StartActivity(typeof(New_Trip_Activity));
            };
   
            fab_newitem.Click += delegate
            { 
                fa_menu.Close(false);
                StartActivity(typeof(New_Item_Activity));
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            MenuInflater.Inflate(Resource.Menu.main_menu, menu);

            return true;
        }
    }

    public class MyFragmentAdapter : FragmentPagerAdapter
    {
        static Resume_Fragment lastResumeFragment =  null;
        static TripCurrent_Fragment lastTripCurrentFragment = null;
        static TripHistory_Fragment lastTripHistoryFragment = null;
        

        public MyFragmentAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        { }

        public override int Count
        {
            get { return 3; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            
            switch (position)
            {
                case 0:
                    Resume_Fragment rF = new Resume_Fragment();
                    lastResumeFragment = rF;
                    return rF;
                case 1:
                    TripCurrent_Fragment tcF = new TripCurrent_Fragment();
                    lastTripCurrentFragment = tcF;
                    return tcF;
                case 2:
                    TripHistory_Fragment thF = new TripHistory_Fragment();
                    lastTripHistoryFragment = thF;
                    return thF;
                default:
                    return null;
            }
        }
        public static Resume_Fragment getLastResumeFragment()
        {
            return lastResumeFragment;
        }
        public static TripCurrent_Fragment getlastTripCurrentFragment()
        {
            return lastTripCurrentFragment;
        }
    }
}

