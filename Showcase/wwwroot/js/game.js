const GameModule = (function () {
    // Private variables
    const gameboardDiv = document.getElementById("gameboard");
    const cellClass = "game-cell";

    // Private function to generate the gameboard
    function generateGameboard() {
        for (let i = 0; i < 3; i++) {
            for (let j = 0; j < 3; j++) {
                const cell = document.createElement("div");
                cell.classList.add(cellClass);
                cell.dataset.row = i;
                cell.dataset.col = j;

                cell.addEventListener("click", function () {
                    // Add logic here for cell click
                    handleCellClick(i, j);
                });

                gameboardDiv.appendChild(cell);
            }
        }
    }

    // Private function to respond to cell click
    function handleCellClick(row, col) {
        console.log(`Clicked on row ${row}, column ${col}`);
        // Add further logic for cell click
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