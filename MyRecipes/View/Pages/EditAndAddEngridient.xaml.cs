using MyRecipes.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyRecipes.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditAndAddEngridient.xaml
    /// </summary>
    public partial class EditAndAddEngridient : Page
    {
        public Ingredient Ingredient
        {
            get { return (Ingredient)GetValue(IngredientProperty); }
            set { SetValue(IngredientProperty, value); }
        }
        public static readonly DependencyProperty IngredientProperty =
            DependencyProperty.Register("Ingredient", typeof(Ingredient), typeof(EditAndAddEngridient));

        public EditAndAddEngridient(Ingredient ingredient)
        {
            Ingredient = ingredient; 

            InitializeComponent();
        }
    }
}
