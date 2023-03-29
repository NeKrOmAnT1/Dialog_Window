using Dialog_Window.Forms;
using Dialog_Window.Models;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> ListProduct { get; set; }
        public Product SelectedProduct { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ListProduct = new();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Forms.Dialog add = new Forms.Dialog();
            add.ShowDialog();
            ListProduct.Add(add.Product);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
                return;

            Dialog dialog = new Dialog(SelectedProduct);
            dialog.ShowDialog();
           // ListProduct.Add(dialog.Product);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
                return;
            Dialog dialog = new Dialog(SelectedProduct);
            MessageBoxResult messageBoxResult = MessageBox.Show("Ты уверен?", "Уверен", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ListProduct.Remove(dialog.Product);
            }
            else
            {
                MessageBox.Show("Понял");
            }
        }
    }
}
