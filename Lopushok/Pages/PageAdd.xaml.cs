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
using Lopushok.DataBase;

namespace Lopushok.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAdd.xaml
    /// </summary>
    public partial class PageAdd : Page
    {
        public PageAdd()
        {
            InitializeComponent();

            var filtItems = Transition.Context.ProductType.ToList();

            filtItems.Insert(0, new ProductType { Title = "Выберите тип" });
            ProductTypeCombo.ItemsSource = filtItems;
            ProductTypeCombo.SelectedIndex = 0;
        }

        private void DataView()
        {
            var tempDataProduct = Transition.Context.Product.ToList();

            if (ProductTypeCombo.SelectedIndex > 0)
                tempDataProduct = tempDataProduct.Where(p => p.ProductTypeID == (ProductTypeCombo.SelectedItem as ProductType).ID).ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product()
                {
                    Title = TxtName.Text,
                    ProductType = ProductTypeCombo.SelectedItem as ProductType,
                    ArticleNumber = TxtArticle.Text,
                    Image = TxtImage.Text,
                    MinCostForAgent = Convert.ToInt32(TxtMinCost.Text),
                    Description = TxtDescription.Text,
                };
                Transition.Context.Product.Add(product);
                Transition.Context.SaveChanges();
                MessageBox.Show("Данные успешно добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            Transition.MainFrame.GoBack();
        }

        private void ProductTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView();
        }
    }
}
