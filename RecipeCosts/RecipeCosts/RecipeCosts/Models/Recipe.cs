using Plugin.CloudFirestore.Attributes;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeCosts.Models
{
    public class Recipe
    {
        [Id]
        public string Id { get; set; }
        public double OverallCosts { get; set; }
        public string Name { get; set; }

        [ServerTimestamp(CanReplace = false)]
        public Timestamp CreatedAt { get; set; }
        [ServerTimestamp]
        public Timestamp UpdatedAt { get; set; }
    }
}
