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
                    Name: item.Name
                });
            });

            $('#product-data').html(html);
        }
    });
}

function displayCreateModal() {
    $.ajax({
        cache: false,
        url: '/Admin/Product/Create',
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

function generateAlias() {
    let name = $('#product-name').val();

    $.ajax({
        url: '/Admin/Product/GetAlias',
        data: {
            name: name
        },
        type: 'GET',
        success: function (alias) {
            $('#product-alias').val(alias);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function onSuccessCreateProductCategory(jsonResponse) {
    if (jsonResponse.success) {
        $('#create-product-modal').modal('hide');

        loadAll();

        toastr.success(jsonResponse.name + " has been created successfully.");
    }
    else {
        toastr.error("Cannot create post category.");
    }
}