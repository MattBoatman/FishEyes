using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Fish_Eyes.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace Fish_Eyes
{
    public partial class MainPage : PhoneApplicationPage
    {
        int fishcount = 0;
        Dictionary<string, int> FishListDic = new Dictionary<string,int>();
        Dictionary<string, int> AngularListDic = new Dictionary<string,int>();
        Dictionary<string, int> combinedLisDic = new Dictionary<string, int>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void FishAdd_Click(object sender, RoutedEventArgs e)
        {
            AddFishorAngular(FishListDic, FishList, Summary_Fish, FishName);
        }

  
        
        
        private void AddAngular_Click(object sender, RoutedEventArgs e)
        {
            AddFishorAngular(AngularListDic, AngularList, Summary_Angulars, AngularName);
        }

        private void FishIncrement_Click(object sender, RoutedEventArgs e)
        {
            incrementCount(FishListDic, FishList, Summary_Fish);
        }

        private void AngularIncrement_Click(object sender, RoutedEventArgs e)
        {
            incrementCount(AngularListDic, AngularList, Summary_Angulars);
        }

        private void FishDecrement_Click(object sender, RoutedEventArgs e)
        {
            decrementCount(FishListDic, FishList, Summary_Fish);
        }

        private void AngularDecrement_Click(object sender, RoutedEventArgs e)
        {
            decrementCount(AngularListDic, AngularList, Summary_Angulars);
        }

        private void FishName_GotFocus(object sender, RoutedEventArgs e)
        {
            FishName.SelectAll();
            FishName.Background = new SolidColorBrush(Colors.Gray);
            FishName.Foreground = new SolidColorBrush(Colors.White);
        }

        private void AngularName_GotFocus(object sender, RoutedEventArgs e)
        {
            AngularName.SelectAll();
            AngularName.Background = new SolidColorBrush(Colors.Gray);
            AngularName.Foreground = new SolidColorBrush(Colors.White);
        }

        private void AngularName_LostFocus(object sender, RoutedEventArgs e)
        {
            AngularName.Text = "Angular Name";
            AngularName.Background = new SolidColorBrush(Colors.Black);
            AngularName.Foreground = new SolidColorBrush(Colors.White);
        }

        private void FishName_LostFocus(object sender, RoutedEventArgs e)
        {
            FishName.Text = "Fish Name";
            FishName.Background = new SolidColorBrush(Colors.Black);
            FishName.Foreground = new SolidColorBrush(Colors.White);
        }

        private void FishClear_Click(object sender, RoutedEventArgs e)
        {
            FishListDic.Clear();
            FishList.Items.Clear();
            Summary_All.Text = "Leaderboard";
            Summary_Fish.Text = "Fish Leaderboard";
        }

        private void ClearAngular_Click(object sender, RoutedEventArgs e)
        {
            AngularListDic.Clear();
            AngularList.Items.Clear();
            Summary_All.Text = "Leaderboard";
            Summary_Angulars.Text = "Angular Leaderboard";
        }

        
        
        private void AddFishorAngular(Dictionary<string, int> dic, ListPicker list, TextBlock tbl, TextBox tb)
        {
            StringBuilder longlines = new StringBuilder();
            if (dic.ContainsKey(tb.Text))
            {
                MessageBox.Show("List already contains this item");
            }
            else
            {
                dic.Add(tb.Text, 0);
                Dictionary<string, int>.KeyCollection keyColl = dic.Keys;
                list.Items.Clear();
                foreach (string s in keyColl)
                {
                    list.Items.Add(s);
                }
                foreach (KeyValuePair<string, int> kvp in dic)
                {
                    longlines.AppendLine(kvp.Key.ToString() + " Count: " + kvp.Value.ToString());
                }
                tbl.Text = longlines.ToString();
            }
            totalNumbers();
        }


        private void incrementCount(Dictionary<string, int> dic, ListPicker list, TextBlock tbl)
        {
            
            if (list.SelectedItem == null)
            {
                MessageBox.Show("Please select a fish");
            }
            else
            {
                string tempname;
                StringBuilder longlines = new StringBuilder();
                tempname = list.SelectedItem.ToString();
                if (!dic.ContainsKey(tempname))
                {
                    dic.Add(tempname, 0);
                }
                dic[tempname]++;
                var ordered = dic.OrderByDescending(x => x.Value);
                foreach (KeyValuePair<string, int> kvp in ordered)
                {
                    longlines.AppendLine(kvp.Key.ToString() + " Count: " + kvp.Value.ToString());
                }
                tbl.Text = "Fish Leaderboard" + '\n' + longlines.ToString();
            }
            totalNumbers();
        }

        private void decrementCount(Dictionary<string, int> dic, ListPicker list, TextBlock tbl)
        {
            if (FishList.SelectedItem == null)
            {
                MessageBox.Show("Please select a fish");
            }
            else
            {
                string tempname;
                StringBuilder longlines = new StringBuilder();
                tempname = list.SelectedItem.ToString();
                if (!dic.ContainsKey(tempname))
                {
                    dic.Add(tempname, 0);
                }
                dic[tempname]--;
                var ordered = dic.OrderByDescending(x => x.Value);
                foreach (KeyValuePair<string, int> kvp in ordered)
                {
                    longlines.AppendLine(kvp.Key.ToString() + " Count: " + kvp.Value.ToString());
                }
                tbl.Text = "Angular Leaderboard" + '\n' + longlines.ToString();
            }
            totalNumbers();
        }

        private void totalNumbers()
        {
            StringBuilder longlines = new StringBuilder();
            foreach (var item in AngularListDic)
            {
                combinedLisDic[item.Key] = item.Value;
            }
            foreach (var item in FishListDic)
            {
                combinedLisDic[item.Key] = item.Value;
            }
           
            var ordered = combinedLisDic.OrderByDescending(x => x.Value);
            foreach (KeyValuePair<string, int> kvp in ordered)
            {
                longlines.AppendLine(kvp.Key.ToString() + " Count: " + kvp.Value.ToString());
            }
            Summary_All.Text = longlines.ToString();

        }

        

         

        
    }
}