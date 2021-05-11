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

//caculate price order
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

            var date = new Date($('#TimeStart').val());
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();

            var TimeStart = [year, month, day].join('-')

            $.ajax({
                type: "GET",
                url: url,
                data: { datePick: TimeStart, time: Time },
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
