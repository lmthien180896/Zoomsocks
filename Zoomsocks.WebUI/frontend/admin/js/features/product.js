$(function () {
    loadAll();    
});

function loadAll() {
    $.ajax({
        url: '/Admin/Product/LoadAll',
        data: {
            name: name
        },
        dataType: 'json',
        type: 'GET',
        success: function (data) {
            var html = '';
            var template = $('#template-product').html();

            $.each(data.list, function (index, item) {
                html += Mustache.render(template, {
                    Id: item.Id,
                    Name: item.Name,
                    Category: item.Category
                });
            });

            $('#product-data').html(html);
        }
    });
}

function displayConfirmDeletetionModal(id, name) {
    $.ajax({
        cache: false,
        url: '/Admin/Product/Delete',
        data: {
            productId: id,
            name: name
        },
        type: 'GET',
        success: function (htmlResult) {
            let $modal = $(htmlResult).appendTo('body').modal({
                backdrop: 'static',
                keyboard: false
            });
            $modal.on('hidden.bs.modal', function () {
                $modal.remove();
            });
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function onSuccessDeleteProduct() {
    $('#confirm-delete-product-modal').modal('hide');

    toastr.success('Delete successfully.');

    loadAll();
}

function displayEditModal(id) {
    $.ajax({
        cache: false,
        url: '/Admin/Product/Edit',
        data: {
            id: id
        },
        type: 'GET',
        success: function (htmlResult) {
            let $modal = $(htmlResult).appendTo('body').modal({
                backdrop: 'static',
                keyboard: false
            });
            $modal.on('hidden.bs.modal', function () {
                $modal.remove();
            });
        },
        error: function (err) {
            toastr.error('Cannot get this post data.');
        }
    });
}

function onSuccessEditProduct (jsonResponse) {
    if (jsonResponse.success) {
        $('#edit-product-modal').modal('hide');

        loadAll();

        toastr.success("Updated post successfully.");
    }
    else {
        toastr.error("Cannot update this post.");
    }
}