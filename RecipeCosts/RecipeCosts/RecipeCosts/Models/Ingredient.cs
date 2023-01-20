using Plugin.CloudFirestore.Attributes;
using UnitsNet;
using System;
using System.Collections.Generic;
using System.Text;
using Plugin.CloudFirestore;

namespace RecipeCosts.Models
{
    public class Ingredient
    {
        [Id]
        public string Id { get; set; }
        public string UserId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public IQuantity Quantity { get; set; }
        [ServerTimestamp(CanReplace = false)]
        public Timestamp CreatedAt { get; set; }
        [ServerTimestamp]
        public Timestamp UpdatedAt { get; set; }
    }
}
