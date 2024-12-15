
let getModalFormUrl = $("#annual-leave-form-modal").data("get-modal-content");
let getEditModalFormUrl = $("#annual-leave-form-modal").data("get-edit-modal-content");
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

$("#annual-leave-form-modal").on("show.bs.modal", function (e) {
    $('.modal-body').html(spinnerHtml);

    let crudMethod = $(e.relatedTarget).data("crud-method");

    if (crudMethod === "create") {
        $.get(getModalFormUrl, function (data) {
            $(".modal-title").text("Book Annual Leave");
            $('.modal-body').html(data);
            $("#leave-form").attr("data-crud-method", "create");
        });
    }
    else if (crudMethod === "edit") {
        $.get(getEditModalFormUrl, { externalReference: $(e.relatedTarget).data("external-reference") }, function (data) {
            $(".modal-title").text("Edit Annual Leave");
            $('.modal-body').html(data);
            $("#leave-form").attr("data-crud-method", "edit");
        });
    }

})

$("#annual-leave-form-modal").on("hide.bs.modal", function () {
    $('.modal-body').html("");
})

$("#annual-leave-form-modal #save-changes-button").on("click", function () {
    let $form = $("#leave-form");
    let crudMethod = $form.data("crud-method");

    $form.on("submit", function (e) {
        e.preventDefault();

        let postFormUrl;

        if (crudMethod === "create") {
            postFormUrl = $form.data("create-url");
        }
        else if (crudMethod === "edit") {
            postFormUrl = $form.data("edit-url");
        }

        $(".spinner-container").html(spinnerHtml);

        $.post(postFormUrl, $form.serialize(), function (result) {

            if (result.isSuccess) {
                $("#annual-leave-form-modal").modal("hide");
                toastr.success('Time off successfully edited!');
                refreshPartials();
            }
            else {
                toastr.error(result.error);
                $(".spinner-container").html("");
            }

        })
    });

    $form.trigger("submit");
})

