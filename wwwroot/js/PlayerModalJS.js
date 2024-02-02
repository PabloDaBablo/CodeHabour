$(document).ready(function () {
    GetPlayers();
});

/* Read Data */
function GetPlayers() {
    $.ajax({
        url:'/PlayerModal/GetPlayers',
        type:'get',
        dataType:'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = ' ';
                object += '<tr>';
                object += '<td colspan="5">' + 'Players not found' + '</td>';
                object += '</tr>';
                $('#tblBody').html(object);//doesnt work at the moment

            }
            else {
                var object = ' ';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + (item.divAge || '') + '</td>';
                    object += '<td>' + (item.playerFirstName || '') + '</td>'; 
                    object += '<td>' + (item.playerLastName || '') + '</td>'; 
                    object += '<td>' + (item.playerNumber || '') + '</td>'; 
                    object += '<td>' + (item.teamName || '') + '</td>'; 
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a></td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object);

            }

        },
        error: function () {
            alert('Unable to Read Player Data.');
        }
    })
} 

$(document).ready(function () {
    var currentPage = 1;
    var pageSize = 10; 

    function GetPlayers(page) {
        $.ajax({
            url: `/PlayerModal/GetPlayers?page=${page}&pageSize=${pageSize}`, 
            type: 'GET',
            success: function (response) {
                updateTable(response);
                $('#pageIndicator').text(`Page: ${currentPage}`);
            },
            error: function (error) {
                console.error("Error fetching players:", error);
                alert('Unable to Read Player Data.');
            }
        });
    }

    function updateTable(players) {
        var tableBody = $('#tblBody');
        tableBody.empty();

        players.forEach(function (player) {
            var row = `<tr>
            <td>${player.divAge || ''}</td>
            <td>${player.playerFirstName || ''}</td>
            <td>${player.playerLastName || ''}</td>
            <td>${player.playerNumber || ''}</td>
            <td>${player.teamName || ''}</td>
            <td><a href="#" class="btn btn-primary btn-sm" onclick="Edit(${player.id})">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(${player.id})">Delete</a></td>
            </tr>`;

            tableBody.append(row);
        });
    }

    $('#prevPage').click(function () {
        if (currentPage > 1) {
            currentPage--;
            GetPlayers(currentPage);
        }
    });

    $('#nextPage').click(function () {
        currentPage++; 
        GetPlayers(currentPage);
    });

    GetPlayers(currentPage);
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
function Edit(id)
{
    $.ajax({
        url:'/PlayerModal/Edit?id=' + id,
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
 