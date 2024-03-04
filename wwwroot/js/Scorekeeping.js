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
    var strikeButton = document.getElementById("strikeNumberButton");
    var ballButton = document.getElementById("ballNumberButton");

    strikeButton.addEventListener("click", function () {
        var strikeCountSpan = document.getElementById("strikeCount");
        var strikeCount = parseInt(strikeCountSpan.textContent, 10);
        strikeCount++;

        if (strikeCount > 3) {
            var outCountSpan = document.getElementById("outNumber");
            var outCount = parseInt(outCountSpan.textContent, 10);
            outCount++;
            if (outCount > 3) {
                outCount = 0;
                var inningSpan = document.getElementById("inningNumber");
                var inningCount = parseInt(inningSpan.textContent, 10);
                inningCount++;
                inningSpan.textContent = inningCount;
            }
            outCountSpan.textContent = outCount;
            strikeCount = 0;
        }

        strikeCountSpan.textContent = strikeCount;
    });

    ballButton.addEventListener("click", function () {
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

