document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("contactForm");

    const validateInput = (input) => {
        const errorElement = document.getElementById(input.name + "Error");

        if (!errorElement) {
            console.error("Error element not found for: " + input.name);
            return;
        }

        errorElement.textContent = "";

        if (!input.value) {
            errorElement.textContent = "Dit veld is verplicht.";
            input.classList.add("is-invalid");
            return false;
        }

        switch (input.name) {
            case "FromEmail":
                if (!/\S+@\S+\.\S+/.test(input.value)) {
                    errorElement.textContent = "Voer een geldig e-mailadres in.";
                    input.classList.add("is-invalid");
                    return false;
                }
                break;

            case "Mobile":
                if (!/^\d*$/.test(input.value)) { 
                    errorElement.textContent = "Voer alleen cijfers in.";
                    input.classList.add("is-invalid");
                    return false;
                }
                break;

            default:
                if (input.value.length > input.maxLength) {
                    errorElement.textContent = `Maximale lengte is ${input.maxLength} karakters.`;
                    input.classList.add("is-invalid");
                    return false;
                }
        }

        input.classList.remove("is-invalid");
        input.classList.add("is-valid");
        return true;
    };

    form.querySelectorAll("input, textarea").forEach((input) => {
        input.addEventListener("input", () => validateInput(input));
        input.addEventListener("blur", () => validateInput(input));
    });

    form.addEventListener("submit", (event) => {
        let valid = true;
        form.querySelectorAll("input, textarea").forEach((input) => {
            if (!validateInput(input)) {
                valid = false;
            }
        });

        if (!valid) {
            event.preventDefault();
        }
    });
});
