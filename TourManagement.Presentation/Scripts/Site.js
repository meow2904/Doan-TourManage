//check tour is valid
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

$(document).ready(() => {
    var price_room = parseInt($('#price_room').text().replace(/\,/g, ""))
    if (price_room === 0) {
        console.log('hihih')
        $('#div_numberroom').attr('hidden', 'hidden')
    }
})
//caculate price order
$('#QuantityChild, #QuantityAdult, #NumberRoom').change(function () {
    var vl = $(this).val()
    var quan_child = parseInt($('#QuantityChild').val())
    var quan_adult = parseInt($('#QuantityAdult').val())
    var quan_room = parseInt($('#NumberRoom').val())
    var quan_remain = parseInt($('#quantityRemain').text())

    var price_adult = parseInt($('#price_adult').text().replace(/\,/g, ""))
    var price_child = parseInt($('#price_child').text().replace(/\,/g, ""))
    var price_room = parseInt($('#price_room').text().replace(/\,/g, ""))


    
    console.log(price_room)

    var sum_quan = quan_child + quan_adult

    if (sum_quan > quan_remain) {
        $(this).val(vl - 1)
        alert("Số lượng chỗ đặt vượt quá số lượng còn !")
    }
    else {

        var sum_price = (quan_adult * price_adult) + (quan_child * price_child) + (quan_room * price_room)

        $("#sum_price").text(sum_price)
        $('#total').val(sum_price)
    }
})




//display name image
function Getname() {
    var input = document.getElementById('filesInput');
    console.log(typeof (input));
    var output = document.getElementById('Image');
    var image = "";
    for (var i = 0; i < input.files.length; i++) {
        image = image + input.files.item(i).name + '-';
    }
    output.value = image.substring(0, (image.length - 1));
}


function TakeEmployees(url) {
    $(function () {
        $("#TimeStart").change(function () {
            var Time = $("#Time").val()

            var date = $('#TimeStart').val();
            $.ajax({
                type: "GET",
                url: url,
                data: { datePick: date, time: Time },
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    let rs = response.split("-")
                    let dropdown = $("#EmployeeID")
                    dropdown.empty()

                    for (let item of rs) {
                        dropdown.append($('<option></option>').val(item.split(',')[0]).html(item.split(',')[1]))
                    }
                },

                error: function (errormes) {
                }
            })
        })
    })
}


//read html
function strip(html) {
    var tmp = document.createElement("div");
    tmp.innerHTML = html;
    return tmp.textContent || tmp.innerText || "";
}


//
