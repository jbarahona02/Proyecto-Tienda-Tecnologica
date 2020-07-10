$(document).ready(function () {

    // Permite levantar la vista modal con el formulario para agregar un material.
    $("#btnAgregar").click(function () {
        $('.ui.modal').modal('show');
    });

    // Va a ejecutar el context menu
    $("#tblMateriales tbody tr").each(function (indice, fila) {
        var codigo = $(fila).attr("id");

            $.contextMenu({
                selector: '#'+codigo,
                callback: function (key, options) {
                    var m = "clicked: " + key;
                    //window.console && console.log(m) || alert(m);

                    switch (key) {
                        case "edit":
                            $("#modalModificarMaterial").load(`ModificarMaterialView?codMat=${codigo}`, function () {
                                $('#modalModificarMaterial').modal('show');

                                var $modificarMaterialForm = $("#modificarMaterialForm");

                                $modificarMaterialForm.submit(function (e) {
                                    e.preventDefault();
                                    e.stopPropagation();

                                    var path = `${$("html").attr("data-root")}/`;

                                    var modificarReq = $.ajax({
                                        url: `${path}Material/ModificarMaterial`,
                                        type: "post",
                                        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                                        dataType: "json",
                                        data: $modificarMaterialForm.serialize()
                                    });

                                    modificarReq.done(function (data) {
                                        if (data.tipo == 1) {
                                            iziToast.success({
                                                title: 'Realizado',
                                                message: data.men
                                            });

                                            location.reload()
                                        }
                                        else {
                                            iziToast.success({
                                                title: 'Sin cambios',
                                                message: data.men
                                            });
                                        }
                                    });

                                    modificarReq.fail(function () {
                                        iziToast.error({
                                            title: 'Error',
                                            message: "No se a ejecutado la consulta ajax"
                                        });
                                    });
                                });
                            });
                            break;

                        case "delete":

                            // Permite preguntar si si quiere eliminar un registro
                            iziToast.question({
                                timeout: 2000,
                                close: false,
                                overlay: true,
                                displayMode: 'once',
                                id: 'question',
                                zindex: 999,
                                title: 'Hey',
                                message: '¿Está seguro de eliminar el registro?',
                                position: 'center',
                                buttons: [
                                    // Boton si
                                    ['<button><b> Si </b></button>', function (instance, toast) {

                                        var path = `${$("html").attr("data-root")}/`;

                                        var eliminarReq = $.ajax({
                                            url: `${path}Material/eliminarMaterial`,
                                            type: "post",
                                            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                                            dataType: "json",
                                            data: { codMat : codigo }
                                        });

                                        eliminarReq.done(function (data) {
                                            iziToast.success({
                                                title: 'Realizado',
                                                message: data.men
                                            });
                                            var mensaje = data.men;
                                            if (mensaje != "No se puede eliminar el registro") {
                                                location.reload();
                                                
                                            }
                                        });

                                        eliminarReq.fail(function () {
                                            iziToast.error({
                                                title: 'Error',
                                                message: 'No se ejecuto la consulta ajax',
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
                    "edit": { name: "Editar", icon: "edit" },
                    "delete": { name: "Eliminar", icon: "delete" },
                    "sep1": "---------",
                    "quit": {
                        name: "Quit", icon: function () {
                            return 'context-menu-icon context-menu-icon-quit';
                        }
                    }
                }
            });
    });

    // Permite llevar cuando haya un submit del formulario 
    var $crearMaterialForm = $("#crearMaterialForm");

    $crearMaterialForm.submit(function (e) {
        e.preventDefault();
        e.stopPropagation();

        var path = `${$("html").attr("data-root")}/`;

        // Ejecuta la consulta ajax para poder a traves de la vista mandar a llamar un método del controlador
        var crearReq = $.ajax({
            url: `${path}Material/crearMaterial`,
            type: "post",
            contenType: 'application/x-www-form-urlencoded; charset=utf-8',
            dataType: "json",
            data: $crearMaterialForm.serialize()
        });

        crearReq.done(function (data) {
            if (data.tipo == 1) {
                iziToast.success({
                    title: 'Realizado',
                    message: data.mensaje
                });
                location.reload();
            }
            else {
                iziToast.error({
                    title: 'Error',
                    message: data.mensaje
                });
            }
        });

        crearReq.fail(function () {
            iziToast.error({
                title: 'Error',
                message: "No se a ejecutado la consulta ajax"
            });
        });
    });

    // Permite mandar a llamar el método por ajax cuando haga submit el en formulario

   
});