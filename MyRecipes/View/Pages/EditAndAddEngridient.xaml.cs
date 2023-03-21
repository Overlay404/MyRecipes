using MyRecipes.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MyRecipes.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditAndAddEngridient.xaml
    /// </summary>
    public partial class EditAndAddEngridient : Page
    {
        public EditAndAddEngridient(Ingredient ingredient)
        {
            Ingredient = ingredient;

            Units = App.db.Unit.Local.ToList();

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (AskClose())
            {
                case MessageBoxResult.Yes:
                    IngredientPage.Instance.IngredientDataGrid.Items.Refresh();

                    MainWindow.Instance.ProductFrame.Navigate(new IngredientPage());

                    MainWindow.Instance.AllCostIngredientText.Visibility = Visibility.Visible;
                    MainWindow.Instance.CountIngredientText.Visibility = Visibility.Visible;
                    break;

                case MessageBoxResult.No:
                    foreach (var entry in App.db.ChangeTracker.Entries().Where(entry => entry.State == System.Data.Entity.EntityState.Modified))
                        entry.CurrentValues.SetValues(entry.OriginalValues);

                    IngredientPage.Instance.IngredientDataGrid.Items.Refresh();

                    MainWindow.Instance.ProductFrame.Navigate(new IngredientPage());

                    MainWindow.Instance.AllCostIngredientText.Visibility = Visibility.Visible;
                    MainWindow.Instance.CountIngredientText.Visibility = Visibility.Visible;

                    break;
            }
        }

        private void TransitionInMainFrame()
        {

        }


        #region Методы сообщений
        private MessageBoxResult Ask() => MessageBox.Show("Вы действительно хотите сохранить эти маленькие данные",
                                                           "Уведомление",
                                                           MessageBoxButton.YesNo,
                                                           MessageBoxImage.Warning);

        private MessageBoxResult AskClose() => MessageBox.Show("Вы действительно хотите выйти, данные не сохранятся",
                                               "Уведомление",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Warning);
        #endregion
    }
}
