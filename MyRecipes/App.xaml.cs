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

        public App()
        {
            db.Ingredient.Load();
            db.Unit.Load();
        }
    }
}
