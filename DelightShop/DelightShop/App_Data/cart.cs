using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace DelightShop
{
    public class cart
    {
        // LỚP GIỎ HÀNG CHI TIẾT
        public class CartItem
        {
            public int CartID { get; set; }
            public string ProductImg { get; set; }
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }


        //Lấy thông tin trong giỏ hàng
        public static List<CartItem> GetCartItems(int customerId)
        {
            List<CartItem> cartItems = new List<CartItem>();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {

                string query = @"
            SELECT 
                GioHang.MaGH, 
                GioHangChiTiet.MaSP, 
                SanPham.TenSP, 
                GioHangChiTiet.SoLuong, 
                SanPham.DonGia, 
                SanPham.HinhAnh 
            FROM 
                GioHang 
            JOIN 
                GioHangChiTiet ON GioHang.MaGH = GioHangChiTiet.MaGH
            JOIN 
                SanPham ON GioHangChiTiet.MaSP = SanPham.MaSP
            WHERE 
                GioHang.MaKH = @customerId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CartItem cartItem = new CartItem
                    {
                        CartID = Convert.ToInt32(reader["MaGH"]),
                        ProductID = Convert.ToInt32(reader["MaSP"]),
                        ProductName = reader["TenSP"].ToString(),
                        Quantity = Convert.ToInt32(reader["SoLuong"]),
                        Price = Convert.ToDecimal(reader["DonGia"]),
                        ProductImg = reader["HinhAnh"].ToString()
                    };
                    cartItems.Add(cartItem);
                }
                reader.Close();
            }

            return cartItems;
        }

        //Xóa sản phẩm khỏi giỏ hàng
        public static void DeleteProductFromCart(int cartId, int productId)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Truy vấn để xóa sản phẩm khỏi giỏ hàng
                string query = "DELETE FROM GioHangChiTiet WHERE MaGH = @cartId AND MaSP = @productId";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm các tham số vào truy vấn SQL
                cmd.Parameters.Add("@cartId", SqlDbType.Int).Value = cartId;
                cmd.Parameters.Add("@productId", SqlDbType.Int).Value = productId;

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Nếu có dòng bị ảnh hưởng (tức là xóa thành công), có thể thông báo hoặc thực hiện thêm hành động
                        Console.WriteLine("Xóa sản phẩm thành công.");
                    }
                    else
                    {
                        // Nếu không có dòng nào bị ảnh hưởng, có thể là sản phẩm không tồn tại trong giỏ hàng
                        Console.WriteLine("Không tìm thấy sản phẩm trong giỏ hàng.");
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ nếu có lỗi trong quá trình thực thi
                    Console.WriteLine("Có lỗi khi xóa sản phẩm: " + ex.Message);
                }
                finally
                {
                    // Đảm bảo đóng kết nối dù có lỗi hay không
                    conn.Close();
                }
            }
        }

    }
}