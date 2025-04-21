using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DelightShop
{
    public class DepartmentsAndSuppliers
    {
        private static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        public class Department
        {
            public int departmentID { get; set; }
            public string departmentName { get; set; }
            public DateTime departmentDate { get; set; }
            public int departmentQuantity { get; set; }
        }

        public class Supplier
        {
            public int supplierID { get; set; }
            public string supplierName { get; set; }
            public string supplierAddress { get; set; }
            public string supplierEmail { get; set; }
            public string supplierPhone { get; set; }
            public string supplierWebsite { get; set; }
        }

        // Lấy danh sách tất cả phòng ban
        public static List<Department> getAllDepartments()
        {
            List<Department> departments = new List<Department>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT MaPhong, TenPhong, NamTL, SoLuongNV FROM PhongBan";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Department department = new Department()
                    {
                        departmentID = Convert.ToInt32(reader["MaPhong"]),
                        departmentName = reader["TenPhong"].ToString(),
                        departmentDate = Convert.ToDateTime(reader["NamTL"]),
                        departmentQuantity = Convert.ToInt32(reader["SoLuongNV"])
                    };
                    departments.Add(department);
                }
            }
            return departments;
        }

        // Thêm mới một phòng ban
        public static bool insertDepartment(string name, DateTime establishedDate, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO PhongBan (TenPhong, NamTL, SoLuongNV) VALUES (@TenPhong, @NamTL, @SoLuongNV)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TenPhong", name);
                cmd.Parameters.AddWithValue("@NamTL", establishedDate);
                cmd.Parameters.AddWithValue("@SoLuongNV", quantity);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        // Cập nhật phòng ban
        public static bool updateDepartment(int departmentID, string name, DateTime establishedDate, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE PhongBan SET TenPhong = @TenPhong, NamTL = @NamTL, SoLuongNV = @SoLuongNV WHERE MaPhong = @MaPhong";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TenPhong", name);
                cmd.Parameters.AddWithValue("@NamTL", establishedDate);
                cmd.Parameters.AddWithValue("@SoLuongNV", quantity);
                cmd.Parameters.AddWithValue("@MaPhong", departmentID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        // Xóa phòng ban
        public static bool deleteDepartment(int departmentID)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM PhongBan WHERE MaPhong = @MaPhong";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhong", departmentID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        // Lấy tất cả nhà cung cấp (Supplier)
        public static List<Supplier> getAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT MaNCC, TenNCC, DiaChi, SDTNCC, EmailNCC, WebsiteNCC FROM NhaCungCap";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Supplier supplier = new Supplier()
                    {
                        supplierID = Convert.ToInt32(reader["MaNCC"]),
                        supplierName = reader["TenNCC"].ToString(),
                        supplierAddress = reader["DiaChi"].ToString(),
                        supplierPhone = reader["SDTNCC"].ToString(),
                        supplierEmail = reader["EmailNCC"].ToString(),
                        supplierWebsite = reader["WebsiteNCC"].ToString()
                    };
                    suppliers.Add(supplier);
                }
            }
            return suppliers;
        }

        // Thêm mới nhà cung cấp
        public static bool insertSupplier(string name, string address, string phone, string email, string website)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO NhaCungCap (TenNCC, DiaChi, SDTNCC, EmailNCC, WebsiteNCC) VALUES (@TenNCC, @DiaChi, @SDTNCC, @EmailNCC, @WebsiteNCC)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TenNCC", name);
                cmd.Parameters.AddWithValue("@DiaChi", address);
                cmd.Parameters.AddWithValue("@SDTNCC", phone);
                cmd.Parameters.AddWithValue("@EmailNCC", email);
                cmd.Parameters.AddWithValue("@WebsiteNCC", website);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        // Cập nhật nhà cung cấp
        public static bool updateSupplier(int supplierID, string name, string address, string phone, string email, string website)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE NhaCungCap SET TenNCC = @TenNCC, DiaChi = @DiaChi, SDTNCC = @SDTNCC, EmailNCC = @EmailNCC, WebsiteNCC = @WebsiteNCC WHERE MaNCC = @MaNCC";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TenNCC", name);
                cmd.Parameters.AddWithValue("@DiaChi", address);
                cmd.Parameters.AddWithValue("@SDTNCC", phone);
                cmd.Parameters.AddWithValue("@EmailNCC", email);
                cmd.Parameters.AddWithValue("@WebsiteNCC", website);
                cmd.Parameters.AddWithValue("@MaNCC", supplierID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        // Xóa nhà cung cấp
        public static bool deleteSupplier(int supplierID)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNCC", supplierID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        //lấy mã nhà cung cấp
        public static List<int> GetAllSupplierIDs()
        {
            List<int> supplierIDs = new List<int>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT MaNCC FROM NhaCungCap";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    supplierIDs.Add(Convert.ToInt32(reader["MaNCC"]));
                }

                reader.Close();
            }

            return supplierIDs;
        }

        //tìm kiếm nhà cung cấp
        public static List<Supplier> SearchSuppliersByName(string keyword)
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT MaNCC, TenNCC, DiaChi, SDTNCC, EmailNCC, WebsiteNCC " +
                               "FROM NhaCungCap WHERE TenNCC LIKE @keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Supplier supplier = new Supplier()
                    {
                        supplierID = Convert.ToInt32(reader["MaNCC"]),
                        supplierName = reader["TenNCC"].ToString(),
                        supplierAddress = reader["DiaChi"].ToString(),
                        supplierPhone = reader["SDTNCC"].ToString(),
                        supplierEmail = reader["EmailNCC"].ToString(),
                        supplierWebsite = reader["WebsiteNCC"].ToString()
                    };
                    suppliers.Add(supplier);
                }
            }

            return suppliers;
        }

    }
}
