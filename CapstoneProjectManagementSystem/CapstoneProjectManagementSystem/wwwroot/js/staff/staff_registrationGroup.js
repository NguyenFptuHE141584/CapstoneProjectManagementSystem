
const requestMain = document.querySelector('.requestMain');
const acceptedMain = document.querySelector('.acceptedMain');
const rejectedMain = document.querySelector('.rejectedMain');
const requestPendingBtn = document.querySelector('.requestPendingBtn');
const requestAcceptedBtn = document.querySelector('.requestAcceptedBtn');
const requestRejectedBtn = document.querySelector('.requestRejectedBtn');


requestPendingBtn.addEventListener('click', () => {
    requestMain.classList.remove('hide--list');
    rejectedMain.classList.add('hide--list');
    acceptedMain.classList.add('hide--list');
    requestPendingBtn.classList.add('active');
    requestAcceptedBtn.classList.remove('active');
    requestRejectedBtn.classList.remove('active');
})
requestAcceptedBtn.addEventListener('click', () => {
    requestMain.classList.add('hide--list');
    rejectedMain.classList.add('hide--list');
    acceptedMain.classList.remove('hide--list');
    requestPendingBtn.classList.remove('active');
    requestAcceptedBtn.classList.add('active');
    requestRejectedBtn.classList.remove('active');
})
requestRejectedBtn.addEventListener('click', () => {
    requestMain.classList.add('hide--list');
    rejectedMain.classList.remove('hide--list');
    acceptedMain.classList.add('hide--list');
    requestPendingBtn.classList.remove('active');
    requestAcceptedBtn.classList.remove('active');
    requestRejectedBtn.classList.add('active');
})


