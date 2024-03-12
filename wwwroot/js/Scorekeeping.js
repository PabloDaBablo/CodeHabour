var selectedPlayerId;
let actionQueue = [];

$(document).ready(function () {
    GetPlayers(gameId);
    GetPlayersHome(gameId)
    document.getElementById('team2Dropdown').addEventListener('change', function () {
        const playerId = this.value;
        if (playerId === "0") return; 

        const selectedPlayer = playersData.find(player => player.id.toString() === playerId);
        if (selectedPlayer) {
            addPlayerToField(selectedPlayer.id, selectedPlayer.playerLastName, selectedPlayer.playerNumber, 200, 280); 

            if (playerPositions[0] === null) {
                playerPositions[0] = selectedPlayer.id;
            }

           
            drawField();
        }
    });

    if (gameId) {
        updateTeamsPlaying(gameId);
    }

});
function GetPlayers(gameId) {
    $.ajax({
        url: `/Scorekeeping/GetPlayers?gameId=${gameId}`,
        type: 'GET',
        dataType: 'json',
        success: function (players) {

            
        },
        error: function (xhr, status, error) {
            alert("An error occurred: " + error);
        }
    });
}

function GetPlayersHome(gameId) {
    $.ajax({
        url: `/Scorekeeping/GetPlayersHome?gameId=${gameId}`,
        type: 'GET',
        dataType: 'json',
        success: function (players) {

            
        },
        error: function (xhr, status, error) {
            alert("An error occurred: " + error);
        }
    });
}

/*Counters for Balls, Strikes, Innings, Outs*/
document.addEventListener('DOMContentLoaded', function () {
    const INNING_LIMIT = 7;
    var strikeButton = document.getElementById("strikeNumberButton");
    var ballButton = document.getElementById("ballNumberButton");
    var inningSpan = document.getElementById("inningNumber");

    function checkInningLimit() {
        var inningCount = parseInt(inningSpan.textContent, 10);
        return inningCount < INNING_LIMIT;
    }

    //gameOverMessageDisplay

    strikeButton.addEventListener("click", function () {
        if (!checkInningLimit()) {
            document.getElementById("gameOverMessageDisplay").textContent = "Game Over"
            return;
        }

        var strikeCountSpan = document.getElementById("strikeCount");
        var strikeCount = parseInt(strikeCountSpan.textContent, 10);
        strikeCount++;
        if (strikeCount >= 3) {
            var outCountSpan = document.getElementById("outNumber");
            var outCount = parseInt(outCountSpan.textContent, 10);
            outCount++;
            if (outCount > 3) {
                outCount = 0;
                if (document.getElementById("inningTopOrBottom").textContent == "Top of") {
                    document.getElementById("inningTopOrBottom").textContent = "Bottom of";
                } else if (document.getElementById("inningTopOrBottom").textContent == "Bottom of") {
                    var inningCount = parseInt(inningSpan.textContent, 10);
                    inningCount++;
                    inningSpan.textContent = inningCount;
                    document.getElementById("inningTopOrBottom").textContent = "Top of";
                }
            }
            outCountSpan.textContent = outCount;
            strikeCount = 0;
        }

        strikeCountSpan.textContent = strikeCount;
    });

    ballButton.addEventListener("click", function () {
        if (!checkInningLimit()) {
            document.getElementById("gameOverMessageDisplay").textContent = "Game Over"
            return;
        }

        var ballCountSpan = document.getElementById("ballCount");
        var ballCount = parseInt(ballCountSpan.textContent, 10);
        ballCount++;

        if (ballCount >= 4) {
            console.log("walk");
            ballCount = 0;
        }

        ballCountSpan.textContent = ballCount;
    });
});


/*Change games and lineups*/
/*document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('team1Dropdown').addEventListener('change', function () {
        var selectedGameText = this.options[this.selectedIndex].text;
        document.getElementById('teamsPlaying').textContent = selectedGameText;
    });
});
*/

