var selectedPlayerId;

$(document).ready(function () {
    GetPlayers(gameId);
    GetPlayersHome(gameId)
    document.getElementById('team2Dropdown').addEventListener('change', function () {
        const playerId = this.value;
        if (playerId === "0") return; 

        const selectedPlayer = playersData.find(player => player.id.toString() === playerId);
        if (selectedPlayer) {
            addPlayerToField(selectedPlayer.id, selectedPlayer.playerLastName, selectedPlayer.playerNumber, 200, 280); // Adjust x, y as needed

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

            data.forEach(player => {
                var option = new Option(player.playerLastName, player.id);
                lineupDropdown.options.add(option);
            });

            playersData = data; 
        })
        .catch(error => console.error('Error:', error));
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
function advancePlayers() {
    if (!playerIdSVG) {
        console.log("No player selected to advance.");
        return;
    }

    const playerIdToAdvance = parseInt(playerIdSVG);
    const currentPlayerIndex = playerPositions.findIndex(id => id === playerIdToAdvance);

    if (currentPlayerIndex === -1) {
        console.log("Selected player is not on any base.");
        return; 
    }

    if (currentPlayerIndex < playerPositions.length - 1) {
        let baseHitType = null;
        if (currentPlayerIndex === 0) {
            baseHitType = '1B'; 
        } else if (currentPlayerIndex === 1) {
            baseHitType = '2B'; 
        } else if (currentPlayerIndex === 2) {
            baseHitType = '3B'; 
        }

        playerPositions[currentPlayerIndex] = null;
        playerPositions[currentPlayerIndex + 1] = playerIdToAdvance;

        if (baseHitType) {
            updatePlayerBaseHit(playerIdToAdvance, baseHitType);
        }
    } else {
        playerScored(playerIdToAdvance);
        playerPositions[currentPlayerIndex] = null;
    }

    drawField();
}


function playerScored(playerId) {
    $.ajax({
        url: '/Scorekeeping/PlayerScored', 
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ PlayerId: playerId}), 
        success: function (response) {
            console.log('Player scored a run.', response);
        },
        error: function (error) {
            console.error('Error updating player score.', error);
        }
    });
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
    $.post('/Scorekeeping/PlayerStruckOut', { playerId: playerId })
        .done(function (data) {
            console.log("Player striked out.", data);
        })
        .fail(function (error) {
            console.error("Error updating player strikeout.", error);
        });
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
    document.getElementById('advance-player').addEventListener('click', advancePlayers);
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
    if (!selectedPlayerId) {
        alert("No player selected!");
        return;
    }

    
    const playerIndex = playerPositions.indexOf(parseInt(selectedPlayerId));

    const playerGroup = document.getElementById(`player-group-${selectedPlayerId}`);
    if (playerGroup) {
        playerGroup.remove();
        console.log(`Player ${selectedPlayerId} removed from field.`);

        if (playerIndex !== -1) {
            playerPositions[playerIndex] = null;
        }

        selectedPlayerId = null;
        drawField(); 
    } else {
        console.error(`Player group with ID ${selectedPlayerId} not found.`);
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
function startTimer(duration, displayElement, messageElement, ballButton, strikeButton) {
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
        }
    }, 1000);
}

// Start the timer with a 10-second countdown
document.addEventListener('DOMContentLoaded', function () {
    var displayElement = document.getElementById('clock');
    var messageElement = document.getElementById('gameOverMessageDisplay');
    var ballButton = document.getElementById('ballNumberButton');
    var strikeButton = document.getElementById('strikeNumberButton');

    startTimer(10000, displayElement, messageElement, ballButton, strikeButton);
});

