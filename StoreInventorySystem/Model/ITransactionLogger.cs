using System.Collections.Generic;

namespace StoreInventorySystem.Model
{
    public interface ITransactionLogger
    {
        void LogTransaction(string productName, int quantityChanged, string transactionType, string notes = "");
        IReadOnlyList<InventoryTransaction> GetAllTransactions();
    }
}