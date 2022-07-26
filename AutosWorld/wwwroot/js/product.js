var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "10%" },
            { "data": "model", "width": "10%" },
            
            { "data": "year", "width": "10%" },
            { "data": "price", "width": "10%" },
            { "data": "description", "width": "10%" },
            { "data": "category.name", "width": "10%" },
            { "data": "coverType.name", "width": "10%" },
            {
                "data": "id", "render": function (data) {
                    return `
                          <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}"  class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>
                            <a asp-controller="Category" asp-action="Delete" asp-route-id="@cat.Id" class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i> Delete</a>
                          </div>
                    `;
                }, "width": "10%" }
        ]
    });
} 
