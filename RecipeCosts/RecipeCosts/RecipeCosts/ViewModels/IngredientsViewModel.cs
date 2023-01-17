﻿using Newtonsoft.Json;
using Plugin.CloudFirestore;
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
    public class IngredientsViewModel : BaseViewModel
    {
        private Ingredient selectedIngredient;

        public Ingredient SelectedIngredient
        {
            get { return selectedIngredient; }
            set { selectedIngredient = value; }
        }

        private ObservableCollection<Ingredient> ingredients;

        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { 
                ingredients = value;
                OnPropertyChanged();
            }
        }


        public Command AddIngredientCommand { get; set; }
        public Command<Ingredient> IngredientTapped { get; set; }

        public IngredientsViewModel()
        {
            AddIngredientCommand = new Command(OnAddIngredient);
            IngredientTapped = new Command<Ingredient>(OnEditItem);

            Ingredients = new ObservableCollection<Ingredient>();
        }

        public async void OnApprearing()
        {
            var appUserId = Preferences.Get(PreferenceKeys.PREF_CURRENT_APP_USER_ID, "");

            if (String.IsNullOrEmpty(appUserId))
            {
                await App.Current.MainPage.DisplayAlert("Error", "App user not found.", "OK");
            } else
            {
                var query = await CrossCloudFirestore
                    .Current
                    .Instance
                    .Collection(FirebaseCollectionKeys.COL_INGREDIENTS)
                    .WhereEqualsTo("UserId", appUserId)
                    .GetAsync();

                var fetchedIngredients = query.ToObjects<Ingredient>();

                UpdateIngredients(fetchedIngredients);
            }
        }

        public async void OnAddIngredient()
        {
            //await Shell.Current.GoToAsync($"{nameof(IngredientDetailPage)}?{nameof(IngredientDetailViewModel.PropertyDictId)}={null}");
            await Shell.Current.GoToAsync($"{nameof(IngredientDetailPage)}");
        }

        public async void OnEditItem(Ingredient ingredient)
        {
            if (ingredient == null)
            {
                return;
            }

            string propertyId = "passedIngredeint";
            if (Application.Current.Properties.ContainsKey(propertyId))
            {
                Application.Current.Properties.Remove(propertyId);
            }
            Application.Current.Properties.Add(propertyId, ingredient);

            await Shell.Current.GoToAsync($"{nameof(IngredientDetailPage)}?{nameof(IngredientDetailViewModel.PropertyDictId)}={propertyId}");
        }

        private void UpdateIngredients(IEnumerable<Ingredient> newIngredients)
        {
            Ingredients.Clear();

            foreach (var ingredient in newIngredients)
            {
                Ingredients.Add(ingredient);
            }
        }
    }
}
