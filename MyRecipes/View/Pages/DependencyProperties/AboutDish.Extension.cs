using MyRecipes.Model;
using System.Collections.Generic;
using System.Windows;

namespace MyRecipes.View.Pages
{
    partial class AboutDish
    {
        public decimal CostDish
        {
            get { return (decimal)GetValue(CostDishProperty); }
            set { SetValue(CostDishProperty, value); }
        }

        public static readonly DependencyProperty CostDishProperty =
            DependencyProperty.Register("CostDish", typeof(decimal), typeof(AboutDish));


        public IEnumerable<CookingStage> CookingStage
        {
            get { return (IEnumerable<CookingStage>)GetValue(CookingStageProperty); }
            set { SetValue(CookingStageProperty, value); }
        }

        public static readonly DependencyProperty CookingStageProperty =
            DependencyProperty.Register("CookingStage", typeof(IEnumerable<CookingStage>), typeof(AboutDish));


        public IEnumerable<IngredientOfStage> IngredientOfStage
        {
            get { return (IEnumerable<IngredientOfStage>)GetValue(IngredientOfStageProperty); }
            set { SetValue(IngredientOfStageProperty, value); }
        }

        public static readonly DependencyProperty IngredientOfStageProperty =
            DependencyProperty.Register("IngredientOfStage", typeof(IEnumerable<IngredientOfStage>), typeof(AboutDish));

        public Dish DishObject
        {
            get { return (Dish)GetValue(DishesProperty); }
            set { SetValue(DishesProperty, value); }
        }

        public static readonly DependencyProperty DishesProperty =
            DependencyProperty.Register("DishObject", typeof(Dish), typeof(AboutDish));
    }
}
