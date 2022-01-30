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
    /// Логика взаимодействия для PageEdit.xaml
    /// </summary>
    public partial class PageEdit : Page
    {
        public PageEdit(Product product)
        {
            InitializeComponent();
            ProductTypeCombo.SelectedItem = (int)product.ProductTypeID;
            ProductTypeCombo.SelectedValuePath = "ID";
            ProductTypeCombo.DisplayMemberPath = "Title";
            ProductTypeCombo.ItemsSource = Transition.Context.ProductType.ToList();

            ProductObj.ID = product.ID;
            TxtName.Text = product.Title;
            TxtArticle.Text = Convert.ToString(product.ArticleNumber);
            TxtDescription.Text = product.Description;
            TxtImage.Text = product.Image;
            TxtMinCost.Text = Convert.ToString(product.MinCostForAgent);
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
                IEnumerable<Product> products = Transition.Context.Product.Where(p => p.ID == ProductObj.ID).AsEnumerable().
                    Select(p =>
                    {
                        p.Title = TxtName.Text;
                        p.ArticleNumber = TxtArticle.Text;
                        if (string.IsNullOrWhiteSpace(TxtDescription.Text))
                            p.Description = "";
                        else
                            p.Description = TxtDescription.Text;
                        if (string.IsNullOrWhiteSpace(TxtImage.Text))
                            p.Image = "";
                        else
                            p.Description = TxtImage.Text;
                        p.MinCostForAgent = Convert.ToDecimal(TxtMinCost.Text);
                        return p;
                    });
                foreach (Product pro in products)
                {
                    Transition.Context.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                }
                Transition.Context.SaveChanges();
                MessageBox.Show("Данные успешно изменены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }
        }

        private void ProductTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView();
        }
    }
}
