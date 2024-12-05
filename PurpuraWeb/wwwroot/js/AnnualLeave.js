
$("#save-changes-button").on("click", function () {
    let $form = $("#leave-form");

    $form.on("submit", function (e) {
        e.preventDefault();

        var url = $form.data("action-url");

        $.post(url, $form.serialize(), function (success) {
            if (success) alert("success!")
            else alert("failure...")
        })
    });

    $form.trigger("submit");
})