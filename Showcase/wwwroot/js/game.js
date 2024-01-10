const GameModule = (function () {

    const gameboardDiv = document.getElementById("gameboard");
    const cellClass = "game-cell";

    const playerX = 'X';
    const playerO = 'O';
    let currentPlayer = playerX;

    let connection;

    function initSignalR() {
        
        connection = new signalR.HubConnectionBuilder()
            .withUrl("/gameHub")
            .build();

        connection.start()
            .then(() => {
                console.log('Connected to GameHub');
                // Start het spel wanneer verbinding is gemaakt
                createGroup();
            })
            .catch((error) => {
                console.error('Error connecting to GameHub:', error);
            });

        connection.on('GroupCreated', (startingPlayer) => {
            console.log(`Group created. First player: ${startingPlayer}`);
            currentPlayer = startingPlayer;
        });

        connection.on('GroupJoined', (joiningPlayer) => {
            console.log(`Player joined. Player: ${joiningPlayer}`);
        });

        connection.on('GameStarted', (startingPlayer) => {
            console.log(`Game started. First player: ${startingPlayer}`);
            currentPlayer = startingPlayer;
        });

        connection.on('Move', (move) => {
            console.log(`Received move: ${move}`);
            updateGameboard(move);
            // Handle the move logic here
        });

        connection.on('UpdateGameboard', (move) => {
            console.log(`Received gameboard update: ${move}`);
            // Handle the gameboard update logic here
            updateGameboard(move);
        });
    }

    function createGroup() {
        // Roep de SignalR-hub aan om het spel te starten
        connection.invoke('CreateGroup')
            .catch((error) => {
                console.error('Error starting game:', error);
            });
    }

    function generateGameboard() {
        for (let i = 0; i < 3; i++) {
            for (let j = 0; j < 3; j++) {
                const cell = document.createElement("div");
                cell.classList.add(cellClass);
                cell.dataset.row = i;
                cell.dataset.col = j;

                cell.addEventListener("click", function () {
                    handleCellClick(i, j);
                });

                cell.addEventListener("mouseover", function () {
                    handleCellHover(i, j);
                });

                cell.addEventListener("mouseout", function () {
                    handleCellUnhover(i, j);
                });

                gameboardDiv.appendChild(cell);
            }
        }
    }

    function handleCellClick(row, col) {
        console.log(`Clicked on row ${row}, column ${col}`);
        const cell = document.querySelector(`.${cellClass}[data-row="${row}"][data-col="${col}"]`);
        if (!cell.classList.contains('occupied')) {
            const imageUrl = currentPlayer === playerX ? 'url(/images/x.png)' : 'url(/images/o.png)';
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

    function handleCellUnhover(row, col) {
        const cell = document.querySelector(`.${cellClass}[data-row="${row}"][data-col="${col}"]`);
        if (!cell.classList.contains('occupied')) {
            cell.style.backgroundImage = 'none';
        }
    }
    function updateGameboard(move) {
        const [row, col] = move.split(',').map(Number);
        const cell = document.querySelector(`.${cellClass}[data-row="${row}"][data-col="${col}"]`);

        if (!cell.classList.contains('occupied')) {
            const imageUrl = currentPlayer === playerX ? 'url(/images/x.png)' : 'url(/images/o.png)';
            cell.style.backgroundImage = imageUrl;
            cell.classList.add('occupied');
            currentPlayer = currentPlayer === playerX ? playerO : playerX;
        }
    }

    return {
        init: function () {
            console.log("Game module initialized");
            initSignalR(); 
            generateGameboard();
        }
    };
})();

document.addEventListener("DOMContentLoaded", function () {
    GameModule.init();
});
