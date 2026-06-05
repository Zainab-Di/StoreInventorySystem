using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreInventorySystem.Model;
using System;
using System.Collections.Generic;

namespace StoreInventorySystem.Test
{
    public class FakeTransactionLogger : ITransactionLogger
    {
        public List<InventoryTransaction> LoggedTransactions { get; } = new List<InventoryTransaction>();

        public void LogTransaction(string productName, int quantityChanged, string transactionType, string notes = "")
        {
            LoggedTransactions.Add(new InventoryTransaction(productName, quantityChanged, transactionType, notes));
        }

        public IReadOnlyList<InventoryTransaction> GetAllTransactions()
        {
            return LoggedTransactions.AsReadOnly();
        }
    }


    [TestClass]
    public class InventoryTests
    {
        private FakeTransactionLogger _fakeLogger;
        private Inventory _inventory;

        [TestInitialize]
        public void Setup()
        {
            _fakeLogger = new FakeTransactionLogger();
            _inventory = new Inventory(_fakeLogger);
        }

        [TestMethod]
        public void TestAddProduct_ShouldAddSuccessfully()
        {
            // Arrange
            Product p = new Product("Laptop", 5, 1000m);

            // Act
            _inventory.AddProduct(p);

            // Assert
            Assert.AreEqual(1, _inventory.GetAllProducts().Count);
            Assert.AreEqual("Laptop", _inventory.GetAllProducts()[0].Name);

            // تأكيد تسجيل الحركة من خلال اللوجر المطابق
            Assert.AreEqual(1, _fakeLogger.LoggedTransactions.Count);
            Assert.AreEqual("إضافة", _fakeLogger.LoggedTransactions[0].TransactionType);
        }

        [TestMethod]
        public void TestAddProduct_NegativeQuantity_ShouldThrowException()
        {
            try
            {
                Product p = new Product("Keyboard", -5, 100m);
                _inventory.AddProduct(p);

                Assert.Fail("كان من المتوقع حدوث ArgumentException بسبب الكمية السالبة.");
            }
            catch (ArgumentException)
            {
                // نجاح الفحص
            }
        }

        [TestMethod]
        public void TestRemoveProduct_ShouldRemoveSuccessfully()
        {
            // Arrange
            Product p = new Product("Mouse", 10, 50m);
            _inventory.AddProduct(p);

            // Act
            _inventory.RemoveProduct("Mouse");

            // Assert
            Assert.AreEqual(0, _inventory.GetAllProducts().Count);
            Assert.AreEqual("حذف", _fakeLogger.LoggedTransactions[1].TransactionType);
        }

        [TestMethod]
        public void TestRemoveProduct_NotFound_ShouldThrowException()
        {
            try
            {
                _inventory.RemoveProduct("NotExist");
                Assert.Fail("كان من المتوقع حدوث ArgumentException.");
            }
            catch (ArgumentException)
            {
                // نجاح
            }
        }

        [TestMethod]
        public void TestUpdateQuantity_ShouldUpdateSuccessfully()
        {
            // Arrange
            Product p = new Product("Tablet", 3, 500m);
            _inventory.AddProduct(p);

            // Act
            _inventory.UpdateQuantity("Tablet", 10);

            // Assert
            Assert.AreEqual(10, _inventory.GetAllProducts()[0].Quantity);
            Assert.AreEqual("تحديث كمية", _fakeLogger.LoggedTransactions[1].TransactionType);
        }

        [TestMethod]
        public void TestSearchProduct_ShouldReturnProduct()
        {
            // Arrange
            Product p = new Product("Camera", 4, 1200m);
            _inventory.AddProduct(p);

            // Act
            var result = _inventory.SearchProduct("Camera");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Camera", result.Name);
        }

        [TestMethod]
        public void TestSearchProduct_NotFound_ShouldReturnNull()
        {
            var result = _inventory.SearchProduct("NotExist");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestCalculateTotalInventoryValue_ShouldReturnCorrectValue()
        {
            // Arrange
            _inventory.AddProduct(new Product("Item1", 2, 100m));
            _inventory.AddProduct(new Product("Item2", 3, 200m));

            // Act
            decimal totalValue = _inventory.CalculateTotalInventoryValue();

            // Assert
            Assert.AreEqual(800m, totalValue);
        }
    }
}