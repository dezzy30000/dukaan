$(function () {
    var browserWindow = $(window);
    var storageKey = 'simpleCart_items';
    var dropdowncartbutton = $('#dropdown-cart');
    var dropdowncartquantitytext = $('#dropdown-cart-quantity-text');
    var dropdowncartcaret = $('#dropdown-cart-caret');
    var cartlink = $('#cart-link');
    var cartlinkquantitytext = $('#cart-link-quantity-text');

    // Open navigation dropdown on hover (only when width >= 768px)
    $('ul.nav li.dropdown').hover(function () {
        if (browserWindow.width() >= 767) {
            $(this).addClass('open');
        }
    }, function () {
        if (browserWindow.width() >= 767) {
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

    // Tooltip
    $('button[data-toggle="tooltip"]').tooltip({ container: 'body', animation: false });
    $('a[data-toggle="tooltip"]').tooltip({ container: 'body', animation: false });

    // Back top Top
    browserWindow.scroll(function () {
        if ($(this).scrollTop() > 70) {
            $('.back-top').fadeIn();
        } else {
            $('.back-top').fadeOut();
        }
    });

    //TODO: Performance tweak. Only do on add and remove. Server will render cart on return from server.
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

    simpleCart.bind('afterSave', function () {
        Cookies.set(storageKey, localStorage.getItem(storageKey));
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