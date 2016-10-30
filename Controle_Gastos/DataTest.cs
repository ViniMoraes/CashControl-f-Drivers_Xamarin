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

namespace Controle_Gastos
{
   public class DataTest
    {
            public DataTest()
            {
            }

        public static List<DataTest> Data_Itens()
        {
            var newDataList = new List<DataTest>();
            newDataList.Add(new DataTest("Comida", 1, -28));
            newDataList.Add(new DataTest("Comida", 1, -10));
            newDataList.Add(new DataTest("Comida", 1, -5));
            newDataList.Add(new DataTest("Combustivel", 1, -500));
            newDataList.Add(new DataTest("Comida", 2, 80));
            newDataList.Add(new DataTest("Outros", 2, 34));
            newDataList.Add(new DataTest("Frete", 2, 2000));
            newDataList.Add(new DataTest("Combustivel", 3, 1300));
            newDataList.Add(new DataTest("Frete", 3, 3000));
            return newDataList;
        }

        public DataTest(string newcategoria= "category test", int newtrip = 0, int newvalue = 0)
            {
                categoria = newcategoria;
                trip = newtrip;
                value = newvalue;
            }

            public string categoria { get; set; }
            public int trip { get; set; }
            public int value { get; set; }
        }

    }

public class DataTest_G
{
    public DataTest_G()
    {
    }
    public static List<DataTest_G> Data_Groups()
    {
        var newDataList = new List<DataTest_G>();
        newDataList.Add(new DataTest_G("Goiania", 1));
        newDataList.Add(new DataTest_G("Campinas", 2));
        newDataList.Add(new DataTest_G("Pernambuco", 3));
        return newDataList;
    }

    public DataTest_G(string tripname = "category test", int trip_id = 0)
    {
        id = trip_id;
        trip = tripname;
    }

    public string trip { get; set; }
    public int id { get; set; }
    
}