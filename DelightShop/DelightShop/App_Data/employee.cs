using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI.WebControls;

namespace DelightShop
{
    public class employee
    {
        public int employeeID {  get; set; }
        public int employeeTypeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string gender { get; set; }
        public DateTime dob { get; set; }

        private static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

        // Thêm nhân viên mới
        public static int InsertEmployee(string firstName, string lastName, string phone, string gender, DateTime dob, int employeeTypeID)
        {
            int newEmployeeID = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"
                        INSERT INTO NhanVien (HoNV, TenNV, SDTNV, GioiTinhNV, NgaySinhNV, MaPhong) 
                        VALUES (@HoNV, @TenNV, @SDTNV, @GioiTinhNV, @NgaySinhNV, @MaPhong);
                        SELECT SCOPE_IDENTITY();"; // Lấy giá trị IDENTITY của MaNV vừa thêm

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.Add("@HoNV", SqlDbType.NVarChar, 30).Value = firstName;
                    cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar, 30).Value = lastName;
                    cmd.Parameters.Add("@SDTNV", SqlDbType.NVarChar, 10).Value = phone;
                    cmd.Parameters.Add("@GioiTinhNV", SqlDbType.NVarChar, 20).Value = gender;
                    cmd.Parameters.Add("@NgaySinhNV", SqlDbType.DateTime).Value = dob;
                    cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = employeeTypeID;

                    conn.Open();
                    newEmployeeID = Convert.ToInt32(cmd.ExecuteScalar() ?? 0); // Thực thi câu lệnh và lấy giá trị IDENTITY
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while inserting the employee.", ex);
            }

            return newEmployeeID; // Trả về mã nhân viên mới
        }

        // Sửa thông tin nhân viên
        public static void UpdateEmployee(int employeeID, string firstName, string lastName, string phone, string gender, DateTime dob, int employeeTypeID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"
                        UPDATE NhanVien 
                        SET HoNV = @HoNV, TenNV = @TenNV, SDTNV = @SDTNV, GioiTinhNV = @GioiTinhNV, 
                            NgaySinhNV = @NgaySinhNV, MaPhong = @MaPhong
                        WHERE MaNV = @MaNV";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.Add("@HoNV", SqlDbType.NVarChar, 30).Value = firstName;
                    cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar, 30).Value = lastName;
                    cmd.Parameters.Add("@SDTNV", SqlDbType.NVarChar, 10).Value = phone;
                    cmd.Parameters.Add("@GioiTinhNV", SqlDbType.NVarChar, 20).Value = gender;
                    cmd.Parameters.Add("@NgaySinhNV", SqlDbType.DateTime).Value = dob;
                    cmd.Parameters.Add("@MaPhong", SqlDbType.Int).Value = employeeTypeID;
                    cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = employeeID;

                    conn.Open();
                    cmd.ExecuteNonQuery(); 
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while updating the employee.", ex);
            }
        }

        // Lấy tất cả nhân viên
        public static List<employee> GetAllEmployees()
        {
            List<employee> employees = new List<employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT MaNV, HoNV, TenNV, SDTNV, GioiTinhNV, NgaySinhNV, MaPhong FROM NhanVien";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employee emp = new employee
                        {
                            employeeID = reader.GetInt32(0),
                            firstName = reader.GetString(1),
                            lastName = reader.GetString(2),
                            phone = reader.GetString(3),
                            gender = reader.GetString(4),
                            dob = reader.GetDateTime(5),
                            employeeTypeID = reader.GetInt32(6)
                        };
                        employees.Add(emp);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while retrieving employees.", ex);
            }

            return employees; 
        }

        //tìm kiếm nv
        public static List<employee> SearchEmployeesByName(string keyword)
        {
            List<employee> employees = new List<employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"SELECT MaNV, HoNV, TenNV, SDTNV, GioiTinhNV, NgaySinhNV, MaPhong
                             FROM NhanVien
                             WHERE HoNV + ' ' + TenNV LIKE @keyword";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        employee emp = new employee
                        {
                            employeeID = reader.GetInt32(0),
                            firstName = reader.GetString(1),
                            lastName = reader.GetString(2),
                            phone = reader.GetString(3),
                            gender = reader.GetString(4),
                            dob = reader.GetDateTime(5),
                            employeeTypeID = reader.GetInt32(6)
                        };
                        employees.Add(emp);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi tìm kiếm nhân viên.", ex);
            }

            return employees;
        }

        public static List<employee> SortEmployees(List<employee> employees, string sortCriteria)
        {
            switch (sortCriteria.ToLower())
            {
                case "sort_name":
                    return employees.OrderBy(e => e.firstName).ToList();
                case "sort_id":
                    return employees.OrderBy(e => e.employeeID).ToList();
                case "sort_date":
                    return employees.OrderBy(e => e.dob).ToList();
                case "sort_department":
                    return employees.OrderBy(e => e.employeeTypeID).ToList();
                default:
                    return employees; // Nếu không có tiêu chí, trả về danh sách gốc
            }
        }

        public static List<int> GetAllEmployeeIDs()
        {
            List<int> employeeIDs = new List<int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "SELECT MaNV FROM NhanVien";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employeeIDs.Add(Convert.ToInt32(reader["MaNV"]));
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while retrieving employee IDs.", ex);
            }

            return employeeIDs;
        }

    }
}