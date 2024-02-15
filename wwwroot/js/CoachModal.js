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
                    object += '<td>' + (coach.division || '') + '</td>';
                    object += '<td>' + (coach.coachName || '') + '</td>';
                    object += '<td>' + (coach.coachNumber || '') + '</td>';
                    var teamNames = coach.teams.map(function (team) { return team.teamName; }).join(', ');
                    object += '<td>' + (teamNames || 'No Assigned Team') + '</td>';
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + coach.id + ')">Edit</a> </td>';
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
    resetValidationStates();
    $('#ID').val('0');
    $('#CoachModal').modal('show');
    $('#modalTitle').text('Add Coach');
    $('#Update').css('display', 'none');
    $('#Save').css('display', 'block');
})

function Insert() {

    if (!validateCoachForm()) {
        console.log("Validation failed.");
        return;
    }

    

    var formData = {
        id: $('#ID').val(),
        coachMemberID: $('#CoachMemberID').val(),
        coachName: $('#CoachName').val(),
        coachNumber: $('#CoachNumber').val() || null,
        SelectedTeamIds: $('#TeamCoach').val() ? $('#TeamCoach').val().map(Number) : [],
        DivisionID: $('#DivisionID').val()
    };


    $.ajax({
        url: '/CoachModal/Insert',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData), 
        success: function (response) {

            if (response.error) {
                alert(response.error)
            }
            else {
                HideModal();
                GetCoaches();
                alert(response)
            }
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON && xhr.responseJSON.error) {
                alert(xhr.responseJSON.error);
            } else {
                alert("An unexpected error occurred.");
            }
        }
    });
}
function HideModal() {
    ClearData()
    $('#CoachModal').modal('hide');
};


function Edit(id) {
    $.ajax({
        url: '/CoachModal/Edit?id=' + id,
        type: 'get',
        contentType: 'application/json;charset=utf-8',
        datatype: 'json',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('Unable to Edit Coach Data.');
            } else if (response.length == 0) {
                alert('Coach Data Not Found with the ID.');
            } else {
                $('#CoachModal').modal('show')
                $('#modalTitle').text('Edit Coach');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');

                $('#ID').val(response.id);
                $('#CoachMemberID').val(response.coachMemberID);
                $('#CoachName').val(response.coachName);
                $('#CoachNumber').val(response.coachNumber);
                var teamIDs = response.teams.map(team => team.TeamID);
                $('#TeamCoach').val(teamIDs).trigger('change'); 
                $('#DivisionID').val(response.DivisionID);
            }
        },
        error: function () {
            alert('Unable to Edit Coach Data.');
        }
    });
};


function Update() {
    if (!validateCoachForm()) {
        console.log("Validation failed.");
        return;
    }
    var selectedTeamIds = $('#TeamCoach').val().map(Number);

    var formData = new Object();

    formData.id = $('#ID').val(),
    formData.coachMemberID = $('#CoachMemberID').val(),
    formData.coachName = $('#CoachName').val(),
    formData.coachNumber = $('#CoachNumber').val() ? parseInt($('#CoachNumber').val(), 10) : null,
    formData.SelectedTeamIds = selectedTeamIds;
    formData.DivisionID = $('#DivisionID').val();   

    $.ajax({
        url: '/CoachModal/Update',
        data: JSON.stringify(formData),
        contentType: 'application/json',
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save Coach Data.');
            }
            
            else {
                HideModal()
                GetCoaches();
                alert(response)
            }
        },
        error: function () {
            alert('Unable to save Coach Data.');
        }
    })

};

function Delete(id) {
    if (confirm('Are you sure to delete this record?')) {
        $.ajax({
            url: '/CoachModal/Delete?id=' + id,
            type: 'post',
            success: function (response) {
                if (response == null || response == undefined) {
                    alert('Unable to delete Coach Data.');
                }
                else if (response.length == 0) {
                    alert('Coach Data Not Found with the ID.');
                }
                else {
                    GetCoaches()
                    alert(response)
                }
            },
            error: function () {
                alert('Unable to delete Coach Data.');
            }
        })
    }
};

function Validate() {
    var isValid = true;


    if ($('#CoachMemberID').val().trim() == "") {
        $('#CoachMemberID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CoachMemberID').css('border-color', 'lightgrey');
    }

    if ($('#CoachName').val().trim() == "") {
        $('#CoachName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CoachName').css('border-color', 'lightgrey');
    }

    if ($('#CoachNumber').val().trim() == "") {
        $('#CoachNumber').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CoachNumber').css('border-color', 'lightgrey');
    }

    if ($('#TeamCoach').val() == "") {
        $('#TeamCoach').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#TeamCoach').css('border-color', 'lightgrey');
    }

    return isValid;
};

$('#CoachMemberID').change(function () {
    Validate();
});
$('#CoachName').change(function () {
    Validate();
});
$('#CoachNumber').change(function () {
    Validate();
});
$('#CoachPosition').change(function () {
    Validate();
});
$('#TeamCoach').change(function () {
    Validate();
});

function ClearData() {
    $('#CoachMemberID').val('');
    $('#CoachName').val('');
    $('#CoachNumber').val('');
    $('#TeamCoach').val('');
};

function validateCoachForm() {
    let isValid = true;
    const regexAlphaNumeric = /^[a-zA-Z0-9]+$/;
    const regexAlphaSpaces = /^[a-zA-Z\s]+$/;
    const regexNumericRange = /^(?:[0-9]|[1-9][0-9])$/;

    validateField('CoachMemberID', regexAlphaNumeric, 'Coach Member ID should contain letters and numbers only.');

    validateField('CoachName', regexAlphaSpaces, 'Coach Name should contain only letters and spaces.');


    function validateField(fieldId, regex, errorMessage) {
        if (!regex.test($(`#${fieldId}`).val())) {
            showError(fieldId, errorMessage);
            isValid = false;
        } else {
            clearError(fieldId);
        }
    }

    return isValid;
}

function showError(fieldId, message) {
    $(`#${fieldId}`).addClass('is-invalid');
    $(`#${fieldId}Error`).text(message).show();
}

function clearError(fieldId) {
    $(`#${fieldId}`).removeClass('is-invalid');
    $(`#${fieldId}Error`).hide();
}


function resetValidationStates() {
    clearError('CoachMemberID');
    clearError('CoachName');
    
}

$('#CoachModal').on('hidden.bs.modal', function () {
    resetValidationStates(); 
});
function showError(fieldId, message) {
    $(`#${fieldId}`).addClass('is-invalid');
    $(`#${fieldId}Error`).text(message).show();
}

function clearError(fieldId) {
    $(`#${fieldId}`).removeClass('is-invalid');
    $(`#${fieldId}Error`).hide();
}

$('#Save').click(function () {
    if (validateCoachForm()) {
        Insert();
    }
});