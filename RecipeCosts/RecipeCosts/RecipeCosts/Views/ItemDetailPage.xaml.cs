using RecipeCosts.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace RecipeCosts.Views
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