using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreInventorySystem.Model;
using System;

namespace StoreInventorySystem.Test
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void TestAddProduct_ShouldAddSuccessfully()
        {
            Inventory inv = new Inventory();
            Product p = new Product("Laptop", 5, 1000);
            inv.AddProduct(p);

            Assert.AreEqual(1, inv.GetAllProducts().Count);
            Assert.AreEqual("Laptop", inv.GetAllProducts()[0].Name);

            // تأكيد أن الحركة سُجلت
            Assert.AreEqual(1, inv.GetAllTransactions().Count);
            Assert.AreEqual("إضافة", inv.GetAllTransactions()[0].TransactionType);
        }

        [TestMethod]
        public void TestAddProduct_NegativeQuantity_ShouldThrowException()
        {
            Inventory inv = new Inventory();
            try
            {
                Product p = new Product("Keyboard", -5, 100);
                inv.AddProduct(p);

                // إذا وصل الكود إلى هذا السطر ولم يحدث خطأ، فالفحص يعتبر فاشلاً
                Assert.Fail("كان من المتوقع حدوث ArgumentException ولكن لم يحدث شيء.");
            }
            catch (ArgumentException)
            {
                // النجاح: الكود قام برمي الخطأ المطلوب بنجاح، الفحص يمر (Pass)
            }
        }

        [TestMethod]
        public void TestRemoveProduct_ShouldRemoveSuccessfully()
        {
            Inventory inv = new Inventory();
            Product p = new Product("Mouse", 10, 50);
            inv.AddProduct(p);
            inv.RemoveProduct("Mouse");

            Assert.AreEqual(0, inv.GetAllProducts().Count);

            // تأكيد أن الحركة سُجلت
            Assert.AreEqual("حذف", inv.GetAllTransactions()[1].TransactionType);
        }

        [TestMethod]
        public void TestRemoveProduct_NotFound_ShouldThrowException()
        {
            Inventory inv = new Inventory();
            try
            {
                inv.RemoveProduct("NotExist");
                Assert.Fail("كان من المتوقع حدوث ArgumentException ولكن لم يحدث شيء.");
            }
            catch (ArgumentException)
            {
                // النجاح
            }
        }

        [TestMethod]
        public void TestUpdateQuantity_ShouldUpdateSuccessfully()
        {
            Inventory inv = new Inventory();
            Product p = new Product("Tablet", 3, 500);
            inv.AddProduct(p);
            inv.UpdateQuantity("Tablet", 10);

            Assert.AreEqual(10, inv.GetAllProducts()[0].Quantity);

            // تأكيد أن الحركة سُجلت
            Assert.AreEqual("تحديث كمية", inv.GetAllTransactions()[1].TransactionType);
            Assert.AreEqual(7, inv.GetAllTransactions()[1].QuantityChanged);
        }

        [TestMethod]
        public void TestUpdateQuantity_Negative_ShouldThrowException()
        {
            Inventory inv = new Inventory();
            Product p = new Product("Phone", 2, 800);
            inv.AddProduct(p);
            try
            {
                inv.UpdateQuantity("Phone", -1);
                Assert.Fail("كان من المتوقع حدوث ArgumentException ولكن لم يحدث شيء.");
            }
            catch (ArgumentException)
            {
                // النجاح
            }
        }

        [TestMethod]
        public void TestSearchProduct_ShouldReturnProduct()
        {
            Inventory inv = new Inventory();
            Product p = new Product("Camera", 4, 1200);
            inv.AddProduct(p);

            var result = inv.SearchProduct("Camera");
            Assert.IsNotNull(result);
            Assert.AreEqual("Camera", result.Name);
        }

        [TestMethod]
        public void TestSearchProduct_NotFound_ShouldReturnNull()
        {
            Inventory inv = new Inventory();
            var result = inv.SearchProduct("NotExist");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestCalculateTotalInventoryValue_ShouldReturnCorrectValue()
        {
            Inventory inv = new Inventory();
            inv.AddProduct(new Product("Item1", 2, 100));
            inv.AddProduct(new Product("Item2", 3, 200));

            decimal totalValue = inv.CalculateTotalInventoryValue();
            Assert.AreEqual(800, totalValue); // (2*100 + 3*200)
        }
        [TestMethod]
        public void TestGetAllTransactions_ShouldReturnAll()
        {
            Inventory inv = new Inventory();
            inv.AddProduct(new Product("Book", 5, 20));
            inv.UpdateQuantity("Book", 10);
            inv.RemoveProduct("Book");

            var transactions = inv.GetAllTransactions();
            Assert.AreEqual(3, transactions.Count);
            Assert.AreEqual("إضافة", transactions[0].TransactionType);
            Assert.AreEqual("تحديث كمية", transactions[1].TransactionType);
            Assert.AreEqual("حذف", transactions[2].TransactionType);
        }
    }
}