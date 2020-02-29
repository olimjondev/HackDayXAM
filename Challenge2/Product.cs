using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge2
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
    }

    public class ProductView : Product
    {
        public DateTime TimeStamp { get; set; }
    }
}
