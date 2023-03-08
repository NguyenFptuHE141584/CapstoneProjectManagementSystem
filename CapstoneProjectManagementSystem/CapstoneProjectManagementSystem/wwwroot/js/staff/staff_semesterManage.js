$(document).ready(function () {
    $(".backToSemesterMain").click(function () {
        location.reload(true);
    });

    $(".editBtn").click(function () {
        $(".backToSemesterMain").toggle('showBtn');
    });
    $('#nextBtn').attr('disabled', 'disabled');
    $('#nextBtn').css('cursor', 'not-allowed');
    $('#nextBtn').css('opacity', '0.5');

    $('#saveEditSemester').attr('disabled', 'disabled');
    $('#saveEditSemester').css('cursor', 'not-allowed');
    $('#saveEditSemester').css('opacity', '0.5');

    $('.starSemester').click(function () {
        $('.createNewSemester').toggle('hide-form');
    });
    $('.cancelBtn').click(function () {
        $('.createNewSemester').toggle('hide-form');
    });
    $('.createNewSemester').click(function (e) {
        if (e.target === e.currentTarget) {
            $('.createNewSemester').toggle('hide-form');
        }
    });

    $('#newSemesterName').blur(function () {
        if ($('#newSemesterName').val().replace(/\s/g, "").length <= 0) {
            $('#errornewSemesterName').html('This field is required');
        } else if ($('#newSemesterName').val().length > 50) {
            $('#errornewSemesterName').html('Input less than 50 characters');
        } else {
            $('#errornewSemesterName').html('');
        }
    })

    $('#newSemesterCode').blur(function () {
        if ($('#newSemesterCode').val().replace(/\s/g, "").length <= 0) {
            $('#errornewSemesterCode').html('This field is required');
        } else if ($('#newSemesterCode').val().length > 20) {
            $('#errornewSemesterCode').html('Input less than 20 characters');
        } else {
            $('#errornewSemesterCode').html('');
        }
    })

    $('#newSemesterStartTime').blur(function () {
        if ($('#newSemesterStartTime').val().replace(/\s/g, "").length <= 0) {
            $('#errornewSemesterStartTime').html('This field is required');
        } else {
            $('#errornewSemesterStartTime').html('');
        }
    })

    $('#newSemesterEndTime').blur(function () {
        if ($('#newSemesterEndTime').val().replace(/\s/g, "").length <= 0) {
            $('#errornewSemesterEndTime').html('This field is required');
        } else {
            $('#errornewSemesterEndTime').html('');
        }
    })

    $('body').on('blur keyup change', '#newSemesterName, #newSemesterCode, #newSemesterStartTime, #newSemesterEndTime', function () {
        if ($('#newSemesterName').val().replace(/\s/g, "").length <= 0 || $('#newSemesterName').val().length > 50
            || $('#newSemesterCode').val().replace(/\s/g, "").length <= 0 || $('#newSemesterCode').val().length > 20
            || $('#newSemesterStartTime').val().replace(/\s/g, "").length <= 0 || $('#newSemesterEndTime').val().replace(/\s/g, "").length <= 0) {
            $('#nextBtn').attr('disabled', 'disabled');
            $('#nextBtn').css('cursor', 'not-allowed');
            $('#nextBtn').css('opacity', '0.5');
        } else {
            $('#nextBtn').removeAttr('disabled', 'disabled');
            $('#nextBtn').css('cursor', 'pointer');
            $('#nextBtn').css('opacity', '1');
        }
    })

    $('#showSemesterInputAbbreviation').blur(function () {
        if ($('#showSemesterInputAbbreviation').val().replace(/\s/g, "").length <= 0) {
            $('#errorshowSemesterInputAbbreviation').html('This field is required');
        } else if ($('#showSemesterInputAbbreviation').val().length > 50) {
            $('#errorshowSemesterInputAbbreviation').html('Input less than 50 characters');
        } else {
            $('#errorshowSemesterInputAbbreviation').html('');
        }
    })

    $('#showSemesterInputCode').blur(function () {
        if ($('#showSemesterInputCode').val().replace(/\s/g, "").length <= 0) {
            $('#errorshowSemesterInputCode').html('This field is required');
        } else if ($('#showSemesterInputCode').val().length > 20) {
            $('#errorshowSemesterInputCode').html('Input less than 20 characters');
        } else {
            $('#errorshowSemesterInputCode').html('');
        }
    })

    $('#showSemesterInputStart').blur(function () {
        if ($('#showSemesterInputStart').val().replace(/\s/g, "").length <= 0) {
            $('#errorshowSemesterInputStart').html('This field is required');
        } else {
            $('#errorshowSemesterInputStart').html('');
        }
    })

    $('#showSemesterInputEnd').blur(function () {
        if ($('#showSemesterInputEnd').val().replace(/\s/g, "").length <= 0) {
            $('#errorshowSemesterInputEnd').html('This field is required');
        } else {
            $('#errorshowSemesterInputEnd').html('');
        }
    })

    $('body').on('blur keyup change', '#showSemesterInputAbbreviation, #showSemesterInputCode , #showSemesterInputStart, #showSemesterInputEnd', function () {
        if ($('#showSemesterInputAbbreviation').val().replace(/\s/g, "").length <= 0 || $('#showSemesterInputAbbreviation').val().length > 50
            || $('#showSemesterInputCode').val().replace(/\s/g, "").length <= 0 || $('#showSemesterInputCode').val().length > 20
            || $('#showSemesterInputStart').val().replace(/\s/g, "").length <= 0 || $('#showSemesterInputEnd').val().replace(/\s/g, "").length <= 0) {
            $('#saveEditSemester').attr('disabled', 'disabled');
            $('#saveEditSemester').css('cursor', 'not-allowed');
            $('#saveEditSemester').css('opacity', '0.5');
        } else {
            $('#saveEditSemester').removeAttr('disabled', 'disabled');
            $('#saveEditSemester').css('cursor', 'pointer');
            $('#saveEditSemester').css('opacity', '1');
        }
    })
});

