using Dialog_Window.Models;
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
using System.Windows.Shapes;

namespace Dialog_Window.Forms
{
    /// <summary>
    /// Логика взаимодействия для Dialog.xaml
    /// </summary>
    public partial class Dialog : Window
    {
        public Product Product { get; set; }
        public Dialog()
        {
            InitializeComponent();
            Product = new Product();
            DataContext = Product;
        }
        public Dialog(Product product)
        {
            InitializeComponent();
            Product = product;
            DataContext = Product;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
                this.Close();

            
        }
    }
}
