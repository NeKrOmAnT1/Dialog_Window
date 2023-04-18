using Dialog_Window.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using ModernWpf;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Dialog_Window.Forms
{
   
    public partial class Dialog : Window
    {
        public Product Product { get; set; }
        
       
        public Dialog()
        {
            InitializeComponent();
            Product = new Product();
            DataContext = Product;
            Product.ID = Guid.NewGuid();
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

            
                try
                {
                    Sqlite sqlite = new Sqlite();
                    Product product = new Product
                    {
                        Price = Product.Price,
                        Name = Product.Name,
                        ID = Product.ID,
                        Description = Product.Description
                    };

                    string show = "Уникальный идентификатор: " + Product.ID + "\r\n" + "Имя товара: " + Product.Name + "\r\n" + "Описание товара: " + Product.Description + "\r\n" + "Цена товара: " + Product.Price + " RUB";
                    MessageBox.Show(show, "Данные, занесенные в базу данных");

                    sqlite.Products.Add(product);
                    sqlite.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.ShowDialog();
            
        }

        private void bnt_exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.ShowDialog();
        }
    }
}
