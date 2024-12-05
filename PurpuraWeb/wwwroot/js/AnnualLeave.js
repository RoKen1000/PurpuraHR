
$("#save-changes-button").on("click", function () {
    let $form = $("#leave-form");

    $form.on("submit", function (e) {
        e.preventDefault();

        var url = $form.data("action-url");

        $.post(url, $form.serialize(), function (success) {
            $("#book-time-off-modal").modal("hide");

            if (success) toastr.success('Time off successfully booked!');
            else toastr.error('Something went wrong...');
        })
    });

    $form.trigger("submit");
})