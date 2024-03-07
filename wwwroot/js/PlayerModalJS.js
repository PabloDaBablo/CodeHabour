 var currentSortColumn = 'divAge';
var currentSortDirection = 'asc';
var currentPage = 1;
var pageSize = 10;
var totalPages = 0;

function GetPlayers(page) {
    var divisionId = $('#divisionId').val();
    var firstNameSearch = $('#firstNameSearch').val();
    var lastNameSearch = $('#lastNameSearch').val();
    var numberSearch = $('#numberSearch').val();
    var teamId = $('#teamId').val(); 

    
    var data = {
        page: page,
        pageSize: pageSize,
        sortColumn: currentSortColumn,
        sortDirection: currentSortDirection,
        divisionId: divisionId,
        firstNameSearch: firstNameSearch,
        lastNameSearch: lastNameSearch,
        numberSearch: numberSearch,
        teamId: teamId 
    };

    console.log(data);

    $.ajax({
        url: '/PlayerModal/GetPlayers',
        type: 'GET',
        data: data, 
        success: function (response) {
            updateTable(response.players);
            totalPages = response.totalPages;
            updatePaginationControls();
        },
        error: function (error) {
            console.error("Error fetching players: ", error);
            alert('Unable to load player data.');
        }
    });
}


$(document).ready(function () {
    GetPlayers(currentPage);

    $(document).on('click', '.sortable', function () {
        var sortColumn = $(this).data('sort');
        console.log("Sorting by: " + sortColumn);
        sortData(sortColumn);
    });

    $('#prevPage').click(function () {
        if (currentPage > 1) {
            currentPage--;
            GetPlayers(currentPage);
        }
    });

    $('#nextPage').click(function () {
        if (currentPage < totalPages) {
            currentPage++;
            GetPlayers(currentPage);
        }
    });

    $('#Save').off('click').on('click', function (e) {
        e.preventDefault();
        console.log('Save button clicked');
        if (validatePlayerForm()) {
            console.log('Validation passed, proceeding to insert.');
            Insert();
        } else {
            console.log('Validation failed, insert not called.');
        }
    });
    $('#PlayerModal').on('hidden.bs.modal', function () {
        clearAllValidationErrorMessages();
    });

    function clearAllValidationErrorMessages() {
        const inputIds = ['PlayerMemberID', 'PlayerFirstName', 'PlayerLastName', 'PlayerNumber'];

        inputIds.forEach(function (inputId) {
            clearValidationError(inputId);
        });
    }

    function clearValidationError(inputId) {
        $(`#${inputId}`).removeClass('is-invalid');
        $(`#${inputId}Error`).text('');
    }

    $("#filterButton").click(function (e) {
        e.preventDefault(); 
        GetPlayers(1);
    });
});


$('#btnAdd').click(function () {
    $('#PlayerModal').modal('show');
    resetValidationStates();
    $('#PlayerMemberID').prop('disabled', false);
    $('#modalTitle').text('Add Player');
    $('#Update').css('display', 'none');
    $('#Save').css('display', 'block');
})

/* Insert Data */
function Insert() {

    var formData = new Object();
    formData.id = $('#ID').val();
    formData.playerMemberID = $('#PlayerMemberID').val();
    formData.playerFirstName = $('#PlayerFirstName').val();
    formData.playerLastName = $('#PlayerLastName').val();
    formData.playerNumber = $('#PlayerNumber').val();
    formData.teamID = $('#TeamID').val();
    formData.divisionID = $('#DivisionID').val();

    $.ajax({
        url: '/PlayerModal/Insert',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to Add Player Data.');
            }
            else if (response.error) {
                alert(response.error)
            }
            else {
                alert(response)
                HideModal()
                GetPlayers();
            }
        },
        error: function () {
            alert('Unable to Add Player Data.');
        }
    })

};

/* Hide Modal */
function HideModal() {
    console.log('HideModal called');
    ClearData()
    $('#PlayerModal').modal('hide');
};

/* Clear Modal */
function ClearData() {
    $('#PlayerMemberID').val('');
    $('#PlayerFirstName').val('');
    $('#PlayerLastName').val('');
    $('#PlayerNumber').val('');
    $('#TeamID').val('');
    $('#DivisionID').val('');
};


