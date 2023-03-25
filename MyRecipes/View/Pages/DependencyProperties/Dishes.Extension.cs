using MyRecipes.Model;
using System.Collections.Generic;
using System.Windows;

namespace MyRecipes.View.Pages
{
    partial class Dishes
    {
        public List<Category> Categories
        {
            get { return (List<Category>)GetValue(CategoriesProperty); }
            set { SetValue(CategoriesProperty, value); }
        }
        public static readonly DependencyProperty CategoriesProperty =
            DependencyProperty.Register("Categories", typeof(List<Category>), typeof(Dishes));



        public IEnumerable<Dish> DishCollection
        {
            get { return (IEnumerable<Dish>)GetValue(DishCollectionProperty); }
            set { SetValue(DishCollectionProperty, value); }
        }

        public static readonly DependencyProperty DishCollectionProperty =
            DependencyProperty.Register("DishCollection", typeof(IEnumerable<Dish>), typeof(Dishes));



    }
}
