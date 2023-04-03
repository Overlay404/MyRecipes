using System.Collections.Generic;
using System.Linq;

namespace MyRecipes.Model
{
    partial class Dish
    {
        public decimal AllSumDish => CookingStage.Sum(c =>
                c.IngredientOfStage.Sum(i => (decimal)i.Quantity * (i.Ingredient.Cost / (decimal)i.Ingredient.CostForCount))) / ServingQuantity;
        public int? TimeInMiutes => CookingStage.Select(c => c.TimeInMinutes).FirstOrDefault();
        public IEnumerable<Ingredient> Ingredients => CookingStage.SelectMany(c => c.IngredientOfStage.Select(i => i.Ingredient)).ToList();
        public IEnumerable<IngredientOfStage> IngredientOfStage => CookingStage.SelectMany(c => c.IngredientOfStage).ToList();

    }
}
