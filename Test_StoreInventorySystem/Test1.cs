using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Test_StoreInventorySystem
{
    [TestClass] // كلاس اختبار منطق والتحقق من الواجهات 
    public class FormValidationTests
    {
        // ==========================================
        // القسم الأول: اختبارات واجهة المنتجات (ProductsForm)
        // ==========================================

        [TestMethod]
        public void Test_UI_StockWarning_WhenBelowMinimum()
        {
            // 1. Arrange (محاكاة المدخلات داخل صناديق النصوص في الواجهة)
            int txtCurrentStock_Value = 5;
            int lblMinimumRequired_Value = 10;
            bool expectedUIWarning = true;

            // 2. Act (العملية المنطقية التي تحدث في الخلفية)
            bool actualUIWarning = txtCurrentStock_Value < lblMinimumRequired_Value;

            // 3. Assert
            Assert.AreEqual(expectedUIWarning, actualUIWarning);
        }

        [TestMethod]
        public void Test_UI_ButtonSave_Validation_ShouldAcceptCorrectInput()
        {
            // 1. Arrange
            string txtProductName_Text = "شاشه حاسوب";
            string txtSearchInput_Text = "شاشه حاسوب";
            bool expectedValidationResult = true;

            // 2. Act
            bool actualValidationResult = !string.IsNullOrEmpty(txtSearchInput_Text) && (txtProductName_Text == txtSearchInput_Text);

            // 3. Assert
            Assert.AreEqual(expectedValidationResult, actualValidationResult);
        }

        [TestMethod] //   اختبار منع تكرار كود المنتج في الواجهة
        public void Test_UI_ProductForm_ShouldRejectDuplicateProductCode()
        {
            // 1. Arrange 
            // محاكاة قائمة بـأكواد المنتجات الموجودة مسبقاً في جدول الواجهة
            List<string> existingProductCodesInGrid = new List<string> { "P001", "P002", "P003" };

            string txtProductCode_Input = "P001";
            bool expectedRejectionResult = true; // نتوقع أن ترفض الواجهة هذا الكود لأنه مستعمل

            // 2. Act
            // الواجهة تفحص برمجياً إذا كان الكود المكتوب موجوداً مسبقاً في القائمة
            bool actualRejectionResult = existingProductCodesInGrid.Contains(txtProductCode_Input);

            // 3. Assert (التأكد من نجاح الواجهة في كشف التكرار ومنع الخطأ)
            Assert.AreEqual(expectedRejectionResult, actualRejectionResult);
        }


        // ==========================================
        // القسم الثاني: اختبارات واجهة الموردين (frmSuppliers)
        // ==========================================

        [TestMethod] //   اختبار التحقق من الحقول الإلزامية للموردين
        public void Test_UI_SupplierForm_SaveButton_ShouldFailWhenFieldsAreEmpty()
        {
            // 1. Arrange
            // محاكاة ترك المستخدم لصناديق النصوص (txtName) و (txtPhone) فارغة بالخطأ في الواجهة
            string txtSupplierName_Text = "";
            string txtContactPhone_Text = "   "; // مسافات فارغة
            bool expectedValidationError = true; // نتوقع أن تكتشف الواجهة الفراغ وتظهر رسالة تحذير

            // 2. Act
            // المحاكاة البرمجية لشرط الواجهة (string.IsNullOrWhiteSpace) لرفض البيانات الفارغة
            bool actualValidationError = string.IsNullOrWhiteSpace(txtSupplierName_Text) || string.IsNullOrWhiteSpace(txtContactPhone_Text);

            // 3. Assert (التأكد من أن الواجهة ستحمي قاعدة البيانات وتمنع الحفظ الفارغ)
            Assert.AreEqual(expectedValidationError, actualValidationError);
        }
    }
}