$('body').on('click', '.displayForm,.displayAcceptedForm,.displayRejectedForm', function () {
    var registeredGroupId = $(this).attr('id');
    $('.nameProject').html('');
    $('.professional').html('');
    $('.specialty').html('');
    $('.abbreviations').html('');
    $('.vietnamTitle').html('');
    $('#basicInformSupervisor1').html('');
    $('#basicInformSupervisor2').html('');
    $('.commentStudent').html('');
    $('.numMember').html('');
    $('#memberinfor').html('');
    $("div").remove("#memberinfor");
    AjaxCall('/RegistrationGroup/GetDetailRegistrationOfGroupStudent?registeredGroupId=' + registeredGroupId, "POST").done(function (response) {
        var nameProject =
            '<div class="inforProject">'
            + '<h3>' + response.detailRegistration.groupIdea.projectEnglishName + '</h3>'
            + '<p>Create at: <span>' + moment(response.detailRegistration.createdAt).format('DD-MM-YYYY'); + '</span></p>'
            + '</div >'

        $('.nameProject').append(nameProject);
        var professional = '<p class="title">Professional</p>'
            + '<p class="content">' + response.detailRegistration.groupIdea.profession.professionFullName + '</p >'
        $('.professional').append(professional);

        var specialty = '<p class="title">Specialty</p>'
            + ' <p class="content">' + response.detailRegistration.groupIdea.specialty.specialtyFullName + '</p>'
        $('.specialty').append(specialty);

        var abbreviations = '<p class="title">Abbreviations</p>'
            + '<p class="content">' + response.detailRegistration.groupIdea.abrrevation + '</p>'
        $('.abbreviations').append(abbreviations);

        var vietnamTitle = ' <p class="title">Vietnamese Title</p>'
            + '<p class="content">' + response.detailRegistration.groupIdea.projectVietNameseName + '</p> '
        $('.vietnamTitle').append(vietnamTitle);
        if (response.detailRegistration.registeredSupervisorName1 != ""
            || response.detailRegistration.registeredSupervisorPhone1 != ""
            || response.detailRegistration.registeredSupervisorEmail1 != "") {
            var basicInformSupervisor1 = '<div class="inputInformSupervisor">'
                + '<p> Full Name</p>'
                + '<p>' + response.detailRegistration.registeredSupervisorName1 + '</p>'
                + '</div >'
                + '<div class="inputInformSupervisor">'
                + '<p>Phone Number</p>'
                + '<p>' + response.detailRegistration.registeredSupervisorPhone1 + '</p>'
                + '</div>'
                + '<div class="inputInformSupervisor">'
                + '<p>Email</p>'
                + '<p>' + response.detailRegistration.registeredSupervisorEmail1 + '</p>'
                + '</div>'
            $('#basicInformSupervisor1').append(basicInformSupervisor1);
        } else {
            $('#basicInformSupervisor1').html('');
        }
        if (response.detailRegistration.registeredSupervisorName2 != ""
            || response.detailRegistration.registeredSupervisorPhone2 != ""
            || response.detailRegistration.registeredSupervisorEmail2 != "") {
            var basicInformSupervisor2 = '<div class="inputInformSupervisor">'
                + '<p> Full Name</p>'
                + '<p>' + response.detailRegistration.registeredSupervisorName2 + '</p>'
                + '</div >'
                + '<div class="inputInformSupervisor">'
                + '<p>Phone Number</p>'
                + '<p>' + response.detailRegistration.registeredSupervisorPhone2 + '</p>'
                + '</div>'
                + '<div class="inputInformSupervisor">'
                + '<p>Email</p>'
                + '<p>' + response.detailRegistration.registeredSupervisorEmail2 + '</p>'
                + '</div>'
            $('#basicInformSupervisor2').append(basicInformSupervisor2);
        } else {
            $('#basicInformSupervisor2').html('');
        }

        if (response.detailRegistration.studentComment != "") {
            var commentStudent = '<p class="title">Student Comment</p>'
                + '<p>' + response.detailRegistration.studentComment + '</p>'
            $('.commentStudent').append(commentStudent);
        } else {
            $('.commentStudent').html('');
        }
        var numMember = '<p>Total: <span>' + response.detailRegistration.groupIdea.numberOfMember + ' memmbers</span></p>'
        $('.numMember').append(numMember);

        var memberInfor = '';
        memberInfor += '<div class="showInfomember" id="memberinfor">'
            + '<div class="info" >'
            + '<img src = "' + response.listInforStudentInGroupIdea.find(x => x.isLeader == true).user.avatar + '" alt = "">'
            + '<div class="memInfo">'
            + '<a>' + response.listInforStudentInGroupIdea.find(x => x.isLeader == true).user.fptEmail + '</a>'
            + '<div class = "majorOfStu">'
            + '<p>Profession : <span style="color:black;font-weight: bold;">' + response.listInforStudentInGroupIdea.find(x => x.isLeader == true).profession.professionFullName + '</span></p>'
            + '<p>Specialty : <span style="color:black;font-weight: bold;">' + response.listInforStudentInGroupIdea.find(x => x.isLeader == true).specialty.specialtyFullName + '</span></p>'
            + '</div>'
            + '</div>'
            + '</div>'
            + '<div class="role">'
            + '<p>Leader</p >'
            + ' </div >'
            + '</div>';
        for (var i = 0; i < response.listInforStudentInGroupIdea.length; i++) {
            if (response.listInforStudentInGroupIdea[i].isLeader == false) {
                memberInfor += '<div class="showInfomember" id="memberinfor">'
                    + '<div class="info" >'
                    + '<img src = "' + response.listInforStudentInGroupIdea[i].user.avatar + '" alt = "">'
                    + '<div class="memInfo">'
                    + '<a>' + response.listInforStudentInGroupIdea[i].user.fptEmail + '</a>'
                    + '<div class = "majorOfStu">'
                    + '<p>Profession : <span style="color:black;font-weight: bold;">' + response.listInforStudentInGroupIdea[i].profession.professionFullName + '</span></p>'
                    + '<p>Specialty : <span style="color:black;font-weight: bold;">' + response.listInforStudentInGroupIdea[i].specialty.specialtyFullName + '</span></p>'
                    + '</div>'
                    + '</div>'
                    + '</div>'
                    + '<div class="role">'
                    + '<p>member</p >'
                    + ' </div >'
                    + '</div>';
            }
        }
        $('.members').append(memberInfor);
    }).fail(function (error) {
        alert(error.statusText);
    });
    $('.showForm').toggle('hide-form');
})


