$(document).ready(function () {
    GetTeams();

    $('#Save').off('click.insert').on('click.insert', function (e) {
        e.preventDefault();
        Insert();
    });

    $('#Update').click(function () {
        if (Validate()) {
            Update();
        }
    });
});

/* Read Data */
function GetTeams() {
    $.ajax({
        url: '/TeamModal/GetTeams',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = ' ';
                object += '<tr>';
                object += '<td colspan="5">Teams not found</td>';
                object += '</tr>';
                $('#tblBody').html(object);
            } else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + (item.division || '') + '</td>';
                    object += '<td>' + (item.teamName || '') + '</td>';

                    var coachNames = item.coaches.map(function (coach) {
                        return coach.coachName;
                    }).join(', ');
                    object += '<td>' + coachNames + '</td>';
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-secondary btn-sm" onclick="Details(' + item.id + ')">Details</a></td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object);
            }
        },
        error: function () {
            alert('Unable to Read Team Data.');
        }
    });
};

$('#btnAdd').click(function () {
    $('#TeamModal').modal('show');
    $('#modalTitle').text('Add Team');
    $('#Update').css('display', 'none');
    $('#Save').css('display', 'block');
});

let insertInProgress = false;
function Insert() {

    if (!Validate()) {
        return;
    }

    var formData = {
        teamName: $('#TeamName').val(),
        coachID: $('#CoachID').val(),
    };

    if (insertInProgress) {
        console.log("Insert already in progress.");
        return;
    }

    insertInProgress = true;
    console.log('Insert function started');

    $.ajax({
        url: '/TeamModal/Insert',
        data: formData,
        type: 'post',
        success: function (response) {
            HideModal();
            GetTeams();
            alert('Team added successfully');
        },
        error: function () {
            alert('Unable to Add Team Data.');
        }
    }).always(function () { 
        insertInProgress = false; 
        console.log('Insert function ended');
    });
}



function HideModal() {
    ClearData()
    $('#TeamModal').modal('hide');
};

function HideDetailsModal() {
    $('#TeamDetailsModal').modal('hide');
};


function ClearData() {
    $('#TeamName').val('');
    ClearValidationError('TeamName');
};




function Edit(id) {
    $.ajax({
        url: '/TeamModal/Edit?id=' + id,
        type: 'get',
        contentType: 'application/json;charset=utf-8',
        datatype: 'json',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('Unable to Edit Team Data.');
            }
            else if (response.length == 0) {
                alert('Team Data Not Found with the ID.');
            }
            else {
                $('#TeamModal').modal('show')
                $('#modalTitle').text('Edit Team');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');


                $('#ID').val(response.id);
                $('#TeamName').val(response.teamName);
            }
        },
        error: function () {
            alert('Unable to Edit Temm Data.');
        }
    })
};

function Update() {
    var result = Validate();
    if (result == false) {
        return false;
    }

    var formData = new Object();
    formData.id = $('#ID').val();
    formData.teamName = $('#TeamName').val();


    $.ajax({
        url: '/TeamModal/Update',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save Team Data.');
            }
            else {
                HideModal()
                GetTeams();
                alert(response)
            }
        },
        error: function () {
            alert('Unable to save Team Data.');
        }
    })
};


function Delete(id) {
    if (confirm('Are you sure to delete this record?')) {
        $.ajax({
            url: '/TeamModal/Delete?id=' + id,
            type: 'post',
            success: function (response) {
                if (response == null || response == undefined) {
                    alert('Unable to delete Team Data.');
                }
                else if (response.length == 0) {
                    alert('Team Data Not Found with the ID.');
                }
                else {
                    GetTeams()
                    alert(response)
                }
            },
            error: function () {
                alert('Unable to delete Team Data.');
            }
        })
    }
};

function Details(id) {
    $.ajax({
        url: '/TeamModal/Details?id=' + id, 
        type: 'GET',
        success: function (data) {
            populateTeamDetailsModal(data);
            $('#TeamDetailsModal').modal('show');
        },
        error: function (error) {
            console.error("Error fetching team details:", error);
            alert("Could not fetch Team details. Please try again.");
        }
    });
}

function populateTeamDetailsModal(data) {
    $('#modalTitle').text('Team Details'); 
   
    $('.modal-body dd').empty();
    
    $('#TeamDetailsModal').find('.modal-body dd').eq(0).text(data.teamName);
    $('#TeamDetailsModal').find('.modal-body dd').eq(1).html(data.players.join('<br>')); 
    $('#TeamDetailsModal').find('.modal-body dd').eq(2).text(data.coaches.join(', '));
}


function Validate() {
    var isValid = true;
    var teamName = $('#TeamName').val();
    var nameRegex = /^[a-zA-Z\s]+$/; 

    if (teamName == "") {
        ShowValidationError('TeamName', "Team name is required");
        isValid = false;
    } else if (!nameRegex.test(teamName)) {
        ShowValidationError('TeamName', "Team name must contain only letters and spaces");
        isValid = false;
    } else {
        ClearValidationError('TeamName');
    }

    return isValid;
}

function ShowValidationError(fieldId, message) {
    $(`#${fieldId}`).addClass('is-invalid');
    $(`#${fieldId}Error`).text(message).show();
}

function ClearValidationError(fieldId) {
    $(`#${fieldId}`).removeClass('is-invalid');
    $(`#${fieldId}Error`).hide();
}

function ClearData() {
    $('#TeamName').val('');
    ClearValidationError('TeamName');
}


