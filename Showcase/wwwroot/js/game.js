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

        connection.on('UpdateGameStatus', (nextPlayer) => {
            console.log(`Next player: ${nextPlayer}`);
            if (nextPlayer === currentPlayer) {
                // Jouw beurt, voer hier de nodige acties uit
            } else {
                // Niet jouw beurt, voer hier optionele acties uit (bijv. het bord vergrendelen)
            }
        });

        connection.on('GroupJoined', (joiningPlayer) => {
            console.log(`Player joined. Player: ${joiningPlayer}`);
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

    function handleCellClick(row, col) {
        const cell = document.querySelector(`.${cellClass}[data-row="${row}"][data-col="${col}"]`);

        if (!cell.classList.contains('occupied')) {
            const imageUrl = currentPlayer === playerX ? 'url(/images/x.png)' : 'url(/images/o.png)';

            // Check of het de beurt van de huidige speler is voordat een zet wordt geplaatst
            if (currentPlayer != playerO) {
                console.log("Het is niet jouw beurt!");
                return;
            }

            cell.style.backgroundImage = imageUrl;
            cell.classList.add('occupied');
            currentPlayer = currentPlayer === playerX ? playerO : playerX;
            connection.invoke('MakeMove', `${row},${col}`);
        }
    }



    function handleCellHover(row, col) {
        const cell = document.querySelector(`.${cellClass}[data-row="${row}"][data-col="${col}"]`);
        if (!cell.classList.contains('occupied')) {
            const imageUrl = currentPlayer === playerX ? 'url(/images/x.png)' : 'url(/images/o.png)';
            cell.style.backgroundImage = imageUrl;
            cell.style.backgroundSize = 'cover';
        }
    }

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
