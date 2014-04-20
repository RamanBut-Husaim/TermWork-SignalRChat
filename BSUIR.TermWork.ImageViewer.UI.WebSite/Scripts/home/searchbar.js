$('#search-bar').on('keyup', '#search-input', function() {
    var element = this;
    if (element.value.length < 3) {
        $('#search-bar-button').prop('disabled', true);
    } else {
        $('#search-bar-button').prop('disabled', false);
    }
});