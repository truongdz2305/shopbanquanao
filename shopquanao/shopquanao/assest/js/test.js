// Hieu ung bat tat tria tim //
    document.querySelectorAll('.heart-icon').forEach(icon => {
        icon.addEventListener('click', () => {
            icon.classList.toggle('active');
        });
    });


// Hieu ung chuyen chu xanh //    
document.addEventListener('DOMContentLoaded', function() {
    const links = document.querySelectorAll('.flex.space-x-4 a');
    links.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault(); // Ngăn chặn hành vi mặc định của liên kết
            links.forEach(l => l.classList.remove('active'));
            this.classList.add('active');
        });
    });
});

    function toggleHeart(element) {
        element.classList.toggle('active');
    }
    