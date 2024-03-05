$(document).ready(function () {
    GetPlayers(gameId);
});
function GetPlayers(gameId) {
    $.ajax({
        url: `/Scorekeeping/GetPlayers?gameId=${gameId}`,
        type: 'GET',
        dataType: 'json',
        success: function (players) {

            const positionToSVGId = {
                "Catcher": "catcher",
                "First Base": "first-base",
                "Second Base": "second-base",
                "Third Base": "third-base",
                "Pitcher": "pitcher",
                "Short Stop": "shortstop",
                "Left Field": "left-field",
                "Center Field": "center-field",
                "Right Field": "right-field"
            };

            players.forEach(player => {
                let svgPositionId = positionToSVGId[player.position];
                let svgLabelId = `label-${svgPositionId}`;

                let labelElement = document.getElementById(svgLabelId);
                if (labelElement) {
                    labelElement.textContent = `${player.playerLastName} #${player.playerNumber}`;
                }
            });
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
document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('team1Dropdown').addEventListener('change', function () {
        var selectedGameText = this.options[this.selectedIndex].text;
        document.getElementById('teamsPlaying').textContent = selectedGameText;
    });
});

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

