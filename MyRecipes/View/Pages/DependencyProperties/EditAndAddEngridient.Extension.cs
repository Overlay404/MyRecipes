using MyRecipes.Model;
using System.Windows;
using System.Collections.Generic;

namespace MyRecipes.View.Pages
{
    partial class EditAndAddEngridient
    {
        public Ingredient Ingredient
        {
            get { return (Ingredient)GetValue(IngredientProperty); }
            set { SetValue(IngredientProperty, value); }
        }
        public static readonly DependencyProperty IngredientProperty =
            DependencyProperty.Register("Ingredient", typeof(Ingredient), typeof(EditAndAddEngridient));

        public List<Unit> Units
        {
            get { return (List<Unit>)GetValue(UnitsProperty); }
            set { SetValue(UnitsProperty, value); }
        }

        public static readonly DependencyProperty UnitsProperty =
            DependencyProperty.Register("Units", typeof(List<Unit>), typeof(EditAndAddEngridient));

        public string ContentButtonEditOrAdd
        {
            get { return (string)GetValue(ContentButtonEditOrAddProperty); }
            set { SetValue(ContentButtonEditOrAddProperty, value); }
        }
        public static readonly DependencyProperty ContentButtonEditOrAddProperty =
            DependencyProperty.Register("ContentButtonEditOrAdd", typeof(string), typeof(EditAndAddEngridient));
    }
}
