using MyRecipes.Model;
using MyRecipes.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyRecipes.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddIngredientInDishes.xaml
    /// </summary>
    public partial class AddIngredientInDishes : Window
    {
        public IEnumerable<Ingredient> Ingredients
        {
            get { return (IEnumerable<Ingredient>)GetValue(IngredientsProperty); }
            set { SetValue(IngredientsProperty, value); }
        }

        public static readonly DependencyProperty IngredientsProperty =
            DependencyProperty.Register("Ingredients", typeof(IEnumerable<Ingredient>), typeof(AddIngredientInDishes));

        public CookingStage Stage { get; private set; }

        public AddIngredientInDishes(CookingStage stage)
        {
            Ingredients = App.db.Ingredient.Local;
            Stage = stage;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Ingredient.SelectedItem == null) return;
            var ingredient = Ingredient.SelectedItem as Ingredient;
            var cookingStage = Stage;
            if (!int.TryParse(Count.Text.Trim(), out int count))
            {
                Count.Text = "1";
                return;
            }

            var objectIngredientOfStage = App.db.IngredientOfStage.FirstOrDefault(i => i.CookingStageId == cookingStage.Id && i.IngredientId == ingredient.Id);

            if (objectIngredientOfStage != null)
            {
                objectIngredientOfStage.Quantity += count;
            }
            else
            {
                App.db.IngredientOfStage.Local.Add(new IngredientOfStage
                {
                    Ingredient = ingredient,
                    CookingStage = cookingStage,
                    Quantity = count
                });
            }

            AddCookingStageInDishes.Instance.UpdateIngredientOfStage();
            AddCookingStageInDishes.Instance.UpdateDataGrid();

            App.db.SaveChanges();
            AddCookingStageInDishes.Instance.IngredientOfStageInCookingStage = AboutDish.Instance.Dish.IngredientOfStage.Where(i => i.CookingStageId == Stage.Id);
            Close();
        }
    }
}
