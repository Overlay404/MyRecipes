using MyRecipes.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для IngredientPage.xaml
    /// </summary>
    public partial class IngredientPage : Page
    {
        private int CountEntriestOnPage = 4; // количество записей на одной странице

        private IEnumerable<Ingredient> TestIEnumerableIngredients;

        public IngredientPage()
        {
            Ingredient = App.db.Ingredient.Local;
            TestIEnumerableIngredients = Ingredient;

            NumberPage = 1;
            TotalNumberPages = 0;

            NumberEntriestOnOnePage = new List<int>();

            CallingMethodsBeforeInitialization();

            InitializeComponent();

            CallingMethodsAfterInitialization();
        }

        #region Основыние методы до и после Инициализации
        private void CallingMethodsBeforeInitialization()
        {
            ValidateTotalCountPage();
            PageProcessing();
        }

        private void CallingMethodsAfterInitialization()
        {
            
        }
        #endregion

        #region Редактирование
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientDataGrid.SelectedItem == null)
                return;

            Ingredient ingredient = IngredientDataGrid.SelectedItem as Ingredient;
            

        }
        #endregion

        #region Удаление
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Кнопки перехода по страницам 
        private void CountEntriestComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateCountEntriestOnPage();
            ValidateTotalCountPage();
            PageProcessing();
        }
        
        private void ShiftOnOnePageLeft(object sender, RoutedEventArgs e)
        {
            if ((NumberPage - 1) <= 0)
                return;

            NumberPage--;

            PageProcessing();
        }

        private void ShiftOnAllPageLeft(object sender, RoutedEventArgs e)
        {
            NumberPage = 1;

            PageProcessing();
        }

        private void ShiftOnOnePageRigth(object sender, RoutedEventArgs e)
        {
            if (TotalNumberPages <= NumberPage)
                return;

            NumberPage++;

            PageProcessing();
        }

        private void ShiftOnAllPageRigth(object sender, RoutedEventArgs e)
        {
            NumberPage = TotalNumberPages;

            PageProcessing();
        }
        #endregion

        #region Методы для страниц
        private void PageProcessing()
        {
            Ingredient = TestIEnumerableIngredients;

            Ingredient = Ingredient.Cast<Ingredient>()
                                   .Skip((NumberPage - 1) * CountEntriestOnPage)
                                   .Take(CountEntriestOnPage);
        }

        private void ValidateTotalCountPage()
        {
            TotalNumberPages = (int)Math.Ceiling(Convert.ToDouble(TestIEnumerableIngredients.Cast<Ingredient>().Count()) / Convert.ToDouble(CountEntriestOnPage));

            ValidateNumberEntriestOnOnePage();
        }

        private void ValidateNumberEntriestOnOnePage()
        {
            if (NumberEntriestOnOnePage.Count() == 0)
                for (int i = 1; i <= TestIEnumerableIngredients.Count(); i++)
                    NumberEntriestOnOnePage.Add(i);
        }

        private void ValidateCountEntriestOnPage()
        {
            if (CountEntriestComboBox.SelectedItem == null)
                return;

            CountEntriestOnPage = (int)CountEntriestComboBox.SelectedItem;
        }
        #endregion

    }
}
