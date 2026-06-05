using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreInventorySystem.Model
{
    public class Inventory
    {
        private List<Product> products = new List<Product>();
        private readonly ITransactionLogger _transactionLogger;

        // تطبيق مبدأ DIP: نمرر الانترفيس عبر المشيد (Constructor Injection)
        public Inventory(ITransactionLogger transactionLogger)
        {
            _transactionLogger = transactionLogger ?? throw new ArgumentNullException(nameof(transactionLogger));
        }

        public void AddProduct(Product p)
        {
            if (p == null)
                throw new ArgumentNullException("المنتج لا يمكن أن يكون فارغاً.");

            products.Add(p);
            _transactionLogger.LogTransaction(p.Name, p.Quantity, "إضافة");
        }

        public void RemoveProduct(string name)
        {
            // Refactoring: استدعاء الدالة بدلاً من تكرار سطر الـ LINQ (Lecture 1)
            var product = SearchProduct(name);
            if (product == null)
                throw new ArgumentException("المنتج غير موجود في المخزون.");

            products.Remove(product);
            _transactionLogger.LogTransaction(name, -product.Quantity, "حذف");
        }

        public void UpdateQuantity(string name, int newQuantity)
        {
            // Refactoring: منع تكرار الكود (Code Reuse)
            var product = SearchProduct(name);
            if (product == null)
                throw new ArgumentException("المنتج غير موجود في المخزون.");

            if (newQuantity < 0)
                throw new ArgumentException("الكمية لا يمكن أن تكون سالبة.");

            int diff = newQuantity - product.Quantity;
            product.Quantity = newQuantity;
            _transactionLogger.LogTransaction(name, diff, "تحديث كمية");
        }

        public Product SearchProduct(string name)
        {
            return products.FirstOrDefault(p => p.Name == name);
        }

        public decimal CalculateTotalInventoryValue()
        {
            return products.Sum(p => p.CalculateTotalValue());
        }

        // حماية البيانات (Encapsulation) لمنع واجهة المستخدم من التعديل المباشر
        public IReadOnlyList<Product> GetAllProducts()
        {
            return products.AsReadOnly();
        }

        public IReadOnlyList<InventoryTransaction> GetAllTransactions()
        {
            return _transactionLogger.GetAllTransactions();
        }
    }
}