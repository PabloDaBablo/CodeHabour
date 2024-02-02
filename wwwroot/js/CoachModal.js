$(document).ready(function () {
    GetCoaches();
});

function GetCoaches() {
    $.ajax({
        url: '/CoachModal/GetCoaches',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = ' ';
                object += '<tr>';
                object += '<td colspan="5">Coaches not found</td>';
                object += '</tr>';
                $('#tblBody').html(object);
            }
            else {
                var object = '';
                $.each(response, function (index, coach) {
                    object += '<tr>';
                    object += '<td>' + (coach.coachName || '') + '</td>';
                    object += '<td>' + (coach.coachNumber || '') + '</td>';
                    object += '<td>' + (coach.coachPosition || '') + '</td>';

                    var teamNames = coach.teams.map(function (team) { return team.teamName; }).join(', ');
                    object += '<td>' + (teamNames || 'No Assigned Team') + '</td>';

                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + coach.id + ')">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + coach.id + ')">Delete</a> </td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object);
            }
        },
        error: function () {
            alert('Unable to Read Coach Data.');
        }
    });
}

$('#btnAdd').click(function () {
    $('#CoachModal').modal('show');
    $('#modalTitle').text('Add Player');
    $('#Update').css('display', 'none');
    $('#Save').css('display', 'block');
})

function Insert() {
    var formData = {
        id: $('#ID').val(),
        coachMemberID: $('#CoachMemberID').val(),
        coachName: $('#CoachName').val(),
        coachNumber: $('#CoachNumber').val(),
        coachPosition: $('#CoachPosition').val(),
        SelectedTeamIds: $('#TeamCoach').val() ? $('#TeamCoach').val().map(Number) : []
    };

    console.log(formData); 

    $.ajax({
        url: '/CoachModal/Insert',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData), 
        success: function (response) {
            alert('Success: ' + response);
            HideModal();
            GetCoaches();
        },
        error: function (xhr, status, error) {
            console.error('Error: ' + error);
            alert('Error: ' + xhr.responseText);
        }
    });
}
function HideModal() {
    //ClearData()
    $('#CoachModal').modal('hide');
};