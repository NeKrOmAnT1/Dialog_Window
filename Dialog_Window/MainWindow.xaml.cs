using Dialog_Window.Forms;
using Dialog_Window.Models;
using Microsoft.EntityFrameworkCore;
using ModernWpf;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            sqlite.Database.Migrate();
            List<Product> products = sqlite.Products.ToList();
            ProductsList.ItemsSource = products;
            //QRCodeGenerator qrGenerator = new QRCodeGenerator();

            //foreach (Product product in products)
            //{
            //    string combined = "Уникальный идентификатор: " + product.ID + "\r\n" + "Имя товара: " + product.Name + "\r\n" + "Описание товара: " + product.Description + "\r\n" + "Цена товара: " + product.Price + " RUB";
            //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(combined, QRCodeGenerator.ECCLevel.Q);
            //    QRCode qrCode = new QRCode(qrCodeData);
            //    BitmapImage qrCodeImage = new BitmapImage();
            //    using (MemoryStream stream = new MemoryStream())
            //    {
            //        qrCode.GetGraphic(20).Save(stream, ImageFormat.Png);
            //        stream.Seek(0, SeekOrigin.Begin);
            //        qrCodeImage.BeginInit();
            //        qrCodeImage.CacheOption = BitmapCacheOption.OnLoad;
            //        qrCodeImage.StreamSource = stream;
            //        qrCodeImage.EndInit();
            //    }

            //    ListProduct.Add(new Product { Name = product.Name, Price = product.Price, QRCode = qrCodeImage, Description = product.Description, ID = product.ID });
            //}

            //ProductsList.ItemsSource = ListProduct;

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Forms.Dialog add = new Forms.Dialog();
            Close();
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

    }
}
