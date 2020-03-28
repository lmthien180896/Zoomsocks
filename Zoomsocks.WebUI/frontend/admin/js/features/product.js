﻿$(function () {
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

function onSuccessCreateProduct(jsonResponse) {
    if (jsonResponse.success) {
        $('#create-product-modal').modal('hide');

        loadAll();

        toastr.success(jsonResponse.name + " has been created successfully.");
    }
    else {
        toastr.error("Cannot create post category.");
    }
}

function chooseImage() {
    var finder = new CKFinder();

    finder.selectActionFunction = function (url) {
        $('#product-image').val(url);
        $('#img').prop("src", url);
        $('#btn-delete-image').removeClass('hide');
    };

    finder.popup();
}

function chooseMoreImages() {
    var finder = new CKFinder();
    finder.selectActionFunction = function (url) {             
        if ($('#product-more-images').val().indexOf(url) !== -1) {
            alert("Image has already been added.");
        }
        else {
            $('#imagelist').append(`
        <div class="image-item">
            <img src='${url}' style="height: 100px" />
            <button type="button" class="btn-danger" onclick="deleteMoreImage(event)">
                Delete
            </button>
        </div>`);

            let moreImagesList = $('#product-more-images').val();
            if (moreImagesList === "") {
                moreImagesList = moreImagesList + url;
            }
            else {
                moreImagesList = moreImagesList + "," + url;
            }
            $('#product-more-images').val(moreImagesList);
        }
    };

    finder.popup();
}

function deleteImage() {
    $('#product-image').val("");
    $('#img').prop("src", "");
    $('#btn-delete-image').addClass('hide');
}

function deleteMoreImage(event) {
    let removeSource = event.currentTarget.previousElementSibling.attributes[0].value;
    let moreImagesList = $('#product-more-images').val();

    moreImagesList = moreImagesList.replace(removeSource + ",", "");
    moreImagesList = moreImagesList.replace("," + removeSource, "");
    moreImagesList = moreImagesList.replace(removeSource, "");
    $('#product-more-images').val(moreImagesList);
    event.currentTarget.parentElement.remove();
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