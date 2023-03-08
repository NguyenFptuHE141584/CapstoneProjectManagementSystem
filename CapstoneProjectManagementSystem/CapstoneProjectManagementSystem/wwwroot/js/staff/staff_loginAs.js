$(document).ready(function () {
    $('#btnLoginAs').click(function () {
        var studentEmail = $('#inputEmailOfStudent').val();
        AjaxCall('/LoginAs/LoginAs', JSON.stringify(studentEmail), 'POST').done(function (response) {
            if (response == 0) {
                Swal.fire({
                    icon: 'error',
                    title: '<p class="popupTitle">Something is wrong! Try again later</p>'
                }).then(function () {
                    $('#inputEmailOfStudent').val('');
                });
            }
            if (response == 1) {
                Swal.fire({
                    icon: 'success',
                    title: '<p class="popupTitle">Login As successfully</p>'
                }).then(function () {
                    window.location.href = '/StudentHome/Index'
                });
            }
            if (response == 2) {
                Swal.fire({
                    icon: 'error',
                    title: '<p class="popupTitle">Login As unsuccessfully</p>'
                }).then(function () {
                    $('#inputEmailOfStudent').val('');
                });
            }

        }).fail(function (error) {

        })
    })
})

// Call Ajax
function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        dataType: "json",
        data: data,
        type: "POST",
        contentType: "application/json; charset=utf-8",
    });
}
