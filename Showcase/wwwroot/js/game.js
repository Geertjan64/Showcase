var gameModule = (function () {
    let connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

    let currentPlayerSymbol;
    let currentPlayerId;

    var gameBoard = [];

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("createGameButton").addEventListener("click", function () {
        document.getElementById("createGameButton").style.display = "none";
        document.getElementById("gameList").style.display = "none";

        connection.invoke("CreateGame").catch(function (err) {
            return console.error(err.toString());
        });
    });

    connection.on("gameCreated", function (gameId) {
        console.log(`Game created with ID: ${gameId}`);
        createJoinButton(gameId);
    });

    connection.on("gameJoined", function (gameId, playerId) {
        console.log(`Player ${playerId} joined game with ID: ${gameId}`);
    });

    connection.on("playerConnected", function (connectionId, symbol) {
        console.log(`Player ${symbol} connected with ID: ${connectionId}`);

        if (!currentPlayerId) {
            currentPlayerId = connectionId;
            currentPlayerSymbol = symbol;
            console.log(`Starting player: ${currentPlayerId} met symbool ${currentPlayerSymbol}`);
        }

        playerSymbols[connectionId] = symbol;

        if (gameBoard.length === 0) {
            initializeBoard();
        }

        var notifications = document.getElementById("notifications");
        notifications.innerHTML = `You are added to the game. Waiting for the next player.`;
    });

    connection.on("startGame", function () {
        startGame();
    });

    connection.on("updateCell", function (row, col, symbol) {
        gameBoard[row][col] = symbol;
        updateBoardUI();
    });

    function startGame() {
        console.log("Game started");
        initializeBoard();
        updateBoardUI();
    }
 
    function createJoinButton(gameId) {
        var list = document.getElementById("availableGames");
        var joinButton = document.createElement("button");
        joinButton.textContent = "Join game " + gameId;
        joinButton.id = gameId;
        joinButton.addEventListener("click", function () {
            connection.invoke("JoinGame", gameId).catch(function (err) {
                return console.error(err.toString());
            });
        });

        var listItem = document.createElement("li");
        listItem.appendChild(joinButton);
        list.appendChild(listItem);
    }

    function updateBoardUI() {
        var board = document.getElementById("gameBoard");

        if (!board) {
            board = document.createElement("div");
            board.id = "gameBoard";
            document.body.appendChild(board);
        }

        board.innerHTML = "";

        for (var i = 0; i < 3; i++) {
            for (var j = 0; j < 3; j++) {
                var cell = document.createElement("div");
                cell.className = "cell";
                cell.dataset.row = i;
                cell.dataset.col = j;

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

        var isStartingPlayer = currentPlayerId && playerSymbols[currentPlayerId] === currentPlayerSymbol;

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
