using System;

namespace StoreInventorySystem.Model
{
    public class InventoryTransaction
    {
        public DateTime TransactionDate { get; set; }
        public string ProductName { get; set; }
        public int QuantityChanged { get; set; }
        public string TransactionType { get; set; }
        public string Notes { get; set; }

        public InventoryTransaction(string productName, int quantityChanged, string transactionType, string notes = "")
        {
            TransactionDate = DateTime.Now;
            ProductName = productName;
            QuantityChanged = quantityChanged;
            TransactionType = transactionType;
            Notes = notes;
        }

        public string GetTransactionDetails()
        {
            return $"[{TransactionDate}] - المنتج: {ProductName}, التغيير: {QuantityChanged}, النوع: {TransactionType}, ملاحظات: {Notes}";
        }
    }
}