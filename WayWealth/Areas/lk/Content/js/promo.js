$(function () {
    var num = 15;

    var promoViewed = localStorage.getItem('promoViewed');

    var modalContainer = $('div#modals');
    var holdModals = document.createDocumentFragment();

    for (var i = 0; i < num; i++) {
        var div = document.createElement('div');
        div.classList.add('modal-drop');
        div.style.top = Math.floor((Math.random() * 100)) + 'vh';
        div.style.left = Math.floor((Math.random() * 100)) + 'vw';
        div.style.transitionDelay = Math.random() + 's';
        holdModals.append(div);
    }

    modalContainer.append(holdModals);

    if (!promoViewed) {
        modalContainer.css("display","block");
        window.setTimeout(function () {
            modalContainer.addClass("active");
            $('html').addClass('ovrflow-h');
        }, 0.1);
        window.setTimeout(function () {
            modalContainer.addClass("animation-end");
        }, 2000);
    }

    $('button#btnClose').on('click', function () {
        modalContainer.removeClass("active animation-end");
        localStorage.setItem('promoViewed', 1);

        window.setTimeout(function () {
            $('html').removeClass('ovrflow-h');
            modalContainer.css("display", "none");
        }, 2000);
    });

    $('button#btnContinue').on('click', function () {
        modalContainer.removeClass("active animation-end");
        localStorage.setItem('promoViewed', 1);

        window.setTimeout(function () {
            $('html').removeClass('ovrflow-h');
            modalContainer.css("display", "none");
        }, 2000);
    });
});