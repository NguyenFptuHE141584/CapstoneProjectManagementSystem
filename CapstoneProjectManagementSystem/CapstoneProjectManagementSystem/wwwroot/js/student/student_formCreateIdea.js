const ul = document.querySelector(".addTag"),
    input = document.getElementById("inputTag");

let maxTags = 10,
    tags = [];

createTag();

$(document).ready(function () {
    $('#btnCreate').attr('disabled', 'disabled');
    $('#btnCreate').css('cursor', 'not-allowed');
    $('#btnCreate').css('opacity', '0.5');
})

function createTag() {
    ul.querySelectorAll("li").forEach(li => li.remove());
    tags.slice().reverse().forEach(tag => {
        let liTag = `<li>${tag} <i class="fa-solid fa-xmark" onclick="remove(this, '${tag}')"></i></li>`;
        ul.insertAdjacentHTML("afterbegin", liTag);
    });
}

function remove(element, tag) {
    let index = tags.indexOf(tag);
    tags = [...tags.slice(0, index), ...tags.slice(index + 1)];
    element.parentElement.remove();
}

function addTag(e) {
    if (e.key == "Enter") {
        let tag = e.target.value.replace(/\s+/g, ' ');
        if (tag.length > 1 && !tags.includes(tag)) {
            if (tags.length < 10) {
                tag.split(',').forEach(tag => {
                    tags.push(tag);
                    createTag();
                });
            }
        }
        e.target.value = "";
    }
}

input.addEventListener("keyup", addTag);


var listMember = [];

listMember.push({
    'studentId': $('#studentId').text(),
    'avatar': $('#avatar').attr('src'),
    'fptEmail': $('#fptEmail').text()
});


$(function () {
    $('#professionDDL').on("change", function () {
        var profession = $('#professionDDL').val();
        AjaxCall('/CreateIdea/GetCorrespondingSpecialty', JSON.stringify(profession), "POST").done(function (response) {
            $('#specialtyDDL').html('');
            var options = '';
            options += '<option selected="selected" value="0">Specialty</option>';
            if (response != null) {
                for (var i = 0; i < response.length; i++) {
                    var specialty = response[i]
                    options += '<option value="' + specialty.specialtyID + '">' + specialty.specialtyFullName + '</option>';
                }
            }
            $('#specialtyDDL').append(options);
        }).fail(function (error) {
            alert(error.StatusText);
        });
    });

    var maxMember;
    $('#specialtyDDL').on("change", function () {
        var specialty = $('#specialtyDDL').val();
        AjaxCall('/CreateIdea/GetMaxMemberOfSpecialty', JSON.stringify(specialty), "POST").done(function (response) {
            maxMember = response.maxMember;
        }).fail(function (error) {
            alert(error.StatusText);
        });
    });


    $('#btnAdd').on("click", function () {
        var txtMember = $('#txtMember').val();
        var memberAdded = '';

        var profession = $('#professionDDL').val();
        var specialty = $('#specialtyDDL').val();
        $('#noti').append($('#noti').html(''));
        AjaxCall('/CreateIdea/CheckStudentExistWhenBeforeAdded', JSON.stringify(txtMember), "POST").done(function (response) {
            if (response != null) {
                if (profession == 0 || specialty == 0) {
                    $('#noti').append('You must select profession and specialty.');
                } else {
                    if (listMember.length + 1 > maxMember) {
                        $('#noti').append('The specialty you chose only allows you to make a group with a maximum of ' + maxMember + ' members.');
                    }
                    else if (listMember.some(item => item.studentId === response.studentID)) {
                        $('#noti').append('This student has been added your group');
                    }
                    else {
                        listMember.push({
                            'studentId': response.studentID,
                            'avatar': response.user.avatar,
                            'fptEmail': response.user.fptEmail
                        });
                        memberAdded =
                            '<div class="existMem" id="existMember">'
                            + '<div class="nameMem">'
                            + '<p hidden >' + response.studentID + '</p >'
                            + '<img src = "' + response.user.avatar + '" alt = "">'
                            + '<p>' + response.user.fptEmail + '</p >'
                            + '</div>'
                            + '</div>';
                        $('.addMem').append(memberAdded);
                    }
                }
                $('#txtMember').val('');
            } else {
                $('#txtMember').val('');
                $('#noti').append('This student was not found or already in group.');
            }
        })
    });

    $('#btnCreate').click(function () {

        var strTag = '';
        for (var i = 0; i < tags.length; i++) {
            if (tags[tags.length - 1] == tags[i]) {
                strTag += tags[i]
            } else {
                strTag += tags[i] + ',';
            }
        }
        var profession = {};
        profession.professionID = $('#professionDDL').val()
        var specialty = {};
        specialty.specialtyID = $('#specialtyDDL').val()
        var groupIdea = {};
        groupIdea.projectEnglishName = $('#txtEnglishTitle').val();
        groupIdea.abrrevation = $('#txtShortTitle').val();
        groupIdea.projectVietNameseName = $('#txtVietNameseTitle').val();
        groupIdea.description = $('#desDetail').val();
        groupIdea.profession = profession;
        groupIdea.specialty = specialty;
        groupIdea.ProjectTags = strTag;
        groupIdea.maxMember = maxMember;
        groupIdea.Students = listMember;

        $('#notiWhenCreate').append($('#notiWhenCreate').html(''));
        AjaxCall('/CreateIdea/CreateIdea', JSON.stringify(groupIdea), "POST").done(function (response) {
            if (response == true) {
                Swal.fire({
                    icon: 'success',
                    title: "<p class='popupTitle'>You created an idea succesfully</p>"
                }).then(function () {
                    window.location = "/MyGroup/Index";
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: "<p class='popupTitle'>You created an idea unsuccesfully</p>",
                    timer: 1000,
                    showConfirmButton: false
                }).then(function () {
                    window.location = "/CreateIdea/Index";
                });
            }
        });
    });
});