$('body').on('click', '.closeFormBtn', function () {
    $('.showForm').toggle('hide-form');
});
$('body').on('click', '.showForm', function (e) {
    if (e.target === e.currentTarget) {
        $('.showForm').toggle('hide-form');
    }
});



//////////////////////////////////////////////////////////////////////////////////


// load list request registration group in filter request pending 
$(document).ready(function () {
    var searchText = $('#searchNameRequestInput').val();
    if ($('#requestPendingBtn').hasClass('active')) {
        GetListRegisteredGroup(0, searchText, 'none', 0);
    }
})

$('#requestPendingBtn,#requestAcceptedBtn,#requestRejectedBtn,#searchNameRequestInput').on('click load change', function () {
    var searchText = $('#searchNameRequestInput').val();
    if ($('#requestPendingBtn').hasClass('active')) {
        GetListRegisteredGroup(0, searchText, 'none', 0);
    }
    if ($('#requestAcceptedBtn').hasClass('active')) {
        GetListRegisteredGroup(1, searchText, 'none', 0);
    }
    if ($('#requestRejectedBtn').hasClass('active')) {
        GetListRegisteredGroup(2, searchText, 'none', 0);
    }
});


var startNum;
var numberOfRecordsPerPage;
var countResult;
var endNum;

/*previous page*/
$('body').on('click', '#previousBtn', function () {
    var searchText = $('#searchNameRequestInput').val();
    if ($('#requestPendingBtn').hasClass('active')) {
        if (!(startNum == 1)) {
            GetListRegisteredGroup(0, searchText, 'previous', startNum);
        }
    }
    if ($('#requestAcceptedBtn').hasClass('active')) {
        if (!(startNum == 1)) {
            GetListRegisteredGroup(1, searchText, 'previous', startNum);
        }
    }
    if ($('#requestRejectedBtn').hasClass('active')) {
        if (!(startNum == 1)) {
            GetListRegisteredGroup(2, searchText, 'previous', startNum);
        }
    }
});

/*next page*/
$('body').on('click', '#nextbtn', function () {
    var searchText = $('#searchNameRequestInput').val();
    if ($('#requestPendingBtn').hasClass('active')) {
        if ((startNum + numberOfRecordsPerPage - 1) < countResult) {
            GetListRegisteredGroup(0, searchText, 'next', startNum);
        }
    }
    if ($('#requestAcceptedBtn').hasClass('active')) {
        if ((startNum + numberOfRecordsPerPage - 1) < countResult) {
            GetListRegisteredGroup(1, searchText, 'next', startNum);
        }
    }
    if ($('#requestRejectedBtn').hasClass('active')) {
        if ((startNum + numberOfRecordsPerPage - 1) < countResult) {
            GetListRegisteredGroup(2, searchText, 'next', startNum);
        }
    }
});


