using Newtonsoft.Json;
using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeCosts.Model
{
    public class User : HasId
    {
        [Id]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        [Ignored]
        [JsonIgnore]
        public string Password { get; set; }
        [Ignored]
        [JsonIgnore]
        public string ConfirmPassword { get; set; }
    }
}
