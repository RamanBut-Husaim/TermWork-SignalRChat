$(document).ready(function () {
    $.ajaxSetup({ cache: false });
});

$('#image-actions-list').on('click', '#file-upload-link', function () {
    document.getElementById('file-uploader').click();
});

$('#file-uploader').on('change', function() {
    $('#file-upload-form').submit();
});

$('#image-actions-list').on('click', '.ajax-link-modal-window', function () {
    var element = $(this);
    var targetUrl = element.attr('data-target-url');
    
    var dataGet = element.attr('data-get');
    var dataObject = '';
    if (dataGet !== '') {
        dataObject = $.parseJSON(dataGet);
    }
    $.ajax({
        url: targetUrl,
        type: 'GET',
        data: dataObject
    })
        .done(function (data) {
            var targetId = element.attr('data-target-id');
            $('#' + targetId).html(data);
            var modalId = element.attr('data-modal-id');
            var modalObject = $('#' + modalId);
            if (modalObject.length === 0) {
                modalObject = $('#error-modal');
            }
            $.validator.unobtrusive.parse(modalObject);
            modalObject.modal('show');
            modalObject.on('hidden.bs.modal', function () {
                $('#' + targetId).html('');
            });

            handleFormEvent(modalObject, targetId, modalId);
        });
});

$('#user-image-list').on('click', '.ajax-modal-window', function () {
    var element = $(this);
    var targetUrl = element.attr('data-target-url');
    var dataGet = element.attr('data-get');
    var dataObject = '';
    if (dataGet !== '') {
        dataObject = $.parseJSON(dataGet);
    }
    $.ajax({
        url: targetUrl,
        type: 'GET',
        data: dataObject
    })
        .done(function (data) {
            var targetId = element.attr('data-target-id');
            $('#' + targetId).html(data);
            var modalId = element.attr('data-modal-id');
            var modalObject = $('#' + modalId);
            if (modalObject.length === 0) {
                modalObject = $('#error-modal');
            }
            $.validator.unobtrusive.parse(modalObject);
            modalObject.modal('show');
            modalObject.on('hidden.bs.modal', function () {
                $('#' + targetId).html('');
            });

            handleFormEvent(modalObject, targetId, modalId);
        });
});

function handleFormEvent(modalObject, targetId, modalId) {
    $('form', modalObject).submit(function () {
        if (!$(this).valid())
            return false;

        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize()
        })
            .done(function (result) {
                if (result.success) {
                    modalObject.modal('hide');
                    location.reload(true);
                } else {
                    modalObject = $('#' + modalId);
                    if (modalObject.length === 0) {
                        modalObject = $('#error-modal');
                    }
                    modalObject.modal('hide');
                    $('.modal-backdrop').remove();
                    $('#' + targetId).html(result);
                    modalObject = $('#' + modalId);
                    if (modalObject.length === 0) {
                        modalObject = $('#error-modal');
                    }
                    $(modalObject).modal('show');
                    modalObject.on('hidden.bs.modal', function () {
                        $('#' + targetId).html('');
                    });
                    $.validator.unobtrusive.parse(modalObject);
                    handleFormEvent(modalObject, targetId, modalId);
                }
            });
        return false;
    });
}