const saveBtn = document.querySelector('.saveBtn');
const editBtn = document.querySelector('.editBtn');
const showSemesterInputAbbreviation = document.querySelector('#showSemesterInputAbbreviation');
const showSemesterInputStar = document.querySelector('#showSemesterInputStart');
const showSemesterInputEnd = document.querySelector('#showSemesterInputEnd');
const showSemesterInputCode = document.querySelector('#showSemesterInputCode');

if (editBtn != null) {
    editBtn.addEventListener('click', () => {
        showSemesterInputAbbreviation.disabled = false;
        showSemesterInputStar.disabled = false;
        showSemesterInputEnd.disabled = false;
        showSemesterInputCode.disabled = false;
        editBtn.classList.toggle('hide-btn');
        saveBtn.classList.toggle('hide-btn');
    })
}

$('body').on('click', '.saveBtn', function () {
    var semester = {};
    semester.semesterId = $('#semesterId').html();
    semester.semesterName = $('#showSemesterInputAbbreviation').val();
    semester.semesterCode = $('#showSemesterInputCode').val();
    semester.startTime = $('#showSemesterInputStart').val();
    semester.endTime = $('#showSemesterInputEnd').val();

    AjaxCall('/SemesterManage/UpdateCurrentSemester', JSON.stringify(semester), 'POST').done(function (response) {
        if (response >= 1) {
            window.location.href = '/SemesterManage/Index';
        } else
            alert('Update semester error');
    }).fail(function (error) {
        alert(error.StatusText);
    });
});

$('body').on('click', '.submitBtn', function () {

    var newSemester = {};
    newSemester.semesterName = $('#newSemesterName').val();
    newSemester.semesterCode = $('#newSemesterCode').val();
    newSemester.startTime = $('#newSemesterStartTime').val();
    newSemester.endTime = $('#newSemesterEndTime').val();

    AjaxCall('/SemesterManage/AddNewSemester', JSON.stringify(newSemester), 'POST').done(function (response) {
        if (response >= 1) {
            window.location.href = '/SemesterManage/SetupMajor';
        } else {
            alert('Add new semester error');
        }
    }).fail(function (error) {
        alert(errortatusText);
    });
})

$('body').on('click', '#closeSemesterBtn', function () {
    AjaxCall('/SemesterManage/CloseSemesterCurrent', 'POST').done(function (response) {
        if (response = 1) {
            window.location.href = '/SemesterManage/Index';
        } else {
            alert('Add new semester error');
        }
    }).fail(function (error) {
        alert(errortatusText);
    });
})



function AjaxCall(url, data, type) {
    return $.ajax({
        url: url,
        dataType: "json",
        data: data,
        type: "POST",
        contentType: "application/json; charset=utf-8",
    });
}
