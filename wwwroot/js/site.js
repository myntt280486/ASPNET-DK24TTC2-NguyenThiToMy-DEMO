// Site-wide JavaScript

// Update cart count on page load
$(document).ready(function() {
    updateCartCount();
    
    // Add fade-in animation to main content
    $('main').addClass('fade-in');
    
    // Auto-hide alerts after 5 seconds
    setTimeout(function() {
        $('.alert').fadeOut('slow');
    }, 5000);
});

// Function to update cart count
function updateCartCount() {
    // This would typically fetch from server
    // For now, we'll just show 0
    $('#cart-count').text('0');
}

// Confirm delete actions
$('.delete-confirm').on('click', function(e) {
    if (!confirm('Bạn có chắc chắn muốn xóa?')) {
        e.preventDefault();
    }
});

// Number input validation
$('input[type="number"]').on('input', function() {
    var min = parseInt($(this).attr('min'));
    var max = parseInt($(this).attr('max'));
    var val = parseInt($(this).val());
    
    if (val < min) $(this).val(min);
    if (val > max) $(this).val(max);
});

// Smooth scroll to top
$('.scroll-to-top').on('click', function() {
    $('html, body').animate({scrollTop: 0}, 'slow');
    return false;
});

// Add loading state to buttons on form submit
$('form').on('submit', function() {
    $(this).find('button[type="submit"]').prop('disabled', true).html('<span class="spinner-border spinner-border-sm me-2"></span>Đang xử lý...');
});

// Format currency
function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(amount);
}

// Show toast notification
function showToast(message, type = 'success') {
    var alertClass = type === 'success' ? 'alert-success' : 'alert-danger';
    var icon = type === 'success' ? 'fa-check-circle' : 'fa-exclamation-circle';
    
    var toast = $('<div class="alert ' + alertClass + ' alert-dismissible fade show position-fixed top-0 end-0 m-3" role="alert" style="z-index: 9999;">' +
        '<i class="fas ' + icon + '"></i> ' + message +
        '<button type="button" class="btn-close" data-bs-dismiss="alert"></button>' +
        '</div>');
    
    $('body').append(toast);
    
    setTimeout(function() {
        toast.fadeOut('slow', function() {
            $(this).remove();
        });
    }, 3000);
}
