using MyRecipes.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyRecipes.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Dishes.xaml
    /// </summary>
    public partial class Dishes : Page
    {
        readonly List<Dish> dishes = App.db.Dish.Local.ToList();
        readonly List<Category> categories = new List<Category>();
        public Dishes()
        {
            InitializateCategory();
            Categories = categories;
            DishCollection = dishes;

            InitializeComponent();
        }

        private void InitializateCategory()
        {
            categories.Add(new Category { Name = "Все" });
            App.db.Category.Local.ToList().ForEach((Category itemCategory) => categories.Add(itemCategory));
        }

        private void CostForCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListDish();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListDish();
        }

        private void UpdateListDish()
        {
            if (CostForCountComboBox == null || Search == null) return;

            List<Dish> dishSort = App.db.Dish.Local.Where
                (d => d.Category.Name.Equals((CostForCountComboBox.SelectedItem as Category).Name) && 
                d.Name.ToLower().StartsWith(Search.Text.Trim().ToLower())).ToList();

            if (CostForCountComboBox.SelectedIndex != 0) DishCollection = dishSort;
            else DishCollection = dishes.Where(d => d.Name.ToLower().StartsWith(Search.Text.Trim().ToLower()));
        }
    }
}
