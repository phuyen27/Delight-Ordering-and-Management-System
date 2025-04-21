<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerManagement.aspx.cs" Inherits="DelightShop.Admin.CustomerManagement" MasterPageFile="~/Admin/sidebar.Master"%>

<asp:Content ContentPlaceHolderID="adminContent" Runat="server">
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const rows = document.querySelectorAll('.customer-row');
            rows.forEach(row => {
                row.addEventListener('click', function () {
                    document.getElementById('<%= CustomerID.ClientID %>').value = row.getAttribute('data-id');
            document.getElementById('<%= HoKH.ClientID %>').value = row.getAttribute('data-name').split(' ')[0];
            document.getElementById('<%= TenKH.ClientID %>').value = row.getAttribute('data-name').split(' ')[1];
            document.getElementById('<%= SDTKH.ClientID %>').value = row.getAttribute('data-phone');
            document.getElementById('<%= EmailKH.ClientID %>').value = row.getAttribute('data-email');
            document.getElementById('<%= NgaySinhKH.ClientID %>').value = row.getAttribute('data-birth');
            document.getElementById('<%= DiaChi.ClientID %>').value = row.getAttribute('data-address');

            // Cập nhật giá trị giới tính
            var gender = row.getAttribute('data-gender');
            console.log(gender); // Check the gender value
            var genderSelect = document.getElementById('<%= GioiTinh.ClientID %>');
            genderSelect.value = gender; // Cập nhật giá trị vào select
        });
    });
        });

    </script>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Lấy số liệu từ các label
        var totalFemale = parseInt(document.getElementById('<%= lblTotalFemale.ClientID %>').innerText) || 0;
        var totalMale = parseInt(document.getElementById('<%= lblTotalMale.ClientID %>').innerText) || 0;

        var ctx = document.getElementById('genderChart').getContext('2d');
        var genderChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ['Female', 'Male'],  
                datasets: [{
                    label: 'Customer Gender Distribution',
                    data: [totalFemale, totalMale],  // Dữ liệu số khách nữ và nam
                    backgroundColor: ['#FFB6C1', '#ADD8E6'],  // Màu sắc cho từng nhóm
                    borderColor: ['#FF69B4', '#1E90FF'],  // Viền cho các nhóm
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top'
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                return tooltipItem.label + ': ' + tooltipItem.raw + ' customers';
                            }
                        }
                    }
                }
            }
        });
    });
    </script>

        <main>

            <div class="head-title">
	            <div class="left">
		            <h1>Quản lý khách hàng</h1>
		           
	            </div>
            </div>

                <ul class="box-info">
			        <div>
                        <li>
				            <i class='bx bx-female'></i>
				            <span class="text">
					            <h3><asp:Label ID="lblTotalFemale" runat="server"></asp:Label></h3>
					            <p>Khách nữ</p>
				            </span>
                        </li>
                        <li>
				            <i class='bx bx-male'></i>
				            <span class="text">
						            <h3><asp:Label ID="lblTotalMale" runat="server"></asp:Label></h3>
					            <p>Khách nam</p>
				            </span>
                        </li>
			        </div>	
                
                    <div class="chart-container">
                          <canvas id="genderChart" width="400" height="400"></canvas>
                    </div>
			    </ul>
                
			    <!-- Customer Table -->
			    <div class="table-data">
				    <div class="customer_list">
                         <h3>Danh sách khách hàng</h3>
					    <div class="head_customer">						   
