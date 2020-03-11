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
            }
        });
    }
};

productCategoryList.init();