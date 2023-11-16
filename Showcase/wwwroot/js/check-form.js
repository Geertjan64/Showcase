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

        errorElement: "span", // Gebruik een span voor foutberichten
        errorClass: "text-danger", // Klasse voor foutberichten

        highlight: function (element, errorClass, validClass) {
            // Voeg rode rand toe aan het ongeldige element
            $(element).addClass("is-invalid").removeClass("is-valid");
        },

        unhighlight: function (element, errorClass, validClass) {
            // Verwijder rode rand van het geldige element
            $(element).removeClass("is-invalid").addClass("is-valid");
        },

        errorPlacement: function (error, element) {
            // Plaats foutbericht naast het element
            error.insertAfter(element);
        },

        onkeyup: function (element, event) {
            // Live validatie tijdens het typen
            this.element(element);
        },

        onfocusout: function (element) {
            // Validatie bij focusverlies
            this.element(element);
        }
    });
});
