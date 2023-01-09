using RecipeCosts.Model.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeCosts.Models.Keys
{
    public static class ApiKeys
    {
        public static ApiInfo Firebase = new ApiInfo()
        {
            ApiName = "Firebase",
            AesKey = Convert.FromBase64String(""),
            AesIV = Convert.FromBase64String(""),
            ApiKeyEncrypted = ""
        };
    }
}
