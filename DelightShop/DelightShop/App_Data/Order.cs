using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DelightShop
{
    public class order
    {
        public int orderID { get; set; }
        public decimal total { get; set; }
        public string orderDate { get; set; }
        public string userAVT { get; set; }
        public string Status { get; set; }
        public int userID { get; set; }

        private static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        //LỚP CHI TIẾT ĐƠN ĐẶT HÀNG
        public class orderDetail
    {
        public int orderID { get;set; }
        public int orderDetailID { get; set; }
        public int productID { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string orderDetailImg { get; set; }
        public string orderDetailName { get; set; }

    }

        public class receipt
        {
            public int receiptID { get; set; }
            public DateTime receiptDate { get; set; }
            public decimal receiptTotalPrice { get; set; }
            public int receiptStaff { get; set; }

            public int supplierID { get; set; }

        }

        public class receiptDetail
        {
            public int receiptDetailID { get; set; }
            public int receiptProductID { get; set; }
            public int productQuantity { get; set; }
            public decimal Price { get; set; }
        }

        //lấy tất cả chi tiết phiếu nhập có cùng mã
        public static List<receiptDetail> GetReceiptDetailsWithID(int receiptId)
        {
            List<receiptDetail> details = new List<receiptDetail>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            SELECT MaPNH, MaSP, SoLuong, DonGiaNhap
            FROM ChiTietPhieuNhap
            WHERE MaPNH = @receiptId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@receiptId", receiptId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    receiptDetail detail = new receiptDetail
                    {
                        receiptDetailID = Convert.ToInt32(reader["MaPNH"]),
                        receiptProductID = Convert.ToInt32(reader["MaSP"]),
                        productQuantity = Convert.ToInt32(reader["SoLuong"]),
                        Price = Convert.ToDecimal(reader["DonGiaNhap"])
                    };
                    details.Add(detail);
                }
                reader.Close();
            }

            return details;
        }
        //lấy toàn bộ ctpn:
        public static List<receiptDetail> GetAllReceiptDetails()
        {
            List<receiptDetail> details = new List<receiptDetail>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
        SELECT MaPNH, MaSP, SoLuong, DonGiaNhap
        FROM ChiTietPhieuNhap";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    receiptDetail detail = new receiptDetail
                    {
                        receiptDetailID = Convert.ToInt32(reader["MaPNH"]),
                        receiptProductID = Convert.ToInt32(reader["MaSP"]),
                        productQuantity = Convert.ToInt32(reader["SoLuong"]),
                        Price = Convert.ToDecimal(reader["DonGiaNhap"])
                    };
                    details.Add(detail);
                }

                reader.Close();
            }

            return details;
        }

        //sửa ctph
        public static bool UpdateReceiptDetail(receiptDetail detail)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            UPDATE ChiTietPhieuNhap
            SET SoLuong = @SoLuong,
                DonGiaNhap = @DonGiaNhap
            WHERE MaPNH = @MaPNH AND MaSP = @MaSP";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPNH", detail.receiptDetailID);
                cmd.Parameters.AddWithValue("@MaSP", detail.receiptProductID);
                cmd.Parameters.AddWithValue("@SoLuong", detail.productQuantity);
                cmd.Parameters.AddWithValue("@DonGiaNhap", detail.Price);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        //xóa:
        public static bool DeleteReceiptDetail(int receiptId, int productId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
        DELETE FROM ChiTietPhieuNhap 
        WHERE MaPNH = @receiptId AND MaSP = @productId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@receiptId", receiptId);
                cmd.Parameters.AddWithValue("@productId", productId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }


        //thêm ctph
        public static bool InsertReceiptDetail(receiptDetail detail)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            INSERT INTO ChiTietPhieuNhap (MaPNH, MaSP, SoLuong, DonGiaNhap)
            VALUES (@MaPNH, @MaSP, @SoLuong, @DonGiaNhap)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPNH", detail.receiptDetailID);
                cmd.Parameters.AddWithValue("@MaSP", detail.receiptProductID);
                cmd.Parameters.AddWithValue("@SoLuong", detail.productQuantity);
                cmd.Parameters.AddWithValue("@DonGiaNhap", detail.Price);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        //lấy tất cả phiếu nhập hàng
        public static List<receipt> getReceipts(int supplierId)
        {
            List<receipt> receipts = new List<receipt>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
        SELECT 
            P.MaPNH, P.NgayNhap, P.TongTien, P.MaNV, P.MaNCC
        FROM 
            PhieuNhapHang P
        WHERE 
            P.MaNCC = @supplierId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@supplierId", supplierId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    receipt receiptItem = new receipt
                    {
                        receiptID = Convert.ToInt32(reader["MaPNH"]),
                        receiptDate = Convert.ToDateTime(reader["NgayNhap"]),
                        receiptTotalPrice = Convert.ToDecimal(reader["TongTien"]),
                        receiptStaff = Convert.ToInt32(reader["MaNV"]),
                        supplierID = Convert.ToInt32(reader["MaNCC"])
                    };
                    receipts.Add(receiptItem);
                }
                reader.Close();
            }

            return receipts;
        }
        
        //thêm phiếu nhập
        public static int InsertReceipt(DateTime receiptDate, decimal totalPrice, int staffId, int supplierId)
        {
            int newReceiptID = 0;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            INSERT INTO PhieuNhapHang (NgayNhap, TongTien, MaNV, MaNCC)
            VALUES (@NgayNhap, @TongTien, @MaNV, @MaNCC);
            SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayNhap", receiptDate);
                cmd.Parameters.AddWithValue("@TongTien", totalPrice);
                cmd.Parameters.AddWithValue("@MaNV", staffId);
                cmd.Parameters.AddWithValue("@MaNCC", supplierId);

                conn.Open();
                newReceiptID = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }

            return newReceiptID;
        }

        // sửa phiếu nhập
        public static bool UpdateReceipt(int receiptID, DateTime receiptDate, decimal totalPrice, int staffId, int supplierId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            UPDATE PhieuNhapHang
            SET NgayNhap = @NgayNhap,
                TongTien = @TongTien,
                MaNV = @MaNV,
                MaNCC = @MaNCC
            WHERE MaPNH = @MaPNH";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayNhap", receiptDate);
                cmd.Parameters.AddWithValue("@TongTien", totalPrice);
                cmd.Parameters.AddWithValue("@MaNV", staffId);
                cmd.Parameters.AddWithValue("@MaNCC", supplierId);
                cmd.Parameters.AddWithValue("@MaPNH", receiptID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public static List<receipt> getReceiptsAll()
        {
            List<receipt> receipts = new List<receipt>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
        SELECT 
            P.MaPNH, P.NgayNhap, P.TongTien, P.MaNV, P.MaNCC
        FROM 
            PhieuNhapHang P";  // Không có WHERE

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    receipt receiptItem = new receipt
                    {
                        receiptID = Convert.ToInt32(reader["MaPNH"]),
                        receiptDate = Convert.ToDateTime(reader["NgayNhap"]),
                        receiptTotalPrice = Convert.ToDecimal(reader["TongTien"]),
                        receiptStaff = Convert.ToInt32(reader["MaNV"]),
                        supplierID = Convert.ToInt32(reader["MaNCC"])
                    };
                    receipts.Add(receiptItem);
                }
                reader.Close();
            }

            return receipts;
        }


        //lấy tất cả chi tiết phiếu nhập
        public static List<receiptDetail> getReceiptDetails(int receiptId)
        {
            List<receiptDetail> receiptDetails = new List<receiptDetail>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
        SELECT 
            C.MaPNH, C.MaSP, C.SoLuong, C.DonGiaNhap
        FROM 
            ChiTietPhieuNhap C
        WHERE 
            C.MaPNH = @receiptId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@receiptId", receiptId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    receiptDetail receiptDetailItem = new receiptDetail
                    {
                        receiptDetailID = Convert.ToInt32(reader["MaPNH"]),  // Assuming it's the same as receipt ID, otherwise adjust
                        receiptProductID = Convert.ToInt32(reader["MaSP"]),
                        productQuantity = Convert.ToInt32(reader["SoLuong"]),
                        Price = Convert.ToDecimal(reader["DonGiaNhap"])
                    };
                    receiptDetails.Add(receiptDetailItem);
                }
                reader.Close();
            }

            return receiptDetails;
        }


        //hiển thị đơn đặt hàng theo mã khách hàng
        public static List<order> getOrder(int customerId)
        {
            List<order> orderItems = new List<order>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {

                string query = @"
            SELECT 
                MaDH,TongTien,NgayDat, MaKH, TrangThai
            FROM 
                DonDH 
            WHERE 
                MaKH = @customerId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    order orderItem = new order
                    {
                        orderID = Convert.ToInt32(reader["MaDH"]),
                        orderDate = reader["NgayDat"].ToString(),
                        total = Convert.ToDecimal(reader["TongTien"]),
                        Status = reader["TrangThai"].ToString(),
                        userID = Convert.ToInt32(reader["MaKH"])

                    };
                    orderItems.Add(orderItem);
                }
                reader.Close();
            }

            return orderItems;
        }

        //lấy tất cả đơn hàng
        public static List<order> getOrderAll()
        {
            List<order> orderItems = new List<order>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {

                string query = @"
            SELECT 
                DonDH.MaDH,
                DonDH.TongTien,
                DonDH.NgayDat,
                KhachHang.HinhAnh,
                TrangThai,
                DonDH.MaKH
            FROM 
                DonDH
            JOIN 
                KhachHang ON DonDH.MaKH = KhachHang.MaKH;";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    order orderItem = new order
                    {
                        orderID = Convert.ToInt32(reader["MaDH"]),
                        orderDate = reader["NgayDat"].ToString(),
                        total = reader["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TongTien"]),
                        userAVT = reader["HinhAnh"].ToString(),
                        Status = reader["TrangThai"].ToString(),
                        userID = Convert.ToInt32(reader["MaKH"])
                    };
                    orderItems.Add(orderItem);
                }
                reader.Close();
            }

            return orderItems;
        }

        //hiển thị đơn đặt hàng chi tiết theo mã đơn đặt hàng
        public static List<orderDetail> getOrderDetail(int orderID)
        {
            List<orderDetail> orderDetails = new List<orderDetail>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
        SELECT 
            MaCTDH, SoLuong, SanPham.HinhAnh, SanPham.TenSP, SanPham.DonGia
        FROM 
            ChiTietDH 
        JOIN
            SanPham on SanPham.MaSP = ChiTietDH.MaSP
        WHERE 
            MaDH = @orderID";  // Đảm bảo đã khai báo @orderID

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@orderID", orderID);  // Khai báo tham số orderID

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orderDetail OrderDetail = new orderDetail
                    {
                        orderDetailID = Convert.ToInt32(reader["MaCTDH"]),
                        quantity = Convert.ToInt32(reader["SoLuong"]),
                        orderDetailImg = reader["HinhAnh"].ToString(),
                        orderDetailName = reader["TenSP"].ToString(),
                        price = Convert.ToDecimal(reader["DonGia"])
                    };
                    orderDetails.Add(OrderDetail);
                }
                reader.Close();
            }

            return orderDetails;
        }

        //Thêm đơn đặt hàng
        public static int InsertDonDH(int customerId, decimal totalAmount, DateTime ngayDat, string Status)
        {
            int newOrderId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"
                INSERT INTO DonDH (MaKH, TongTien, NgayDat, TrangThai) 
                VALUES (@MaKH, @TongTien, @NgayDat, @TrangThai);

                SELECT SCOPE_IDENTITY();"; // Lấy giá trị IDENTITY của MaDH vừa thêm

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = customerId;
                    cmd.Parameters.Add("@TongTien", SqlDbType.Decimal).Value = totalAmount;
                    cmd.Parameters.Add("@NgayDat", SqlDbType.DateTime).Value = ngayDat;
                    cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 20).Value = Status;

                    conn.Open();
                    newOrderId = Convert.ToInt32(cmd.ExecuteScalar() ?? 0); // Thực thi câu lệnh và lấy giá trị IDENTITY
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while inserting the order.", ex);
            }

            return newOrderId; // Trả về mã đơn hàng mới
        }


        // CẬP NHẬT ĐƠN ĐẶT HÀNG
        public static bool UpdateDonDH(int orderId, int customerId, decimal totalAmount, DateTime ngayDat, string status)
        {
            bool isUpdated = false; // Biến kiểm tra xem có cập nhật thành công không
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            UPDATE DonDH 
            SET MaKH = @MaKH, TongTien = @TongTien, NgayDat = @NgayDat, TrangThai = @TrangThai
            WHERE MaDH = @MaDH";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDH", orderId); // Mã đơn hàng cần cập nhật
                cmd.Parameters.AddWithValue("@MaKH", customerId); // Mã khách hàng
                cmd.Parameters.AddWithValue("@TongTien", totalAmount); // Tổng tiền
                cmd.Parameters.AddWithValue("@NgayDat", ngayDat); // Ngày đặt
                cmd.Parameters.AddWithValue("@TrangThai", status); // Trạng thái đơn hàng

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi câu lệnh và trả về số dòng bị ảnh hưởng
                conn.Close();

                // Nếu có ít nhất 1 dòng bị ảnh hưởng, cập nhật thành công
                if (rowsAffected > 0)
                {
                    isUpdated = true;
                }
            }

            return isUpdated; // Trả về true nếu cập nhật thành công, ngược lại trả về false
        }

        //Thêm chi tiết đơn đặt hàng
        public static void InsertChiTietDH(int maDH, int maSP, int soLuong)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    INSERT INTO ChiTietDH (MaDH, MaSP, SoLuong) 
                    VALUES (@MaDH, @MaSP, @SoLuong)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm các tham số để tránh SQL Injection
                cmd.Parameters.AddWithValue("@MaDH", maDH);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        //Lấy tất cả chi tiết đơn đặt hàng
        public static List<orderDetail> GetAllChiTietDH()
        {
            List<orderDetail> orderDetails = new List<orderDetail>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                SELECT 
                    MaCTDH, SoLuong, SanPham.HinhAnh, SanPham.TenSP, SanPham.DonGia, MaDH
                FROM 
                    ChiTietDH 
                JOIN
                    SanPham on SanPham.MaSP = ChiTietDH.MaSP";  

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orderDetail OrderDetail = new orderDetail
                    {
                        orderDetailID = Convert.ToInt32(reader["MaCTDH"]),
                        quantity = Convert.ToInt32(reader["SoLuong"]),
                        orderDetailImg = reader["HinhAnh"].ToString(),
                        orderDetailName = reader["TenSP"].ToString(),
                        price = Convert.ToDecimal(reader["DonGia"]),
                        orderID = Convert.ToInt32(reader["MaDH"])
                    };
                    orderDetails.Add(OrderDetail);
                }
                reader.Close();
            }

            return orderDetails;

        }

        //chỉnh sửa chitietdh
        public static void UpdateChiTietDH(int maCTDH, int soLuong, decimal donGia)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            UPDATE ChiTietDH
            SET SoLuong = @SoLuong, DonGia = @DonGia
            WHERE MaCTDH = @MaCTDH";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm các tham số để tránh SQL Injection
                cmd.Parameters.AddWithValue("@MaCTDH", maCTDH);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@DonGia", donGia);


                // Mở kết nối và thực thi câu lệnh
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        //xóa
        public static void DeleteChiTietDH(int maCTDH)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM ChiTietDH WHERE MaCTDH = @MaCTDH";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@MaCTDH", maCTDH);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}