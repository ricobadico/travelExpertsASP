//[Ronnie]
$(document).ready(function () {

    // validate the password
    $('#password').keyup(function () {
        let pswd = $(this).val();

        //validate at least one letter
        if (pswd.match(/[a-z]/)) {
            $('#letter').removeClass('invalid-pwd').addClass('valid-pwd');
        } else {
            $('#letter').removeClass('valid-pwd').addClass('invalid-pwd');
        }

        //validate at least one capital letter
        if (pswd.match(/[A-Z]/)) {
            $('#capital').removeClass('invalid-pwd').addClass('valid-pwd');
        } else {
            $('#capital').removeClass('valid-pwd').addClass('invalid-pwd');
        }

        //validate at least one number
        if (pswd.match(/\d/)) {
            $('#number').removeClass('invalid-pwd').addClass('valid-pwd');
        } else {
            $('#number').removeClass('valid-pwd').addClass('invalid-pwd');
        }

        //validate the length
        if (pswd.length < 8) {
            $('#length').removeClass('valid-pwd').addClass('invalid-pwd');
        } else {
            $('#length').removeClass('invalid-pwd').addClass('valid-pwd');
        }
    }); // end of password validation

    // validation for Confirm Password Field while entering
    $("#pwd-confirm").keyup(function () {
        if ($(this).val() == "") {
            //while it is empty field
            $("#confirmPwdErrMessage").show();
            $("#confirmPwdErrMessage").html("The Confirm Password field is required.");
            $(this).focus;
        }
        else {
            $("#confirmPwdErrMessage").hide();
        }
    });

    // convert zipcode to uppercase
    $('#zipcode').keyup(function () {
        $(this).val($(this).val().toUpperCase());
    });

    // validate the username
    $(".next").click(function () {
        var name = $("#username").val();

        // assign the fields
        var current_fs, next_fs, previous_fs; //fieldsets
        var opacity;
        current_fs = $(this).parent().parent();
        next_fs = $(this).parent().parent().next();

        if ($("#username").valid()) {
            $.ajax({
                method: 'GET',
                url: "Customer/Exist",
                data: { username: name }
            }).done(function (resutls) {
                var passed = false;
                if (resutls == "")
                    passed = true;
                else {
                    $("#usrNameErrMessage").html(resutls);
                    $("#username").focus();
                }


                var valid = false;
                // validate password field
                if (current_fs[0] == $("#account-field")[0]) {
                    var firstPwdValid = $("#password").valid();
                    if ($("#pwd-confirm").val() != "") {
                        // confirm whether passwords are the same
                        if ($("#pwd-confirm").val() == $("#password").val()) {
                            valid = true;
                        }
                        else {
                            $("#confirmPwdErrMessage").show();
                            $("#confirmPwdErrMessage").html("Password and Confirm Password do not match");
                            $("#pwd-confirm").focus();
                            valid = false;
                        }
                    }
                    else { //the Confirm Password is not entered
                        $("#confirmPwdErrMessage").show();
                        $("#confirmPwdErrMessage").html("The Confirm Password field is required.");
                        valid = false;
                    }
                }
                else if (current_fs[0] == $("#info-field")[0]) {
                    valid = $("#msform").valid();
                }

                // if it passes all the validation for current field
                if (passed && valid) {
                    //add class Active
                    $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

                    //show the next fieldset
                    next_fs.show();

                    //hide the current fieldset with style
                    current_fs.animate({ opacity: 0 }, {
                        step: function (now) {
                            // for making fieldset appear animation
                            opacity = 1 - now;

                            current_fs.css({
                                'display': 'none',
                                'position': 'relative'
                            });
                            next_fs.css({ 'opacity': opacity });
                        },
                        duration: 600
                    });
                } // end of if all validation in current field set passed
            }); // end of ajax
        } // end of non-empty username
    }); //end of next click


    $(".previous").click(function () {

        current_fs = $(this).parent().parent();
        previous_fs = $(this).parent().parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fieldset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    }); // end of previous click

}); // end of document ready