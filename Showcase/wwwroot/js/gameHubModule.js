const gameHubModule = (function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/gameHub")
        .build();

    connection.start().then(function () {
        console.log("Connected to GameHub");
    }).catch(function (err) {
        console.error("Error connecting to GameHub:", err);
    });

    connection.on("GameStarted", function (player) {
        console.log(`Game started. Current player: ${player}`);
        // Voeg hier eventuele logica toe die wordt uitgevoerd wanneer een nieuw spel begint
    });

    return {
        getConnection: function () {
            return connection;
        }
    };
})();