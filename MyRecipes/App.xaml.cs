using MyRecipes.Model;
using System.Data.Entity;
using System.Windows;

namespace MyRecipes
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RecipesEntities db = new RecipesEntities();

        public App() =>
            LoadTablesOfDataBase();

        /// <summary>
        ///  Загрузка всех таблиц из базы
        /// </summary>
        private static void LoadTablesOfDataBase()
        {
            db.Ingredient.Load();
            db.Unit.Load();
            db.Category.Load();
            db.Dish.Load();
            db.CookingStage.Load();
            db.IngredientOfStage.Load();
        }
    }
}
