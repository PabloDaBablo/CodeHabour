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
});


$('#btnAdd').click(function () {
    $('#PlayerModal').modal('show');
    $('#modalTitle').text('Add Player');
    $('#Update').css('display', 'none');
    $('#Save').css('display', 'block');
})

/* Insert Data */
function Insert() {

    var res = Validate();
    if (res == false) {
        return false;
    }


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

function Validate() {
    var isValid = true;

    if ($('#PlayerMemberID').val().trim() == "") {
        $('#PlayerMemberID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('PlayerMemberID').css('border-color', 'lightgrey');
    }

    if ($('#PlayerFirstName').val().trim() == "") {
        $('#PlayerFirstName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PlayerFirstName').css('border-color', 'lightgrey');
    }

    if ($('#PlayerLastName').val().trim() == "") {
        $('#PlayerLastName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PlayerLastName').css('border-color', 'lightgrey');
    }

    if ($('#PlayerNumber').val().trim() == "") {
        $('#PlayerNumber').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PlayerNumber').css('border-color', 'lightgrey');
    }

    if ($('#TeamID').val() == "") {
        $('#TeamID').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#TeamID').css('border-color', 'lightgrey');
    }

    if ($('#DivisionID').val() == "") {
        $('#DivisionID').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#DivisionID').css('border-color', 'lightgrey');
    }

    return isValid;
};

$('#PlayerMemberID').change(function () {
    Validate();
});
$('#PlayerFirstName').change(function () {
    Validate();
});
$('#PlayerLastName').change(function () {
    Validate();
});
$('#PlayerNumber').change(function () {
    Validate();
});
$('#TeamID').change(function () {
    Validate();
});



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
    var result = Validate();
    if (result == false) {
        return false;
    }

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
                button.text('Active').removeClass('btn-secondary btn-warning').addClass('btn-success');
            } else {
                button.text('Inactive').removeClass('btn-success').addClass('btn-secondary btn-warning');
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

