var productCategoryCreate = {
    init: function () {
        productCategoryCreate.registerEvent();
    },

    registerEvent: function () {
        $('#product-category-name').off('keyup').on('keyup', function (e) {
            if (e.which === 13) {
                $('#poduct-category-create-form').submit();
            }

            productCategoryCreate.generateAlias($('#product-category-name').val());
        });

        $('#btn-delete-product-category').off('click').on('click', function (e) {
            e.preventDefault();
            displayConfirmDeletetionModal(this.data('id'));
        });
    },

    generateAlias: function (name) {
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
    },

    displayConfirmDeletetionModal: function (id) {
        $.ajax({
            cache: false,
            url: '/Admin/ProductCategory/Delete',
            data: {
                productCategoryId: id
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
};

productCategoryCreate.init();