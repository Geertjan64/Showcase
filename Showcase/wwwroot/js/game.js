const GameModule = (function () {
    // Private variables
    const gameboardDiv = document.getElementById("gameboard");
    const cellClass = "game-cell";
    const playerX = 'X';
    const playerO = 'O';
    let currentPlayer = playerX; // Start met speler X

    // Private function to generate the gameboard
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

    // Private function to handle cell click
    function handleCellClick(row, col) {
        console.log(`Clicked on row ${row}, column ${col}`);
        const cell = document.querySelector(`.${cellClass}[data-row="${row}"][data-col="${col}"]`);
        if (!cell.classList.contains('occupied')) {
            const imageUrl = currentPlayer === playerX ? 'url(/images/x.png)' : 'url(/images/o.png)';
            cell.style.backgroundImage = imageUrl;
            cell.classList.add('occupied');
            currentPlayer = currentPlayer === playerX ? playerO : playerX;
        }
    }

    // Private function to handle cell hover
    function handleCellHover(row, col) {
        const cell = document.querySelector(`.${cellClass}[data-row="${row}"][data-col="${col}"]`);
        if (!cell.classList.contains('occupied')) {
            const imageUrl = currentPlayer === playerX ? 'url(/images/x.png)' : 'url(/images/o.png)';
            cell.style.backgroundImage = imageUrl;
            cell.style.backgroundSize = 'cover';
        }
    }

    // Private function to handle cell unhover
    function handleCellUnhover(row, col) {
        const cell = document.querySelector(`.${cellClass}[data-row="${row}"][data-col="${col}"]`);
        if (!cell.classList.contains('occupied')) {
            cell.style.backgroundImage = 'none';
        }
    }

    // Public interface (accessible from outside)
    return {
        init: function () {
            console.log("Game module initialized");
            generateGameboard();
        }
    };
})();

// Start the GameModule when the DOM is loaded
document.addEventListener("DOMContentLoaded", function () {
    GameModule.init();
});
