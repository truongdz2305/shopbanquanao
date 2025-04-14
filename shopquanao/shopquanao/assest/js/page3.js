document.getElementById('checkout-form').addEventListener('submit', function(event) {
    event.preventDefault();
    alert('Form submitted!');
});

document.getElementById('deliver-button').addEventListener('click', function() {
    alert('Delivery option selected');
});
