using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DelightShop
{  
    public class Customer
    {
       
        public int CustomerID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }

        public string Name { get; set; }
        public string avt { get; set; }

        public string Gender { get; set; }

        private static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;


        // Phương thức lấy thông tin khách hàng từ cơ sở dữ liệu
        public static Customer GetCustomer(string username, string password)
        {
            Customer customer = null;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT HinhAnh,MaKH,GioiTinh, SDTKH, MatKhau, HoKH,TenKH, EmailKH, NgaySinhKH,DiaChi FROM KhachHang WHERE EmailKH = @username AND MatKhau = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customer = new Customer()
                        {
                            CustomerID = Convert.ToInt32(reader["MaKH"]),
                            Username = reader["EmailKH"].ToString(),
                            Password = reader["MatKhau"].ToString(),
                            Address = reader["DiaChi"].ToString(),
                            Phone = reader["SDTKH"].ToString(),
                            Date = reader["NgaySinhKH"].ToString(),
                            avt = reader["HinhAnh"].ToString(),
                            Name = reader["TenKH"].ToString() +" "+ reader["HoKH"].ToString(),
                            Gender = reader["GioiTinh"].ToString()
                        };
                    }
                }
                reader.Close();
            }

            return customer;
        }


        //Lấy tất cả khách hàng trong csdl
        public static List<Customer> GetCustomerAll()
        {
            List<Customer> customers = new List<Customer>(); // Khởi tạo danh sách khách hàng

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT HinhAnh, MaKH, SDTKH, MatKhau, HoKH, TenKH, EmailKH, NgaySinhKH, DiaChi FROM KhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Thêm khách hàng vào danh sách
                        customers.Add(new Customer()
                        {
                            CustomerID = Convert.ToInt32(reader["MaKH"]),
                            Username = reader["EmailKH"].ToString(),
                            Password = reader["MatKhau"].ToString(),
                            Address = reader["DiaChi"].ToString(),
                            Phone = reader["SDTKH"].ToString(),
                            Date = reader["NgaySinhKH"].ToString(),
                            avt = reader["HinhAnh"].ToString(),
                            Name = reader["TenKH"].ToString() + " " + reader["HoKH"].ToString()
                        });
                    }
                }
                reader.Close();
            }

            return customers; // Trả về danh sách khách hàng
        }


        //ĐĂNG KÝ
        public static void InsertCustomer(string firstname, string lastname, string dob, string email, string phonenumber, string address, string password, string avt, string gender)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
        INSERT INTO KhachHang (HoKH, TenKH, SDTKH, EmailKH, NgaySinhKH, DiaChi, MatKhau, HinhAnh,GioiTinh) 
        VALUES (@HoKH, @TenKH, @SDTKH, @EmailKH, @NgaySinhKH, @DiaChi, @MatKhau, @HinhAnh, @GioiTinh)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm các tham số để tránh SQL Injection
                cmd.Parameters.AddWithValue("@HoKH", firstname);
                cmd.Parameters.AddWithValue("@TenKH", lastname);
                cmd.Parameters.AddWithValue("@SDTKH", phonenumber);
                cmd.Parameters.AddWithValue("@EmailKH", email);
                cmd.Parameters.AddWithValue("@NgaySinhKH", DateTime.Parse(dob));
                cmd.Parameters.AddWithValue("@DiaChi", address);
                cmd.Parameters.AddWithValue("@MatKhau", password);
                cmd.Parameters.AddWithValue("@HinhAnh", avt);
                cmd.Parameters.AddWithValue("@GioiTinh", gender);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        // cập nhật mật khẩu (CHỨC NĂNG QUÊN MẬT KHẨU)
        public static bool IsEmailAndPhoneExist(string email, string phone)
        {
            bool exists = false;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT COUNT(1) FROM KhachHang WHERE EmailKH = @Email AND SDTKH = @Phone";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                exists = count > 0;  // If count is greater than 0, it means the email and phone number exist
                conn.Close();
            }

            return exists;
        }




        public static void UpdateCustomer(int customerId, string firstname, string lastname, string dob, string email, string phonenumber, string address, string gender)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"
UPDATE KhachHang 
SET HoKH = @HoKH, 
    TenKH = @TenKH, 
    SDTKH = @SDTKH, 
    EmailKH = @EmailKH, 
    NgaySinhKH = @NgaySinhKH, 
    DiaChi = @DiaChi,
