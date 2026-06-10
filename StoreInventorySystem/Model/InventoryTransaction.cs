using System;
using System.Collections.Generic;

namespace StoreInventorySystem.Model
{
    //يحتوي على تفاصيل كل حركة في المخزن ويطبق الواجهة لتسجيل الحركات
    public class InventoryTransaction : ITransactionLogger
    {
        //الخصائص لتخزين تفاصيل الحركة مثل التاريخ، اسم المنتج، كمية التغيير، نوع الحركة، وملاحظات إضافية
        public DateTime TransactionDate { get; set; }
        public string ProductName { get; set; }
        public int QuantityChanged { get; set; }
        public string TransactionType { get; set; }
        public string Notes { get; set; }
        //كبسلة البيانات : لحفظ سلامة البيانات وعدم السماح بالتعديل المباشر من خارج الكلاس
        private List<InventoryTransaction> _transactions = new List<InventoryTransaction>();

        //  مشيد افتراضي لاستخدامه لتسجيل الحركات وادارتها (Logger) داخل المخزن
        public InventoryTransaction() { }

        //  مشيد لإنشاء كائن معاملة فردية لتقليل تكرار الكود
        public InventoryTransaction(string productName, int quantityChanged, string transactionType, string notes = "")
        {
            TransactionDate = DateTime.Now;
            ProductName = productName;
            QuantityChanged = quantityChanged;
            TransactionType = transactionType;
            Notes = notes;
        }
        //تطبيق الواجهة لتسجيل الحركات، حيث يتم إضافة كل حركة جديدة إلى القائمة الخاصة بالحركات
        public void LogTransaction(string productName, int quantityChanged, string transactionType, string notes = "")
        {
            _transactions.Add(new InventoryTransaction(productName, quantityChanged, transactionType, notes));
        }

        public IReadOnlyList<InventoryTransaction> GetAllTransactions()
        {
            return _transactions.AsReadOnly(); // حماية القائمة  (Encapsulation)
        }
        //دالة لنتسيق المخرجات
        public string GetTransactionDetails()
        {
            return $"[{TransactionDate}] - المنتج: {ProductName}, التغيير: {QuantityChanged}, النوع: {TransactionType}, ملاحظات: {Notes}";
        }
    }
}