//get list registered group
function GetListRegisteredGroup(status, searchText, pagingType, recordNumber) {
    $('.informRequest').remove();
    AjaxCall('/RegistrationGroup/GetListRegisteredGroup?status=' + status + '&&searchText=' + searchText + '&&pagingType=' + pagingType + '&&recordNumber=' + recordNumber, "POST").done(function (response) {
        var informRequest = '';
        var count = 1;
        if (response.registeredGroups != null) {
            if (status == 0) {
                $('.requestMain').html('');
                $('.requestMain').append('<div class="titleRequest">'
                    + '<div class= "formTitle">'
                    + '<p>No.</p>'
                    + '<p>Name Capstone</p>'
                    + '<p>Major</p>'
                    + '<p>Supervisor</p>'
                    + '</div>'
                    + '<p>Action'
                    + '<svg width="14" height="10" viewBox="0 0 14 14" fill="none" xmlns="http://www.w3.org/2000/svg">'
                    + '<path d="M6.99967 0.333008C3.31967 0.333008 0.333008 3.31967 0.333008 6.99967C0.333008 10.6797 3.31967 13.6663 6.99967 13.6663C10.6797 13.6663 13.6663 10.6797 13.6663 6.99967C13.6663 3.31967 10.6797 0.333008 6.99967 0.333008ZM7.66634 10.333H6.33301V6.33301H7.66634V10.333ZM7.66634 4.99967H6.33301V3.66634H7.66634V4.99967Z"'
                    + 'fill="#1C1F27" fill-opacity="0.3" />'
                    + '</svg>'
                    + '</p>'
                    + '</div>');
                for (var i = 0; i < response.registeredGroups.length; i++) {
                    informRequest += '<div class="informRequest">'
                        + '<div class="displayForm" id="' + response.registeredGroups[i].registeredGroupID + '">'
                        + '<p>' + count + ' </p>'
                        + '<p class="nameProjectRequest">' + response.registeredGroups[i].groupIdea.projectEnglishName + '</p>'
                        + '<p>' + response.registeredGroups[i].groupIdea.specialty.specialtyFullName + '</p>'
                        + '<div class="supervisor">'
                        + '<p>' + response.registeredGroups[i].registeredSupervisorName1 + '</p>'
                        + '<p>' + response.registeredGroups[i].registeredSupervisorName2 + '</p>'
                        + '</div>'
                        + '</div>'
                        + '<div class="buttonAccept">'
                        + '<button class="acceptBtn" value="' + response.registeredGroups[i].registeredGroupID + '">Accept</button>'
                        + '<button class="rejectBtn" value="' + response.registeredGroups[i].registeredGroupID + '">Reject</button>'
                        + '</div>'
                        + '</div>'
                    count++;
                }
                $('.requestMain').append(informRequest);
                startNum = response.startNum;
                numberOfRecordsPerPage = response.numberOfRecordsPerPage;
                countResult = response.countResult
                endNum = (startNum + numberOfRecordsPerPage - 1) > countResult ? countResult : startNum + numberOfRecordsPerPage - 1;
                $('.requestMain').append('<div class="requestMain">'
                    + '<div class= "pagination">'
                    + '<a href="#" id="previousBtn"><i class="fa-solid fa-angle-left"></i></a>'
                    + '<div class="numPage">'
                    + '<p class="number" id="pagingCount">' + startNum + '-' + endNum + ' in ' + countResult + ' results</p>'
                    + '</div>'
                    + '<a href="#" id="nextbtn"><i class="fa-solid fa-angle-right"></i></a>'
                    + '</div>'
                    + '</div>')
            }
            if (status == 1) {
                $('.acceptedMain').html('');
                $('.acceptedMain').append('<div class="titleAcceptedRequest">'
                    + '<div class= "formAcceptedTitle">'
                    + '<p>No.</p>'
                    + '<p>Name Capstone</p>'
                    + '<p>Major</p>'
                    + '<p>Supervisor</p>'
                    + '</div>'
                    + '<p class="status">Status</p>'
                    + '<p>Action</p>'
                    + '</div>');
                for (var i = 0; i < response.registeredGroups.length; i++) {
                    informRequest += '<div class="informRequest">'
                        + '<div class="displayAcceptedForm" id="' + response.registeredGroups[i].registeredGroupID + '">'
                        + '<p>' + count + '</p>'
                        + '<p class="nameProjectRequest">' + response.registeredGroups[i].groupIdea.projectEnglishName + '</p>'
                        + '<p>' + response.registeredGroups[i].groupIdea.specialty.specialtyFullName + '</p>'
                        + '<div class="supervisor">'
                        + '<p>' + response.registeredGroups[i].registeredSupervisorName1 + '</p>'
                        + '<p>' + response.registeredGroups[i].registeredSupervisorName2 + '</p>'
                        + '</div>'
                        + '</div>'
                        + '<p class="acceptStatus">Accepted</p>'
                        + '<button class="reject" value="' + response.registeredGroups[i].registeredGroupID + '">Reject</button>'
                        + '</div>'
                    count++;
                }
                $('.acceptedMain').append(informRequest);
                startNum = response.startNum;
                numberOfRecordsPerPage = response.numberOfRecordsPerPage;
                countResult = response.countResult
                endNum = (startNum + numberOfRecordsPerPage - 1) > countResult ? countResult : startNum + numberOfRecordsPerPage - 1;
                $('.acceptedMain').append('<div class="acceptedMain">'
                    + '<div class= "pagination">'
                    + '<a href="#" id="previousBtn"><i class="fa-solid fa-angle-left"></i></a>'
                    + '<div class="numPage">'
                    + '<p class="number" id="pagingCount">' + startNum + '-' + endNum + ' in ' + countResult + ' results</p>'
                    + '</div>'
                    + '<a href="#" id="nextbtn"><i class="fa-solid fa-angle-right"></i></a>'
                    + '</div>'
                    + '</div>')
            }
            if (status == 2) {
                $('.rejectedMain').html('');
                $('.rejectedMain').append('<div class="titleRejectedRequest">'
                    + '<div class= "formRejectedTitle">'
                    + '<p>No.</p>'
                    + '<p>Name Capstone</p>'
                    + '<p>Major</p>'
                    + '<p>Supervisor</p>'
                    + '</div >'
                    + '<p class="status">Status</p>'
                    + '<p>Reason</p>'
                    + '</div>');
                for (var i = 0; i < response.registeredGroups.length; i++) {
                    informRequest += '<div class="informRequest">'
                        + ' <div class="displayRejectedForm" id="' + response.registeredGroups[i].registeredGroupID + '">'
                        + '<p>' + count + '</p>'
                        + '<p class="nameProjectRequest">' + response.registeredGroups[i].groupIdea.projectEnglishName + '</p>'
                        + '<p>' + response.registeredGroups[i].groupIdea.specialty.specialtyFullName + '</p>'
                        + '<div class="supervisor">'
                        + '<p>' + response.registeredGroups[i].registeredSupervisorName1 + '</p>'
                        + '<p>' + response.registeredGroups[i].registeredSupervisorName2 + '</p>'
                        + '</div>'
                        + '</div>'
                        + '<p class="rejectStatus">Rejected</p>'
                        + '<p class="reason">' + response.registeredGroups[i].staffComment + '</p>'
                        + '</div>'
                    count++;
                }
                $('.rejectedMain').append(informRequest);
                startNum = response.startNum;
                numberOfRecordsPerPage = response.numberOfRecordsPerPage;
                countResult = response.countResult
                endNum = (startNum + numberOfRecordsPerPage - 1) > countResult ? countResult : startNum + numberOfRecordsPerPage - 1;
                $('.rejectedMain').append('<div class="rejectedMain">'
                    + '<div class= "pagination">'
                    + '<a href="#" id="previousBtn"><i class="fa-solid fa-angle-left"></i></a>'
                    + '<div class="numPage">'
                    + '<p class="number" id="pagingCount">' + startNum + '-' + endNum + ' in ' + countResult + ' results</p>'
                    + '</div>'
                    + '<a href="#" id="nextbtn"><i class="fa-solid fa-angle-right"></i></a>'
                    + '</div>'
                    + '</div>')
            }
        } else {
            if (status == 0) {
                $('.requestMain').html('');
                $('.requestMain').append("<h2 style='color:red'>You don't have any requests yet.</h2>");
            }
            if (status == 1) {
                $('.acceptedMain').html('');
                $('.acceptedMain').append("<h2 style='color:red'>You don't have any requests yet.</h2>");
            }
            if (status == 2) {
                $('.rejectedMain').html('');
                $('.rejectedMain').append("<h2 style='color:red'>You don't have any requests yet.</h2>");
            }
        }
    }).fail(function (error) {
        alert(error.statusText);
    });
}


