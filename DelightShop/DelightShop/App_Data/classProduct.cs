using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DelightShop
{
    public class classProduct
    {
        private static string connectionString = "Server=UYENBABY2K4\\SQLEXPRESS;Database=DelightManager;Integrated Security=True;";

        // Hàm lấy danh sách sản phẩm
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaSP, MaLoaiSP, TenSP, DonGia, XuatXu, MoTa, HinhAnh, SLTon FROM SanPham";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = Convert.ToInt32(reader["MaSP"]),
                        CategoryId = Convert.ToInt32(reader["MaLoaiSP"]),
                        Name = reader["TenSP"].ToString(),
                        Price = Convert.ToDecimal(reader["DonGia"]),
                        Origin = reader["XuatXu"].ToString(),
                        Description = reader["MoTa"].ToString(),
                        Img = reader["HinhAnh"].ToString(),
                        QuantityInStock = Convert.ToInt32(reader["SLTon"])
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }

        //Hàm thêm sản phẩm
        public static void AddProduct(int MaLoaiSP, string TenSP, decimal donGia, string XuatXu, string MoTa, string HinhAnh, int SLTon)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO SanPham (MaLoaiSP, TenSP, DonGia, XuatXu, MoTa, HinhAnh, SLTon) " +
                               "VALUES (@MaLoaiSP, @TenSP, @DonGia, @XuatXu, @MoTa, @HinhAnh, @SLTon)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaLoaiSP", MaLoaiSP);
                cmd.Parameters.AddWithValue("@TenSP", TenSP);
                cmd.Parameters.AddWithValue("@DonGia", donGia);
                cmd.Parameters.AddWithValue("@XuatXu", XuatXu);
                cmd.Parameters.AddWithValue("@MoTa", MoTa);
                cmd.Parameters.AddWithValue("@HinhAnh", HinhAnh);
                cmd.Parameters.AddWithValue("@SLTon", SLTon);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //hàm sửa sản phẩm
        public static void UpdateProduct(int maSP,int MaLoaiSP, string TenSP, decimal donGia, string XuatXu, string MoTa, int SLTon)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE SanPham " +
                               "SET MaLoaiSP = @MaLoaiSP, TenSP = @TenSP, DonGia = @DonGia, " +
                               "XuatXu = @XuatXu, MoTa = @MoTa,  SLTon = @SLTon " +
                               "WHERE MaSP = @ProductId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                cmd.Parameters.AddWithValue("@MaLoaiSP", MaLoaiSP);
                cmd.Parameters.AddWithValue("@TenSP", TenSP);
                cmd.Parameters.AddWithValue("@DonGia", donGia);
                cmd.Parameters.AddWithValue("@XuatXu", XuatXu);
                cmd.Parameters.AddWithValue("@MoTa", MoTa);
                cmd.Parameters.AddWithValue("@SLTon", SLTon);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //SẮP XẾP
        //Sắp xếp theo số lượng bán chạy
        public static List<Product> GetProductsBySales()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Câu truy vấn SQL để lấy sản phẩm và sắp xếp theo số lượng bán được
                string query = @"
            SELECT sp.MaSP, sp.MaLoaiSP, sp.TenSP, sp.DonGia, sp.XuatXu, sp.MoTa, sp.HinhAnh, sp.SLTon, 
                   COALESCE(SUM(ct.SoLuong), 0) AS TongSoLuongBan
            FROM SanPham sp
            LEFT JOIN ChiTietDH ct ON sp.MaSP = ct.MaSP
            GROUP BY sp.MaSP, sp.MaLoaiSP, sp.TenSP, sp.DonGia, sp.XuatXu, sp.MoTa, sp.HinhAnh, sp.SLTon
            ORDER BY TongSoLuongBan DESC"; // Sắp xếp theo tổng số lượng bán được

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = Convert.ToInt32(reader["MaSP"]),
                        CategoryId = Convert.ToInt32(reader["MaLoaiSP"]),
                        Name = reader["TenSP"].ToString(),
                        Price = Convert.ToDecimal(reader["DonGia"]),
                        Origin = reader["XuatXu"].ToString(),
                        Description = reader["MoTa"].ToString(),
                        Img = reader["HinhAnh"].ToString(),
                        QuantityInStock = Convert.ToInt32(reader["SLTon"])
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }

        //hàm sắp xếp sản phẩm theo giá
        public static List<Product> GetSortedByPrice(string sortOrder = "asc")
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT sp.MaSP, sp.MaLoaiSP, sp.TenSP, sp.DonGia, sp.XuatXu, sp.MoTa, sp.HinhAnh, sp.SLTon
            FROM SanPham sp
            ORDER BY sp.DonGia ";

                // Thêm điều kiện sắp xếp theo giá tăng hoặc giảm
                if (sortOrder == "asc")
                {
                    query += "ASC";  // Giá tăng dần
                }
                else if (sortOrder == "desc")
                {
                    query += "DESC"; // Giá giảm dần
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = Convert.ToInt32(reader["MaSP"]),
                        CategoryId = Convert.ToInt32(reader["MaLoaiSP"]),
                        Name = reader["TenSP"].ToString(),
                        Price = Convert.ToDecimal(reader["DonGia"]),
                        Origin = reader["XuatXu"].ToString(),
                        Description = reader["MoTa"].ToString(),
                        Img = reader["HinhAnh"].ToString(),
                        QuantityInStock = Convert.ToInt32(reader["SLTon"])
                    };
                    products.Add(product);
                }
                reader.Close();
            }

            return products;
        }


        //sắp xếp sản phẩm theo tên:
        public static List<Product> GetSortedByName(string sortOrder = "AtoZ")
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT sp.MaSP, sp.MaLoaiSP, sp.TenSP, sp.DonGia, sp.XuatXu, sp.MoTa, sp.HinhAnh, sp.SLTon
            FROM SanPham sp
            ORDER BY sp.TenSP ";

                // Thêm điều kiện sắp xếp theo tên từ A đến Z hoặc Z đến A
                if (sortOrder == "AtoZ")
                {
                    query += "ASC"; // Tên sản phẩm từ A đến Z
                }
                else if (sortOrder == "ZtoA")
                {
                    query += "DESC"; // Tên sản phẩm từ Z đến A
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = Convert.ToInt32(reader["MaSP"]),
                        CategoryId = Convert.ToInt32(reader["MaLoaiSP"]),
                        Name = reader["TenSP"].ToString(),
                        Price = Convert.ToDecimal(reader["DonGia"]),
                        Origin = reader["XuatXu"].ToString(),
                        Description = reader["MoTa"].ToString(),
                        Img = reader["HinhAnh"].ToString(),
                        QuantityInStock = Convert.ToInt32(reader["SLTon"])
                    };
                    products.Add(product);
                }
                reader.Close();
            }

            return products;
        }

        // Hàm chèn sản phẩm vào giỏ hàng
        public static void InsertCartItem(int customerId, int productId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Kiểm tra xem khách hàng đã có giỏ hàng chưa
                int cartId = GetCartIdByCustomerId(customerId);

                // Nếu không có giỏ hàng, tạo giỏ hàng mới
                if (cartId == 0)
                {
                    string createCartQuery = "INSERT INTO GioHang (MaKH) OUTPUT INSERTED.MaGH VALUES (@CustomerId)";
                    SqlCommand createCartCmd = new SqlCommand(createCartQuery, conn);
                    createCartCmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cartId = (int)createCartCmd.ExecuteScalar(); // Lấy MaGH của giỏ hàng mới
                }

                // Thêm sản phẩm vào giỏ hàng (GioHangChiTiet)
                string addProductQuery = @"
            IF EXISTS (SELECT 1 FROM GioHangChiTiet WHERE MaGH = @CartId AND MaSP = @ProductId)
            BEGIN
                UPDATE GioHangChiTiet SET SoLuong = SoLuong + @Quantity WHERE MaGH = @CartId AND MaSP = @ProductId
            END
            ELSE
            BEGIN
                INSERT INTO GioHangChiTiet (MaGH, MaSP, SoLuong) VALUES (@CartId, @ProductId, @Quantity)
            END";

                SqlCommand addProductCmd = new SqlCommand(addProductQuery, conn);
                addProductCmd.Parameters.AddWithValue("@CartId", cartId);
                addProductCmd.Parameters.AddWithValue("@ProductId", productId);
                addProductCmd.Parameters.AddWithValue("@Quantity", quantity);

                addProductCmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        // Hàm tìm kiếm ID giỏ hàng của khách hàng dựa trên MaKH
        public static int GetCartIdByCustomerId(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaGH FROM GioHang WHERE MaKH = @CustomerId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();

                return result != null ? Convert.ToInt32(result) : 0; // Nếu không tìm thấy giỏ hàng, trả về 0
            }
        }

        // Hàm lấy ID sản phẩm từ tên sản phẩm
        public static int GetProductIdByName(string productName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaSP FROM SanPham WHERE TenSP = N@ProductName"; // Thêm N trước biến
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductName", productName);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1; // Trả về ID sản phẩm, nếu không tìm thấy thì trả về -1
            }
        }

        // Hàm lấy sản phẩm theo category
        public static List<Product> GetProductsWithCategory(int categoryId)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Cập nhật câu truy vấn để lọc sản phẩm theo MaLoaiSP
                string query = "SELECT MaSP, MaLoaiSP, TenSP, DonGia, XuatXu, MoTa, HinhAnh, SLTon FROM SanPham WHERE MaLoaiSP = @CategoryId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId); // Thêm tham số vào câu truy vấn

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = Convert.ToInt32(reader["MaSP"]),
                        CategoryId = Convert.ToInt32(reader["MaLoaiSP"]),
                        Name = reader["TenSP"].ToString(),
                        Price = Convert.ToDecimal(reader["DonGia"]),
                        Origin = reader["XuatXu"].ToString(),
                        Description = reader["MoTa"].ToString(),
                        Img = reader["HinhAnh"].ToString(),
                        QuantityInStock = Convert.ToInt32(reader["SLTon"])
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }


        //HÀM TÌM KIẾM SẢN PHẨM
        public static List<Product> GetProductsBySearch(string searchQuery)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaSP, MaLoaiSP, TenSP, DonGia, XuatXu, MoTa, HinhAnh, SLTon FROM SanPham WHERE TenSP LIKE N'%' + @SearchQuery + '%'";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchQuery", searchQuery); // Thêm tham số searchQuery vào câu truy vấn

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = Convert.ToInt32(reader["MaSP"]),
                        CategoryId = Convert.ToInt32(reader["MaLoaiSP"]),
                        Name = reader["TenSP"].ToString(),
                        Price = Convert.ToDecimal(reader["DonGia"]),
                        Origin = reader["XuatXu"].ToString(),
                        Description = reader["MoTa"].ToString(),
                        Img = reader["HinhAnh"].ToString(),
                        QuantityInStock = Convert.ToInt32(reader["SLTon"])
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }

        //tìm loại sp
        public static List<category> SearchCategoriesByName(string keyword)
        {
            List<category> categories = new List<category>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaLoaiSP, TenLoaiSP FROM LoaiSP WHERE TenLoaiSP LIKE @keyword";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    category cat = new category()
                    {
                        categoryID = Convert.ToInt32(reader["MaLoaiSP"]),
                        categoryName = reader["TenLoaiSP"].ToString()
                    };
                    categories.Add(cat);
                }

                reader.Close();
            }

            return categories;
        }

        //load tất cả loại sp
        public static List<category> GetCategories()
        {
            List<category> categories = new List<category>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaLoaiSP, TenLoaiSP FROM LoaiSP";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    category cat = new category()
                    {
                        categoryID = Convert.ToInt32(reader["MaLoaiSP"]),
                        categoryName = reader["TenLoaiSP"].ToString()
                    };
                    categories.Add(cat);
                }

                reader.Close();
            }

            return categories;
        }

        // thêm loại sp mới
        public static void AddCategory(category cat)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO LoaiSP (TenLoaiSP) VALUES (@TenLoaiSP)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenLoaiSP", cat.categoryName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        //sửa thông tin loại sp
        public static void UpdateCategory(category cat)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE LoaiSP SET TenLoaiSP = @TenLoaiSP WHERE MaLoaiSP = @MaLoaiSP";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenLoaiSP", cat.categoryName);
                cmd.Parameters.AddWithValue("@MaLoaiSP", cat.categoryID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // lấy mã loại sp
        public static List<int> GetAllCategoryIDs()
        {
            List<int> categoryIDs = new List<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaLoaiSP FROM LoaiSP";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["MaLoaiSP"]);
                    categoryIDs.Add(id);
                }

                reader.Close();
            }

            return categoryIDs;
        }

    }

    // Lớp Product để lưu thông tin sản phẩm
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int QuantityInStock { get; set; }
    }

    public class category
    {
        public int categoryID { get; set;}
        public string categoryName { get; set; }
    }

    //load tất cả loại sp

}