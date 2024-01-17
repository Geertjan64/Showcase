// game.js

var gameModule = (function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

    var currentPlayerSymbol;
    var playerSymbols = {};
    var gameBoard = [];

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("startGameButton").addEventListener("click", function () {
        document.getElementById("startGameButton").style.display = "none";

        connection.invoke("AddPlayer").catch(function (err) {
            return console.error(err.toString());
        });
    });

    connection.on("playerConnected", function (connectionId, symbol) {
        console.log(`Player ${symbol} connected with ID: ${connectionId}`);
        currentPlayerSymbol = symbol;
        playerSymbols[connectionId] = symbol;

        // Initialize the game board if not already initialized
        if (gameBoard.length === 0) {
            initializeBoard();
        }

        updateBoardUI();

        var notifications = document.getElementById("notifications");
        notifications.innerHTML = `You are added to the game. Waiting for the next player.`;
    });

    connection.on("playerDisconnected", function (connectionId) {
        console.log(`Player disconnected: ${connectionId}`);
        // Implement logic to update the UI when a player disconnects
    });

    connection.on("startGame", function () {
        console.log("Game started");
        initializeBoard();
        updateBoardUI();
        document.getElementById("notifications").innerHTML = "";
    });

    connection.on("updateCell", function (row, col, symbol) {
        gameBoard[row][col] = symbol;
        updateBoardUI();
    });

    connection.on("gameOver", function (winner) {
        document.getElementById("notifications").innerHTML = winner
            ? `Player ${winner} wins!`
            : "It's a draw!";
        document.getElementById("restartGameButton").style.display = "block";
    });

    function updateBoardUI() {
        var board = document.getElementById("gameBoard");

        if (!board) {
            board = document.createElement("div");
            board.id = "gameBoard";
            document.body.appendChild(board);
        }

        // Clear existing cells
        board.innerHTML = "";

        for (var i = 0; i < 3; i++) {
            for (var j = 0; j < 3; j++) {
                var cell = document.createElement("div");
                cell.className = "cell";
                cell.dataset.row = i;
                cell.dataset.col = j;

                // Set the text content based on the symbol
                cell.textContent = gameBoard[i][j] || "";

                cell.addEventListener("click", function () {
                    onCellClick(this);
                });

                console.log(`Created cell at row ${i}, col ${j} with content: ${cell.textContent}`);

                board.appendChild(cell);
            }
        }
    }

    function initializeBoard() {
        gameBoard = [
            ['', '', ''],
            ['', '', ''],
            ['', '', '']
        ];
    }

    function onCellClick(cell) {
        var row = parseInt(cell.dataset.row);
        var col = parseInt(cell.dataset.col);
        console.log("Cell clicked:", cell.dataset.row, cell.dataset.col);

        if (gameBoard[row][col] === "" && currentPlayerSymbol) {
            connection.invoke("MakeMove", row, col, currentPlayerSymbol).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }

    document.getElementById("restartGameButton").addEventListener("click", function () {
        connection.invoke("RestartGame").catch(function (err) {
            return console.error(err.toString());
        });
    });

    function init() {
        document.addEventListener("DOMContentLoaded", () => {
            gameModule.init();
        });
    }

    return {
        init: init,
    };
})();

gameModule.init();
