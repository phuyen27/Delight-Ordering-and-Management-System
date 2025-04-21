using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DelightShop
{
    public class admin
    {
        private static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public static double GetDataFunction(string query)
        {
            double total = 0;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                object result = command.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    total = Convert.ToDouble(result); // Chuyển đổi kết quả về kiểu double
                }
            }

            return total;
        }


        //Xóa dữ liệu
        public static void DeleteItems(int itemID,string query,string itemSQL)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
    //            string query = "DELETE FROM KhachHang WHERE MaKH = @MaKH";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm tham số để tránh SQL Injection
                cmd.Parameters.AddWithValue(itemSQL, itemID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        //lấy mã:
        public static List<int> GetIDs(string query, string variable)
        {
            List<int> receiptIDs = new List<int>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    receiptIDs.Add(Convert.ToInt32(reader[variable]));
                }

                reader.Close();
            }

            return receiptIDs;
        }


        public static int GetCount(string query)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static decimal GetDecimal(string query)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public static Dictionary<string, decimal> GetMonthlyRevenue()
        {
            Dictionary<string, decimal> revenueByMonth = new Dictionary<string, decimal>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            SELECT 
                FORMAT(NgayDat, 'MM/yyyy') AS ThangNam,
                SUM(TongTien) AS DoanhThu
            FROM DonDH
            GROUP BY FORMAT(NgayDat, 'MM/yyyy')
            ORDER BY MIN(NgayDat)
        ";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string month = reader["ThangNam"].ToString();
                    decimal revenue = Convert.ToDecimal(reader["DoanhThu"]);
                    revenueByMonth.Add(month, revenue);
                }
            }

            return revenueByMonth;
        }

    }
}