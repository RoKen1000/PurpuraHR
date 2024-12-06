
let getModalFormUrl = $("#book-time-off-modal").data("get-modal-content");
let getDayCountUrl = $("#day-count-container").data("get-day-count");
let getBookedLeaveTableUrl = $("#booked-leave-table-container").data("get-booked-leave-container");

let spinnerHtml = `<div class="d-flex justify-content-center">
                        <div class="spinner-border" style="color: mediumpurple;" role="status"></div>
                    </div>`;

refreshPartials();

function refreshPartials() {
    $(".spinner-container").html(spinnerHtml);

    $.when($.get(getDayCountUrl), $.get(getBookedLeaveTableUrl)).done(function (dayCountData, leaveTableData) {
        $('#day-count-container').html(dayCountData[0]);
        $('#booked-leave-table-container').html(leaveTableData[0]);
        $(".spinner-container").html("");
    })
}

$("#book-time-off-modal").on("show.bs.modal", function () {
    $('.modal-body').html(spinnerHtml);

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

        $(".spinner-container").html(spinnerHtml);

        $.post(postFormUrl, $form.serialize(), function (success) {
            $("#book-time-off-modal").modal("hide");

            if (success) {
                toastr.success('Time off successfully booked!');
                refreshPartials();
            }
            else {
                toastr.error('Something went wrong...');
                $(".spinner-container").html("");
            }

        })
    });

    $form.trigger("submit");
})