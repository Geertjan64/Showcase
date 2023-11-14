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
                required: true
            },
            FromEmail: {
                required: true,
                email: true
            },
            Mobile: {
                required: true,
                digits: true
            },
            Body: {
                required: true
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