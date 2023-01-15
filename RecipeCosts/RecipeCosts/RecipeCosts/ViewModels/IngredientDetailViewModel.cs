using Plugin.CloudFirestore;
using RecipeCosts.Model;
using RecipeCosts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RecipeCosts.ViewModels
{
    public class IngredientDetailViewModel
    {
        private Ingredient ingredient;

        public Ingredient Ingredient
        {
            get { return ingredient; }
            set { ingredient = value; }
        }

        private string ingredientId;

        public string IngredientId
        {
            get { return ingredientId; }
            set { ingredientId = value; }
        }

        private string ingredientName;

        public string IngredientName
        {
            get { return ingredientName; }
            set { 
                ingredientName = value;
                Ingredient.Name = ingredientName;
            }
        }

        private double ingredientPrice;

        public double IngredientPrice
        {
            get { return ingredientPrice; }
            set { 
                ingredientPrice = value;
                Ingredient.Price = ingredientPrice;
            }
        }

        public bool IsNewItem { get; set; }

        public Command SaveCommand { get; set; }

        public IngredientDetailViewModel()
        {
            SaveCommand = new Command(OnSaveClicked);

            OnAppearing();
        }

        public async void OnSaveClicked()
        {
            if (Ingredient != null)
            {
                var appUserId = Preferences.Get(PreferenceKeys.PREF_CURRENT_APP_USER_ID, "");
                if (String.IsNullOrEmpty(appUserId))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "App user id not found", "OK");
                }
                else
                {
                    Ingredient.UserId = appUserId;

                    if (IsNewItem)
                    {
                        await CrossCloudFirestore.
                            Current.
                            Instance.
                            Collection(FirebaseCollectionKeys.COL_INGREDIENTS).AddAsync(Ingredient);
                    }
                    else
                    {
                        await CrossCloudFirestore.
                            Current.
                            Instance.
                            Collection(FirebaseCollectionKeys.COL_INGREDIENTS).Document(Ingredient.Id).UpdateAsync(Ingredient);
                    }
                }
            }

            await Shell.Current.GoToAsync("..");
        }

        public void OnAppearing()
        {
            Ingredient = new Ingredient();

            if (IngredientId == null)
            {
                IsNewItem = true;
            } else
            {
                IsNewItem = false;
            }
        }
    }
}