function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split('&');
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split('=');
        if (decodeURIComponent(pair[0]) == variable) {
            return decodeURIComponent(pair[1]);
        }
    }
    console.log('Query variable %s not found', variable);
}

var gameId = getQueryVariable('gameId');

function updateTeamsPlaying(gameId) {
    $.ajax({
        url: '/Scorekeeping/GetGameDetails', 
        data: { gameId: gameId },
        success: function (data) {
            $('#teamsPlaying').text(data.homeTeamName + " vs " + data.awayTeamName);
        },
        error: function (error) {
            console.error("Error fetching game details:", error);
            $('#teamsPlaying').text("Error loading teams");
        }
    });
}


document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('baseball-image').addEventListener('click', function (event) {
        var menu = document.getElementById('popup-menu');
        menu.style.left = event.pageX + 'px';
        menu.style.top = event.pageY + 'px';
        menu.style.display = 'block';
    });

    document.addEventListener('click', function (event) {
        var menu = document.getElementById('popup-menu');
        if (event.target.id !== 'baseball-image') {
            menu.style.display = 'none';
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {
    fetchLineup(gameId);
});

var playersData = []; 

function fetchLineup(gameId) {
    fetch(`/Scorekeeping/GetPlayersHome?gameId=${gameId}`)
        .then(response => response.json())
        .then(data => {
            var lineupDropdown = document.getElementById('team2Dropdown');
            lineupDropdown.innerHTML = '<option value="0" selected>Select a player...</option>';
            let gamePlayedPromises = [];

            data.forEach(player => {
                var option = new Option(player.playerLastName, player.id);
                lineupDropdown.options.add(option);
                gamePlayedPromises.push(incrementGamesPlayed(player.id));
                logAction('GamePlayed', { playerId: player.id }, gameId);
                saveGameState();
            });

            Promise.all(gamePlayedPromises)
                .then(() => {
                    playersData = data;
                    placeFirstPlayer(); 
                })
                .catch(error => console.error('Error incrementing games played for lineup:', error));
        })
        .catch(error => console.error('Error fetching lineup:', error));
}

function placeFirstPlayer() {
    if (playersData.length > 0) {
        const firstPlayerId = playersData[0].id;
        playerPositions[0] = firstPlayerId;
        addPlayerToField(firstPlayerId, playersData[0].playerLastName, playersData[0].playerNumber, getBaseCoordinates(0).x, getBaseCoordinates(0).y);
        logAction('PlayerPlaced', { playerId: firstPlayerId }, gameId);
        drawField();
        saveGameState();

        incrementPlateAppearances(firstPlayerId)
            .then(() => console.log('Incremented Plate Appearances for the first player.'))
            .catch(error => console.error('Error incrementing plate appearances for the first player:', error));
    }
}
function addPlayerToField(playerId, lastName, number, startX, startY) {
    const svgNs = "http://www.w3.org/2000/svg";
    let svgContainer = document.querySelector("svg");
    let playerIconUrl = "/Images/icon.png"; 

    let playerGroup = document.createElementNS(svgNs, "g");
    playerGroup.setAttribute("id", `player-group-${playerId}`);
    playerGroup.setAttribute("class", "player-group");
    playerGroup.setAttribute("data-player-id", playerId); 

    let playerImage = document.createElementNS(svgNs, "image");
    playerImage.setAttributeNS("http://www.w3.org/1999/xlink", "href", playerIconUrl);
    playerImage.setAttribute("x", startX);
    playerImage.setAttribute("y", startY);
    playerImage.setAttribute("height", "30");
    playerImage.setAttribute("width", "30");
    playerGroup.appendChild(playerImage);

    let playerNameText = document.createElementNS(svgNs, "text");
    playerNameText.setAttribute("x", startX);
    playerNameText.setAttribute("y", startY + 40);
    playerNameText.setAttribute("font-family", "Verdana");
    playerNameText.setAttribute("font-size", "10");
    playerNameText.setAttribute("fill", "black");
    playerNameText.textContent = lastName;
    playerGroup.appendChild(playerNameText);

    let playerNumberText = document.createElementNS(svgNs, "text");
    playerNumberText.setAttribute("x", startX);
    playerNumberText.setAttribute("y", startY + 50);
    playerNumberText.setAttribute("font-family", "Verdana");
    playerNumberText.setAttribute("font-size", "10");
    playerNumberText.setAttribute("fill", "black");
    playerNumberText.textContent = number;
    playerGroup.appendChild(playerNumberText);

    playerGroup.addEventListener('click', function () {
        selectPlayer(playerId.toString()); 
    });

    svgContainer.appendChild(playerGroup);
};


let playerPositions = [null, null, null, null]; 

let score = 0; 

let strikeCount = 0; 

let playerIdSVG = 0;

document.getElementById('strikeNumberButton').addEventListener('click', function () {
    strikeCount += 1; 

    if (strikeCount >= 3) {
        if (playerPositions[0] !== null) {
            removePlayerSVG(playerPositions[0]);
            playerStrikedOut(playerPositions[0]); 
            playerPositions[0] = null; 
        }
        strikeCount = 0; 
    }

    
});
function clearPlayerIcons() {
    const existingIcons = document.querySelectorAll(".player-group");
    existingIcons.forEach(icon => icon.remove());
}
function advancePlayerBaseHit(hitType) {
    saveGameState();
    if (!playerIdSVG) {
        alert("No player selected!");
        return;
    }

    
    let newPositionIndex;
    switch (hitType) {
        case '1B':
            newPositionIndex = 1; 
            logAction('Single', { playerId: playerIdSVG }, gameId)
            updatePlayerBaseHit(playerIdSVG, hitType)
            break;
        case '2B':
            newPositionIndex = 2; 
            logAction('Double', { playerId: playerIdSVG }, gameId)
            updatePlayerBaseHit(playerIdSVG, hitType)
            break;
        case '3B':
            newPositionIndex = 3; 
            logAction('Triple', { playerId: playerIdSVG }, gameId)
            updatePlayerBaseHit(playerIdSVG, hitType)
            break;
        case 'HR':
            newPositionIndex = 0; 
            logAction('HomeRun', { playerId: playerIdSVG }, gameId)
            handleHomeRun(playerIdSVG); 
            break;
        default:
            console.error("Invalid hit type:", hitType);
            return;
    }

    
    const currentPlayerIndex = playerPositions.findIndex(id => id === parseInt(playerIdSVG));
    if (currentPlayerIndex !== -1) {
        playerPositions[currentPlayerIndex] = null; 
    }
    if (newPositionIndex !== 0) { 
        playerPositions[newPositionIndex] = parseInt(playerIdSVG);
    }

    
    movePlayerToBase(playerIdSVG, newPositionIndex);
    loadPlayerOntoHomeBase();
    drawField(); 
}

function handleHomeRun(playerId) {
    updatePlayerBaseHit(playerId, 'HR');
    removePlayerSVG(playerId);
    score++
    saveGameState();
    document.getElementById('score').textContent = `Home Score: ${score}`;  
}

function movePlayerToBase(playerId, baseIndex) {
    const baseCoords = getBaseCoordinates(baseIndex);
    let playerGroup = document.querySelector(`#player-group-${playerId}`);
    if (playerGroup) {
        let playerImage = playerGroup.querySelector("image");
        let playerNameText = playerGroup.querySelector("text");

        playerImage.setAttribute("x", baseCoords.x);
        playerImage.setAttribute("y", baseCoords.y);
        playerNameText.setAttribute("x", baseCoords.x);
        playerNameText.setAttribute("y", baseCoords.y + 40); 

        console.log(`Moved player ${playerId} to new position.`);
    } else {
        console.error(`Player group with ID ${playerId} not found.`);
    }
}

function removePlayerFromFirstBase() {
    if (playerPositions[1] !== null) { 
        removePlayerSVG(playerPositions[1]); 
        playerPositions[1] = null; 
        console.log('Player removed from first base');
        updatePlayerPositionsSVG(); 
    }
    drawField();
};

function updatePlayerPositionsSVG() {
    const baseCoords = [
        { x: 200, y: 300 }, // Home
        { x: 300, y: 200 }, // 1st Base
        { x: 200, y: 100 }, // 2nd Base
        { x: 100, y: 200 }  // 3rd Base
    ];

    clearPlayerIcons();

    playerPositions.forEach((playerId, index) => {
        if (playerId !== null) {
            const player = playersData.find(p => p.id.toString() === playerId.toString());
            if (player) {
                addPlayerToField(player.id, player.playerLastName, player.playerNumber, baseCoords[index].x, baseCoords[index].y);
            }
        }
    });

    document.getElementById('score').textContent = `Home Score: ${score}`;
}

function drawField() {
    clearPlayerIcons(); 

    playerPositions.forEach((playerId, index) => {
        if (playerId) {
            const playerData = playersData.find(p => p.id === playerId);
            if (playerData) {
                const coords = getBaseCoordinates(index);
                addPlayerToField(playerId, playerData.playerLastName, playerData.playerNumber, coords.x, coords.y);
            }
        }
    });

    
    console.log('Field redrawn with player positions:', playerPositions);
}

function getBaseCoordinates(index) {
    
    const baseCoords = [
        { x: 200, y: 300 }, // Home
        { x: 300, y: 200 }, // 1st Base
        { x: 200, y: 100 }, // 2nd Base
        { x: 100, y: 200 }  // 3rd Base
    ];
    return baseCoords[index];
}



function removePlayerSVG(playerId) {
    document.getElementById(`player-icon-${playerId}`)?.remove();
    document.getElementById(`player-name-${playerId}`)?.remove();
    document.getElementById(`player-number-${playerId}`)?.remove();
};


function playerStrikedOut(playerId) {
    $.ajax({
        url: '/Scorekeeping/PlayerStruckOut',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ playerId: playerId }),
        success: function (response) {
            console.log('Player striked out.', response);
        },
        error: function (error) {
            console.error('Error updating player strikeout.', error);
        }
    });
}
function playerScored(playerId) {
    score++;
    document.getElementById('score').textContent = `Home Score: ${score}`;

    $.ajax({
        url: '/Scorekeeping/PlayerScored',
        type: 'POST',
        contentType: 'application/json', 
        data: JSON.stringify({ PlayerId: playerId }), 
        success: function (response) {
            console.log('Player scored a run.', response);
        },
        error: function (error) {
            console.error('Error updating player score.', error);
        }
    });
}


