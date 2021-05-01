
$(document).ready(() => {
    if ($('#remaining').text() == 0) {
        $('#btnBook').addClass('disabled')
    }
    else {
        $('#btnBook').removeClass('disabled')
    }
})

$(document).ready(() => {
    $("#btnSend").on("click", () => {
        $("#cusinfor").empty()
        $("#cusinfor").append("<h2>Cảm ơn quý khách đã đặt phòng</h2>")
    })
})

function getURL() {
    return window.location.href
}