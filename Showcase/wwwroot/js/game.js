// game.js

var gameModule = (function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("startGameButton").addEventListener("click", function () {
        // Verberg de "Start Game" knop na klikken
        document.getElementById("startGameButton").style.display = "none";

        // Wanneer op de knop "Start Game" wordt geklikt, voeg de speler toe
        connection.invoke("AddPlayer").catch(function (err) {
            return console.error(err.toString());
        });
    });

    connection.on("playerConnected", function (connectionId, symbol) {
        console.log(`Player ${symbol} connected with ID: ${connectionId}`);
        currentPlayerSymbol = symbol;
        playerSymbols[connectionId] = symbol;

        // Update de UI om te laten zien dat de speler is toegevoegd
        updateBoardUI();

        // Voeg een melding toe aan de pagina
        var notifications = document.getElementById("notifications");
        notifications.innerHTML = `Je bent toegevoegd aan de game. Wachten op de volgende speler.`;
    });

    connection.on("playerDisconnected", function (connectionId) {
        console.log(`Player disconnected: ${connectionId}`);
        // Implementeer logica om de UI bij te werken als een speler de verbinding verbreekt
    });

    connection.on("startGame", function () {
        console.log("Game started");
        // Voeg hier de logica toe om het spel te starten
        initializeBoard();
        updateBoardUI();

        // Verberg de meldingen
        document.getElementById("notifications").innerHTML = "";
    });

    // ... (andere bestaande code)

    function updateBoardUI() {
        // Voeg hier de logica toe om de UI bij te werken, bijvoorbeeld het weergeven van de spelers en het bord
        // ...
    }

    function initializeBoard() {
        // Voeg hier de initiële setup toe, zoals het toevoegen van spelers en het initialiseren van het bord
        // ...
    }

    // ... (andere bestaande functies)

    function init() {
        // Voeg hier de initiële setup toe, zoals het toevoegen van event listeners
        // ...
    }

    return {
        init: init,
    };
})();

document.addEventListener("DOMContentLoaded", () => {
    gameModule.init();
});
