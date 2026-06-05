using System;

namespace StoreInventorySystem.Model
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Product(string name, int quantity, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("اسم المنتج لا يمكن أن يكون فارغاً.");

            if (quantity < 0)
                throw new ArgumentException("الكمية لا يمكن أن تكون سالبة.");

            if (price < 0)
                throw new ArgumentException("السعر لا يمكن أن يكون سالباً.");

            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public string GetDetails()
        {
            return $"المنتج: {Name}, الكمية: {Quantity}, السعر: {Price} دينار";
        }

        public decimal CalculateTotalValue()
        {
            return Quantity * Price;
        }
    }
}