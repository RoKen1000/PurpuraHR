
function initialiseEmailCheckAjax(url) {
    $("#Email").on("focusout", function () {
        const $emailExistsField = $("#EmailExists");
        $emailExistsField.val(false);

        const $validationDiv = $("#existing-email-validation");
        $validationDiv.css({ "margin-bottom": "10px" });
        $validationDiv.html(`<div class="d-flex justify-content-center">
                                <div class="spinner-border" style="color: mediumpurple;" role="status">
                                    <span class="visually-hidden">Loading...</span>
                                </div>
                            </div>`);

        if (emailValid($(this).val())) {
            $.post(url, { input: $(this).val() }, function (exists) {
                if (exists) {
                    $validationDiv.html("<i class='bi bi-check' style='color: green; font-size: large;'></i> Email exists and employee will be assigned to the associated user.");
                    $emailExistsField.val(true);
                }
                else {
                    $validationDiv.html("<i class='bi bi-info-circle'></i> Email does not exist. If employee is created now then the user will be automatically assigned to this company after registering.");
                }
            });
        }
        else {
            $validationDiv.html("");
            return;
        }
    });
}

function emailValid(value) {
    if (value == null || !value.length) {
        return false;
    }

    const regex = /^(\w{1,}|\w{1,}\.\w{1,}|\w{1,}\.\w{1}\.\w{1,})@\w{2,}\.(\w{2}\.\w{2}|\w{2,})$/;

    return regex.test(value);
}