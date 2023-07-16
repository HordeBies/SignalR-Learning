var dataTable;
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/order").build();

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Home/GetAllOrder"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "15%" },
            { "data": "itemName", "width": "15%" },
            { "data": "count", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href=""
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> </a>
                      
					</div>
                        `
                },
                "width": "5%"
            }
        ],
        "order": [[0, 'desc']],
    });
}


connection.on("NewOrderCreated", function () {
    Swal.fire({
        position: 'top-end',
        icon: 'info',
        title: 'There is a new Order',
        showConfirmButton: false,
        timer: 1500
    });
    dataTable.ajax.reload();
});

connection.start().then(function () {
    console.log("connection started");
});