//accept in request pending
$(document).ready(function () {
    var registeredGroupId;
    var acceptBtn;
    var rejectBtn;
    $('body').on('click', '.acceptBtn', function (e) {
        $('.showFormConfirm').toggle('hide-form');
        acceptBtn = $('.acceptBtn');
        acceptBtn.attr('disabled', 'disabled');
        acceptBtn.css('cursor', 'not-allowed');
        acceptBtn.css('opacity', '0.5');

        rejectBtn = $('.rejectBtn');
        rejectBtn.attr('disabled', 'disabled');
        rejectBtn.css('cursor', 'not-allowed');
        rejectBtn.css('opacity', '0.5');

        registeredGroupId = $(this).val();
        $('body').on('click', '#submitBtn', function (p) {
            AjaxCall('/RegistrationGroup/AcceptRegisteredGroup?registeredGroupID=' + registeredGroupId, "POST").done(function (response) {
                if (response == true) {
                    Swal.fire({
                        icon: 'success',
                        title: '<p class="popupTitle">Accepted Successfully</p>'
                    }).then(function () {
                        $(document).ready(function () {
                            var searchText = $('#searchNameRequestInput').val();
                            if ($('#requestPendingBtn').hasClass('active')) {
                                GetListRegisteredGroup(0, searchText, 'none', 0);
                            }
                        })
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: "<p class='popupTitle'>Something is wrong! Try again later</p>"
                    }).then(function () {
                        location.reload(true);
                    });
                }
            }).fail(function (error) {
                Swal.fire({
                    icon: 'error',
                    title: "<p class='popupTitle'>Something is wrong! Try again later</p>"
                }).then(function () {
                    location.reload(true);
                });;
            });
            $('.showFormConfirm').toggle('hide-form');
            e.stopPropogation();
        });
    });
    $('body').on('click', '.discardBtn', function () {
        acceptBtn.removeAttr('disabled', 'disabled');
        acceptBtn.css('cursor', 'pointer');
        acceptBtn.css('opacity', '1');

        rejectBtn.removeAttr('disabled', 'disabled');
        rejectBtn.css('cursor', 'pointer');
        rejectBtn.css('opacity', '1');
        $('.showFormConfirm').toggle('hide-form');
    });
    $('body').on('click', '.showFormConfirm', function (e) {
        if (e.target === e.currentTarget) {
            acceptBtn.removeAttr('disabled', 'disabled');
            acceptBtn.css('cursor', 'pointer');
            acceptBtn.css('opacity', '1');

            rejectBtn.removeAttr('disabled', 'disabled');
            rejectBtn.css('cursor', 'pointer');
            rejectBtn.css('opacity', '1');
            $('.showFormConfirm').toggle('hide-form');
        }
    });
})