// Validate Create Idea
$(document).ready(function () {
    $('#professionDDL').blur(function () {
        if ($('#professionDDL').val() == 0) {
            $('#errorprofessionDDL').html('This field is required');
        } else {
            $('#errorprofessionDDL').html('');
        }
    });

    $('#specialtyDDL').blur(function () {
        if ($('#specialtyDDL').val() == 0) {
            $('#errorspecialtyDDL').html('This field is required');
        } else {
            $('#errorspecialtyDDL').html('');
        }
    }); 
    $('#txtEnglishTitle').blur(function () {
        if ($('#txtEnglishTitle').val().replace(/\s/g, "").length <= 0) {
            $('#errortxtEnglishTitle').html('This field is required');
        } else if ($('#txtEnglishTitle').val().length > 100) {
            $('#errortxtEnglishTitle').html('Input less than 100 characters');
        } else {
            $('#errortxtEnglishTitle').html('');
        }
    })
    $('#txtShortTitle').blur(function () {
        if ($('#txtShortTitle').val().replace(/\s/g, "").length <= 0) {
            $('#errortxtShortTitle').html('This field is required');
        } else if ($('#txtShortTitle').val().length > 20) {
            $('#errortxtShortTitle').html('Input less than 20 characters');
        } else {
            $('#errortxtShortTitle').html('');
        }
    })
    $('#txtVietNameseTitle').blur(function () {
        if ($('#txtVietNameseTitle').val().replace(/\s/g, "").length <= 0) {
            $('#errortxtVietNameseTitle').html('This field is required');
        } else if ($('#txtVietNameseTitle').val().length > 100) {
            $('#errortxtVietNameseTitle').html('Input less than 100 characters');
        } else {
            $('#errortxtVietNameseTitle').html('');
        }
    })
    $('#desDetail').blur(function () {
        if ($('#desDetail').val().replace(/\s/g, "").length <= 0) {
            $('#errordesDetail').html('This field is required');
        } else if ($('#desDetail').val().length > 2000) {
            $('#errordesDetail').html('Input less than 2000 characters');
        } else {
            $('#errordesDetail').html('');
        }
    })

    $('body').on('keyup change', '#txtEnglishTitle, #txtShortTitle, #txtVietNameseTitle, #desDetail, #professionDDL, #specialtyDDL', function () {
        if ($('#txtEnglishTitle').val().replace(/\s/g, "").length <= 0 || $('#txtEnglishTitle').val().length > 100
            || $('#professionDDL').val() == 0 || $('#specialtyDDL').val() == 0
            || $('#txtShortTitle').val().replace(/\s/g, "").length <= 0 || $('#txtShortTitle').val().length > 20
            || $('#txtVietNameseTitle').val().replace(/\s/g, "").length <= 0 || $('#txtVietNameseTitle').val().length > 100
            || $('#desDetail').val().replace(/\s/g, "").length <= 0 || $('#desDetail').val().length > 2000) {
            $('#btnCreate').attr('disabled', 'disabled');
            $('#btnCreate').css('cursor', 'not-allowed');
            $('#btnCreate').css('opacity', '0.5');
        } else {
            $('#btnCreate').removeAttr('disabled', 'disabled');
            $('#btnCreate').css('cursor', 'pointer');
            $('#btnCreate').css('opacity', '1');
        }
    })
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