document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('strikeNumberButton').addEventListener('click', function () {

        removePlayerFromFirstBase();
        
    });
});


function selectPlayer(playerId) {
    
    if (selectedPlayerId === playerId) {
        document.querySelectorAll('.player-group').forEach(group => {
            if (group.getAttribute('data-player-id') === playerId) {
                group.classList.toggle('selected');
            }
        });
        playerIDSVG = null; 
        console.log(`Player ${playerId} deselected`);
    } else {
        
        document.querySelectorAll('.player-group.selected').forEach(group => {
            group.classList.remove('selected');
        });
        
        const selectedGroup = document.querySelector(`#player-group-${playerId}`);
        if (selectedGroup) {
            selectedGroup.classList.add('selected');
            selectedPlayerId = playerId;
            playerIdSVG = playerId;
            console.log(`Player ${playerId} selected`);
        } else {
            console.error(`Player group with ID ${playerId} not found.`);
        }
    }
}

function removePlayerFromField() {
    if (!playerIdSVG) {
        alert("No player selected!");
        return;
    }

    
    const playerIndex = playerPositions.indexOf(parseInt(playerIdSVG));

    const playerGroup = document.getElementById(`player-group-${playerIdSVG}`);
    if (playerGroup) {
        playerGroup.remove();
        console.log(`Player ${playerIdSVG} removed from field.`);

        if (playerIndex !== -1) {
            playerPositions[playerIndex] = null;
        }

        selectedPlayerId = null;
        drawField(); 
    } else {
        console.error(`Player group with ID ${playerIdSVG} not found.`);
    }
}

