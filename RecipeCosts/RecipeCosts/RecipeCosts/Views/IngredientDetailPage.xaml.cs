using RecipeCosts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeCosts.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientDetailPage : ContentPage
    {
        public IngredientDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as IngredientDetailViewModel;
            vm.OnAppearing();
        }
    }
}