using System.Collections.Generic;

namespace StoreInventorySystem.Model
{
    //تطبيق التجريد 
    public interface ITransactionLogger
    {
        //تسجيل أي حركة تحدث في المخزن
        void LogTransaction(string productName, int quantityChanged, string transactionType, string notes = "");
        // استرجاع كل الحركات المسجلة لعرضها في واجهة المستخدم
        // يمنع التعديل على القائمة من خارج الكلاس يحافظ سلامه البيانات
        IReadOnlyList<InventoryTransaction> GetAllTransactions();
    }
}