document.getElementById('player-out').addEventListener('click', function () {
    removePlayerFromField();
    document.getElementById('popup-menu').style.display = 'none';
});

document.getElementById('baseball-image').addEventListener('click', function (event) {
    var menu = document.getElementById('popup-menu');
    menu.style.left = event.pageX + 'px';
    menu.style.top = event.pageY + 'px';
    menu.style.display = 'block';

    event.stopPropagation();
});


document.addEventListener('click', function (event) {
    if (!event.target.closest('.player-group') && !event.target.closest('#popup-menu') && !event.target.closest('#baseball-image')) {
        if (selectedPlayerId) {
            deselectPlayer(); 
        }
    }
}, true);
document.addEventListener('click', function (event) {
    const menu = document.getElementById('popup-menu');
    
    if (!event.target.closest('#popup-menu') && !event.target.closest('.player-group') && !event.target.closest('#baseball-image')) {
        menu.style.display = 'none';
    }
});

function deselectPlayer() {
    
    if (selectedPlayerId) {
        const selectedGroup = document.querySelector(`#player-group-${selectedPlayerId}`);
        if (selectedGroup) {
            selectedGroup.classList.remove('selected');
        }
        console.log(`Player ${selectedPlayerId} deselected`);
        selectedPlayerId = null; 
    }
}

