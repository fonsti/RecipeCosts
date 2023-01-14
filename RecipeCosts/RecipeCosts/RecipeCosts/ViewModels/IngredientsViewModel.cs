using RecipeCosts.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RecipeCosts.ViewModels
{
    public class IngredientsViewModel : BaseViewModel
    {
        public Command AddIngredientCommand { get; set; }

        public IngredientsViewModel()
        {
            AddIngredientCommand = new Command(OnAddIngredient);
        }

        public async void OnAddIngredient()
        {
            await Shell.Current.GoToAsync(nameof(IngredientDetailPage));
        }
    }
}