GioiTinh = @GioiTinh
WHERE MaKH = @MaKH";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Thêm các tham số để tránh SQL Injection
                    cmd.Parameters.AddWithValue("@MaKH", customerId);
                    cmd.Parameters.AddWithValue("@HoKH", firstname);
                    cmd.Parameters.AddWithValue("@TenKH", lastname);
                    cmd.Parameters.AddWithValue("@SDTKH", phonenumber);
                    cmd.Parameters.AddWithValue("@EmailKH", email);
                    cmd.Parameters.AddWithValue("@NgaySinhKH", DateTime.Parse(dob));  // Kiểm tra xem ngày có đúng định dạng không
                    cmd.Parameters.AddWithValue("@DiaChi", address);
                    cmd.Parameters.AddWithValue("@GioiTinh", gender);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();  // Lấy số dòng bị ảnh hưởng

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Không tìm thấy khách hàng để cập nhật.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lỗi hoặc hiển thị thông báo lỗi chi tiết
                // Có thể ghi lỗi vào file log hoặc thông báo lỗi chi tiết ra màn hình
                Console.WriteLine($"Lỗi khi cập nhật khách hàng: {ex.Message}");
                throw;  // Ném lại lỗi để xử lý ở nơi gọi nếu cần
            }
        }

     


        //Thêm dữ liệu cho bảng ThongTinThanhToan
        public static void InsertThongTinThanhToan(int maDH, string tenNN, string sdtNN, string diaChiNH, DateTime ngayThanhToan, string hinhThucTT, string tinhTrang)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                INSERT INTO ThongTinThanhToan (MaDH, TenNN, SDTNN, DiaChiNH, NgayThanhToan, HinhThucTT, TinhTrang) 
                VALUES (@MaDH, @TenNN, @SDTNN, @DiaChiNH, @NgayThanhToan, @HinhThucTT, @TinhTrang)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm các tham số để tránh SQL Injection
                cmd.Parameters.AddWithValue("@MaDH", maDH);
                cmd.Parameters.AddWithValue("@TenNN", tenNN);
                cmd.Parameters.AddWithValue("@SDTNN", sdtNN);
                cmd.Parameters.AddWithValue("@DiaChiNH", diaChiNH);
                cmd.Parameters.AddWithValue("@NgayThanhToan", ngayThanhToan);
                cmd.Parameters.AddWithValue("@HinhThucTT", hinhThucTT);
                cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        //Thêm dữ liệu bảng FAQ
        public static void InsertFAQ(int maKH, string comment)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                INSERT INTO FAQ (MaKH, comment, dateFAQ) 
                VALUES (@MaKH, @Comment, @DateFAQ)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm các tham số để tránh SQL Injection
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                cmd.Parameters.AddWithValue("@Comment", comment);
                cmd.Parameters.AddWithValue("@DateFAQ", DateTime.Now);  // Insert the current date and time

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static List<Customer> GetCustomersByNameSearch(string searchQuery)
        {
            List<Customer> customers = new List<Customer>(); // Khởi tạo danh sách khách hàng

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Câu truy vấn tìm kiếm chỉ dựa vào tên (HoKH và TenKH)
                string query = @"
            SELECT HinhAnh, MaKH, SDTKH, MatKhau, HoKH, TenKH, EmailKH, NgaySinhKH, DiaChi
            FROM KhachHang
            WHERE HoKH LIKE N'%' + @SearchQuery + '%' OR TenKH LIKE N'%' + @SearchQuery + '%'
        ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchQuery", searchQuery); // Thêm tham số searchQuery vào câu truy vấn

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Thêm khách hàng vào danh sách
                        customers.Add(new Customer()
                        {
                            CustomerID = Convert.ToInt32(reader["MaKH"]),
                            Username = reader["EmailKH"].ToString(),
                            Password = reader["MatKhau"].ToString(),
                            Address = reader["DiaChi"].ToString(),
                            Phone = reader["SDTKH"].ToString(),
                            Date = reader["NgaySinhKH"].ToString(),
                            avt = reader["HinhAnh"].ToString(),
                            Name = reader["TenKH"].ToString() + " " + reader["HoKH"].ToString()
                        });
                    }
                }
                reader.Close();
            }

            return customers; // Trả về danh sách khách hàng
        }



        //Lấy dữ liệu FAQ:
        public static List<FAQ> GetFAQs()
        {
            List<FAQ> faqs = new List<FAQ>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT KhachHang.HinhAnh, MaFAQ, comment, dateFAQ FROM FAQ " +
                               "JOIN KhachHang ON KhachHang.MaKH = FAQ.MaKH"; 

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FAQ faq = new FAQ()
                    {
                        FAQId = Convert.ToInt32(reader["MaFAQ"]),  // MaFAQ ở cột thứ 1
                        avtCustomer = reader["HinhAnh"].ToString(), // Hình ảnh từ bảng KhachHang
                        comment = reader["comment"].ToString(),
                        dateFAQ = reader["dateFAQ"].ToString() // dateFAQ ở cột thứ 4
                    };
                    faqs.Add(faq);
                }

                conn.Close();
            }

            return faqs;
        }


        //LỚP THÔNG TIN THANH TOÁN
        public class Payment
        {
            public string NameReceiver { get; set; }
            public string AddressReceiver { get; set; }
            public string phoneReceiver { get; set; }
            public string DatePayment { get; set; }
            public string TypePayment { get; set; }
        }

        //LỚP FAQ
        public class FAQ
        {
            public int FAQId { get; set; }
            public string avtCustomer {  get; set; }
            public string dateFAQ { get; set; }
            public string comment { get;set; }
        }
    }
}