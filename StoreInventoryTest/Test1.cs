using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StoreInventoryTest
{
    [TestClass] // هذه الإشارة التي تجعل الفيجوال ستوديو يتعرف على الكلاس كملف اختبار
    public class StockMovementTests
    {
        [TestMethod] // هذه الإشارة التي تجعل زر الـ Play يرى الدالة ويختبرها
        public void Test_Inbound_Movement_Logic()
        {
            int currentStock = 10;
            int enteredQty = 4;
            int actualResult = currentStock + enteredQty;

            // التأكد التلقائي أن الحسبة تساوي 14
            Assert.AreEqual(14, actualResult);
        }
    }
}