document.addEventListener("DOMContentLoaded", function() {
    var loadMoreBtn = document.querySelector(".load a");
    var hiddenProductItems = document.querySelectorAll(".product li:nth-child(18), .product li:nth-child(17), .product li:nth-child(16),.product li:nth-child(15),.product li:nth-child(14),.product li:nth-child(13)");

    loadMoreBtn.addEventListener("click", function(event) {
        event.preventDefault(); // Ngăn chặn hành động mặc định của nút
        hiddenProductItems.forEach(function(item) {
            item.style.display = "block"; // Hiển thị các phần tử ẩn
        });
        loadMoreBtn.style.display = "none"; 
    });
});


// Lấy tham chiếu đến ảnh window ở trang test
const windowImage = document.querySelector('#icon img');

// Lấy tham chiếu đến ảnh iconpage2 ở trang test1
const iconImage = document.querySelector('#header1 #icon img');

// Lấy tham chiếu đến danh sách sản phẩm ở trang test
const productListTest = document.querySelector('.product');

// Lấy tham chiếu đến danh sách sản phẩm ở trang test1
const productListTest1 = document.querySelector('#test1 .product');

// Định nghĩa hàm để thay đổi bố cục của danh sách sản phẩm khi ấn vào ảnh
function changeLayout(image, productList) {
    // Kiểm tra nếu ảnh hiện tại đã được nhấp
    if (image.classList.contains('active')) {
        // Xóa lớp active khỏi ảnh
        image.classList.remove('active');
        // Hiển thị lại danh sách sản phẩm ban đầu
        productList.style.display = 'flex';
    } else {
        // Nếu chưa có lớp active, thêm lớp active vào ảnh
        image.classList.add('active');
        // Ẩn danh sách sản phẩm
        productList.style.display = 'none';
    }
}

// Thêm sự kiện click cho ảnh window
windowImage.addEventListener('click', function() {
    changeLayout(this, productListTest);
});

// Thêm sự kiện click cho ảnh iconpage2
iconImage.addEventListener('click', function() {
    changeLayout(this, productListTest1);
});
