﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dialog_Window.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Guid ID { get; set; }
        public string Description { get; set; }

        public string QRCode { get; set; }

        
    }
}
