$(function () {
    grecaptcha.ready(function () {
        grecaptcha.execute('@GoogleCaptchaConfig.Value.SiteKey', { action: 'submit' }).then(function (token) {
            // Add your logic to submit to your backend server here.
            console.log(token);
            document.getElementById("ContactFormModelToken").value = token;
        });
    });
});