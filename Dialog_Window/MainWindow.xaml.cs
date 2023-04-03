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
        //1. Решить вопрос с GUID
        //2. Перенос QRCode в другое окно
        //3. QRCode должен выдавать JSON
        //4. Сделать привязку к базе данных (легко)
        //5. Поработать со стилизацией (Вероятнее всего через Column и Row)


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

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Forms.Dialog add = new Forms.Dialog();
            add.ShowDialog();
            ListProduct.Add(add.Product);
        }
       

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
                return;

            Dialog dialog = new Dialog(SelectedProduct);
            dialog.ShowDialog();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProduct == null)
                return;
            Dialog dialog = new Dialog(SelectedProduct);
            MessageBoxResult messageBoxResult = MessageBox.Show("Ты уверен?", "Уверен", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ListProduct.Remove(dialog.Product);
            }
           
        }
    }
}
