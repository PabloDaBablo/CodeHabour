var currentSortColumn = 'divAge';
var currentSortDirection = 'asc';
var currentPage = 1;
var pageSize = 10;
var totalPages = 0;

function GetPlayers(page) {
    $.ajax({
        url: `/PlayerModal/GetPlayers?page=${page}&pageSize=${pageSize}&sortColumn=${currentSortColumn}&sortDirection=${currentSortDirection}`,
        type: 'GET',
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

function sortData(sortColumn) {
    if (currentSortColumn === sortColumn) {
        currentSortDirection = currentSortDirection === 'asc' ? 'desc' : 'asc';
    } else {
        currentSortColumn = sortColumn;
        currentSortDirection = 'asc';
    }
    GetPlayers(currentPage);
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
        if (validateAllFields()) {
            Insert();
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
});


$('#btnAdd').click(function () {
    $('#PlayerModal').modal('show');
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
            else {
                HideModal()
                GetPlayers();
                alert(response)
            }
        },
        error: function () {
            alert('Unable to Add Player Data.');
        }
    })

};

/* Hide Modal */
function HideModal() {
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
            else {
                HideModal()
                GetPlayers();
                alert(response)
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
                else {
                    GetPlayers()
                    alert(response)
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

function showValidationError(inputId, message) {
    $(`#${inputId}`).addClass('is-invalid');
    $(`#${inputId}Error`).text(message);
}

function clearValidationError(inputId) {
    $(`#${inputId}`).removeClass('is-invalid');
    $(`#${inputId}Error`).text('');
}

function validatePlayerMemberID(value) {
    var regex = /^[a-zA-Z0-9]+$/;
    if (!regex.test(value)) {
        showValidationError('PlayerMemberID', 'MemberID should only contain letters and numbers.');
        return false;
    }
    clearValidationError('PlayerMemberID');
    return true;
}

function validateName(value, fieldName) {
    var regex = /^[a-zA-Z\s]+$/;
    if (!regex.test(value)) {
        showValidationError(fieldName, `First or Last name should only contain letters and spaces.`);
        return false;
    }
    clearValidationError(fieldName);
    return true;
}

function validatePlayerNumber(value) {
    var number = parseInt(value, 10);
    if (isNaN(number) || number < 0 || number > 99) {
        showValidationError('PlayerNumber', 'Player number should be an integer between 0 and 99.');
        return false;
    }
    clearValidationError('PlayerNumber');
    return true;
}

function validateAllFields() {
    var isValid = true;
    isValid &= validatePlayerMemberID($('#PlayerMemberID').val());
    isValid &= validateName($('#PlayerFirstName').val(), 'PlayerFirstName');
    isValid &= validateName($('#PlayerLastName').val(), 'PlayerLastName');
    isValid &= validatePlayerNumber($('#PlayerNumber').val());

    return isValid === 1; 
}