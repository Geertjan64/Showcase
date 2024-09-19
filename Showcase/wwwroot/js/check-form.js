$(document).ready(function () {
    $("#contactForm").validate({
        rules: {
            FirstName: {
                required: true
            },
            LastName: {
                required: true
            },
            Subject: {
                required: true,
                maxlength: 200
            },
            FromEmail: {
                required: true,
                email: true
            },
            Mobile: {
                required: true,
                digits: true,
                maxlength: 20

            },
            Body: {
                required: true,
                maxlength: 600

            }
        },
        messages: {
            FirstName: "Voer uw voornaam in",
            LastName: "Voer uw achternaam in",
            Subject: "Voer het onderwerp in",
            FromEmail: {
                required: "Voer uw e-mailadres in",
                email: "Voer een geldig e-mailadres in"
            },

            Mobile: {
                required: "Voer uw mobiele nummer in",
                digits: "Voer alleen cijfers in"
            },
            Body: "Voer uw bericht in"
        },

        errorElement: "span", 
        errorClass: "text-danger",

        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },

        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid").addClass("is-valid");
        },

        errorPlacement: function (error, element) {
            error.insertAfter(element);
        },

        onkeyup: function (element, event) {
            this.element(element);
        },

        onfocusout: function (element) {
            this.element(element);
        }
    });
});
