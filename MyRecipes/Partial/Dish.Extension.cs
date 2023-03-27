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
        public int? TimeInMiutes => CookingStage.Select(c => c.TimeInMinutes).FirstOrDefault();
        public IEnumerable<IEnumerable<Ingredient>> Ingredients => CookingStage.Select(c => c.IngredientOfStage.Select(i => i.Ingredient)).ToList();

    }
}
