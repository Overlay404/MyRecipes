using MyRecipes.View.Pages;
using System.Windows;
using System.Windows.Navigation;

namespace MyRecipes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        public string NameChapter
        {
            get { return (string)GetValue(NameChapterProperty); }
            set { SetValue(NameChapterProperty, value); }
        }

        public static readonly DependencyProperty NameChapterProperty =
            DependencyProperty.Register("NameChapter", typeof(string), typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();

            Instance = this;
            ProductFrame.Navigate(new IngredientPage());
        }

        //Кнопка Блюд
        private void Button_Click_1(object sender, RoutedEventArgs e) =>
            ProductFrame.Navigate(new Dishes());

        //Кнопка ингредиенты
        private void Button_Click_2(object sender, RoutedEventArgs e) =>
            ProductFrame.Navigate(new IngredientPage());

        //Событие навигации
        private void ProductFrame_Navigated(object sender, NavigationEventArgs e)
        {
            switch (ProductFrame.Content.ToString().Split('.')[3])
            {
                case "DishesPage":
                    NameChapter = "Список блюд";
                    break;
                case "IngredientPage":
                    NameChapter = "Список ингредиентов";
                    break;
            }

        }

        //Кнопка выхода из приложения
        private void Button_Click(object sender, RoutedEventArgs e) =>
            Application.Current.Shutdown();
    }
}
