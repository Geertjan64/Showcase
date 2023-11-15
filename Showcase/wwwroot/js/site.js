// Write your JavaScript code.
function checkCookiePermission() {
    var consentCookie = document.cookie.replace(/(?:(?:^|.*;\s*)\.AspNet\.Consent\s*=\s*([^;]*).*$)|^.*$/, "$1");

    var page = document.querySelector('.page');

    if (consentCookie === "yes" && page) {
        page.classList.add('cookies-accepted');
    }
}

window.addEventListener('load', function () {
    checkCookiePermission();
});