function updatePlayerBaseHit(playerId, baseHitType) {
    $.ajax({
        url: '/Scorekeeping/PlayerBaseHit',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ PlayerId: playerId, BaseHitType: baseHitType }),
        success: function (response) {
            console.log("Base hit updated successfully", response);
            drawField()
        },
        error: function (xhr, status, error) {
            console.error("Error updating base hit", error);
        }
    });
}
//TImer
function startTimer(duration, displayElement, messageElement, ballButton, strikeButton, addRunToAwayTeamButton, undoRunToAwayTeamButton) {
    var endTime = new Date(Date.now() + duration);

    var timerInterval = setInterval(function () {
        var remainingTime = endTime - Date.now();
        var hours = Math.floor((remainingTime % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((remainingTime % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((remainingTime % (1000 * 60)) / 1000);

        displayElement.textContent = `${hours}h ${minutes}m ${seconds}s`;

        if (remainingTime < 0) {
            clearInterval(timerInterval);
            displayElement.textContent = 'Time Up!';
            messageElement.textContent = 'Game Over';
            ballButton.disabled = true;
            strikeButton.disabled = true;
            addRunToAwayTeamButton.disabled = true;
            undoRunToAwayTeamButton.disabled = true;
        }
    }, 1000);
};

function advanceToNextBase() {
    saveGameState();

    if (!playerIdSVG) {
        alert("No player selected!");
        return;
    }

    const currentPlayerIndex = playerPositions.findIndex(id => id === parseInt(playerIdSVG));
    if (currentPlayerIndex === -1) {
        alert("Selected player is not on a base.");
        return;
    }

    let nextPositionIndex = currentPlayerIndex + 1;
    if (nextPositionIndex > 3) {

        nextPositionIndex = 0;
        logAction('Run', { playerId: playerIdSVG }, gameId)
        playerScored(playerIdSVG);
        saveGameState();
    }


    playerPositions[currentPlayerIndex] = null;
    if (nextPositionIndex !== 0) {
        playerPositions[nextPositionIndex] = parseInt(playerIdSVG);
    }

    
    movePlayerToBase(playerIdSVG, nextPositionIndex);
    loadPlayerOntoHomeBase();
    drawField();
}

// Start the timer with a 10-second countdown
document.addEventListener('DOMContentLoaded', function () {
    var displayElement = document.getElementById('clock');
    var messageElement = document.getElementById('gameOverMessageDisplay');
    var ballButton = document.getElementById('ballNumberButton');
    var strikeButton = document.getElementById('strikeNumberButton');
    var addRunToAwayTeamButton = document.getElementById('addToAwayScore');
    var undoRunToAwayTeamButton = document.getElementById('undoAwayButton');
    startTimer(10000, displayElement, messageElement, ballButton, strikeButton, addRunToAwayTeamButton, undoRunToAwayTeamButton);
});

function updatePlayerOutStatistic(playerId, outType) {
    $.ajax({
        url: '/Scorekeeping/PlayerOut',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ PlayerId: playerId, OutType: outType }),
        success: function (response) {
            console.log("Out statistic updated successfully", response);
            drawField(); 
        },
        error: function (xhr, status, error) {
            console.error("Error updating out statistic", error);
        }
    });
}

function handleOut(outType) {
    saveGameState();
    logAction(outType, { playerId: playerIdSVG }, gameId);

    if (!playerIdSVG) {
        alert("No player selected!");
        return;
    }

    updatePlayerOutStatistic(playerIdSVG, outType);
    removePlayerFromField();

    var outCount = parseInt(document.getElementById("outNumber").textContent, 10);
    outCount++;

    if (outCount >= 3) {
        outCount = 0;
        if (document.getElementById("inningTopOrBottom").textContent === "Top of") {
            document.getElementById("inningTopOrBottom").textContent = "Bottom of";
        } else {
            var inningNumber = parseInt(document.getElementById("inningNumber").textContent, 10);
            inningNumber++;
            document.getElementById("inningNumber").textContent = inningNumber;
            document.getElementById("inningTopOrBottom").textContent = "Top of";
        }
    }

    document.getElementById("outNumber").textContent = outCount;

    loadPlayerOntoHomeBase();

    playerIdSVG = null;

    checkGameEnd();
}

function checkGameEnd() {
    var inningLimit = 7;
    var currentInning = parseInt(document.getElementById("inningNumber").textContent, 10);
    if (currentInning > inningLimit ||
        (currentInning === inningLimit && document.getElementById("inningTopOrBottom").textContent === "Bottom of")) {
        document.getElementById("gameOverMessageDisplay").textContent = "Game Over";
        disableGameControls();
    }
}

function disableGameControls() {
    document.getElementById('strikeNumberButton').disabled = true;
    document.getElementById('ballNumberButton').disabled = true;
    document.getElementById('groundout').disabled = true;
    document.getElementById('flyout').disabled = true;
    document.getElementById('popout').disabled = true;
    document.getElementById('homerun').disabled = true;
    document.getElementById('single').disabled = true;
    document.getElementById('double').disabled = true;
    document.getElementById('triple').disabled = true;
    document.getElementById('addToAwayScore').disabled = true;
    document.getElementById('undoAwayButton').disabled = true;
    document.getElementById('popup-menu').style.display = 'none';
}

document.getElementById('single').addEventListener('click', function () { advancePlayerBaseHit('1B'); });
document.getElementById('double').addEventListener('click', function () { advancePlayerBaseHit('2B'); });
document.getElementById('triple').addEventListener('click', function () { advancePlayerBaseHit('3B'); });
document.getElementById('homerun').addEventListener('click', function () { advancePlayerBaseHit('HR'); });

document.getElementById('advance-player').addEventListener('click', advanceToNextBase);

document.getElementById('runs-batted-in').addEventListener('click', function () { RunsBattedIn(playerIdSVG); logAction('RBI', { playerId: playerIdSVG }, gameId); saveGameState(); })

document.getElementById('sacrifice').addEventListener('click', function () { Sacrifice(playerIdSVG); logAction('SAC', { playerId: playerIdSVG }, gameId); saveGameState(); })

document.getElementById('stolen-base').addEventListener('click', function () { StolenBase(playerIdSVG); logAction('SB', { playerId: playerIdSVG }, gameId); saveGameState(); })

document.getElementById('base-on-balls').addEventListener('click', function () { BaseOnBalls(playerIdSVG); logAction('BB', { playerId: playerIdSVG }, gameId); saveGameState(); });

$(document).ready(function () {
    $('#groundout').click(function () { handleOut('GO'); });
    $('#flyout').click(function () { handleOut('FO'); });
    $('#popout').click(function () { handleOut('PO'); });
});

let currentBatterIndex = 0; 

function moveToNextBatter() {
    currentBatterIndex++;
    if (currentBatterIndex >= playersData.length) {
        currentBatterIndex = 0; 
    }
}

function loadPlayerOntoHomeBase() {


    if (playerPositions[0] === null) {
        moveToNextBatter();
        const playerToLoad = playersData[currentBatterIndex];
        if (playerToLoad) {
            playerPositions[0] = playerToLoad.id; 
            incrementPlateAppearances(playerToLoad.id);
            logAction('PlayerPlaced', { playerId: playerToLoad.id }, gameId)
            saveGameState();
            addPlayerToField(playerToLoad.id, playerToLoad.playerLastName, playerToLoad.playerNumber, getBaseCoordinates(0).x, getBaseCoordinates(0).y);
        }
    }
}

let gameStateStack = [];

function saveGameState() {
    let currentState = {
        playerPositions: [...playerPositions],
        score: score,
        currentBatterIndex: currentBatterIndex,
    };
    gameStateStack.push(currentState);
    console.log("Game state saved", currentState, gameStateStack);
}

function undoLastAction() {
    console.log("Attempting to undo. Current stack size:", gameStateStack.length);
    if (gameStateStack.length > 0) {
        let prevState = gameStateStack.pop();
        console.log("Restoring state to", prevState);
        playerPositions = prevState.playerPositions;
        score = prevState.score;
        currentBatterIndex = prevState.currentBatterIndex;
        undoLastActionCont();
        drawField();
    } else {
        console.log("No more actions to undo.");
    }
}

document.getElementById('undo-button').addEventListener('click', undoLastAction);

function logAction(actionType, data, gameID) {
    fetch('/Scorekeeping/LogAction', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ actionType, data: JSON.stringify(data), gameID }) 
    })
        .then(response => response.json())
        .then(result => console.log(result))
        .catch(error => console.error('Error logging action:', error));
}

