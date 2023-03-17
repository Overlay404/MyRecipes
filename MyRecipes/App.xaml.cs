using MyRecipes.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        }
    }
}
