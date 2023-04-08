using Dialog_Window.Models;
using Microsoft.EntityFrameworkCore;
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

namespace Dialog_Window.Forms
{
    public partial class Edit_Product : Window
    {
        public Product Product { get; set; }
        public Edit_Product(Product product)
        {
            InitializeComponent();
            Product = product;
            DataContext = Product;
        }

        private void btn_qrcode_Click(object sender, RoutedEventArgs e)
        {
            string combined = "Уникальный идентификатор: " + tb_id.Text + "\r\n" + "Имя товара: " + tb_name.Text + "\r\n" + "Описание товара: " + tb_description.Text + "\r\n" + "Цена товара: " + tb_price.Text + " RUB";
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(combined, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qr = qrCode.GetGraphic(150);
            image_qrcoder.Source = Convert(qr);
            Product.QRCode = Convert(qr);
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (Product.QRCode == null)
            {
                MessageBox.Show("Необходимо сгенерировать QR-Code");
                return;
            }
            else
            {
                DialogResult = true;

                Product product = new Product
                {
                    Price = Product.Price,
                    Name = Product.Name,
                    ID = Guid.NewGuid(),
                    Description = Product.Description
                };
                string show = "Уникальный идентификатор: " + Product.ID + "\r\n" + "Имя товара: " + Product.Name + "\r\n" + "Описание товара: " + Product.Description + "\r\n" + "Цена товара: " + Product.Price + " RUB";
                MessageBox.Show(show, "Изменные данные, занесенные в базу данных");

                Close();
            }

        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult=false;
            Close();
        }
        #region BitmapImage Convert for QR-Code 
        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
        #endregion
    }
} 
