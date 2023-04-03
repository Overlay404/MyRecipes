using MyRecipes.Model;
using MyRecipes.View.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MyRecipes.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddCookingStageInDishes.xaml
    /// </summary>
    public partial class AddCookingStageInDishes : Window
    {
        public static CookingStage CookingStageObject { get; set; }

        public static AddCookingStageInDishes Instance;

        public IEnumerable<IngredientOfStage> IngredientOfStageInCookingStage
        {
            get { return (IEnumerable<IngredientOfStage>)GetValue(IngredientOfStageInCookingStageProperty); }
            set { SetValue(IngredientOfStageInCookingStageProperty, value); }
        }

        public static readonly DependencyProperty IngredientOfStageInCookingStageProperty =
            DependencyProperty.Register("IngredientOfStageInCookingStage", typeof(IEnumerable<IngredientOfStage>), typeof(AboutDish));

        public AddCookingStageInDishes(CookingStage cookingStage = null)
        {
            CookingStageObject = cookingStage;

            CreateObjectInDataBase();
            UpdateIngredientOfStage();
            InitializeComponent();

            Instance = this;

            if (CookingStageObject != null)
            {
                Description.Text = CookingStageObject.Description;
                Time.Text = CookingStageObject.TimeInMinutes.ToString();
            }
        }

        public void UpdateIngredientOfStage() =>
            IngredientOfStageInCookingStage = AboutDish.Instance.Dish.IngredientOfStage.Where(c => c.CookingStageId == CookingStageObject.Id);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Time.Text.Trim(), out int time) == false)
                return;

            CookingStageObject.Description = Description.Text.Trim();
            CookingStageObject.TimeInMinutes = time;

            App.db.SaveChanges();

            AboutDish.Instance.CookingStage = AboutDish.Instance.Dish.CookingStage;

            MainWindow.Instance.ProductFrame.Navigate(AboutDish.Instance.Dish);

            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) =>
            new AddIngredientInDishes(CookingStageObject).ShowDialog();

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (DataIngredient.SelectedItem == null) return;

            App.db.IngredientOfStage.Remove(DataIngredient.SelectedItem as IngredientOfStage);
            App.db.SaveChanges();

            UpdateIngredientOfStage();
            UpdateDataGrid();
        }

        private static void CreateObjectInDataBase()
        {
            ValidateCookingStageObjectOnNull();

            if (App.db.CookingStage.Local.Contains(CookingStageObject) == false)
                App.db.CookingStage.Local.Add(CookingStageObject);
        }

        private static void ValidateCookingStageObjectOnNull()
        {
            if (CookingStageObject == null)
            {
                CookingStageObject = new CookingStage
                {
                    DishId = AboutDish.Instance.Dish.Id
                };
            }
        }

        public void UpdateDataGrid() => DataIngredient.Items.Refresh();
    }
}
