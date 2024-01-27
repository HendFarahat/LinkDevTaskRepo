
function MovedWithEnter(frm) {
    try {
        //
        $('input,select').on("keypress", function (e) {
            /* ENTER PRESSED*/
            if (e.keyCode !== null && e.keyCode == 13) {
                var inputs;
                /* FOCUS ELEMENT */
                if (frm != null) {
                    inputs = $(frm).find("input[type=text],input[type=number],select");

                }
                else {
                    inputs = $("form").find("input[type=text],input[type=number],select");
                }

                var idx = inputs.index(this);

                if (idx == inputs.length - 1) {
                    inputs[0].select();
                } else {
                    while (inputs[idx + 1] != null && ((inputs[idx + 1].readOnly != undefined && inputs[idx + 1].readOnly) || $(inputs[idx + 1]).is(":visible") == false)) {
                        idx++;
                    }
                    if ((idx + 1) < inputs.length) {
                        inputs[idx + 1].focus();
                        if (typeof (inputs[idx + 1]) != "undefined" && inputs[idx + 1].type == "number") {
                            inputs[idx + 1].select();
                        }
                        //  handles submit buttons
                        //inputs[idx + 1].select();
                    }
                }
                return false;
            }
        });

    } catch (e) {
    }
}
//

function CheckValidation(control, Index) {
    debugger
    var $forms = $('form');

    var validators = [];
    for (var FormNo = 0; FormNo < $forms.length; FormNo++) {
        $form = $($forms[FormNo]);
        try {
            $form.valid();

        } catch (e) {

        }
        $form[0].removeAttribute("novalidate");

        var validator = $form.data("validator");
        validators.push(validator);
    }

    if (Index != null)
        $form = $($form[Index]);
    else if ($form.length > 1) {
        $form = $($form[$form.length - 1]);
    }
    $form[0].removeAttribute("novalidate");

    //Handle not valid controls with each form
    $(".has-error").removeClass("has-error");
    //
    $.removeAttr("title");
    var ErrorFound = false;
    if (validators != null && validators.length > 0) {
        for (var validatorNo = 0; validatorNo < validators.length; validatorNo++) {
            if (validators[validatorNo] != null && validators[validatorNo].errorList.length > 0) {

                //
                $("label.error").remove();
                //
                for (var i = 0; i < validators[validatorNo].errorList.length; i++) {
                    try {
                        if (control != null) {
                            if ($(control).find(validators[validatorNo].errorList[i].element).length > 0) {

                                ErrorFound = true;
                                validators[validatorNo].errorList[0].element.focus();
                                $(validators[validatorNo].errorList[i].element).parent().addClass("has-error");
                                validators[validatorNo].errorList[i].element.setAttribute("title", validators[validatorNo].errorList[i].message);
                                $(validators[validatorNo].errorList[i].element).tooltip('show');
                                onblur(validators[validatorNo].errorList[i].element);

                            }
                        }
                        else {
                            if ($(validators[validatorNo].errorList[i].element).hasClass('select2-hidden-accessible')) {
                                ErrorFound = true;
                                var eleFiled = $(validators[validatorNo].errorList[i].element).next(".select2-container");
                                $(eleFiled).tooltip({
                                    title: validators[validatorNo].errorList[i].message,
                                    placement: "top",
                                    html: true,
                                    show: true
                                });
                                $(eleFiled).tooltip('show')

                            } else {
                                ErrorFound = true;
                                validators[validatorNo].errorList[0].element.focus();

                                $(validators[validatorNo].errorList[i].element).parent().addClass("has-error");
                                validators[validatorNo].errorList[i].element.setAttribute("title", validators[validatorNo].errorList[i].message);
                                $(validators[validatorNo].errorList[i].element).tooltip('show');
                                //
                                onblur(validators[validatorNo].errorList[i].element);
                            }
                        }
                    } catch (e) {
                        //
                    }
                }
                //  return !ErrorFound;
            }

        }
        return !ErrorFound;

    }//end of If 


    return true;
    //
}
//