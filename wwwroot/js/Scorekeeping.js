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
