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

                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + coach.id + ')">Edit</a></td>';
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


function Edit(id) {
    $.ajax({
        url: '/PlayerModal/Edit?id=' + id,
        type: 'get',
        contentType: 'application/json;charset=utf-8',
        datatype: 'json',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('Unable to Edit Player Data.');
            }
            else if (response.length == 0) {
                alert('Player Data Not Found with the ID.');
            }
            else {
                $('#PlayerModal').modal('show')
                $('#modalTitle').text('Edit Player');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');


                $('#ID').val(response.id);
                $('#PlayerMemberID').val(response.playerMemberID);
                $('#PlayerFirstName').val(response.playerFirstName);
                $('#PlayerLastName').val(response.playerLastName);
                $('#PlayerNumber').val(response.playerNumber);
                $('#TeamID').val(response.teamID);
                $('#DivisionID').val(response.divisionID);
            }
        },
        error: function () {
            alert('Unable to Edit Player Data.');
        }
    })
};


function Update() {
    //var result = Validate();
    //if (result == false) {
    //    return false;
    //}

    var formData = new Object();

    formData.id = $('#ID').val(),
    formData.coachMemberID = $('#CoachMemberID').val(),
    formData.coachName = $('#CoachName').val(),
    formData.coachNumber = $('#CoachNumber').val(),
    formData.coachPosition = $('#CoachPosition').val()
    formData.SelectedTeamIds = $('#TeamCoach').val() ? $('#TeamCoach').val().map(Number) : []


    $.ajax({
        url: '/CoachModal/Update',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save Player Data.');
            }
            else {
                HideModal()
                GetCoaches();
                alert(response)
            }
        },
        error: function () {
            alert('Unable to save Player Data.');
        }
    })

};
