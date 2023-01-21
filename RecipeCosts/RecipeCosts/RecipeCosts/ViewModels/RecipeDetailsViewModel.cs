using RecipeCosts.Models;
using RecipeCosts.Views.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace RecipeCosts.ViewModels
{
    [QueryProperty(nameof(PropertyDictId), nameof(PropertyDictId))]
    public class RecipeDetailsViewModel : BaseViewModel
    {
        private string propertyDictId;

        public string PropertyDictId
        {
            get { return propertyDictId; }
            set
            {
                propertyDictId = value;
                if (!String.IsNullOrEmpty(value))
                {
                    LoadFromPropertyDict(value);
                }
            }
        }

        private string recipeId;

        public string RecipeId
        {
            get { return recipeId; }
            set { recipeId = value; }
        }

        private string recipeName;

        public string RecipeName
        {
            get { return recipeName; }
            set { recipeName = value; }
        }

        private double recipeOverallCosts;

        public double RecipeOverallCosts
        {
            get { return recipeOverallCosts; }
            set { recipeOverallCosts = value; }
        }

        private ObservableCollection<Ingredient> ingredients;

        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        public bool IsNewItem { get; set; }

        public Command AddIngredientCommand { get; set; }

        public RecipeDetailsViewModel()
        {
            IsNewItem = true;

            Ingredients = new ObservableCollection<Ingredient>();

            AddIngredientCommand = new Command(OnAddIngredient);
        }

        public async void OnAddIngredient()
        {
            await App.Current.MainPage.Navigation.ShowPopupAsync(new SelectIngredientPopup());
        }

        private void LoadFromPropertyDict(string propertyId)
        {
            if (Application.Current.Properties.TryGetValue(propertyId, out object retrievedRecipe))
            {
                var recipe = retrievedRecipe as Recipe;

                this.RecipeId = recipe.Id;
                this.RecipeName = recipe.Name;
                this.RecipeOverallCosts = recipe.OverallCosts;

                IsNewItem = false;
            }
        }
    }
}