$('body').on('click', '.rejectBtn,.reject', function () {
    var rejectBtn = $('.rejectBtn');
    rejectBtn.attr('disabled', 'disabled');
    rejectBtn.css('cursor', 'not-allowed');
    rejectBtn.css('opacity', '0.5');

    var acceptBtn = $('.acceptBtn');
    acceptBtn.attr('disabled', 'disabled');
    acceptBtn.css('cursor', 'not-allowed');
    acceptBtn.css('opacity', '0.5');

    var rejectInAcceptedRequestBtn = $('.reject');
    rejectInAcceptedRequestBtn.attr('disabled', 'disabled');
    rejectInAcceptedRequestBtn.css('cursor', 'not-allowed');
    rejectInAcceptedRequestBtn.css('opacity', '0.5');
    $('.commentReject').toggle('hide-form');
})

$('body').on('click', '.closeBtn', function () {
    if ($('#requestPendingBtn').hasClass('active')) {
        var rejectBtn = $('.rejectBtn');
        rejectBtn.removeAttr('disabled', 'disabled');
        rejectBtn.css('cursor', 'pointer');
        rejectBtn.css('opacity', '1');

        var acceptBtn = $('.acceptBtn');
        acceptBtn.removeAttr('disabled', 'disabled');
        acceptBtn.css('cursor', 'pointer');
        acceptBtn.css('opacity', '1');

        var rejectInAcceptedRequestBtn = $('.reject');
        rejectInAcceptedRequestBtn.removeAttr('disabled', 'disabled');
        rejectInAcceptedRequestBtn.css('cursor', 'pointer');
        rejectInAcceptedRequestBtn.css('opacity', '1');
        $('.commentReject').toggle('hide-form');
    }
    if ($('#requestAcceptedBtn').hasClass('active')) {
        var rejectInAcceptedRequestBtn = $('.reject');
        rejectInAcceptedRequestBtn.removeAttr('disabled', 'disabled');
        rejectInAcceptedRequestBtn.css('cursor', 'pointer');
        rejectInAcceptedRequestBtn.css('opacity', '1');

        var acceptBtn = $('.acceptBtn');
        acceptBtn.removeAttr('disabled', 'disabled');
        acceptBtn.css('cursor', 'pointer');
        acceptBtn.css('opacity', '1');

        var rejectInAcceptedRequestBtn = $('.reject');
        rejectInAcceptedRequestBtn.removeAttr('disabled', 'disabled');
        rejectInAcceptedRequestBtn.css('cursor', 'pointer');
        rejectInAcceptedRequestBtn.css('opacity', '1');
        $('.commentReject').toggle('hide-form');
    }
});