/* Edit Data */
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
            else if (response.error) {
                alert(response.error)
            }
            else {
                $('#PlayerModal').modal('show')
                $('#modalTitle').text('Edit Player');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');


                $('#ID').val(response.id);
                $('#PlayerMemberID').val(response.playerMemberID).prop('disabled', true);
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

/* Update Data */
function Update() {

    var formData = new Object();
    formData.id = $('#ID').val();
    formData.playerMemberID = $('#PlayerMemberID').val();
    formData.playerFirstName = $('#PlayerFirstName').val();
    formData.playerLastName = $('#PlayerLastName').val();
    formData.playerNumber = $('#PlayerNumber').val();
    formData.teamID = $('#TeamID').val();
    formData.divisionID = $('#DivisionID').val();

    $.ajax({
        url: '/PlayerModal/Update',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save Player Data.');
            }
            else if (response.error) {
                alert(response.error)
            }
            else {
                HideModal()
                GetPlayers();
                alert("Successfully edited Player Data")
            }
        },
        error: function () {
            alert('Unable to save Player Data.');
        }
    })

};

/* Delete Data */
function Delete(id) {
    if (confirm('Are you sure to delete this record?')) {
        $.ajax({
            url: '/PlayerModal/Delete?id=' + id,
            type: 'post',
            success: function (response) {
                if (response == null || response == undefined) {
                    alert('Unable to delete Player Data.');
                }
                else if (response.length == 0) {
                    alert('Player Data Not Found with the ID.');
                }
                else if (response.error) {
                    alert(response.error)
                }
                else {
                    GetPlayers()
                    alert("Successfully deleted Player Data")
                }
            },
            error: function () {
                alert('Unable to delete Player Data.');
            }
        })
    }
};
$(document).on('click', '.toggle-status', function () {
    var button = $(this);
    var playerId = button.data('id');

    $.ajax({
        url: '/PlayerModal/ToggleStatus',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: playerId }),
        success: function (response) {
            if (response.isActive) {
                button.text('Active').removeClass('btn-secondary btn-danger').addClass('btn-success');
            } else {
                button.text('Inactive').removeClass('btn-success').addClass('btn-secondary btn-danger');
            }

        },
        error: function (xhr, status, error) {
            console.error("Error toggling status: ", error);
            alert('Error toggling player status.');
        }
    });
});



function updateTable(players) {
    var $tableBody = $('#tblBody');
    $tableBody.empty();

    players.forEach(function (player) {
        var activeStatusClass = player.isActive ? 'btn-success' : 'btn-secondary';
        var activeStatusText = player.isActive ? 'Active' : 'Inactive';
        var row = `
                <tr>
                    <td>${player.divAge}</td>
                    <td>${player.playerFirstName}</td>
                    <td>${player.playerLastName}</td>
                    <td>${player.playerNumber}</td>
                    <td>${player.teamName}</td>
                    <td>
                        <button class="btn ${activeStatusClass} toggle-status" data-id="${player.id}">${activeStatusText}</button>
                    </td>
                    <td>
                        <button class="btn btn-primary btn-sm edit" data-id="${player.id}">Edit</button>
                    </td>
                </tr>
            `;
        $tableBody.append(row);
    });
}


function updatePaginationControls() {
    $('#pageIndicator').text(`Page: ${currentPage} of ${totalPages}`);
    $('#prevPage').prop('disabled', currentPage <= 1);
    $('#nextPage').prop('disabled', currentPage >= totalPages);
};


document.addEventListener('DOMContentLoaded', (event) => {
    document.querySelectorAll('.sortable').forEach(header => {
        header.addEventListener('click', function () {
            console.log("Clicked on header");
        });
    });
});

$(document).on('click', '.edit', function () {
    var id = $(this).data('id');
    Edit(id);
});

function validatePlayerForm() {
    let isValid = true;
    
    const regexAlphaNumeric = /^[a-zA-Z0-9]+$/;
    const regexAlphaSpaces = /^[a-zA-Z\s]+$/;
    const regexNumericRange = /^(?:[0-9]|[1-9][0-9])$/; 

    validateField('PlayerMemberID', regexAlphaNumeric, 'Member ID should contain letters and numbers only.');
    validateField('PlayerFirstName', regexAlphaSpaces, 'First name should contain only letters and spaces.');
    validateField('PlayerLastName', regexAlphaSpaces, 'Last name should contain only letters and spaces.');
    validateField('PlayerNumber', regexNumericRange, 'Player number must be between 0-99.');

    

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
    
    clearError('PlayerMemberID');
    clearError('PlayerFirstName');
    clearError('PlayerLastName');
    clearError('PlayerNumber');
    
}

$('#PlayerModal').on('hidden.bs.modal', function () {
    $('#PlayerMemberID').prop('disabled', false);
    resetValidationStates();
});
