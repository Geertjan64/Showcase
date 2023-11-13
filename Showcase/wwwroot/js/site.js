// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

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