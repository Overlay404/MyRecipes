﻿using MyRecipes.Model;
using System;
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
        public EditAndAddEngridient(Ingredient ingredient = null)
        {
            ValidateIngridientOnNull(ingredient);

            Ingredient = ingredient ?? new Ingredient();

            Units = App.db.Unit.Local.ToList();

            InitializeComponent();
        }

        private void ValidateIngridientOnNull(Ingredient ingredient)
        {
            if (ingredient == null)
                ContentButtonEditOrAdd = "Добавить";
            else
                ContentButtonEditOrAdd = "Сохранить";
        }

        private void Button_Click_Cancellation(object sender, RoutedEventArgs e)
        {
            if (App.db.ChangeTracker.HasChanges() == false)
            {
                TransitionInMainFrame();
                return;
            }

            ValidateOfButtonClosing();
        }

        private void ValidateOfButtonClosing()
        {
            switch (AskClose())
            {
                case MessageBoxResult.Yes:
                    foreach (var entry in App.db.ChangeTracker.Entries().Where(entry => entry.State == System.Data.Entity.EntityState.Modified))
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                    TransitionInMainFrame();
                    break;

                case MessageBoxResult.No:
                    break;
            }
        }

        private void Button_Click_AddOrEdit(object sender, RoutedEventArgs e)
        {
            if (ValidateDate() == false)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            ValidateSaveDataInDateBase();
        }

        private void ValidateSaveDataInDateBase()
        {
            switch (Ask())
            {
                case MessageBoxResult.Yes:

                    try
                    {
                        if (App.db.Ingredient.Local.Contains(Ingredient) == false)
                            App.db.Ingredient.Local.Add(Ingredient);

                        App.db.SaveChanges();
                    }
                    catch (Exception mess)
                    {
                        MessageBox.Show($"Ошибка {mess} \nПопробуйте еще раз");
                        return;
                    }

                    TransitionInMainFrame();
                    break;

                case MessageBoxResult.No:
                    foreach (var entry in App.db.ChangeTracker.Entries().Where(entry => entry.State == System.Data.Entity.EntityState.Modified))
                        entry.CurrentValues.SetValues(entry.OriginalValues);

                    TransitionInMainFrame();
                    break;
            }
        }

        private void TransitionInMainFrame()
        {
            IngredientPage.Instance.IngredientDataGrid.Items.Refresh();

            MainWindow.Instance.ProductFrame.Navigate(new IngredientPage());

            MainWindow.Instance.AllCostIngredientText.Visibility = Visibility.Visible;
            MainWindow.Instance.CountIngredientText.Visibility = Visibility.Visible;
        }

        private bool ValidateDate() =>
            Name.Text != "" &&
            Cost.Text != "" &&
            CountForCount.Text != "" &&
            Cousnt.Text != "" &&
            CostForCountComboBox.SelectedItem != null;

        #region Методы сообщений

        /// <summary>
        /// Общий метод для вывода сообщения пользовотелю о сохранении данных
        /// </summary>
        /// <returns> MessageBoxResult.YesNo </returns>
        private MessageBoxResult Ask() => MessageBox.Show("Вы действительно хотите сохранить измененные данные",
                                                           "Уведомление",
                                                           MessageBoxButton.YesNo,
                                                           MessageBoxImage.Warning);

        /// <summary>
        /// Общий метод для вывода сообщения пользовотелю при выходе
        /// </summary>
        /// <returns> MessageBoxResult.YesNo </returns>
        private MessageBoxResult AskClose() => MessageBox.Show("Вы действительно хотите выйти, данные не сохранятся",
                                               "Уведомление",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Warning);
        #endregion

        #region Проверка значений
        private void Cost_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) =>
            e.Handled = "0123456789.".IndexOf(e.Text) < 0;

        private void CountForCount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) =>
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        private void Cousnt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) =>
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        private void Name_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) =>
            e.Handled = "0123456789.,?/|\\()-_=+*&^%$#@!№;%:?".IndexOf(e.Text) > 0;
        #endregion
    }
}
