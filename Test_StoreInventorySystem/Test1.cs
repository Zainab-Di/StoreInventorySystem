  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using System;

    namespace Test_StoreInventorySystem
    {
  

        [TestClass] // وسم من المنهج يعرّف الفيجوال ستوديو أن هذا كلاس اختبار
        public class ProductTests
        {
            [TestMethod] // وسم يعرّف أن هذه دالة اختبار محددة (Page 12)
            public void Test_CheckQuantityLimit()
            {
                // تطبيق قاعدة الـ AAA المشهورة في التست (Arrange, Act, Assert) حسب المنهج

                // 1. Arrange (تجهيز البيانات والنتيجة المتوقعة)
                int currentQuantity = 5;       // الكمية الحالية في المخزن
                int minimumRequired = 10;      // الحد الأدنى المطلوب للمنتج
                bool expectedResult = true;    // نتوقع أن تكون النتيجة True (الكمية ناقصة تحت الحد)

                // 2. Act (تنفيذ العملية المنطقية المراد اختبارها)
                bool actualResult = currentQuantity < minimumRequired;

                // 3. Assert (التحقق الفعلي بمطابقة النتيجة المتوقعة مع الحقيقية)
                Assert.AreEqual(expectedResult, actualResult);
            }

        [TestMethod] // وسم دالة الاختبار الثانية من المنهج
        public void Test_ProductExistsAndIsNotEmpty()
        {
            // 1. Arrange (تجهيز بيانات الاختبار)
            string productNameFromDatabase = "شاشه حاسوب";
            string searchInput = "شاشه حاسوب";
            bool expectedResult = true; // نتوقع أن يجد النظام تطابقاً كلياً

            // 2. Act (العملية المنطقية المراد اختبارها)
            // نتحقق أن الخانة ليست فارغة وأن اسم المنتج يطابق المدخلات تماماً
            bool actualResult = !string.IsNullOrEmpty(searchInput) && (productNameFromDatabase == searchInput);

            // 3. Assert (التحقق الفعلي بمطابقة النتيجة)
            Assert.AreEqual(expectedResult, actualResult);
        }

    }

    }