export const GameNotifications = (function () {
    function showPlayerDisconnected(playerId) {
        document.getElementById("notifications").textContent = `Speler: ${playerId} heeft het spel verlaten D: )`;
        document.getElementById("notifications").style.display = "block";
    }

    function displayGameMessage(message) {
        var gameMessageElement = document.getElementById("notifications");
        gameMessageElement.textContent = message;
        gameMessageElement.style.display = "block";
    }

    function hideNotifications() {
        var gameMessageElement = document.getElementById("notifications");
        gameMessageElement.style.display = "none";
    }

    return {
        showPlayerDisconnected,
        displayGameMessage,
        hideNotifications
    };
})();