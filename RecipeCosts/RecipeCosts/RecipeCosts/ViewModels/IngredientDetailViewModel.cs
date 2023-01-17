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
    [QueryProperty(nameof(PropertyDictId), nameof(PropertyDictId))]
    public class IngredientDetailViewModel : BaseViewModel
    {
        private string propertyDictId;

        public string PropertyDictId
        {
            get { return propertyDictId; }
            set { 
                propertyDictId = value;
                if (!String.IsNullOrEmpty(value))
                {
                    LoadFromPropertyDict(value);
                }
            }
        }

        private Ingredient ingredient;

        public Ingredient Ingredient
        {
            get { return ingredient; }
            set { 
                ingredient = value;
            }
        }

        private string ingredientId;

        public string IngredientId
        {
            get { return ingredientId; }
            set { 
                ingredientId = value;
                Ingredient.Id = ingredientId;
                OnPropertyChanged();
            }
        }

        private string ingredientName;

        public string IngredientName
        {
            get { return ingredientName; }
            set { 
                ingredientName = value;
                Ingredient.Name = ingredientName;
                OnPropertyChanged();
            }
        }

        private double ingredientPrice;

        public double IngredientPrice
        {
            get { return ingredientPrice; }
            set { 
                ingredientPrice = value;
                Ingredient.Price = ingredientPrice;
                OnPropertyChanged();
            }
        }

        public bool IsNewItem { get; set; }

        public Command SaveCommand { get; set; }

        public IngredientDetailViewModel()
        {
            SaveCommand = new Command(OnSaveClicked);
            Ingredient = new Ingredient();
            IsNewItem = true;
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

        private void LoadFromPropertyDict(string propertyId)
        {
            if (Application.Current.Properties.TryGetValue(propertyId, out object retrievedIngredeint))
            {
                var ingredient = retrievedIngredeint as Ingredient;

                this.IngredientId = ingredient.Id;
                this.IngredientName = ingredient.Name;
                this.IngredientPrice = ingredient.Price;

                IsNewItem = false;
            }
        }

        public void OnAppearing()
        {
        }
    }
}
