$(function () {
    var owlCarousels = [{ "element": $('#home_slider'), "options": { items: 1, loop: true, autoplay: true, autoplayHoverPause: true, dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], } }, { "element": $('#widget_slider_new_arrivals'), "options": { items: 1, dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], responsive: { 0: { items: 1, }, 480: { items: 2, }, 768: { items: 3, }, 992: { items: 1, } } } }, { "element": $('#widget_slider_best_selling'), "options": { items: 1, dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], responsive: { 0: { items: 1, }, 480: { items: 2, }, 768: { items: 3, }, 992: { items: 1, } } } }, { "element": $('#product_slider'), "options": { dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], responsive: { 0: { items: 1, }, 480: { items: 2, }, 768: { items: 3, }, 992: { items: 3, }, 1200: { items: 4, } } } }, { "element": $('#brand_slider'), "options": { dots: false, nav: true, navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], responsive: { 0: { items: 1, }, 480: { items: 2, margin: 15 }, 768: { items: 3, margin: 15 }, 992: { items: 4, margin: 30 }, 1200: { items: 6, margin: 30 } } } }];

    for (var index = 0; index < owlCarousels.length; index++) {
        owlCarousels[index]["element"].owlCarousel(owlCarousels[index]["options"])
    }
});