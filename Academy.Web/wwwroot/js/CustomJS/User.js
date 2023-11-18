var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            {
                "data": "userAvatar",
                "render": function (data) {
                    return `
                        <img class="rounded" src="/images/UserAvatar/${data}" />
                        `
                },
                "width": "15%"
            },
            { "data": "userName", "width": "15%" },
            { "data": "email", "width": "15%" },
            {
                "data": "isActive",
                "render": function (data) {
                    if (data == true) {
                        return `
                        <p class="text-success">فعال</p>
                        `
                    } else {
                        return `
                        <p class="text-danger">غیر فعال</p>
                        `
                    }
                },
                "width": "15%"
            },
            { "data": "registerDate", "width": "15%" },
            {
                "data": "userId",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/User/Upsert?id=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Delete('/Admin/User/Delete/${data}')
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					</div>
                        `
                },
                "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}