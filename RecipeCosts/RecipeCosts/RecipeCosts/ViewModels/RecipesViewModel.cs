using Plugin.CloudFirestore;
using RecipeCosts.Models;
using RecipeCosts.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                string appUserId = Preferences.Get(PreferenceKeys.PREF_CURRENT_APP_USER_ID, null);

                if (appUserId != null)
                {
                    return;
                }

                var cacheQuery = await CrossCloudFirestore
                    .Current
                    .Instance
                    .Collection(FirebaseCollectionKeys.COL_RECIPES)
                    .WhereEqualsTo(FirebaseCollectionKeys.COL_RECIPES_USERID, appUserId)
                    .GetAsync(Source.Cache);

                var cachededRecipes = cacheQuery.ToObjects<Recipe>();

                UpdateRecipes(cachededRecipes, true);

                if (cachededRecipes.ToList().Count() == 0)
                {
                    var serverQuery = await CrossCloudFirestore
                        .Current
                        .Instance
                        .Collection(FirebaseCollectionKeys.COL_RECIPES)
                        .WhereEqualsTo(FirebaseCollectionKeys.COL_RECIPES_USERID, appUserId)
                        .GetAsync(Source.Server);

                    var serverRecipes = serverQuery.ToObjects<Recipe>();

                    UpdateRecipes(serverRecipes);
                }
                else
                {
                    var latestTimeStamp = Recipes.OrderByDescending(x => x.UpdatedAt).FirstOrDefault().UpdatedAt;

                    var timestampQuery = await CrossCloudFirestore
                        .Current
                        .Instance
                        .Collection(FirebaseCollectionKeys.COL_RECIPES)
                        .WhereGreaterThan(FirebaseCollectionKeys.COL_RECIPES_UPDATEDAT, latestTimeStamp)
                        .GetAsync(Source.Server);

                    var timestampRecipes = timestampQuery.ToObjects<Recipe>();

                    UpdateRecipes(timestampRecipes);
                }
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

        private void UpdateRecipes(IEnumerable<Recipe> newRecipes, bool resetList = false)
        {
            if (resetList)
            {
                Recipes.Clear();
            }

            foreach (var recipe in newRecipes)
            {
                Recipes.Add(recipe);
            }
        }
    }
}
