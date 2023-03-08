
const btnNoti = document.querySelector('.btnNoti i');
const displayNoti = document.querySelector('.displayNoti');
const all = document.querySelector('.all');
const unRead = document.querySelector('.unRead');

btnNoti.addEventListener('click', () => {
    displayNoti.classList.toggle('show');
})

document.onclick = function (e) {
    if (!btnNoti.contains(e.target) && !displayNoti.contains(e.target)) {
        displayNoti.classList.remove('show');
    }
}

unRead.addEventListener('click', () => {
    unRead.classList.add('active');
    all.classList.remove('active');
})

all.addEventListener('click', () => {
    unRead.classList.remove('active');
    all.classList.add('active');
})

var count = 0;
$('.numberNotifi').ready(function () {
    AjaxCall('/Notification/CountNotificationNotReadOfUser', 'POST').done(function (response) {
        if (response != 0) {
            count = response;
            if (response >= 10) {
                $('.numberNotifi').html('9+');
            } else {
                $('.numberNotifi').html(response);
            }
        } else {
            $('.numberNotifi').remove('');
        }
    }).fail(function (error) {
        alert(error.statusText);
    })
})

$(document).ready(function () {
    var numberOfRecord = 5;

    $('.btnNoti').click(function () {
        numberOfRecord = 5;
        if (count <= numberOfRecord) {
            GetListNotificationNotReadByReceiverID(numberOfRecord);
            $('#loadMore').html('');
        } else {
            GetListNotificationNotReadByReceiverID(numberOfRecord);
            $('#loadMore').html('Load More');
        }
    })

    $('.unRead').click(function () {
        numberOfRecord = 5;
        if (count <= numberOfRecord) {
            GetListNotificationNotReadByReceiverID(numberOfRecord);
            $('#loadMore').html('');
        } else {
            GetListNotificationNotReadByReceiverID(numberOfRecord);
            $('#loadMore').html('Load More');
        }
    })

    $('.all').click(function () {
        numberOfRecord = 5;
        AjaxCall('/Notification/CountAllNotification', 'POST').done(function (response) {
            if (response != 0) {
                count = response;
                if (count <= numberOfRecord) {
                    GetListAllNotificationByUserID(numberOfRecord);
                    $('#loadMore').html('');
                } else {
                    GetListAllNotificationByUserID(numberOfRecord);
                    $('#loadMore').html('Load More');
                }
            } else {
                $('.listNoti').html('');
                $('.listNoti').append("You don't have any notifications yet.");
            }
        }).fail(function (error) {
            alert(error.statusText);
        })
    })

    $('#loadMore').click(function () {
        numberOfRecord += 5;
        if ($('.unRead').hasClass('active')) {
            GetListNotificationNotReadByReceiverID(numberOfRecord);
            if (numberOfRecord >= count) {
                $('#loadMore').html('');
            }
        }
        if ($('.all').hasClass('active')) {
            GetListAllNotificationByUserID(numberOfRecord);
            if (numberOfRecord >= count) {
                $('#loadMore').html('');
            }
        }
    })


    $('body').on('click', '.notification', function () {
        AjaxCall('/Notification/ReadedNotification?notificationId=' + this.id, 'POST').done(function (response) {
            if (response != null) {
                window.location.href = response;
            }
        }).fail(function (error) {
            alert(error.statusText);
        })
    })
})


// get list notification not read of user
function GetListNotificationNotReadByReceiverID(numberOfRecord) {
    AjaxCall('/Notification/GetListNotificationNotReadByReceiverID?numberOfRecord=' + numberOfRecord, 'POST').done(function (response) {
        $('.listNoti').html('');
        var listNoti = '';
        if (response != null) {
            for (var i = 0; i < response.length; i++) {
                listNoti += '<li id="'+response[i].notificationID+'" class="notification">'
                    + '<div class= "notify-content">'
                    + '<p>' + response[i].notificationContent + '</p>'
                    + '<div class="showTime">'
                    + '<p>' + moment(response[i].createdAt).format('DD-MM-YYYY hh:mm:ss A') + '</p>'
                    + '</div>'
                    + '</div>'
                    + '<div class="unReadMark"></div>'
                    + '</li>';
            }
            if (listNoti == '') {
                $('.listNoti').append("You don't have any notifications yet.");
            } else {
                $('.listNoti').append(listNoti);
            }
        } else {
            $('.listNoti').append("You don't have any notifications yet.")
        }
    }).fail(function (error) {
        alert(error.statusText);
    })
}

// get list all notification of user
function GetListAllNotificationByUserID(numberOfRecord) {
    AjaxCall('/Notification/GetListAllNotificationByUserId?numberOfRecord=' + numberOfRecord, 'POST').done(function (response) {
        $('.listNoti').html('');
        var listAllNoti = '';
        if (response != null) {
            for (var i = 0; i < response.length; i++) {
                if (response[i].readed == 1) {
                    listAllNoti += '<li id="' + response[i].notificationID +'" class="notification">'
                        + '<div class= "notify-content">'
                        + '<p>' + response[i].notificationContent + '</p>'
                        + '<div class="showTime">'
                        + '<p>' + moment(response[i].createdAt).format('DD-MM-YYYY hh:mm:ss A') + '</p>'
                        + '</div>'
                        + '</div>'
                        + '</li>';
                } else {
                    listAllNoti += '<li id="' + response[i].notificationID +'" class="notification">'
                        + '<div class= "notify-content">'
                        + '<p>' + response[i].notificationContent + '</p>'
                        + '<div class="showTime">'
                        + '<p>' + moment(response[i].createdAt).format('DD-MM-YYYY hh:mm:ss A') + '</p>'
                        + '</div>'
                        + '</div>'
                        + '<div class="unReadMark"></div>'
                        + '</li>';
                }
            }
            if (listAllNoti == '') {
                $('.listNoti').append("You don't have any notifications yet.");
            } else {
                $('.listNoti').append(listAllNoti);
            }
        } else {
            $('.listNoti').append("You don't have any notifications yet.")
        }
    }).fail(function (error) {
        alert(error.statusText);
    })
}

function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        dataType: "json",
        data: data,
        type: "POST",
        contentType: "application/json; charset=utf-8",
    });
}

