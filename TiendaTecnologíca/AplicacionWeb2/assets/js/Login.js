$(document).ready(function () {

    var $loginForm = $("#loginForm");

    iziToast.settings({
        title: '',
        titleColor: '',
        message: '',
        messageColor: '',
        messageSize: 14,
        theme: 'light',
        icon: '',
        iconText: '',
        iconColor: '',
        close: true,
        closeOnEscape: true,
        closeOnClick: true,
        displayMode: 0,
        timeout: 4000,
        animateInside: false,
        pauseOnHover: true,
        resetOnHover: false,
        progressBar: true,
        progressBarColor: '',
        progressBarEasing: 'linear',
        transitionIn: 'bounceInLeft',
        transitionOut: 'fadeOutRight',
        buttons: {},
        inputs: {},
        onOpening: () => { },
        onOpened: () => { },
        onClosing: () => { },
        onClosed: () => { }
    });

    $loginForm.submit(function (e) {
        e.preventDefault();
        e.stopPropagation();

        var path = `${$("html").attr("data-root")}/`;

        var loginReq = $.ajax({
            url: `${path}Login/Login`,
            type: "post",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            dataType: "json",
            data: $loginForm.serialize()
        });

        loginReq.done(function (data) {
            if (data.tipo == 1) {
                window.location.href = `${path}Principal/PrincipalView`;
            } else
                iziToast.error({
                    title: 'Error',
                    message: data.mensaje
                });
        });

        loginReq.fail(function () {
            iziToast.error({
                title: 'Error',
                message: "Error en la peticion ajax"
            });
        });
    });
});