using MyRecipes.Model;
using MyRecipes.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для AboutDish.xaml
    /// </summary>
    public partial class AboutDish : Page
    {
        public static AboutDish Instance { get; private set; }

        public Dish Dish { get; }
        
        public int Count;

        public AboutDish(Dish dish)
        {
            CookingStage = dish.CookingStage;
            IngredientOfStage = dish.IngredientOfStage;

            DestroyLogicApplication();

            InitializeComponent();

            UpdateContentControl();

            Instance = this;
            Dish = dish;

            CulcCostDishWithCount();
        }

        public void UpdateContentControl() =>
            ContentControl.Content = DishObject;

        private static void DestroyLogicApplication()
        {
            MainWindow.Instance.AllCostIngredientText.Visibility = Visibility.Collapsed;
            MainWindow.Instance.CountIngredientText.Visibility = Visibility.Collapsed;
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            CountText.Text = (int.Parse(CountText.Text) + 1).ToString();
        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(CountText.Text.Trim()) <= 1)
            {
                CountText.Text = "1";
                return;
            }
            CountText.Text = (int.Parse(CountText.Text) - 1).ToString();
        }

        private void CountTextBox_TextChanged(object sender, TextChangedEventArgs e) =>
            CulcCostDishWithCount();

        public void CulcCostDishWithCount()
        {
            if (CountText == null || int.TryParse(CountText.Text.Trim(), out Count) == false || Dish == null)
            {
                CountText.Text = "1";
                return;
            }
            CostDish = Dish.AllSumDish * Count;
        }

        #region События
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in IngredientOfStage)
            {
                if (item.Quantity > item.Ingredient.AvailableCount)
                {
                    MessageBox.Show("Количество игредиента(ов) в блюде превышает количество ингредиента(ов) в холодильнике");
                    return;
                }
            }
            MainWindow.Instance.ProductFrame.Navigate(new Dishes());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (DataCookingStage.SelectedItem == null)
                return;

            new AddCookingStageInDishes(DataCookingStage.SelectedItem as CookingStage).Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) =>
            new AddCookingStageInDishes().Show();

        private void Button_Click_5(object sender, RoutedEventArgs e) =>
            DeleteCookingStage();

        private void DeleteCookingStage()
        {
            if (MessageBox.Show("Вы уверены? Вы удалите и все ингредиенты для этого этапа!", "Оповещение", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            var objectCookingStage = DataCookingStage.SelectedItem as CookingStage;
            List<IngredientOfStage> listIngredientOfStage = App.db.IngredientOfStage.Where(i => i.CookingStageId == objectCookingStage.Id).ToList();

            App.db.IngredientOfStage.RemoveRange(listIngredientOfStage);
            App.db.CookingStage.Remove(objectCookingStage);

            App.db.SaveChanges();
        }
        #endregion
    }
}
