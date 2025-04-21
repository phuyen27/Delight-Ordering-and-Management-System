function countdownToChristmas() {
    // Lấy thời gian hiện tại
    const now = new Date();

    // Lấy thời gian Giáng Sinh (24/12 năm nay)
    const christmas = new Date(now.getFullYear(), 11, 24, 23, 59, 59); // Đặt thời gian là 23:59:59

    // Kiểm tra nếu đã qua Giáng Sinh thì quay lại năm sau
    if (now > christmas) {
        christmas.setFullYear(now.getFullYear() + 1);
    }

    // Tính toán sự chênh lệch thời gian
    const difference = christmas - now;

    // Tính số ngày còn lại
    const days = Math.floor(difference / (1000 * 60 * 60 * 24));

    // Tính số giờ còn lại
    const hours = Math.floor((difference % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));

    // Tính số phút còn lại
    const minutes = Math.floor((difference % (1000 * 60 * 60)) / (1000 * 60));

    // Tính số giây còn lại
    const seconds = Math.floor((difference % (1000 * 60)) / 1000);

    // Cập nhật các phần tử trong HTML với giá trị tính được
    document.getElementById('countdown__day').innerText = days + ' days';
    document.getElementById('countdown__hour').innerText = hours + 'h';
    document.getElementById('countdown__minute').innerText = minutes + 'm';
    document.getElementById('countdown__second').innerText = seconds + 's';
}

// Cập nhật đồng hồ đếm ngược mỗi giây
setInterval(countdownToChristmas, 1000);

// Gọi ngay hàm để cập nhật ngay khi trang tải
countdownToChristmas();

// Hàm thay đổi màu cho các phần tử countdown__time mỗi 3 giây
function changeCountdownColor() {
    const colors = ['#FF0000', '#00FF00', '#0000FF', '#FFFF00', '#FF00FF', '#00FFFF']; // Danh sách màu sắc
    let currentColorIndex = 0; // Chỉ số màu hiện tại

    // Hàm thay đổi màu sắc của các phần tử
    setInterval(() => {
        // Lấy tất cả các phần tử h3 trong countdown__time
        const countdownItems = document.querySelectorAll('.countdown__time h3');
        
        // Lặp qua các phần tử và thay đổi màu sắc
        countdownItems.forEach(item => {
            item.style.color = colors[currentColorIndex];
        });

        // Cập nhật chỉ số màu tiếp theo, sau khi hết danh sách thì quay lại đầu
        currentColorIndex = (currentColorIndex + 1) % colors.length;
    }, 1000); // Thay đổi mỗi 3 giây
}

// Gọi hàm để thay đổi màu sắc ngay khi trang tải
changeCountdownColor();



/*=============== SCROLL SECTIONS ACTIVE LINK ===============*/
const sections = document.querySelectorAll('section[id]')

const scrollActive = () => {
    const scrollDown = window.scrollY

    sections.forEach (current => {
        const sectionHeight = current.offsetHeight,
              sectionTop = current.offsetTop -58,
              sectionId = current.getAttribute('id'),
              sectionsClass = document.querySelector('.nav__menu a[href*='+sectionId+']')
        if(scrollDown >sectionTop && scrollDown <= sectionTop+sectionHeight){
            sectionsClass.classList.add('active-link')
        }
        else {
            sectionsClass.classList.remove('active-link')
        }
     })
          
}
window.addEventListener('scroll',scrollActive)





// Chuyển slide tự động mỗi 5 giây
setInterval(moveToNextSlide, 5000);


const navLinks = document.querySelectorAll('.slider-nav a');

navLinks.forEach((link, index) => {
    link.addEventListener('click', (e) => {
        e.preventDefault();
        currentSlide = index;
        slider.scrollTo({
            left: slides[currentSlide].offsetLeft,
            behavior: 'smooth'
        });
    });
});

