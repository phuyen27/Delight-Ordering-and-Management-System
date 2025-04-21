<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DelightShop.Admin.Dashboard" MasterPageFile="~/Admin/sidebar.Master"%>

<asp:Content ContentPlaceHolderID="adminContent" Runat="server">
    <!-- MAIN -->
    <main>
        <div class="head-title">
            <div class="left">
                <h1>Tổng quan</h1>
            </div>
           
        </div>

        <ul class="box-info">
            <li>
                <i class='bx bxs-calendar-check' ></i>
                <span class="text">
                    <h3><asp:Label ID="lblTotalOrders" runat="server"></asp:Label></h3>
                    <p>Đơn hàng</p>
                </span>
            </li>
            <li>
                <i class='bx bxs-group' ></i>
                <span class="text">
                      <h3><asp:Label ID="lblTotalCustomer" runat="server"></asp:Label></h3>
                    <p>Khách hàng</p>
                </span>
            </li>
            <li>
                <i class='bx bxs-dollar-circle' ></i>
                <span class="text">
                    <h3><asp:Label ID="lblTotal" runat="server"></asp:Label></h3>
                    <p>Doanh thu</p>
                </span>
            </li>
        </ul>

        <div class="table-data">
            <div class="order">
                <div class="head">
                    <h3>Đơn hàng gần đây</h3>
                    <i class='bx bx-search' ></i>
                    <i class='bx bx-filter' ></i>
                </div>
                <table>
                    <thead>
                        <tr>
                            <th>Khách hàng</th>
                            <th>Ngày đặt</th>
                            <th>Trạng thái</th>
                        </tr>
                    </thead>
                    <tbody>
                       <% 
                                foreach (var item in orderItems) { 
                                    string statusClass = "completed";  // Mặc định là "completed"
        
                                    // Đổi lớp CSS theo trạng thái
                                    if (item.Status == "Processing")
                                    {
                                        statusClass = "process"; // Nếu trạng thái là "processing", dùng lớp "process"
                                    }
                                    else if (item.Status == "Pending")
                                    {
                                        statusClass = "pending"; // Nếu trạng thái là "pending", dùng lớp "pending"
                                    }
                                %>
                                <tr>
                                    <td>
                                        <img src="<%: item.userAVT %>" alt="User Avatar">
                                        <p><%: item.orderID %></p>
                                    </td>
                                    <td><%: DateTime.Parse(item.orderDate).ToString("dd-MM-yyyy") %></td>
                                    <!-- Thay đổi class của span theo status -->
                                    <td><span class="status <%: statusClass %>"><%: item.Status %></span></td>
                                </tr>
                            <% 
                                } 
                            %>
                    </tbody>
                </table>
            </div>
           
            <div class="todo">
                <div class="head">
                    <h3>Ghi chú</h3>
                    <i class='bx bx-plus' id="addTodoBtn"></i>
                </div>
                <ul class="todo-list" id="todoList">
                    <!-- Các ghi chú sẽ được thêm ở đây -->
                </ul>
            </div>

        </div>
    </main>
    <style>
.todo-list li:hover {
    background-color: #e2e6ea;
}

.todo-list li.done {
    text-decoration: line-through;
    background-color: #d4edda;
    color: #155724;
}

.todo-list i.delete-icon {
    color: red;
    cursor: pointer;
}

.todo-list i.check-icon {
    color: green;
    margin-right: 10px;
    cursor: pointer;
}

    </style>
    <script>
    const todoList = document.getElementById("todoList");
    const addBtn = document.getElementById("addTodoBtn");

    // Load todo từ localStorage
    window.onload = function () {
        const savedTodos = JSON.parse(localStorage.getItem("adminTodos")) || [];
        savedTodos.forEach(todo => renderTodo(todo.text, todo.done));
    };

    // Thêm mới ghi chú
    addBtn.addEventListener("click", function () {
        const text = prompt("Nhập ghi chú:");
        if (text) {
            renderTodo(text);
            saveTodo(text, false);
        }
    });

    // Hiển thị một ghi chú
    function renderTodo(text, done = false) {
        const li = document.createElement("li");
        if (done) li.classList.add("done");

        const p = document.createElement("p");
        p.textContent = text;

        const checkIcon = document.createElement("i");
        checkIcon.className = "bx bx-check check-icon";
        checkIcon.title = "Đánh dấu hoàn thành";
        checkIcon.onclick = function () {
            li.classList.toggle("done");
            updateLocalStorage();
        };

        const delIcon = document.createElement("i");
        delIcon.className = "bx bx-trash delete-icon";
        delIcon.title = "Xoá ghi chú";
        delIcon.onclick = function () {
            li.remove();
            updateLocalStorage();
        };

        li.appendChild(p);
        li.appendChild(checkIcon);
        li.appendChild(delIcon);
        todoList.appendChild(li);
    }

    // Lưu mới vào localStorage
    function saveTodo(text, done) {
        const todos = JSON.parse(localStorage.getItem("adminTodos")) || [];
        todos.push({ text: text, done: done });
        localStorage.setItem("adminTodos", JSON.stringify(todos));
    }

    // Cập nhật lại toàn bộ todo trong localStorage
    function updateLocalStorage() {
        const items = todoList.querySelectorAll("li");
        const data = [];
        items.forEach(li => {
            data.push({
                text: li.querySelector("p").textContent,
                done: li.classList.contains("done")
            });
        });
        localStorage.setItem("adminTodos", JSON.stringify(data));
    }
    </script>

</asp:Content>
