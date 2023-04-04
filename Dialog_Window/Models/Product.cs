using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dialog_Window.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Guid ID { get; set; }
        public string Description { get; set; }

        public ImageSource QRCode { get; set; }


    }
}
