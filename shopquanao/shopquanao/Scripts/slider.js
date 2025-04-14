document.addEventListener("DOMContentLoaded", function () {
    const sliders = document.querySelectorAll(".product-slider");

    sliders.forEach(slider => {
        const container = slider.querySelector(".product-container");
        const prevBtn = slider.querySelector(".prev-btn");
        const nextBtn = slider.querySelector(".next-btn");

        nextBtn.addEventListener("click", () => {
            container.scrollBy({ left: 300, behavior: "smooth" });
        });

        prevBtn.addEventListener("click", () => {
            container.scrollBy({ left: -300, behavior: "smooth" });
        });
    });
});
