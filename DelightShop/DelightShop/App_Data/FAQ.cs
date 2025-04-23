using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace DelightShop
{
    public class FAQ
    {
        // Khai báo model đại diện cho từng dòng trong bảng DanhGia
        public class FAQItem
        {
            public int MaDG { get; set; }
            public int MaDH { get; set; }
            public string Comment { get; set; }
            public DateTime DateFAQ { get; set; }
        }

        private static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        //tìm kiếm:
        // Tìm kiếm theo comment (giống kiểu %keyword%)
        public static List<FAQItem> SearchFAQByComment(string keyword)
        {
            List<FAQItem> list = new List<FAQItem>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT MaDG, MaDH, Comment, DateFAQ FROM DanhGia WHERE Comment LIKE @keyword";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FAQItem item = new FAQItem
                    {
                        MaDG = Convert.ToInt32(reader["MaDG"]),
                        MaDH = Convert.ToInt32(reader["MaDH"]),
                        Comment = reader["Comment"].ToString(),
                        DateFAQ = Convert.ToDateTime(reader["DateFAQ"])
                    };
                    list.Add(item);
                }

                reader.Close();
            }

            return list;
        }


        // Lấy tất cả đánh giá
        public static List<FAQItem> GetAllFAQ()
        {
            List<FAQItem> list = new List<FAQItem>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT MaDG, MaDH, Comment, DateFAQ FROM DanhGia";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FAQItem item = new FAQItem
                    {
                        MaDG = Convert.ToInt32(reader["MaDG"]),
                        MaDH = Convert.ToInt32(reader["MaDH"]),
                        Comment = reader["Comment"].ToString(),
                        DateFAQ = Convert.ToDateTime(reader["DateFAQ"])
                    };
                    list.Add(item);
                }

                reader.Close();
            }

            return list;
        }

        // Thêm đánh giá mới
        public static bool InsertFAQ(FAQItem faq)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                INSERT INTO DanhGia (MaDH, Comment)
                VALUES (@MaDH, @Comment)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDH", faq.MaDH);
                cmd.Parameters.AddWithValue("@Comment", faq.Comment);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Cập nhật đánh giá
        public static bool UpdateFAQ(FAQItem faq)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                UPDATE DanhGia
                SET MaDH = @MaDH, Comment = @Comment, DateFAQ = GETDATE()
                WHERE MaDG = @MaDG";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDH", faq.MaDH);
                cmd.Parameters.AddWithValue("@Comment", faq.Comment);
                cmd.Parameters.AddWithValue("@MaDG", faq.MaDG);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Xóa đánh giá
        public static bool DeleteFAQ(int faqId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM DanhGia WHERE MaDG = @MaDG";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDG", faqId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
