// Инициализация всех тултипов
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

// Инициализация всех поповеров
$(function () {
    $('[data-toggle="popover"]').popover();
});

// Обработка отправки форм через AJAX
$(document).on('submit', 'form[data-ajax="true"]', function (e) {
    e.preventDefault();
    var form = $(this);
    var url = form.attr('action');
    var method = form.attr('method') || 'POST';
    var data = form.serialize();

    $.ajax({
        url: url,
        method: method,
        data: data,
        success: function (response) {
            if (response.success) {
                $('#resultMessage').removeClass('alert-danger').addClass('alert-success').text(response.message).show();
                form[0].reset();
            } else {
                $('#resultMessage').removeClass('alert-success').addClass('alert-danger').text(response.message).show();
            }
        },
        error: function () {
            $('#resultMessage').removeClass('alert-success').addClass('alert-danger').text('Произошла ошибка при отправке формы').show();
        }
    });
});