$(document).ready(function () {

    // Permite levantar la vista modal
    $("#btnAgregar").click(function () {
        $('.ui.modal').modal('show');
    });


    // Genera el context menu
    $("#tblClientes tbody tr").each(function (indice, fila) {
        var NIT = $(fila).attr("id");

        $.contextMenu({
            selector: '#'+NIT,
            callback: function (key, options) {
                var m = "clicked: " + key;
                //window.console && console.log(m) || alert(m);

                switch (key) {
                    case "editar":
                        $("#modalModificar").load(`ModificarClienteView?nit=${NIT}`, function () {
                            $('#modalModificar').modal('show'); 

                            var $modificarForm = $("#modificarForm");

                            $modificarForm.submit(function (e) {
                                e.preventDefault();
                                e.stopPropagation();


                                var path = `${$("html").attr("data-root")}/`;


                                var modificarReq = $.ajax({
                                    url: `${path}Cliente/Modificar`,
                                    type: "post",
                                    contenType: 'application/x-www-form-urlencoded; charset=utf-8',
                                    dataType: "json",
                                    data: $modificarForm.serialize()
                                });

                                modificarReq.done(function (data) {
                                    if (data.tipo == 1) {
                                        
                                        iziToast.success({
                                            title: 'Realizado',
                                            message: data.men
                                        });
                                        location.reload();
                                    }
                                    else {
                                        iziToast.error({
                                            title: 'Error',
                                            message: data.men
                                        });
                                    }
                                });

                                modificarReq.fail(function () {
                                    iziToast.error({
                                        title: 'Hubo un problema',
                                        message: "No se a ejecutado la consulta ajax"
                                    });
                                });
                            });
                        });   
                        break;

                    case "eliminar":
                        iziToast.question({
                            timeout: 2000,
                            close: false,
                            overlay: true,
                            displayMode: 'once',
                            id: 'question',
                            zindex: 999,
                            title: 'Eliminar',
                            message: '¿Está seguro de eliminar el registro?',
                            position: 'center',
                            buttons: [
                                ['<button><b> Si </b></button>', function (instance, toast) {

                                    var path = `${$("html").attr("data-root")}/`;

                                    var eliminarReq = $.ajax({
                                        url: `${path}Cliente/eliminar`,
                                        type: "post",
                                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                                        dataType: "json",
                                        data: { nit: NIT}
                                    });

                                    eliminarReq.done(function (data) {
                                            iziToast.success({
                                                title: 'Realizado',
                                                message: data.men
                                        });
                                        location.reload();
                                    });

                                    eliminarReq.fail(function () {
                                        iziToast.warning({
                                            title: 'Error',
                                            message: 'No se ejecuto la consulta ajax'
                                        });
                                    });
                                }, true],
                                ['<button> No </button>', function (instance, toast) {

                                    instance.hide({ transitionOut: 'fadeOut' }, toast, 'button');

                                }],
                            ],
                            onClosing: function (instance, toast, closedBy) {
                                console.info('Closing | closedBy: ' + closedBy);
                            },
                            onClosed: function (instance, toast, closedBy) {
                                console.info('Closed | closedBy: ' + closedBy);
                            }
                        });
                        break;
                }
            },
            items: {
                "editar": { name: "Editar", icon: "edit" },
                "eliminar": { name: "Eliminar ", icon: "delete" },
                "sep1": "---------",
                "salir": {
                    name: "Quit", icon: function () {
                        return 'context-menu-icon context-menu-icon-quit';
                    }
                }
            }
        });
    });

    // Permite generar un accion a traves del submit del formulario para crear un nuevo cliente
    var $crearForm = $("#crearForm");

    $crearForm.submit(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var validacion = false;

        var path = `${$("html").attr("data-root")}/`;

            var crearReq = $.ajax({
                url: `${path}Cliente/Crear`,
                type: "post",
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                dataType: "json",
                data: $crearForm.serialize()
            });

            crearReq.done(function (data) {
                if (data.tipo == 1) {
                    iziToast.success({
                        title: 'Realizado',
                        message: data.mensaje
                    });
                    location.reload();
                } else {
                    iziToast.error({
                        title: 'Error',
                        message: data.mensaje
                    });
                }
            });

            crearReq.fail(function () {
                iziToast.error({
                    title: 'Error',
                    message: "No se ejecuto la consulta ajax"
                });
            });
        
    });


    // Permite recibir el fomulario de moficiar y poder ejecutar el método cuando haya un submit en el 
    



});