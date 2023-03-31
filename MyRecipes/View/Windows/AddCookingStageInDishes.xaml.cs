using MyRecipes.View.Pages;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MyRecipes.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddCookingStageInDishes.xaml
    /// </summary>
    public partial class AddCookingStageInDishes : Window
    {
        public AddCookingStageInDishes()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var idDish = AboutDish.Instance.Dish.Id;
            if(int.TryParse(Time.Text.Trim(), out int time))
            {
                return;
            }

            App.db.CookingStage.Add(new Model.CookingStage
            {
                DishId = idDish,
                Description = Description.Text.Trim(),
                TimeInMinutes = time
            });
            App.db.SaveChanges();
        }
    }
}
