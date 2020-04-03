function initializeViewScript() {
    registerProductNameChangeEvent();
    registerUploadImageButtonClickEvent();
    registerUploadMoreImagesButtonClickEvent();    
}

function registerProductNameChangeEvent() {
    $('#product-name').on('keyup', function () {
        let name = $(this).val();

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

function registerUploadImageButtonClickEvent() {
    $('#btn-upload-image').on('click', function () {
        var finder = new CKFinder();

        finder.selectActionFunction = function (url) {
            $('#product-image').val(url);
            $('#main-image').prop("src", url);
        };

        finder.popup();
    });    
}

function registerUploadMoreImagesButtonClickEvent() {
    $('#btn-upload-more-images').on('click', function () {
        var finder = new CKFinder();

        finder.selectActionFunction = function (url) {
            $('#more-images-section').append(
                `
                <div id="more-img">                    
                    <img class="img-preview" src="${url}"/>
                    <button type="button" class="btn btn-danger" id="btn-remove-img">Remove</button>
                    <input type="text" name="MoreImagesList" value="${url}" hidden>
                </div>
            `
            );
        };

        finder.popup();
    });    
}

$(document).on('click', '#btn-remove-img', function (e) {
    e.currentTarget.parentElement.remove();
});