﻿const arr = document.querySelector('.arr');
const inputSupervisorInform = document.querySelector('.inputSupervisorInform');

arr.addEventListener('click', () => {
    inputSupervisorInform.classList.toggle('show--inputSupervisorInform');
})

$(document).ready(function () {
    //$('.submitFormRegis').click(function () {
    //    $('.showForm').toggle('hide-form');
    //});
    $('#btnDiscard1').click(function () {
        $('.showForm').toggle('hide-form');
    });
    $('.showForm').click(function (e) {
        if (e.target === e.currentTarget) {
            $('.showForm').toggle('hide-form');
        }
    });
});


// Ajax for specialty
$(function () {

    $('#professionDDL').on("change", function () {
        var profession = $('#professionDDL').val();
        AjaxCall('/MyGroup/GetCorrespondingSpecialty', JSON.stringify(profession), "POST").done(function (response) {
            $('#specialtyDDL').html('');
            var options = '';
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

});


// validate form regist submit

$(document).ready(function () {

    var regexPhone = /^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/;
    var regexEmail = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;

    $('#name1').blur(function () {
        if ($('#name1').val().length > 100) {
            $('#showErrorMessageFullName1').html('Input less than 100 characters');
        } else {
            $('#showErrorMessageFullName1').html('');
        }
    })

    $('#phone1').blur(function () {
        if ($('#phone1').val().length == 0) {
            $('#showErrorMessagePhone1').html('');
        } else {
            if (!regexPhone.test($('#phone1').val())) {
                $('#showErrorMessagePhone1').html('Input the correct vietnamese phone numbers');
            } else {
                $('#showErrorMessagePhone1').html('');
            }
        }
    })

    $('#email1').blur(function () {
        if ($('#email1').val().length == 0) {
            $('#showErrorMessageEmail1').html('');
        } else {
            if ($('#email1').val().length < 100) {
                if (!regexEmail.test($('#email1').val())) {
                    $('#showErrorMessageEmail1').html('Email is invalid');
                } else {
                    $('#showErrorMessageEmail1').html('');
                }
            } else {
                $('#showErrorMessageEmail1').html('Input less than 100 characters');
            }
        }
    })

    $('#name2').blur(function () {
        if ($('#name2').val().length > 100) {
            $('#showErrorMessageName2').html('Input less than 100 characters');
        } else {
            $('#showErrorMessageName2').html('');
        }
    })

    $('#phone2').blur(function () {
        if ($('#phone2').val().length == 0) {
            $('#showErrorMessagePhone2').html('');
        } else {
            if (!regexPhone.test($('#phone2').val())) {
                $('#showErrorMessagePhone2').html('Input the correct vietnamese phone numbers');
            } else {
                $('#showErrorMessagePhone2').html('');
            }
        }
    })

    $('#email2').blur(function () {
        if ($('#email2').val().length == 0) {
            $('#showErrorMessageEmail2').html('');
        } else {
            if ($('#email2').val().length < 100) {
                if (!regexEmail.test($('#email2').val())) {
                    $('#showErrorMessageEmail2').html('Email is invalid');
                } else {
                    $('#showErrorMessageEmail2').html('');
                }
            } else {
                $('#showErrorMessageEmail2').html('Input less than 100 characters');
            }
        }
    })

    $('#otherComment').blur(function () {
        if ($('#otherComment').val().length > 150) {
            $('#showErrorMessageOtherComment').html('Input less than 150 characters');
        } else {
            $('#showErrorMessageOtherComment').html('');
        }
    })


    $('body').on('keyup change blur', '#name1,#name2,#email1,#email2,#phone1,#phone2,#otherComment,#checked', function () {
        var check = $('#checked').is(':checked');;
        var name1 = $('#name1').val();
        var phone1 = $('#phone1').val();
        var email1 = $('#email1').val();
        var name2 = $('#name2').val();
        var phone2 = $('#phone2').val();
        var email2 = $('#email2').val();
        var otherComment = $('#otherComment').val();
        if (check) {
            $('.btnCre').removeAttr('disabled', 'disabled');
            $('.btnCre').css('cursor', 'pointer');
            $('.btnCre').css('opacity', '1');
            if (name1.length >= 0 && name1.length < 100 && name2.length >= 0 && name2.length < 100
                && email1.length >= 0 && email1.length < 100 && email2.length >= 0 && email2.length < 100
                && otherComment.length >= 0 && otherComment.length < 150) {
                $('.btnCre').removeAttr('disabled', 'disabled');
                $('.btnCre').css('cursor', 'pointer');
                $('.btnCre').css('opacity', '1');
                if (email1.length != 0 && !regexEmail.test(email1)) {
                    $('.btnCre').attr('disabled', 'disabled');
                    $('.btnCre').css('cursor', 'not-allowed');
                    $('.btnCre').css('opacity', '0.5');
                } else if (email2.length != 0 && !regexEmail.test(email2)) {
                    $('.btnCre').attr('disabled', 'disabled');
                    $('.btnCre').css('cursor', 'not-allowed');
                    $('.btnCre').css('opacity', '0.5');
                } else if (phone1.length != 0 && !regexPhone.test(phone1)) {
                    $('.btnCre').attr('disabled', 'disabled');
                    $('.btnCre').css('cursor', 'not-allowed');
                    $('.btnCre').css('opacity', '0.5');
                } else if (phone2.length !=0 && !regexPhone.test(phone2)) {
                    $('.btnCre').attr('disabled', 'disabled');
                    $('.btnCre').css('cursor', 'not-allowed');
                    $('.btnCre').css('opacity', '0.5');
                } else {
                    $('.btnCre').removeAttr('disabled', 'disabled');
                    $('.btnCre').css('cursor', 'pointer');
                    $('.btnCre').css('opacity', '1');
                }
            } else {
                $('.btnCre').attr('disabled', 'disabled');
                $('.btnCre').css('cursor', 'not-allowed');
                $('.btnCre').css('opacity', '0.5');
            }
        } else {
            $('.btnCre').attr('disabled', 'disabled');
            $('.btnCre').css('cursor', 'not-allowed');
            $('.btnCre').css('opacity', '0.5');
        }
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

