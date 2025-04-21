using iTextSharp.text.pdf;
using iTextSharp.text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace DelightShop.Admin
{
    public partial class Statistical : System.Web.UI.Page
    {
        public int totalOrders = 0;
        public decimal totalRevenue = 0;
        public int totalProductsSold = 0;
        public int totalCustomers = 0;
        public int totalEmployee = 0;
        public int totalSupplier = 0;

        public string monthLabelsJson;
        public string revenueDataJson;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStatistics();
                LoadMonthlyRevenueChart();
            }
        }

        private void LoadStatistics()
        {
            totalOrders = admin.GetCount("SELECT COUNT(*) FROM DonDH");
            totalRevenue = admin.GetDecimal("SELECT ISNULL(SUM(TongTien), 0) FROM DonDH");
            totalProductsSold = admin.GetCount("SELECT SUM(SoLuong) FROM ChiTietDH");
            totalCustomers = admin.GetCount("SELECT COUNT(*) FROM KhachHang");
            totalEmployee = admin.GetCount("SELECT COUNT(*) FROM NhanVien");
            totalSupplier= admin.GetCount("SELECT COUNT(*) FROM NhaCungCap");
        }

        private void LoadMonthlyRevenueChart()
        {
            var revenueData = admin.GetMonthlyRevenue(); // Dictionary<string, decimal>
            monthLabelsJson = JsonConvert.SerializeObject(revenueData.Keys);
            revenueDataJson = JsonConvert.SerializeObject(revenueData.Values);
        }

        protected void btnExportCustomerPdf_Click(object sender, EventArgs e)
        {
            DataTable dt = GetCustomerData();

            if (dt.Rows.Count > 0)
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter.GetInstance(pdfDoc, memoryStream).CloseStream = false;

                pdfDoc.Open();

                // 👑 Load font hỗ trợ Unicode
                string fontPath = Server.MapPath("~/Fonts/TIMES.TTF");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font shopNameFont = new Font(baseFont, 20, Font.BOLD, new BaseColor(0, 102, 204));
                Font reportTitleFont = new Font(baseFont, 16, Font.BOLD, BaseColor.DARK_GRAY);
                Font timeFont = new Font(baseFont, 10, Font.ITALIC, BaseColor.GRAY);
                Font cellFont = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);
                Font headerFont = new Font(baseFont, 12, Font.BOLD, BaseColor.WHITE);

                // 🖼️ Logo bên trái + tiêu đề ở giữa
                PdfPTable headerTable = new PdfPTable(3);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 1f, 3f, 1f }); // tỉ lệ giữa logo / tiêu đề / khoảng trắng

                // Cột 1: Logo
                string logoPath = Server.MapPath("~/assets/img/logoShop.png");
                PdfPCell logoCell;
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(60f, 60f);
                    logoCell = new PdfPCell(logo)
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                }
                else
                {
                    logoCell = new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER };
                }
                headerTable.AddCell(logoCell);

                // Cột 2: Tiêu đề trung tâm
                PdfPCell titleCell = new PdfPCell(new Phrase("CỬA HÀNG DELIGHT", shopNameFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                headerTable.AddCell(titleCell);

                // Cột 3: khoảng trắng (hoặc bạn có thể thêm info khác)
                PdfPCell emptyCell = new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER };
                headerTable.AddCell(emptyCell);

                // Add table lên PDF
                pdfDoc.Add(headerTable);

                // Ngày giờ
                Paragraph dateTime = new Paragraph("Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), timeFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(dateTime);

                // 📄 Tiêu đề báo cáo
                Paragraph reportTitle = new Paragraph("Danh sách khách hàng", reportTitleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(reportTitle);

                // 🔲 Tạo bảng
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                table.WidthPercentage = 100;
                table.SpacingBefore = 10f;
                table.SpacingAfter = 20f;

                // Header bảng
                foreach (DataColumn column in dt.Columns)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, headerFont))
                    {
                        BackgroundColor = new BaseColor(52, 152, 219),
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    table.AddCell(headerCell);
                }

                // Dữ liệu bảng
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        PdfPCell dataCell = new PdfPCell(new Phrase(item.ToString(), cellFont))
                        {
                            Padding = 5
                        };
                        table.AddCell(dataCell);
                    }
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                // Xuất file
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=BaoCao_KhachHang.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }

        private DataTable GetCustomerData()
        {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaKH, HoKH,TenKH, EmailKH, SDTKH, DiaChi FROM KhachHang", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        protected void btnExportOrder_Click(object sender, EventArgs e)
        {
            // Lấy danh sách đơn hàng từ DB
            DataTable dt = GetOrderData();

            if (dt.Rows.Count > 0)
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter.GetInstance(pdfDoc, memoryStream).CloseStream = false;

                pdfDoc.Open();

                // 👑 Load font hỗ trợ Unicode
                string fontPath = Server.MapPath("~/Fonts/TIMES.TTF");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font shopNameFont = new Font(baseFont, 20, Font.BOLD, new BaseColor(0, 102, 204));
                Font reportTitleFont = new Font(baseFont, 16, Font.BOLD, BaseColor.DARK_GRAY);
                Font timeFont = new Font(baseFont, 10, Font.ITALIC, BaseColor.GRAY);
                Font cellFont = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);
                Font headerFont = new Font(baseFont, 12, Font.BOLD, BaseColor.WHITE);

                // 🖼️ Logo bên trái + tiêu đề ở giữa
                PdfPTable headerTable = new PdfPTable(3);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 1f, 3f, 1f }); // tỉ lệ giữa logo / tiêu đề / khoảng trắng

                // Cột 1: Logo
                string logoPath = Server.MapPath("~/assets/img/logoShop.png");
                PdfPCell logoCell;
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(60f, 60f);
                    logoCell = new PdfPCell(logo)
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                }
                else
                {
                    logoCell = new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER };
                }
                headerTable.AddCell(logoCell);

                // Cột 2: Tiêu đề trung tâm
                PdfPCell titleCell = new PdfPCell(new Phrase("CỬA HÀNG DELIGHT", shopNameFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                headerTable.AddCell(titleCell);

                // Cột 3: khoảng trắng (hoặc bạn có thể thêm info khác)
                PdfPCell emptyCell = new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER };
                headerTable.AddCell(emptyCell);

                // Add table lên PDF
                pdfDoc.Add(headerTable);

                // Ngày giờ
                Paragraph dateTime = new Paragraph("Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), timeFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(dateTime);

                // 📄 Tiêu đề báo cáo
                Paragraph reportTitle = new Paragraph("BÁO CÁO ĐƠN HÀNG", reportTitleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(reportTitle);

                // 🔲 Tạo bảng
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                table.WidthPercentage = 100;
                table.SpacingBefore = 10f;
                table.SpacingAfter = 20f;

                // Header bảng
                foreach (DataColumn column in dt.Columns)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, headerFont))
                    {
                        BackgroundColor = new BaseColor(52, 152, 219),
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    table.AddCell(headerCell);
                }

                // Dữ liệu bảng
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        PdfPCell dataCell = new PdfPCell(new Phrase(item.ToString(), cellFont))
                        {
                            Padding = 5
                        };
                        table.AddCell(dataCell);
                    }
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                // Xuất file PDF
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=BaoCao_DonHang.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }

        private DataTable GetOrderData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaDH, MaKH, TongTien, NgayDat, TrangThai FROM DonDH", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        protected void btnExportReceipt_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu phiếu nhập từ DB
            DataTable dtReceipts = GetReceiptData();

            if (dtReceipts.Rows.Count > 0)
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter.GetInstance(pdfDoc, memoryStream).CloseStream = false;

                pdfDoc.Open();

                // 👑 Load font hỗ trợ Unicode
                string fontPath = Server.MapPath("~/Fonts/TIMES.TTF");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font shopNameFont = new Font(baseFont, 20, Font.BOLD, new BaseColor(0, 102, 204));
                Font reportTitleFont = new Font(baseFont, 16, Font.BOLD, BaseColor.DARK_GRAY);
                Font timeFont = new Font(baseFont, 10, Font.ITALIC, BaseColor.GRAY);
                Font cellFont = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);
                Font headerFont = new Font(baseFont, 12, Font.BOLD, BaseColor.WHITE);

                // 🖼️ Logo bên trái + tiêu đề ở giữa
                PdfPTable headerTable = new PdfPTable(3);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 1f, 3f, 1f });

                // Cột 1: Logo
                string logoPath = Server.MapPath("~/assets/img/logoShop.png");
                PdfPCell logoCell;
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(60f, 60f);
                    logoCell = new PdfPCell(logo)
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                }
                else
                {
                    logoCell = new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER };
                }
                headerTable.AddCell(logoCell);

                // Cột 2: Tiêu đề trung tâm
                PdfPCell titleCell = new PdfPCell(new Phrase("CỬA HÀNG DELIGHT", shopNameFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                headerTable.AddCell(titleCell);

                // Cột 3: khoảng trắng
                PdfPCell emptyCell = new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER };
                headerTable.AddCell(emptyCell);

                // Add table lên PDF
                pdfDoc.Add(headerTable);

                // Ngày giờ
                Paragraph dateTime = new Paragraph("Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), timeFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(dateTime);

                // 📄 Tiêu đề báo cáo
                Paragraph reportTitle = new Paragraph("BÁO CÁO PHIẾU NHẬP", reportTitleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(reportTitle);

                // 🔲 Tạo bảng cho phiếu nhập
                PdfPTable table = new PdfPTable(dtReceipts.Columns.Count);
                table.WidthPercentage = 100;
                table.SpacingBefore = 10f;
                table.SpacingAfter = 10f;

                // Header bảng
                foreach (DataColumn column in dtReceipts.Columns)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, headerFont))
                    {
                        BackgroundColor = new BaseColor(52, 152, 219),
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    table.AddCell(headerCell);
                }

                // Dữ liệu bảng
                foreach (DataRow row in dtReceipts.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        PdfPCell dataCell = new PdfPCell(new Phrase(item.ToString(), cellFont))
                        {
                            Padding = 5
                        };
                        table.AddCell(dataCell);
                    }
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                // Xuất file PDF
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=BaoCao_PhieuNhap.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }

        private DataTable GetReceiptData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaPNH, MaNCC, NgayNhap, TongTien FROM PhieuNhapHang", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        protected void btnExportEmployee_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu nhân viên từ DB
            DataTable dtEmployees = GetEmployeeData();

            if (dtEmployees.Rows.Count > 0)
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter.GetInstance(pdfDoc, memoryStream).CloseStream = false;

                pdfDoc.Open();

                // 👑 Load font hỗ trợ Unicode
                string fontPath = Server.MapPath("~/Fonts/TIMES.TTF");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font shopNameFont = new Font(baseFont, 20, Font.BOLD, new BaseColor(0, 102, 204));
                Font reportTitleFont = new Font(baseFont, 16, Font.BOLD, BaseColor.DARK_GRAY);
                Font timeFont = new Font(baseFont, 10, Font.ITALIC, BaseColor.GRAY);
                Font cellFont = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);
                Font headerFont = new Font(baseFont, 12, Font.BOLD, BaseColor.WHITE);

                // 🖼️ Logo bên trái + tiêu đề ở giữa
                PdfPTable headerTable = new PdfPTable(3);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 1f, 3f, 1f });

                // Cột 1: Logo
                string logoPath = Server.MapPath("~/assets/img/logoShop.png");
                PdfPCell logoCell;
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(60f, 60f);
                    logoCell = new PdfPCell(logo)
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                }
                else
                {
                    logoCell = new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER };
                }
                headerTable.AddCell(logoCell);

                // Cột 2: Tiêu đề trung tâm
                PdfPCell titleCell = new PdfPCell(new Phrase("CỬA HÀNG DELIGHT", shopNameFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };
                headerTable.AddCell(titleCell);

                // Cột 3: khoảng trắng
                PdfPCell emptyCell = new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER };
                headerTable.AddCell(emptyCell);

                // Add table lên PDF
                pdfDoc.Add(headerTable);

                // Ngày giờ
                Paragraph dateTime = new Paragraph("Ngày xuất báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), timeFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(dateTime);

                // 📄 Tiêu đề báo cáo
                Paragraph reportTitle = new Paragraph("BÁO CÁO NHÂN VIÊN", reportTitleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                pdfDoc.Add(reportTitle);

                // 🔲 Tạo bảng cho nhân viên
                PdfPTable table = new PdfPTable(dtEmployees.Columns.Count);
                table.WidthPercentage = 100;
                table.SpacingBefore = 10f;
                table.SpacingAfter = 10f;

                // Header bảng
                foreach (DataColumn column in dtEmployees.Columns)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, headerFont))
                    {
                        BackgroundColor = new BaseColor(52, 152, 219),
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    table.AddCell(headerCell);
                }

                // Dữ liệu bảng
                foreach (DataRow row in dtEmployees.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        PdfPCell dataCell = new PdfPCell(new Phrase(item.ToString(), cellFont))
                        {
                            Padding = 5
                        };
                        table.AddCell(dataCell);
                    }
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                // Xuất file PDF
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=BaoCao_NhanVien.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }

        private DataTable GetEmployeeData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaNV, TenNV, SDTNV, NgaySinhNV, GioiTinhNV FROM NhanVien", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

    }
}