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

var now = new Date();

// Add 10 seconds to the current time
var countDownDate = new Date(now.getTime() + 10 * 1000); // 10 seconds * 1000 milliseconds/second

// Update the count down every 1 second
var x = setInterval(function () {
    // Get today's date and time
    var now = new Date().getTime();

    // Find the distance between now and the count down date
    var distance = countDownDate - now;

    // Time calculations for hours, minutes and seconds
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    // Display the result in the element with id="clock"
    document.getElementById("clock").innerHTML = hours + "h " + minutes + "m " + seconds + "s ";

    // If the count down is finished, write some text
    if (distance < 0) {
        clearInterval(x);
        document.getElementById("clock").innerHTML = "Time Up!"; // Change this line to update "clock" instead
        document.getElementById("gameOverMessageDisplay").textContent = "Game Over"

        // Disable the ball and strike buttons
        document.getElementById("ballNumberButton").disabled = true;
        document.getElementById("strikeNumberButton").disabled = true;
    }
}, 1000);
