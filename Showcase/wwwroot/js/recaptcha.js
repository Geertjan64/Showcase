$(function () {
    grecaptcha.ready(function () {
        grecaptcha.execute('@GoogleCaptchaConfig.Value.SiteKey', { action: 'submit' }).then(function (token) {
            console.log(token);
            document.getElementById("ContactFormModelToken").value = token;
        });
    });
});