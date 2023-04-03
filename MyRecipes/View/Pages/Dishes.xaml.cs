using MyRecipes.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            Categories = categories;
            DishCollection = dishes;

            InitializateCategory();

            InitializeComponent();
        }

        private void InitializateCategory()
        {
            categories.Add(new Category { Name = "Все" });
            App.db.Category.Local.ToList().ForEach((Category itemCategory) => categories.Add(itemCategory));
        }

        private void CostForCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            UpdateListDish();

        private void Search_TextChanged(object sender, TextChangedEventArgs e) =>
            UpdateListDish();

        private void UpdateListDish()
        {
            if (CostForCountComboBox == null || Search == null) return;

            IssuingValuesDishSortList();
        }
        private void IssuingValuesDishSortList()
        {
            var dishList = App.db.Dish.Local.Where(d => d.Category.Name.Equals((CostForCountComboBox.SelectedItem as Category).Name) &&
                                                        d.Name.ToLower().StartsWith(Search.Text.Trim().ToLower())).ToList();

            ValidateCostForCountComboBox(dishList);
        }

        private void ValidateCostForCountComboBox(List<Dish> dishSort)
        {
            if (CostForCountComboBox.SelectedIndex != 0)
                DishCollection = dishSort;
            else
                DishCollection = dishes.Where(d => d.Name.ToLower().StartsWith(Search.Text.Trim().ToLower()));
        }

        private void ListView_Selected(object sender, RoutedEventArgs e) =>
            MainWindow.Instance.ProductFrame.Navigate(new AboutDish(DishesListView.SelectedItem as Dish));
    }
}
