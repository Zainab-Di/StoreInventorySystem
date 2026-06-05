using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreInventorySystem.Model
{
    public class Inventory
    {
        private List<Product> products = new List<Product>();
        private List<InventoryTransaction> transactions = new List<InventoryTransaction>();

        public void AddProduct(Product p)
        {
            if (p == null)
                throw new ArgumentNullException("المنتج لا يمكن أن يكون فارغاً.");

            products.Add(p);
            transactions.Add(new InventoryTransaction(p.Name, p.Quantity, "إضافة"));
        }

        public void RemoveProduct(string name)
        {
            var product = products.FirstOrDefault(p => p.Name == name);
            if (product == null)
                throw new ArgumentException("المنتج غير موجود في المخزون.");

            products.Remove(product);
            transactions.Add(new InventoryTransaction(name, -product.Quantity, "حذف"));
        }

        public void UpdateQuantity(string name, int newQuantity)
        {
            var product = products.FirstOrDefault(p => p.Name == name);
            if (product == null)
                throw new ArgumentException("المنتج غير موجود في المخزون.");

            if (newQuantity < 0)
                throw new ArgumentException("الكمية لا يمكن أن تكون سالبة.");

            int diff = newQuantity - product.Quantity;
            product.Quantity = newQuantity;
            transactions.Add(new InventoryTransaction(name, diff, "تحديث كمية"));
        }

        public Product SearchProduct(string name)
        {
            return products.FirstOrDefault(p => p.Name == name);
        }

        public decimal CalculateTotalInventoryValue()
        {
            return products.Sum(p => p.CalculateTotalValue());
        }

        public List<Product> GetAllProducts()
        {
            return products;
        }

        public List<InventoryTransaction> GetAllTransactions()
        {
            return transactions;
        }
    }
}