$('body').on('click', '.commentReject', function (e) {
    if (e.target === e.currentTarget) {
        if ($('#requestPendingBtn').hasClass('active')) {
            var rejectBtn = $('.rejectBtn');
            rejectBtn.removeAttr('disabled', 'disabled');
            rejectBtn.css('cursor', 'pointer');
            rejectBtn.css('opacity', '1');

            var acceptBtn = $('.acceptBtn');
            acceptBtn.removeAttr('disabled', 'disabled');
            acceptBtn.css('cursor', 'pointer');
            acceptBtn.css('opacity', '1');

            var rejectInAcceptedRequestBtn = $('.reject');
            rejectInAcceptedRequestBtn.removeAttr('disabled', 'disabled');
            rejectInAcceptedRequestBtn.css('cursor', 'pointer');
            rejectInAcceptedRequestBtn.css('opacity', '1');
            $('.commentReject').toggle('hide-form');
        }
        if ($('#requestAcceptedBtn').hasClass('active')) {
            var rejectInAcceptedRequestBtn = $('.reject');
            rejectInAcceptedRequestBtn.removeAttr('disabled', 'disabled');
            rejectInAcceptedRequestBtn.css('cursor', 'pointer');
            rejectInAcceptedRequestBtn.css('opacity', '1');

            var acceptBtn = $('.acceptBtn');
            acceptBtn.removeAttr('disabled', 'disabled');
            acceptBtn.css('cursor', 'pointer');
            acceptBtn.css('opacity', '1');

            var rejectInAcceptedRequestBtn = $('.reject');
            rejectInAcceptedRequestBtn.removeAttr('disabled', 'disabled');
            rejectInAcceptedRequestBtn.css('cursor', 'pointer');
            rejectInAcceptedRequestBtn.css('opacity', '1');
            $('.commentReject').toggle('hide-form');
        }
    }
});


////reject in request pending
$(document).ready(function () {
    var registeredGroupId;
    $('body').on('click', '.rejectBtn', function (e) {
        registeredGroupId = $(this).val();
        $('.commentReject').html('');
        $('.commentReject').append('<div class="popup">'
            + '<p>Give to reasons for reject this request to change the topic?</p>'
            + '<textarea name="" id="textAreaComent" cols="60" rows="8" placeholder="Write your reason here..."></textarea>'
            + '<p class="showErrorMessage" id="showErrorMessageCommentReject"></p>'
            + '<div class="button">'
            + '<button class="submitBtn" id="reject1Btn" disabled>Submit</button>'
            + '<button class="closeBtn">Close</button>'
            + '</div>'
            + '</div>');
        $('body').on('click', '#reject1Btn', function (p) {
            var staffComment = $('#textAreaComent').val();
            AjaxCall('/RegistrationGroup/RejectRegisteredGrop?staffComment=' + staffComment + '&&registeredGroupId=' + registeredGroupId, "POST").done(function (response) {
                if (response == true) {
                    Swal.fire({
                        icon: 'success',
                        title: '<p class="popupTitle">Rejected Successfully</p>'
                    }).then(function () {
                        $(document).ready(function () {
                            var searchText = $('#searchNameRequestInput').val();
                            if ($('#requestPendingBtn').hasClass('active')) {
                                GetListRegisteredGroup(0, searchText, 'none', 0);
                            }
                        });
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: "<p class='popupTitle'>Something is wrong! Try again later</p>"
                    }).then(function () {
                        location.reload(true);
                    });
                }
            }).fail(function (error) {
                Swal.fire({
                    icon: 'error',
                    title: "<p class='popupTitle'>Something is wrong! Try again later</p>"
                }).then(function () {
                    location.reload(true);
                });
            });
            $('.commentReject').toggle('hide-form');
            e.stopPropogation();
        })
    })
})

