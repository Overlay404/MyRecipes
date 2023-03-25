using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Model
{
    partial class Dish
    {
        public decimal AllSumDish => CookingStage.Sum(c => c.IngredientOfStage.Sum(i => (decimal)i.Quantity * (i.Ingredient.Cost / (decimal)i.Ingredient.CostForCount))) / ServingQuantity;
    }
}
