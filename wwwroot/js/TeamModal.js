$(document).ready(function () {
    GetTeams();
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
                    object += '<td>' + (item.teamName || '') + '</td>';

                    var coachNames = item.coaches.map(function (coach) {
                        return coach.coachName;
                    }).join(', ');
                    object += '<td>' + coachNames + '</td>';
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id + ')">Delete</a>  <a href="#" class="btn btn-secondary btn-sm" onclick="Details(' + item.id + ')">Details</a></td>';
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

function Insert() {

    var res = Validate();
    if (res == false) {
        return false;
    }


    var formData = new Object();
    formData.teamName = $('#TeamName').val();
    formData.coachID = $('#CoachID').val();

    $.ajax({
        url: '/TeamModal/Insert',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to Add Team Data.');
            }
            else {
                HideModal()
                GetTeams();
                alert(response)
            }
        },
        error: function () {
            alert('Unable to Add Team Data.');
        }
    })

};

function HideModal() {
    ClearData()
    $('#TeamModal').modal('hide');
};

function HideDetailsModal() {
    $('#TeamDetailsModal').modal('hide');
};


function ClearData() {
    $('#TeamName').val('');
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

    if ($('#TeamName').val() == "") {
        $('#TeamName').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#TeamName').css('border-color', 'lightgrey');
    }

    return isValid;
};

$('#TeamName').change(function () {
    Validate();
});
