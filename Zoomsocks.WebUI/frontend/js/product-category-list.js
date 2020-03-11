var homeconfig = {
    pageSize: 5,
    pageIndex: 1,
};

var productCategoryList = {
    init: function () {
        productCategoryList.registerEvent();
    },

    registerEvent: function () {
        $('.btn-delete-product-category').off('click').on('click', function (e) {
            e.preventDefault();
            productCategoryList.displayConfirmDeletetionModal($(this).data('id'), $(this).data('name'));
        });

        $('.btn-edit-product-category').off('click').on('click', function (e) {
            e.preventDefault();
            productCategoryList.displayEditModal($(this).data('id'));
        });

        $('#product-category-name').off('keyup').on('keyup', function (e) {            
            productCategoryList.generateAlias($('#product-category-name').val());
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
            },
            complete: function () {
                productCategoryList.registerEvent();
            }
        });
    },

    displayEditModal: function (id) {
        $.ajax({
            cache: false,
            url: '/Admin/ProductCategory/Edit',
            data: {
                id: id,
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
            },
            complete: function () {
                productCategoryList.registerEvent();
            }
        });
    },

    displayConfirmDeletetionModal: function (id) {
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
            },
            complete: function () {
                productCategoryList.registerEvent();
            }
        });
    }
};

productCategoryList.init();