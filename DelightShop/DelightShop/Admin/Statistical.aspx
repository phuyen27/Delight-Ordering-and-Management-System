<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistical.aspx.cs" Inherits="DelightShop.Admin.Statistical" MasterPageFile="~/Admin/sidebar.Master" %>


<asp:Content ContentPlaceHolderID="adminContent" runat="server">
<style>
     .stat-container {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 24px;
        margin-top: 30px;
    }

    .stat-card {
        background: linear-gradient(135deg, #e0f7fa, #ffffff);
        border-left: 6px solid #28a745;
        padding: 24px 20px;
        border-radius: 12px;
        box-shadow: 0 6px 12px rgba(0, 0, 0, 0.08);
        transition: transform 0.25s ease, box-shadow 0.25s ease;
    }

    .stat-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 12px 20px rgba(0, 0, 0, 0.12);
    }

    .stat-title {
        font-size: 18px;
        font-weight: 600;
        color: #444;
        margin-bottom: 8px;
    }

    .stat-value {
        font-size: 30px;
        font-weight: bold;
        color: #28a745;
    }

    .chart-container {
        margin-top: 50px;
        background-color: #fff;
        padding: 30px 20px;
        border-radius: 12px;
        box-shadow: 0 6px 12px rgba(0, 0, 0, 0.08);
    }

    canvas {
        width: 100% !important;
        height: auto !important;
        max-height: 400px;
    }

    h1 , .chart-title{
        font-size: 28px;
        color: #2e7d32;
        margin-bottom: 20px;
        font-weight: 700;
        display: flex;
        align-items: center;
        gap: 8px;
    }
    .chart-section {
        margin-top: 50px;
        padding: 30px 24px;
        border-radius: 16px;
        background: #1e1e2f;
        box-shadow: 0 8px 18px rgba(0, 0, 0, 0.2);
        color: #fff;
        position: relative;
    }

    .chart-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 20px;
        flex-wrap: wrap;
        gap: 10px;
    }

    
    .download-btn {
        background: linear-gradient(135deg, #4CAF50, #66BB6A);
        color: #fff;
        border: none;
        padding: 10px 16px;
        border-radius: 8px;
        cursor: pointer;
        font-weight: 500;
        font-size: 14px;
        transition: all 0.3s ease;
    }

    .download-btn:hover {
        background: linear-gradient(135deg, #388e3c, #4CAF50);
        transform: scale(1.05);
    }

    canvas {
        width: 100% !important;
        max-height: 450px;
    }

    .down-list {
        display:flex;
        align-items:center;
        justify-content:space-around;
        margin:25px 0;
        
    }
</style>
    <main class="thongKe">
          <div class="head-title">
             
          </div>
        <h1>📊 Báo cáo thống kê</h1>

        <div class="stat-container">
            <div class="stat-card">
                <div class="stat-title">Tổng đơn hàng</div>
                <div class="stat-value"><%= totalOrders %></div>
            </div>

            <div class="stat-card">
                <div class="stat-title">Tổng doanh thu</div>
                <div class="stat-value"><%= totalRevenue.ToString("N0") %> ₫</div>
            </div>

            <div class="stat-card">
                <div class="stat-title">Sản phẩm đã bán</div>
                <div class="stat-value"><%= totalProductsSold %></div>
            </div>

            <div class="stat-card">
                <div class="stat-title">Khách hàng</div>
                <div class="stat-value"><%= totalCustomers %></div>
            </div>

            <div class="stat-card">
                <div class="stat-title">Nhân viên</div>
                <div class="stat-value"><%= totalEmployee %></div>
            </div>

            <div class="stat-card">
                <div class="stat-title">Nhà cung cấp</div>
                <div class="stat-value"><%= totalSupplier %></div>
            </div>
        </div>

        <div class="down-list">
            <asp:Button ID="btnExportCustomerPdf" runat="server" Text="📤 Xuất danh sách khách hàng" CssClass="download-btn" OnClick="btnExportCustomerPdf_Click" />
            <asp:Button ID="btnExportOrder" runat="server" Text="📤 Xuất danh sách đơn hàng" CssClass="download-btn" OnClick="btnExportOrder_Click" />
            <asp:Button ID="btnExportReceipt" runat="server" Text="📤 Xuất danh phiếu nhập" CssClass="download-btn" OnClick="btnExportReceipt_Click" />
            <asp:Button ID="btnExportEmployee" runat="server" Text="📤 Xuất danh sách nhân viên" CssClass="download-btn" OnClick="btnExportEmployee_Click" />

        </div>
    
        <div class="chart-header">
            <div class="chart-title">📈 Doanh thu theo tháng</div>
            <button class="download-btn" onclick="downloadChart()">📥 Tải biểu đồ</button>
        </div>
        <canvas id="revenueChart"></canvas>


    </main>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        const ctx = document.getElementById('revenueChart').getContext('2d');
        const revenueChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: <%= monthLabelsJson %>,
                datasets: [{
                    label: 'Doanh thu',
                    data: <%= revenueDataJson %>,
                    backgroundColor: 'rgba(0, 123, 255, 0.2)',
                    borderColor: '#007bff',
                    borderWidth: 2,
                    fill: true,
                    tension: 0.4
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            callback: function(value) {
                                return value.toLocaleString() + " ₫";
                            }
                        }
                    }
                }
            }
        });
        function downloadChart() {
            const link = document.createElement('a');
            link.download = 'doanh_thu_thang.png';
            link.href = document.getElementById('revenueChart').toDataURL('image/png');
            link.click();
        }
    </script>
</asp:Content>
