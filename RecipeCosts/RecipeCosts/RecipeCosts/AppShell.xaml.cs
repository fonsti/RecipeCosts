using RecipeCosts.ViewModels;
using RecipeCosts.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RecipeCosts
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(IngredientDetailPage), typeof(IngredientDetailPage));
            Routing.RegisterRoute(nameof(RecipeDetailsPage), typeof(RecipeDetailsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
