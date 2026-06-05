using System;
using System.Collections.Generic;

namespace StoreInventorySystem.Model
{
    public class InventoryTransaction : ITransactionLogger
    {
        public DateTime TransactionDate { get; set; }
        public string ProductName { get; set; }
        public int QuantityChanged { get; set; }
        public string TransactionType { get; set; }
        public string Notes { get; set; }

        private List<InventoryTransaction> _transactions = new List<InventoryTransaction>();

        // مشيد افتراضي لاستخدامه كليدجر (Logger) داخل المخزن
        public InventoryTransaction() { }

        // مشيد لإنشاء كائن معاملة فردية
        public InventoryTransaction(string productName, int quantityChanged, string transactionType, string notes = "")
        {
            TransactionDate = DateTime.Now;
            ProductName = productName;
            QuantityChanged = quantityChanged;
            TransactionType = transactionType;
            Notes = notes;
        }

        public void LogTransaction(string productName, int quantityChanged, string transactionType, string notes = "")
        {
            _transactions.Add(new InventoryTransaction(productName, quantityChanged, transactionType, notes));
        }

        public IReadOnlyList<InventoryTransaction> GetAllTransactions()
        {
            return _transactions.AsReadOnly(); // حماية القائمة وفقاً للمحاضرة 3 (Encapsulation)
        }

        public string GetTransactionDetails()
        {
            return $"[{TransactionDate}] - المنتج: {ProductName}, التغيير: {QuantityChanged}, النوع: {TransactionType}, ملاحظات: {Notes}";
        }
    }
}