function undoLastActionCont() {
    fetch('/Scorekeeping/UndoLastAction', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                console.log('Last action undone successfully.');
            } else {
                console.log('Failed to undo last action: ' + data.message);
            }
        })
        .catch(error => console.error('Error undoing last action:', error));
};
//Add to away team score
document.getElementById('addToAwayScore').addEventListener('click', function () {
    // Use the existing gameId variable
    // Increment the score on the frontend immediately for instant feedback
    var awayScoreElement = document.getElementById('awayScoreNumber');
    var currentScore = parseInt(awayScoreElement.textContent, 10);
    awayScoreElement.textContent = currentScore + 1;

    // Send the updated score to the backend
    fetch('/Scorekeeping/AddAwayTeamRun', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ GameId: gameId }),
    })
        .then(response => response.json())
        .then(data => {
            if (!data.success) {
                // If the backend process failed, revert the score increment on the frontend
                awayScoreElement.textContent = currentScore;
                alert('Failed to add run: ' + data.message);
            }
        })
        .catch((error) => {
            console.error('Error:', error);
            // Revert the score if there was an error
            awayScoreElement.textContent = currentScore;
        });
});
//Undo button for away team
document.getElementById('undoAwayButton').addEventListener('click', function () {
    // Call backend to decrement the score
    fetch('/Scorekeeping/DecrementAwayTeamRun', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ GameId: gameId }),
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // Decrement the score in the frontend to reflect the change
                var awayScoreElement = document.getElementById('awayScoreNumber');
                var currentScore = parseInt(awayScoreElement.textContent, 10);
                awayScoreElement.textContent = Math.max(0, currentScore - 1); // Prevent negative scores
            } else {
                alert('Failed to undo run: ' + data.message);
            }
        })
        .catch((error) => {
            console.error('Error:', error);
        });
});

