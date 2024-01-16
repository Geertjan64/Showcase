var gameModule = (function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    var currentPlayerSymbol = null; // Voeg deze regel toe
    var playerSymbols = {};

    connection.on("playerConnected", function (connectionId, symbol) {
        console.log(`Player ${symbol} connected with ID: ${connectionId}`);
        currentPlayerSymbol = symbol;
        playerSymbols[connectionId] = symbol;
        updateBoardUI();
    });

    connection.on("playerDisconnected", function (connectionId) {
        console.log(`Player disconnected: ${connectionId}`);
    });

    connection.on("startGame", function () {
        console.log("Game started");
        updateBoardUI();
    });

    connection.on("moveMade", function (connectionId, row, col) {
        console.log(`Move made by ${connectionId} at position (${row}, ${col})`);
        board[row][col] = playerSymbols[connectionId];
        updateBoardUI();
    });

    connection.on("gameEnd", function () {
        console.log("Game ended");
        // Implement any additional logic for the end of the game
    });

    // Implementeer de logica om zetten van de speler te verwerken en door te geven aan de backend
    // Update de UI met het huidige bord en andere relevante informatie
    // Function to update the UI based on the current state of the board
    function updateBoardUI() {
        var boardElement = document.getElementById("board");
        boardElement.innerHTML = "";

        for (var i = 0; i < 3; i++) {
            for (var j = 0; j < 3; j++) {
                var cellIndex = i * 3 + j;

                var cell = document.createElement("div");
                cell.classList.add("cell");
                cell.dataset.index = cellIndex;

                if (board[cellIndex]) {
                    cell.classList.add(board[cellIndex]); // Voeg deze regel toe
                } else {
                    cell.addEventListener("click", function () {
                        if (currentPlayerSymbol && currentPlayerSymbol === playerSymbols[connection.connectionId]) {
                            var index = parseInt(this.dataset.index);
                            makeMove(index);
                        }
                    });
                }

                boardElement.appendChild(cell);
            }
        }
    }

    function startGame() {
        // Voer hier de initiële setup uit, zoals het toevoegen van spelers
        // en het weergeven van het bord
        updateBoardUI();
    }

    function init() {
        // Roep de startGame-functie aan om het spel te starten
        startGame();
    }

        return {
            init: init,
        // Eventueel kun je hier methoden toevoegen die je nodig hebt in de frontend
    };
})();

document.addEventListener("DOMContentLoaded", () => {
    gameModule.init();
});
