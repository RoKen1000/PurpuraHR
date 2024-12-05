
let getModalFormUrl = $("#book-time-off-modal").data("get-modal-content");
let getDayCountUrl = $("#day-count-container").data("get-day-count");

refreshDayCount();

function refreshDayCount() {
    let spinnerHtml = `<div class="d-flex justify-content-center">
                            <div class="spinner-border" style="color: mediumpurple;" role="status">
                                <span class="visually-hidden">Loading...</span>
                            </div>
                        </div>`;

    $('#day-count-container').html(spinnerHtml);

    $.get(getDayCountUrl, function (data) {
        $('#day-count-container').html(data);
    });
}

$("#book-time-off-modal").on("show.bs.modal", function () {
    $.get(getModalFormUrl, function (data) {
        $('.modal-body').html(data);
    });
})

$("#book-time-off-modal").on("hide.bs.modal", function () {
    $('.modal-body').html("");
})

$("#save-changes-button").on("click", function () {
    let $form = $("#leave-form");

    $form.on("submit", function (e) {
        e.preventDefault();

        let postFormUrl = $form.data("action-url");

        $.post(postFormUrl, $form.serialize(), function (success) {
            $("#book-time-off-modal").modal("hide");

            if (success) {
                toastr.success('Time off successfully booked!');
                refreshDayCount();
            }
            else {
                toastr.error('Something went wrong...');
            }

        })
    });

    $form.trigger("submit");
})