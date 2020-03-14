$(function () {
    loadAll();   
});

function loadAll() {
    $.ajax({
        url: '/Admin/ProductCategory/LoadAll',
        data: {
            name: name
        },
        dataType: 'json',
        type: 'GET',
        success: function (data) {
            var html = '';
            var template = $('#template-product-category').html();

            $.each(data.list, function (index, item) {
                html += Mustache.render(template, {
                    Id: item.Id,
                    Name: item.Name
                });
            });

            $('#product-category-data').html(html);
        }
    });
}

function displayCreateModal() {
    $.ajax({
        cache: false,
        url: '/Admin/ProductCategory/Create',        
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

function onSuccessCreatePostCategory(jsonResponse) {
    if (jsonResponse.success) {
        $('#create-product-category-modal').modal('hide');

        loadAll();

        toastr.success(jsonResponse.name + " has been created successfully.");
    }
    else {
        toastr.error("Cannot create post category.");
    }
}

function generateAlias() {
    let name = $('#product-category-name').val();

    $.ajax({
        url: '/Admin/ProductCategory/GetAlias',
        data: {
            name: name
        },
        type: 'GET',
        success: function (alias) {
            $('#product-category-alias').val(alias);
        },
        error: function (err) {
            console.log(err);
        }       
    });
}

function displayEditModal(id) {
    $.ajax({
        cache: false,
        url: '/Admin/ProductCategory/Edit',
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
            toastr.error('Cannot get this post category data.');
        }        
    });
}

function onSuccessEditPostCategory(jsonResponse) {
    if (jsonResponse.success) {
        $('#edit-product-category-modal').modal('hide');

        loadAll();

        toastr.success("Updated post category successfully.");
    }
    else {
        toastr.error("Cannot update this post category.");
    }
}

function displayConfirmDeletetionModal(id, name) {
    $.ajax({
        cache: false,
        url: '/Admin/ProductCategory/Delete',
        data: {
            productCategoryId: id,
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

function onSuccessDeleteProductCategory() {
    $('#confirm-delete-product-category-modal').modal('hide');

    toastr.success('Delete successfully.');

    loadAll();
}