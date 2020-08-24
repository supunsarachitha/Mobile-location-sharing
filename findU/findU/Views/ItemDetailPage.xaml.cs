using System.ComponentModel;
using Xamarin.Forms;
using findU.ViewModels;

namespace findU.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}