
$(document).ready(() => {
    if ($('#remaining').text() == 0) {
        $('#btnBook').addClass('disabled')
    }
    else {
        $('#btnBook').removeClass('disabled')
    }
})