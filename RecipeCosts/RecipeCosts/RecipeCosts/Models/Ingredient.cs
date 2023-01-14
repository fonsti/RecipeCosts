using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeCosts.Models
{
    public class Ingredient
    {
        public double Price { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}
