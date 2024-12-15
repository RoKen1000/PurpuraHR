
let getModalFormUrl = $("#annual-leave-form-modal").data("get-modal-content");
let getEditModalFormUrl = $("#annual-leave-form-modal").data("get-edit-modal-content");
let getDeleteModalFormUrl = $("#annual-leave-form-modal").data("get-delete-modal-content");
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

function alterSubmitButton($button, changeForDeleteForm) {
    if (!changeForDeleteForm) {
        if (!$button.hasClass("btn-primary")) {
            $button.removeClass("btn-danger").addClass("btn-primary").text("Save changes");
        }
    }
    else {
        $button.removeClass("btn-primary").addClass("btn-danger").text("Delete leave");
    }
}

$("#annual-leave-form-modal").on("show.bs.modal", function (e) {
    $('.modal-body').html(spinnerHtml);

    let crudMethod = $(e.relatedTarget).data("crud-method");

    if (crudMethod === "create") {
        $.get(getModalFormUrl, function (data) {
            $(".modal-title").text("Book Annual Leave");
            $('.modal-body').html(data);
            $("#leave-form").attr("data-crud-method", "create");
            alterSubmitButton($("#save-changes-button"), false)
        });
    }
    else if (crudMethod === "edit") {
        $.get(getEditModalFormUrl, { externalReference: $(e.relatedTarget).data("external-reference") }, function (data) {
            $(".modal-title").text("Edit Annual Leave");
            $('.modal-body').html(data);
            $("#leave-form").attr("data-crud-method", "edit");
            alterSubmitButton($("#save-changes-button"), false)
        });
    }
    else if (crudMethod === "delete") {
        $.get(getDeleteModalFormUrl, { externalReference: $(e.relatedTarget).data("external-reference") }, function (data) {
            $(".modal-title").text("Delete Annual Leave");
            $('.modal-body').html(data);
            $("#leave-form").attr("data-crud-method", "delete");
            $("#delete-warning").html("<p style='color: red; text-align: center;'>WARNING: there is no way to reverse deletion!</p>");
            alterSubmitButton($("#save-changes-button"), true);

            $("#annual-leave-form-modal input").each(function () {
                $(this).prop("readonly", true);
            })

            $("#annual-leave-form-modal textarea").each(function () {
                $(this).prop("readonly", true);
            })

            $("#annual-leave-form-modal select").each(function () {
                $(this).attr("disabled", true);
            })
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

        let postFormUrl, successMessage;

        switch (crudMethod) {
            case "create":
                successMessage = "Time off successfully booked!";
                postFormUrl = $form.data("create-url");
                break;
            case "edit":
                successMessage = "Time off successfully edited!";
                postFormUrl = $form.data("edit-url");
                break;
            case "delete":
                successMessage = "Time off successfully deleted!";
                postFormUrl = $form.data("delete-url");
                break;
        }

        $(".spinner-container").html(spinnerHtml);

        $.post(postFormUrl, $form.serialize(), function (result) {

            if (result.isSuccess) {
                $("#annual-leave-form-modal").modal("hide");
                toastr.success(successMessage);
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

