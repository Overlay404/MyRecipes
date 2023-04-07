using MyRecipes.Model;
using MyRecipes.View.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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

            bool IsExit = CheckingValuesInCount();

            AddCookingStageInDishes.Instance.UpdateIngredientOfStage();
            AddCookingStageInDishes.Instance.UpdateDataGrid();

            App.db.SaveChanges();

            AddCookingStageInDishes.Instance.IngredientOfStageInCookingStage = AboutDish.Instance.Dish.IngredientOfStage.Where(i => i.CookingStageId == Stage.Id);
            AddCookingStageInDishes.Instance.DataIngredient.ItemsSource = AboutDish.Instance.Dish.IngredientOfStage.Where(i => i.CookingStageId == Stage.Id);

            AddCookingStageInDishes.Instance.UpdateDataGrid();

            if (IsExit)
            {
                Close();
            }
        }

        private bool CheckingValuesInCount()
        {
            var ingredient = Ingredient.SelectedItem as Ingredient;
            var cookingStage = Stage;

            if (int.TryParse(Count.Text.Trim(), out int count) == false)
            {
                Count.Text = "1";
                return false;
            }

            return ValidateObjectIngridientOfStage(ingredient, cookingStage, count);

        }

        private bool ValidateObjectIngridientOfStage(Ingredient ingredient, CookingStage cookingStage, int count)
        {
            var ListCookingStageInDish = AboutDish.Instance.Dish.CookingStage.Select(c => c.Id);
            var objectIngredientOfStage = App.db.IngredientOfStage.FirstOrDefault(i => ListCookingStageInDish.Contains(i.CookingStageId) && i.IngredientId == ingredient.Id);

            if (objectIngredientOfStage != null)
            {
                MessageBox.Show("Такой игредиент уже есть в одной из стадий");
                return false;
            }

            else
            {
                App.db.IngredientOfStage.Local.Add(new IngredientOfStage
                {
                    Ingredient = ingredient,
                    CookingStageId = cookingStage.Id,
                    Quantity = count
                });
                return true;
            }


        }
    }
}
