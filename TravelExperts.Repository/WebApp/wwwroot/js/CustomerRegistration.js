
$(document).ready(function () {
    // validate the password
    $('input[type=password]').keyup(function () {
        let pswd = $(this).val();

        // reset the border back to the default while typing
        $('#pwd').css('border', '1px solid #ced4da');

        //validate at least one letter
        if (pswd.match(/[A-z]/)) {
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
                    valid = $("#password").valid();
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