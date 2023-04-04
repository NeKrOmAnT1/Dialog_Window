using Dialog_Window.Models;
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
            Product.ID = Guid.NewGuid();
            
            //QRCodeGenerator qrGenerator = new();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(tb_id.Text, QRCodeGenerator.ECCLevel.Q);
            //QRCode qrCode = new QRCode(qrCodeData);
            //Bitmap qr = qrCode.GetGraphic(150);
            //image_qrcoder.Source = Convert(qr);
            // В таком случае QR-Code не считывается и не изменяется
        }
        public Dialog(Product product)
        {
            InitializeComponent();
            Product = product;
            DataContext = Product;

            //QRCodeGenerator qrGenerator = new();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(tb_id.Text, QRCodeGenerator.ECCLevel.Q);
            //QRCode qrCode = new QRCode(qrCodeData);
            //Bitmap qr = qrCode.GetGraphic(150);
            //image_qrcoder.Source = Convert(qr);
            //Проблема решится сразу же после того, как QR-Code появится в первом окне WPF

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btn_qrcode_Click(object sender, RoutedEventArgs e)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(tb_id.Text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qr = qrCode.GetGraphic(150);
            image_qrcoder.Source = Convert(qr);

        }
       
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
    }
}
