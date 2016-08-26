$(function () {
    var overlay = $('<div id="overlay"></div>');
    $('.Lclose').click(function () {
        $('.Lpopup').hide();
        overlay.appendTo(document.body).remove();
        return false;
    });
    $('.Lx').click(function () {
        $('.Lpopup').hide();
        overlay.appendTo(document.body).remove();
        return false;
    });

    $('.Lclick').click(function () {
        overlay.show();
        overlay.appendTo(document.body);
        $('.Lpopup').show();
        return false;
    });
});
