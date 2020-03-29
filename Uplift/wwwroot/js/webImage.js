var dataTable;
$(function() {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": {
            "url": "/admin/webimage/GetAll",
            "type": "GET",
            "dataType":"json",
        },
        "columns": [
            {"data":"name","width":"50%"},
            {
                "data": "id",
                "render": function(data) {
                    return `<div class="text-center">
                            <a href="/Admin/webimage/Upsert/${data}"
                         class='btn btn-success text-white" style="cursor:pointer;width:100px;'>
                            <i class='far fa-edit'></i> Edit
                            </a>
                            &nbsp
                            <a onclick=Delete("/Admin/webimage/Delete/${data}")
                         class='btn btn-danger text-white' style="cursor:pointer;width:100px;">
                            <i class='far fa-trash-alt'></i> Delete
                            </a>
                            `;
                },
                "width": "30%"
            },
        ],
        "language": {
            "emptyTable":"No Records Found."
        },
        "width":"50%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not able to restore the content!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes,delete it!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            url: url,
            type: "Delete",
            dataType: "json",
            success: (data) => {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable._fnAjaxUpdate();
                }
                else {
                    toastr.error(data.message);
                }
                
            },
            error: (err) => {
                console.error(err);
            }
        });

    });
    
}
