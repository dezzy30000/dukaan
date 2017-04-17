// Function to check element is exist
$.fn.exist = function () {
    return $(this).length > 0;
}

// Function to get window width
function get_width() {
    return $(window).width();
}

$(function () {

    var owlCarousels = [{ "element": $('#home_slider'), "options": { items: 1, loop: true, autoplay: true, autoplayHoverPause: true, dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], } }, { "element": $('#widget_slider_new_arrivals'), "options": { items: 1, dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], responsive: { 0: { items: 1, }, 480: { items: 2, }, 768: { items: 3, }, 992: { items: 1, } } } }, { "element": $('#widget_slider_best_selling'), "options": { items: 1, dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], responsive: { 0: { items: 1, }, 480: { items: 2, }, 768: { items: 3, }, 992: { items: 1, } } } }, { "element": $('#product_slider'), "options": { dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], responsive: { 0: { items: 1, }, 480: { items: 2, }, 768: { items: 3, }, 992: { items: 3, }, 1200: { items: 4, } } } }, { "element": $('#related_product_slider'), "options": { dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], responsive: { 0: { items: 1, }, 480: { items: 2, }, 768: { items: 3, }, 992: { items: 5, }, 1200: { items: 6, } } } }, { "element": $('#brand_slider'), "options": { dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], responsive: { 0: { items: 1, }, 480: { items: 2, margin: 15 }, 768: { items: 3, margin: 15 }, 992: { items: 4, margin: 30 }, 1200: { items: 6, margin: 30 } } } }];

    // open navigation dropdown on hover (only when width >= 768px)
    $('ul.nav li.dropdown').hover(function () {
        if (get_width() >= 767) {
            $(this).addClass('open');
        }
    }, function () {
        if (get_width() >= 767) {
            $(this).removeClass('open');
        }
    });

    // Navigation submenu
    $('ul.dropdown-menu [data-toggle=dropdown]').on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        $(this).parent().siblings().removeClass('open');
        $(this).parent().toggleClass('open');
    });

    // Carousels
    for (var index = 0; index < owlCarousels.length; index++) {
        var element = owlCarousels[index]["element"];
        if (element.exist()) {
            element.owlCarousel(owlCarousels[index]["options"])
        }
    }

    // Tooltip
    $('button[data-toggle="tooltip"]').tooltip({ container: 'body', animation: false });
    $('a[data-toggle="tooltip"]').tooltip({ container: 'body', animation: false });

    // Back top Top
    $(window).scroll(function () {
        if ($(this).scrollTop() > 70) {
            $('.back-top').fadeIn();
        } else {
            $('.back-top').fadeOut();
        }
    });

    // Touchspin
    if ($('.input-qty').exist()) {
        $('.input-qty input').TouchSpin({
            verticalbuttons: true,
            prefix: 'qty'
        });
    }

    var dropdowncartbutton = $('#dropdown-cart');
    var dropdowncartquantitytext = $('#dropdown-cart-quantity-text');
    var dropdowncartcaret = $('#dropdown-cart-caret');
    var cartlink = $('#cart-link');
    var cartlinkquantitytext = $('#cart-link-quantity-text');

    simpleCart.bind('update', function () {
        if (this.quantity() === 0) {
            cartlinkquantitytext.text('items');
            cartlink.addClass('disabled');
            dropdowncartquantitytext.text('items');
            dropdowncartbutton.prop('disabled', true);
            dropdowncartcaret.addClass('invisible');
        } else {
            cartlinkquantitytext.text(this.quantity() === 1 ? 'item' : 'items');
            cartlink.removeClass('disabled');
            dropdowncartquantitytext.text(this.quantity() === 1 ? 'item' : 'items');
            dropdowncartbutton.prop('disabled', false);
            dropdowncartcaret.removeClass('invisible');
        }
    });

    simpleCart({
        cartColumns: [{
                atrr: 'cartitem',
                label: false,
                view: function (item, column) {
                    return '<div class="media"><div class="media-left"><a href="#"><img class="media-object img-thumbnail" src="' + item.get('image').replace('-', '-small-') + '" width="50" alt="product"></a></div>	<div class="media-body"><a href="#" class="media-heading">' + item.get('name') + '</a><div>x ' + item.quantity() + ' ' + this.toCurrency(item.get('price')) + '</div></div><div class="media-right"><a href="javascript:;" class="simpleCart_remove" data-toggle="tooltip" title="Remove"><i class="fa fa-remove"></i></a></div></div>';
                }
            }
        ]
    });

});