<asp:TextBox 
    ID="txtTimKiemKH" 
    runat="server" 
    CssClass="search_customer" 
    OnTextChanged="txtTimKiemKH_TextChanged" 
    AutoPostBack="true" />
						    <i class='bx bx-search'></i>
						    <i class='bx bx-filter'></i>
					    </div>
					    <table>
						    <thead>
							    <tr>
								    <th>Khách hàng</th>
								    <th>Email</th>
								    <th>Ngày sinh</th>
							    </tr>
						    </thead>
						    <tbody>
                                <% foreach (var customer in customers) { %>
                                    <tr class="customer-row" data-id="<%: customer.CustomerID %>"
                                        data-name="<%: customer.Name %>"
                                        data-email="<%: customer.Username %>"
                                        data-phone="<%: customer.Phone %>"
                                        data-address="<%: customer.Address %>"
                                        data-birth="<%: DateTime.Parse(customer.Date).ToString("yyyy-MM-dd") %>"
                                        data-gender="<%: customer.Gender %>">
                                        <td>
                                            <img class="img_user" src="<%: customer.avt %>" alt="User Avatar">
                                            <p><%: customer.Name %></p>
                                        </td>
                                        <td><%: customer.Username %></td>
                                        <td><%: DateTime.Parse(customer.Date).ToString("MM-dd-yyyy") %></td>
                                        <td style="display:none;"><%: customer.CustomerID %></td>
                                    </tr>
                                <% } %>
                            </tbody>


					    </table>
				    </div>

                    <div class="customer">
                        <div class="head">
                            <h3>Thêm khách hàng</h3>
                        </div>
                        <div id="customerForm" class="form_customer">
                            <label for="HoKH">Họ khách:</label>
                            <input type="text" id="HoKH" name="HoKH" required runat="server">

                            <label for="TenKH">Tên khách:</label>
                            <input type="text" id="TenKH" name="TenKH" required runat="server">

                            <label for="SDTKH">Số điện thoại:</label>
                            <input type="text" id="SDTKH" name="SDTKH" required runat="server">

                            <label for="EmailKH">Email:</label>
                            <input type="email" id="EmailKH" name="EmailKH" required runat="server">

                            <label for="NgaySinhKH">Ngày sinh:</label>
                            <input type="date" id="NgaySinhKH" name="NgaySinhKH" required runat="server">

                            <label for="GioiTinh">Giới tính:</label>
                            <select id="GioiTinh" name="GioiTinh" required runat="server">
                                <option value="Male">Nam</option>
                                <option value="Female">Nữ</option>
                                <option value="Other">Khác</option>
                            </select>

                            <label for="DiaChi">Địa chỉ:</label>
                            <input type="text" id="DiaChi" name="DiaChi" required runat="server">
    
                            <asp:HiddenField ID="CustomerID" runat="server" />

                            <div class="buttons">
                                <asp:Button ID="btnEdit" runat="server" CssClass="submit-btn" OnClick="btnEdit_Click" Text="Edit" />
                                <asp:Button ID="btnAdd" runat="server" CssClass="submit-btn" OnClick="btnAdd_Click" Text="Add" />
                                <asp:Button ID="btnDelete" runat="server" CssClass="submit-btn" OnClick="btnDelete_Click" Text="Delete" />
                            </div>

                        </div>
                    </div>
			    </div>




		    </main>
    <style>
.customer {
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    width: 400px;
    margin: 0 ;
}

.head_customer{
    text-align:right;
    margin:5px 0 10px 0;
}

.search_customer{
    border-radius:10px;
    padding:5px;
}

.customer .head_customer h3 {
    font-size: 24px;
    margin-bottom: 20px;
    text-align: center;
    color: #333;
}

.form_customer {
    display: flex;
    flex-direction: column;
}

.form_customer label {
    margin-bottom: 8px;
    font-weight: bold;
    color: #555;
}

.form_customer input,
.form_customer select {
    padding: 12px;
    margin-bottom: 15px;
    border: 1px solid #ccc;
    border-radius: 8px;
    font-size: 14px;
    outline: none;
}

.form_customer input:focus,
.form_customer select:focus {
    border-color: #4CAF50;
}

.form_customer .buttons {
    display: flex;
    justify-content: space-between;
}

.form_customer button {
    padding: 10px 20px;
    font-size: 16px;
    color: #fff;
    background-color: #4CAF50;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    transition: background-color 0.3s;
}

.form_customer button:hover {
    background-color: #45a049;
}

.form_customer .submit-btn:disabled {
    background-color: #ccc;
    cursor: not-allowed;
}

.form_customer .buttons button:nth-child(2) {
    background-color: #2196F3;
}

.form_customer .buttons button:nth-child(3) {
    background-color: #f44336;
}

.form_customer .buttons button:nth-child(2):hover {
    background-color: #1976D2;
}

.form_customer .buttons button:nth-child(3):hover {
    background-color: #e53935;
}
.img_user {
    width:40px;
}

table tbody tr {
    border-bottom: 1px solid #ddd; /* Thêm viền dưới để ngăn cách giữa các hàng */
}

table tbody td {
    padding: 5px;
}

table tbody tr:nth-child(even) {
    background-color: #f9f9f9; /* Thêm màu nền nhẹ cho các dòng chẵn để dễ phân biệt */
}

table tbody tr:nth-child(odd) {
    background-color: #ffffff; /* Màu nền cho các dòng lẻ */
}

table tbody tr:hover {
    background-color: #f1f1f1; /* Thêm hiệu ứng hover cho dòng khi di chuột qua */
}

.customer_list {
    max-height:800px;
    overflow-y: auto;
}

.chart-container {
    max-width: 300px;
    margin: 20px auto;
    padding: 20px;
    background-color: #f8f9fa;
    border-radius: 10px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.box-info {
    align-items:center;
}

.box-info div li {
    margin: 25px 0;
}

.table-data {
    justify-content:center;
}
	</style>
</asp:Content>