function incrementGamesPlayed(playerId) {
    return fetch('/Scorekeeping/GamesPlayed', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ PlayerId: playerId })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.json();
        })
        .then(data => {
            if (!data.success) {
                throw new Error(`Failed to increment Games Played for player ${playerId}: ${data.message}`);
            }
            console.log(`Games Played incremented for player ${playerId}.`);
        });
}

function incrementPlateAppearances(playerId) {
    return fetch('/Scorekeeping/PlateAppearances', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ PlayerId: playerId })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok.');
            }
            return response.json();
        })
        .then(data => {
            if (!data.success) {
                throw new Error(`Failed to increment Plate Appearances for player ${playerId}: ${data.message}`);
            }
            console.log(`Plate Appearances incremented for player ${playerId}.`);
        });
}

function enqueueAction(actionFunc, playerId) {
    actionQueue.push(() => actionFunc(playerId));
}

function dequeueAndExecuteActions() {
    if (actionQueue.length === 0) {
        console.log('All actions processed.');
        return;
    }

    const actionToExecute = actionQueue.shift();
    const promise = actionToExecute();

    if (promise && promise.then) {
        promise.then(() => {
            dequeueAndExecuteActions();
        }).catch(error => {
            console.error('Error executing action:', error);
            dequeueAndExecuteActions();
        });
    } else {
        console.error('The action did not return a promise, check your action functions.');
    }
}

