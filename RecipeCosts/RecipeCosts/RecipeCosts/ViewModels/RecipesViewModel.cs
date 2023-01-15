using RecipeCosts.Models;
using RecipeCosts.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RecipeCosts.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
		private Recipe selectedRecipe;

		public Recipe SelectedRecipe
		{
			get { return selectedRecipe; }
			set { selectedRecipe = value; }
		}

		private ObservableCollection<Recipe> recipes;

		public ObservableCollection<Recipe> Recipes
		{
			get { return recipes; }
			set { recipes = value; }
		}

		public Command AddRecipeCommand { get; set; }

		public RecipesViewModel()
		{
			AddRecipeCommand = new Command(OnAddRecipe);
		}

		public async void OnAppearing()
		{
			if (Preferences.ContainsKey(PreferenceKeys.PREF_CURRENT_APP_USER))
			{

			}
			else
			{
				await App.Current.MainPage.DisplayAlert("Info", "You are not logged in, yet. Please log in.", "OK");
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            }
		}

		public async void OnAddRecipe(object obj)
		{
            await Shell.Current.GoToAsync(nameof(RecipeDetailsPage));
        }
	}
}
