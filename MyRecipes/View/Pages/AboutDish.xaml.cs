﻿using MyRecipes.Model;
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
        public decimal CostDish
        {
            get { return (decimal)GetValue(CostDishProperty); }
            set { SetValue(CostDishProperty, value); }
        }

        public static readonly DependencyProperty CostDishProperty =
            DependencyProperty.Register("CostDish", typeof(decimal), typeof(AboutDish));



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
            DestroyLogicApplication();
            DishObject = dish;
            InitializeComponent();
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

        private void CulcCostDishWithCount()
        {
            if (CountText == null || int.TryParse(CountText.Text.Trim(), out Count) == false || Dish == null)
            {
                CountText.Text = "1";
                return;
            }
            CostDish = Dish.AllSumDish * Count;
        }
    }
}