function RunsBattedIn(playerId) {
    $.ajax({
        url: '/Scorekeeping/RunsBattedIn',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ playerId: playerId }),
        success: function (response) {
            console.log('Player RBI update successful', response);
        },
        error: function (error) {
            console.error('Error updating player RBI.', error);
        }
    });
}

function Sacrifice (playerId) {
    $.ajax({
        url: '/Scorekeeping/Sacrifice',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ playerId: playerId }),
        success: function (response) {
            console.log('Player sacrifice update successful', response);
        },
        error: function (error) {
            console.error('Error updating player sacrifice.', error);
        }
    });
};

function StolenBase(playerId) {
    $.ajax({
        url: '/Scorekeeping/StolenBase',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ playerId: playerId }),
        success: function (response) {
            console.log('Player SB update successful', response);
        },
        error: function (error) {
            console.error('Error updating player SB.', error);
        }
    });
}

function BaseOnBalls(playerId) {
    $.ajax({
        url: '/Scorekeeping/BaseOnBalls',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ playerId: playerId }),
        success: function (response) {
            console.log('Player SB update successful', response);
        },
        error: function (error) {
            console.error('Error updating player SB.', error);
        }
    });
}

document.getElementById('foulball').addEventListener('click', function () {
    if (strikeCount < 2) {
        strikeCount += 1;
        document.getElementById('strikeCount').textContent = strikeCount;
    }
});

document.getElementById('hitByPitch').addEventListener('click', function () {
    advanceToNextBase();
    saveGameState();
    
});

document.getElementById('undo-button').addEventListener('click', function () {
    const undoAll = document.getElementById('undo-all-checkbox').checked;
    if (undoAll) {
        undoAllActionsForGame();
    } else {
        undoLastAction();
    }
});

function undoAllActionsForGame() {
    fetch(`/Scorekeeping/UndoAllActionsForGame?gameId=${gameId}`, { 
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('All actions for the game were undone successfully.');
            } else {
                alert('Failed to undo all actions for the game:', data.message);
            }
        })
}
