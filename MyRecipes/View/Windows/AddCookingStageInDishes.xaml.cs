using MyRecipes.Model;
using MyRecipes.View.Pages;
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

        public AddCookingStageInDishes(CookingStage cookingStage = null)
        {
            InitializeComponent();
            CookingStageObject = cookingStage;
            if(cookingStage != null)
            {
                Description.Text = cookingStage.Description;
                Time.Text = cookingStage.TimeInMinutes.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var idDish = AboutDish.Instance.Dish.Id;

            if (int.TryParse(Time.Text.Trim(), out int time))
            {
                return;
            }

            var objectEditingCookingStage = new CookingStage
            {
                DishId = idDish,
                Description = Description.Text.Trim(),
                TimeInMinutes = time
            };

            if (objectEditingCookingStage == null)
            {
                App.db.CookingStage.Add(objectEditingCookingStage);
            }
            else
            {
                CookingStageObject = objectEditingCookingStage;
                //AboutDish.Instance.Dish.CookingStage.Where(c => c.Id == objectEditingCookingStage.Id) = objectEditingCookingStage;
            }

            App.db.SaveChanges();
        }
    }
}
