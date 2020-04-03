function registerQuickViewProductOnClickedEvent(id) {
    $.ajax({
        cache: false,
        url: '/Product/QuickView',
        type: 'GET',
        data: {
            productId: id
        },
        success: function (htmlResult) {
            $(htmlResult).appendTo('.modal-quickview');
            
            $('.js-modal1').addClass('show-modal1');
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function hideQuickViewModal() {
    $('.js-modal1').removeClass('show-modal1');
}