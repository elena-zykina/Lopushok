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
using System.Windows.Threading;
using Lopushok.DataBase;
using Lopushok.Pages;

namespace Lopushok.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Page
    {
        public ProductList()
        {
            InitializeComponent();
            var filtItems = Transition.Context.ProductType.ToList();
            filtItems.Insert(0, new ProductType { Title = "Все элементы" });
            ProductTypeBox.ItemsSource = filtItems;

            ProductTypeBox.SelectedIndex = 0;
            SortCBox.SelectedIndex = 0;

            ListProduct.ItemsSource = Transition.Context.Product.ToList();
        }

        private void DataView()
        {
            var tempDataProduct = Transition.Context.Product.ToList();

            if (ProductTypeBox.SelectedIndex > 0)
                tempDataProduct = tempDataProduct.Where(p => p.ProductTypeID == (ProductTypeBox.SelectedItem as ProductType).ID).ToList();

            if (SearchProductBox.Text != "Введите для поиска" && SearchProductBox.Text != null)
                tempDataProduct = tempDataProduct
                    .Where(p => p.Title.ToLower().Contains(SearchProductBox.Text.ToLower())
                    || p.ArticleNumber.ToLower().Contains(SearchProductBox.Text.ToLower()))
                    .ToList();
            switch (SortCBox.SelectedIndex)
            {
                case 1:
                    {
                        if (!(bool)CheckSort.IsChecked)
                            tempDataProduct = tempDataProduct
                                    .OrderBy(p => p.Title)
                                    .ToList();
                        else
                            tempDataProduct = tempDataProduct
                                    .OrderByDescending(p => p.Title)
                                    .ToList();

                        ListProduct.ItemsSource = tempDataProduct;
                        break;
                    }
                case 2:
                    {
                        if (!(bool)CheckSort.IsChecked)
                            tempDataProduct = tempDataProduct
                                    .OrderBy(p => p.ProductionWorkshopNumber)
                                    .ToList();
                        else
                            tempDataProduct = tempDataProduct
                                    .OrderByDescending(p => p.ProductionWorkshopNumber)
                                    .ToList();

                        ListProduct.ItemsSource = tempDataProduct;
                        break;
                    }
                case 3:
                    {
                        if (!(bool)CheckSort.IsChecked)
                            tempDataProduct = tempDataProduct
                                    .OrderBy(p => p.MinCostForAgent)
                                    .ToList();
                        else
                            tempDataProduct = tempDataProduct
                                    .OrderByDescending(p => p.MinCostForAgent)
                                    .ToList();

                        ListProduct.ItemsSource = tempDataProduct;
                        break;
                    }
            }
            ListProduct.ItemsSource = tempDataProduct;
        }

        private void SearchProductBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchProductBox.Text != "Введите для поиска")
                DataView();
        }

        private void SearchProductBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchProductBox.Text))
                SearchProductBox.Text = "Введите для поиска";
        }

        private void SearchProductBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchProductBox.Text = null;
        }

        private void SortCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView();
        }

        private void CheckSort_Unchecked(object sender, RoutedEventArgs e)
        {
            DataView();
        }

        private void CheckSort_Checked(object sender, RoutedEventArgs e)
        {
            DataView();
        }

        private void ProductsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.Navigate(new PageAdd());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var dltData = ListProduct.SelectedItems.Cast<Product>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить элемент?",
                "Удаление продуктов", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Transition.Context.Product.RemoveRange(dltData);
                    Transition.Context.SaveChanges();
                    MessageBox.Show("Данные успешно удалены", "Удаление продуктов", MessageBoxButton.OK, MessageBoxImage.Information);

                    DataView();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"При сохранении произошла ошибка:\n{error.Message}", "Удаление продуктов", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Transition.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataView();
            }
        }

        private void ProductTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView();
        }
    }
}
