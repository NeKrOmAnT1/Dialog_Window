using Dialog_Window.Forms;
using Dialog_Window.Models;
using Microsoft.EntityFrameworkCore;
using ModernWpf;
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

namespace Dialog_Window
{
    public partial class MainWindow : Window
    {
        /*
         изображение QR-Code
         */
        public ObservableCollection<Product> ListProduct { get; set; }
        public Product SelectedProduct { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            ListProduct = new();
            DataContext = this;
            this.Loaded += Sqlite_Loaded;
        }
        private void Sqlite_Loaded(object sender, RoutedEventArgs e)
        {
            Sqlite sqlite = new Sqlite();
            List<Product> products = sqlite.Products.ToList();
            ProductsList.ItemsSource = products;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Forms.Dialog add = new Forms.Dialog();
            add.ShowDialog();
            ListProduct.Add(add.Product);
        }


        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsList.SelectedItem != null)
            {
                var product = ProductsList.SelectedItem as Product;
                if (new Forms.Edit_Product(product).ShowDialog() == true)
                {
                    using (var context = new Sqlite())
                    {
                        context.Entry(product).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    ProductsList.Items.Refresh();
                }
            }
        }
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
           
            if (ProductsList.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var product = ProductsList.SelectedItem as Product;
                    using (var context = new Sqlite())
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                        ProductsList.ItemsSource = context.Products.ToList();
                    }
                }
            }

        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Sqlite())
            {
                ProductsList.ItemsSource = context.Products.ToList();
            }
        }
    }
}
