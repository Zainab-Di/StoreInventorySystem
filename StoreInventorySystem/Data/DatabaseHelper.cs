using System;
using System.Data.SqlClient;

namespace StoreInventoryDBSystem
{
    public class DatabaseHelper
    {
      
        private static readonly string ConnectionString =
            @"Server=.;Database=StoreInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";
        //إنشاء وارجاع كائن الاتصال بقاعدة البيانات يمكن إعادة استخدامه في أي مكان في المشروع
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        // دالة لفحص الاتصال يمكن إعادة استخدامها في أي مكان في المشروع
        public static bool CheckConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }
    }
}