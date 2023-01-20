using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeCosts.Models
{
    public class PreferenceKeys
    {
        public const string PREF_CURRENT_APP_USER = "currentAppUser";
        public const string PREF_CURRENT_APP_USER_ID = "currentAppUserId";
    }

    public class FirebaseCollectionKeys
    {
        public const string COL_APP_USERS = "AppUsers";
        public const string COL_INGREDIENTS = "Ingredients";
        public const string COL_INGREDIENT_USERID = "UserId";
        public const string COL_INGREDIENT_UPDATEDAT = "UpdatedAt";
    }
}
