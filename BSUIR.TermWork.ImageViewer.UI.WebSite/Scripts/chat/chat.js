$(function() {
    $.connection.hub.url = '/messaging';
    $.connection.hub.start()
        .fail(function(message) {
        alert(message);
    });

    var proxy = $.connection.messageServiceHub;

    $('#user-account-control').on('click', '#send-button', function() {
        var activeInputBlock = $('#current-message').parent();
        var textValue = $('#current-message-text').val();
        if (textValue) {
            var date = moment().format('M/D/YYYY hh:mm:ss A');
            var newBlock = generateNewElement($('#dual-chat-room-modal-header').attr('data-sender-user-name'), textValue, date);
            activeInputBlock.before(newBlock);

            var sendData = {
                Author: $('#dual-chat-room-modal-header').attr('data-receiver-user-key'),
                Date: date,
                Text: textValue
            };
            proxy.server.sendMessage(sendData)
                .done(function() {
                    $('#current-message-text').val('');
                })
                .fail(function(message) {
                    alert(message);
                });
        }
    });

    proxy.client.showMessage = function(message) {
        if ($('#dual-chat-room-modal').length !== 0 && message) {
            var activeInputBlock = $('#current-message').parent();
            var newBlock = generateNewElement(message.Sender, message.Text, message.Date);
            activeInputBlock.before(newBlock);
        }
    };

    $('#user-account-control').on('click', 'a[data-period="1"]',function (element) {
        var selectedLink = $(this);
        if (selectedLink.parent().hasClass('active') === false) {
            var url = selectedLink.attr('data-href');
            $('#message-list').load(url, function () {
                selectedLink.parent().siblings('.active').removeClass('active');
                selectedLink.parent().addClass('active');
            });
        }
    });

    var generateNewElement = function (author, text, date) {
        var origin = window.location.protocol + '//' + window.location.host;
        var result = '<div class="form-group">' +
                        '<div class="media">' +
                            '<a class="pull-left">' +
                            '<img class="media-object" src="' + origin + '/Content/images/profile_pages/no_preview_sm.png"/>' +
                            '</a>' +
                            '<div class="media-body">' +
                                '<h4 class="media-heading">' + author + ' (' + date + ')</h4>' +
                                '<p align="justify">' +
                                    text +
                                '</p>' +
                            '</div>' +
                        '</div>' +
                     '</div>';
        return result;
    };
});