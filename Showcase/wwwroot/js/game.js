var gameModule = (function () {
    let connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

    let currentPlayerSymbol;
    let currentPlayerId;

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

        // Stel firstPlayerId alleen in als het nog niet is ingesteld
        if (!currentPlayerId) {
            currentPlayerId = connectionId;
            currentPlayerSymbol = symbol;
            console.log(`Starting player: ${currentPlayerId} met symbool ${currentPlayerSymbol}`);
        }

        playerSymbols[connectionId] = symbol;

        // Initialize the game board if not already initialized
        if (gameBoard.length === 0) {
            initializeBoard();
        }

        var notifications = document.getElementById("notifications");
        notifications.innerHTML = `You are added to the game. Waiting for the next player.`;
    });

    connection.on("startGame", function () {
       
        console.log("Game started");
        initializeBoard();
        updateBoardUI();
        
        document.getElementById("notifications").innerHTML = 'Game started. Starting player: ${startingPlayerId}';
    });

    connection.on("updateCell", function (row, col, symbol) {
        gameBoard[row][col] = symbol;
        updateBoardUI();
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

        // Controleer of de huidige speler de startende speler is
        var isStartingPlayer = currentPlayerId && playerSymbols[currentPlayerId] === currentPlayerSymbol;

        // Voer de zet alleen uit als de cel leeg is en de huidige speler de startende speler is
        if (gameBoard[row][col] === "" && isStartingPlayer) {
            connection.invoke("MakeMove", row, col, currentPlayerSymbol).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }


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
