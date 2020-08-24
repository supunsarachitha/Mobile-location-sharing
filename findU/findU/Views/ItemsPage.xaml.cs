using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using findU.Models;
using findU.Views;

namespace findU.Views
{
    public partial class ItemsPage : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ItemsPage()
        {
            InitializeComponent();

            bindData();
        }

        private async void bindData()
        {
            var res = await firebaseHelper.GetAllPersons();
            ItemsListView.ItemsSource = res;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


        bool tapped = false;
        private async void ItemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tapped)
            {
                return;
            }
            else
            {
                tapped = true;
            }


            if (e.CurrentSelection != null)
            {
                Person person = (Person)e.CurrentSelection.FirstOrDefault();
                Common.selectedUser = person.PersonId;
                ItemsListView.SelectedItem = null;

                await Shell.Current.GoToAsync("locationPage");
                tapped = false;

                

                
            }

           
        }
    }
}