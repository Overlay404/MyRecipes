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

        public decimal CostDish
        {
            get { return (decimal)GetValue(CostDishProperty); }
            set { SetValue(CostDishProperty, value); }
        }

        public static readonly DependencyProperty CostDishProperty =
            DependencyProperty.Register("CostDish", typeof(decimal), typeof(AboutDish));




        public IEnumerable<CookingStage> CookingStage
        {
            get { return (IEnumerable<CookingStage>)GetValue(CookingStageProperty); }
            set { SetValue(CookingStageProperty, value); }
        }

        public static readonly DependencyProperty CookingStageProperty =
            DependencyProperty.Register("CookingStage", typeof(IEnumerable<CookingStage>), typeof(AboutDish));



        public IEnumerable<IngredientOfStage> IngredientOfStage
        {
            get { return (IEnumerable<IngredientOfStage>)GetValue(IngredientOfStageProperty); }
            set { SetValue(IngredientOfStageProperty, value); }
        }

        public static readonly DependencyProperty IngredientOfStageProperty =
            DependencyProperty.Register("IngredientOfStage", typeof(IEnumerable<IngredientOfStage>), typeof(AboutDish));



        public Dish DishObject
        {
            get { return (Dish)GetValue(DishesProperty); }
            set { SetValue(DishesProperty, value); }
        }

        public Dish Dish { get; }

        public static readonly DependencyProperty DishesProperty =
            DependencyProperty.Register("DishObject", typeof(Dish), typeof(AboutDish));
        
        public int Count;

        public AboutDish(Dish dish)
        {
            CookingStage = dish.CookingStage;
            IngredientOfStage = dish.IngredientOfStage;
            DestroyLogicApplication();
            DishObject = dish;
            InitializeComponent();
            Instance = this;
            Dish = dish;
            CulcCostDishWithCount();
        }

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

        private void CountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CulcCostDishWithCount();
        }

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AddIngredientInDishes().Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(DataIngredient.SelectedItem == null) return;

            App.db.IngredientOfStage.Remove(DataIngredient.SelectedItem as IngredientOfStage);
            App.db.SaveChanges();
            IngredientOfStage = Dish.IngredientOfStage;
            CulcCostDishWithCount();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(DataCookingStage.SelectedItem == null)
                new AddCookingStageInDishes().Show();
            else
                new AddCookingStageInDishes(DataCookingStage.SelectedItem as CookingStage).Show();
        }
        #endregion
    }
}
