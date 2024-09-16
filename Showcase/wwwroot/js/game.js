var gameModule = (function () {
    let connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
    let gameBoard = [];
    let gameFinished;


    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("createGameButton").addEventListener("click", function () {
        console.log("Create game button clicked")
        document.getElementById("gameBoard").style.display = "";
        initializeBoard();
        updateBoardUI();
        

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
        document.getElementById("gameBoard").style.display = "";

        console.log("Game started");
        initializeBoard();
        updateBoardUI();
    });

    connection.on("GameOver", function () {
        console.log("Game finished!");
        gameFinished = true;
        connection.invoke("CheckWinner").catch(function (err) {
            return console.error(err.toString());
        });
        connection.invoke("SaveGame").catch(function (err) {
            return console.error(err .toString());
        });

        document.getElementById("gameHistoryButton").style.display = "block";
    });

    // Toevoegen van click event listener aan de "Spelhistorie" knop
    document.getElementById("gameHistoryButton").addEventListener("click", function () {
        loadGames(); // Laad de spellen wanneer er op de knop wordt geklikt
        document.getElementById("gameHistoryList").style.display = "block"; // Toon de game history list
    });

    connection.on("gameOverMessage", function (message) {
        console.log(message);
        document.getElementById("backToLobbyButton").style.display = "block";
        displayGameMessage(message);
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

        // Voeg een click event listener toe
        joinButton.addEventListener("click", function () {
            // Verwijder de lijstregel zodra erop wordt geklikt
            var listItem = joinButton.parentElement;
            list.removeChild(listItem);

            // Probeer de game te joinen via de server
            connection.invoke("JoinGame", gameId).catch(function (err) {
                return console.error(err.toString());
            });
        });

        var listItem = document.createElement("li");
        listItem.appendChild(joinButton);
        list.appendChild(listItem);
    }

    function loadGames() {
        connection.on("receiveGames", function (games) {
            var list = document.getElementById("gameHistoryList");
            list.innerHTML = ""; // Maak de lijst leeg voordat je nieuwe items toevoegt

            if (games.length === 0) {
                var noGamesMessage = document.createElement("li");
                noGamesMessage.textContent = "No games found.";
                list.appendChild(noGamesMessage);
            } else {
                games.forEach(function (game) {
                    var listItem = document.createElement("li");
                    listItem.textContent = "Game ID: " + game.gameId; // Pas aan op basis van je model

                    list.appendChild(listItem);
                });
            }
        });

        connection.invoke("GetGamesByUser").catch(function (err) {
            return console.error(err.toString());
        });
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

        if (gameBoard[row][col] == '' && !gameFinished) {
            console.log("Cell clicked:", cell.dataset.row, cell.dataset.col);

            connection.invoke("MakeMove", row, col).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }

    function displayGameMessage(message) {
        var gameMessageElement = document.getElementById("notifications");
        gameMessageElement.textContent = message;
        gameMessageElement.style.display = "block";
    }

    document.getElementById("backToLobbyButton").addEventListener("click", function () {
        document.getElementById("notifications").style.display = "none";
        document.getElementById("gameBoard").style.display = "none";
        document.getElementById("createGameButton").style.display = "block";
        document.getElementById("gameList").style.display = "block";
        document.getElementById("backToLobbyButton").style.display = "none";
        document.getElementById("gameHistoryList").style.display = "none";
        document.getElementById("gameHistoryButton").style.display = "none"
        gameBoard = [];
        gameFinished = false;
        connection.invoke("ResetGame").catch(function (err) {
            loadGames();
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