////reject in request accepted 
$(document).ready(function () {
    var registeredGroupId;
    registeredGroupId = $(this).val();

    $('body').on('click', '.reject', function (e) {
        $('.commentReject').html('');
        $('.commentReject').append('<div class="popup">'
            + '<p>Give to reasons for reject this request to change the topic?</p>'
            + '<textarea name="" id="textAreaComent" cols="60" rows="10" placeholder="Write your reason here..."></textarea>'
            + '<p class="showErrorMessage" id="showErrorMessageCommentReject"></p>'
            + '<div class="button">'
            + '<button class="submitBtn" id="reject2Btn" disabled>Submit</button>'
            + '<button class="closeBtn">Close</button>'
            + '</div>'
            + '</div>');
        registeredGroupId = $(this).val();
        $('body').on('click', '#reject2Btn', function (p) {
            var commentReject = $('#textAreaComent').val();
            AjaxCall('/RegistrationGroup/RejectWhenAccepted?registeredGroupID=' + registeredGroupId + '&&commentReject=' + commentReject, "POST").done(function (response) {
                if (response == true) {
                    Swal.fire({
                        icon: 'success',
                        title: '<p class="popupTitle">Rejected Successfully</p>'
                    }).then(function () {
                        $(document).ready(function () {
                            var searchText = $('#searchNameRequestInput').val();
                            $('#requestAcceptedBtn').addClass('active')
                            GetListRegisteredGroup(1, searchText, 'none', 0);
                        });
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: "<p class='popupTitle'>Something is wrong! Try again later</p>"
                    }).then(function () {
                        location.reload(true);
                    });
                }
            }).fail(function (error) {
                Swal.fire({
                    icon: 'error',
                    title: "<p class='popupTitle'>Something is wrong! Try again later</p>"
                }).then(function () {
                    location.reload(true);
                });
            });
            $('.commentReject').toggle('hide-form');
            e.stopPropogation();
        });
    });
})


$(document).ready(function () {
    //validate commentReject
    $('body').on('blur', '#textAreaComent', function () {
        const commentReject = $('#textAreaComent').val();
        if (commentReject.length == 0) {
            $('#showErrorMessageCommentReject').html('This field is required')
        } else if (commentReject.length > 400) {
            $('#showErrorMessageCommentReject').html('Input less than 400 characters');
        } else {
            $('#showErrorMessageCommentReject').html('');
        }
    })
    // disable button submit
    $('body').on('blur change keyup', '#textAreaComent', function () {
        const commentReject = $('#textAreaComent').val();
        if (commentReject.length == 0) {
            $('#reject1Btn').attr('disabled', 'disabled');
            $('#reject1Btn').css('cursor', 'not-allowed');
            $('#reject1Btn').css('opacity', '0.5');

            $('#reject2Btn').attr('disabled', 'disabled');
            $('#reject2Btn').css('cursor', 'not-allowed');
            $('#reject2Btn').css('opacity', '0.5');
        } else if (commentReject.length > 400) {
            $('#reject1Btn').attr('disabled', 'disabled');
            $('#reject1Btn').css('cursor', 'not-allowed');
            $('#reject1Btn').css('opacity', '0.5');

            $('#reject2Btn').attr('disabled', 'disabled');
            $('#reject2Btn').css('cursor', 'not-allowed');
            $('#reject2Btn').css('opacity', '0.5');
        } else {
            $('#reject1Btn').removeAttr('disabled', 'disabled');
            $('#reject1Btn').css('cursor', 'pointer');
            $('#reject1Btn').css('opacity', '1');

            $('#reject2Btn').removeAttr('disabled', 'disabled');
            $('#reject2Btn').css('cursor', 'pointer');
            $('#reject2Btn').css('opacity', '1');
        }
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
