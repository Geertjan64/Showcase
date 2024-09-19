$(document).ready(function () {
    $("#contactForm").submit(function (e) {
        e.preventDefault();
        var form = $(this);

        $.ajax({
            url: "/api/Mail/send",
            type: "POST",
            data: form.serialize(),
            success: function (data) {
                if (data.success) {
                    alert(data.message);
                    form[0].reset();
                    form.validate().resetForm();
                } else {
                    alert("Er is een fout opgetreden: " + data.message);
                }
            },
            error: function () {
                alert("Er is een fout opgetreden bij het verzenden van het formulier.");
            }
        });
    });
});