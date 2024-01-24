var gameModule = (function () {
    let connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

    var gameBoard = [];

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("createGameButton").addEventListener("click", function () {
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

    connection.on("startGame", function () {
        document.getElementById("createGameButton").style.display = "none";
        document.getElementById("gameList").style.display = "none";

        console.log("Game started");
        initializeBoard();
        updateBoardUI();
    });

    connection.on("updateCell", function (row, col, symbol) {
        gameBoard[row][col] = symbol === 1 ? 'X' : 'O';
        updateBoardUI();
    });

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

    function initializeBoard() {
        gameBoard = [
            ['', '', ''],
            ['', '', ''],
            ['', '', '']
        ];
    }

    function updateBoardUI() {
        var board = document.getElementById("gameBoard");
        board.innerHTML = "";

        for (var i = 0; i < 3; i++) {
            for (var j = 0; j < 3; j++) {
                var cell = document.createElement("div");
                cell.className = "cell";
                cell.dataset.row = i;
                cell.dataset.col = j;

                cell.textContent = gameBoard[i][j];

                cell.addEventListener("click", function () {
                    onCellClick(this);
                });

                console.log(`Created cell at row ${i}, col ${j} with content: ${cell.textContent}`);

                board.appendChild(cell);
            }
        }
    }

    function onCellClick(cell) {
        var row = parseInt(cell.dataset.row);
        var col = parseInt(cell.dataset.col);
        console.log("Cell clicked:", cell.dataset.row, cell.dataset.col);

        connection.invoke("MakeMove", row, col).catch(function (err) {
            return console.error(err.toString());
        });
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
