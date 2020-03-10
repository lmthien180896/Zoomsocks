var homeconfig = {
    pageSize: 5,
    pageIndex: 1,
}
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
    }
};

productCategoryCreate.init();