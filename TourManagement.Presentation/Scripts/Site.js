
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


function cal_price() {
    var quan_child = parseInt(document.getElementById('QuantityChild').value);
    var price_child = parseInt(document.getElementById('price_child').innerHTML.replace(/\,/g,""));

    var quan_adult = parseInt(document.getElementById('QuantityAdult').value);
    var price_adult = parseInt(document.getElementById('price_adult').innerHTML.replace(/\,/g, ""));

    var quan_room = parseInt(document.getElementById('NumberRoom').value);
    var price_room = parseInt(document.getElementById('price_room').innerHTML.replace(/\,/g, ""));

    var quan_remain = document.getElementById('quantityRemain').innerHTML;

    var sum_quan = quan_adult + quan_child;

    
    if (sum_quan > quan_remain) {
        alert("Số lượng chỗ đặt vượt quá số lượng còn !")
    }
    
    var sum_price = (quan_adult * price_adult) + (quan_child * price_child) + (quan_room * price_room);
    document.getElementById('sum_price').innerHTML = sum_price;

}