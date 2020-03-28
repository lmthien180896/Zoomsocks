function showQuickViewProduct(id) {
    $.ajax({
        cache: false,
        url: '/Products/QuickView',
        type: 'GET',
        data: {
            productId: id
        },
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