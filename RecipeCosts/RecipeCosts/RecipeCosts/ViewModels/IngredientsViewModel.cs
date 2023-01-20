using Newtonsoft.Json;
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


        public Command AddIngredientCommand { get; private set; }
        public Command<Ingredient> IngredientTappedCommand { get; private set; }
        public Command IngredientLongPressCommand { get; private set; }

        public IngredientsViewModel()
        {
            AddIngredientCommand = new Command(OnAddIngredient);
            IngredientTappedCommand = new Command<Ingredient>(OnEditItem);
            IngredientLongPressCommand = new Command<Ingredient>(OnDeleteItem);

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
                var cacheQuery = await CrossCloudFirestore
                    .Current
                    .Instance
                    .Collection(FirebaseCollectionKeys.COL_INGREDIENTS)
                    .WhereEqualsTo(FirebaseCollectionKeys.COL_INGREDIENT_USERID, appUserId)
                    .GetAsync(Source.Cache);

                var cachededIngredients = cacheQuery.ToObjects<Ingredient>();

                UpdateIngredients(cachededIngredients, true);

                if (cachededIngredients.ToList().Count() == 0)
                {
                    var serverQuery = await CrossCloudFirestore
                        .Current
                        .Instance
                        .Collection(FirebaseCollectionKeys.COL_INGREDIENTS)
                        .WhereEqualsTo(FirebaseCollectionKeys.COL_INGREDIENT_USERID, appUserId)
                        .GetAsync(Source.Server);

                    var serverIngredients = serverQuery.ToObjects<Ingredient>();

                    UpdateIngredients(serverIngredients);
                }
                else
                {
                    var latestTimeStamp = Ingredients.OrderByDescending(x => x.UpdatedAt).FirstOrDefault().UpdatedAt;

                    var timestampQuery = await CrossCloudFirestore
                        .Current
                        .Instance
                        .Collection(FirebaseCollectionKeys.COL_INGREDIENTS)
                        .WhereGreaterThan(FirebaseCollectionKeys.COL_INGREDIENT_UPDATEDAT, latestTimeStamp)
                        .GetAsync(Source.Server);

                    var timestampIngredients = timestampQuery.ToObjects<Ingredient>();

                    UpdateIngredients(timestampIngredients);
                }
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

        public async void OnDeleteItem(Ingredient ingredient)
        {
            if (ingredient == null)
            {
                return;
            }

            var actionSheetResult = await App.Current.MainPage.DisplayActionSheet("Do you want to delete this Ingredient?", null, null, new string[] { "Delete", "Cancel" });

            if (actionSheetResult.Equals("Delete"))
            {
                await CrossCloudFirestore
                    .Current
                    .Instance
                    .Collection(FirebaseCollectionKeys.COL_INGREDIENTS)
                    .Document(ingredient.Id)
                    .DeleteAsync();

                Ingredients.Remove(ingredient);
            }
        }

        private void UpdateIngredients(IEnumerable<Ingredient> newIngredients, bool resetList = false)
        {
            if (resetList)
            {
                Ingredients.Clear();
            }

            foreach (var ingredient in newIngredients)
            {
                Ingredients.Add(ingredient);
            }
        }
    }
}
