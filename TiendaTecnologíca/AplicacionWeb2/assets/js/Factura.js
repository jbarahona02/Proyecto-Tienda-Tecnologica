$(document).ready(function () {
    var fecha = new Date;

    $("#fecha").val(fecha.getDate() + "/" + (fecha.getMonth() + 1) + "/" + fecha.getFullYear());

    $('#listaCli').dropdown();
    //$("#listaMat").dropdown();



    $("#listaCli").change(function () {

        var path = `${$("html").attr("data-root")}/`;

        var NIT = $("#listaCli").val();

        var peticion = $.ajax({
            url: `${path}Factura/PeticionCliente`,
            type: "post",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            dataType: "json",
            data: { nit: NIT }
        });

        peticion.done(function (data) {
            console.log(data.client);

            $("#nombreCli").val(data.client.nombreCliente);
            $("#telefonoCli").val(data.client.telefono);
        });
    });

    var pathG = `${$("html").attr("data-root")}/`;

    var peticionLista = $.ajax({
        url: `${pathG}Factura/ListaMateriales`,
        type: "get",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        dataType: "json",
    });

    peticionLista.done(function (data) {
        var availableTags = new Array;

        $(data.lista).each(function (inde, e) {
            var tag = `${e.codigoMaterial} - ${e.nombreMaterial}`;

            availableTags.push(tag);
        });

        $("#tags").autocomplete({
            source: availableTags
        });

        localStorage.setItem("lista-materiales", JSON.stringify(data.lista));
    });



    /* var peticionLista = $.ajax({
         url: `${pathG}Factura/ListaMateriales`,
         type: "get",
         contentType: 'application/x-www-form-urlencoded; charset=utf-8',
         dataType: "json",
     });*/

    //peticionLista.done(function (data) {

    //});



    /*
    var path = `${$("html").attr("data-root")}/`;

    var peticionLista = $.ajax({
        url: `${path}Factura/ListaMateriales`,
        type: "get",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        dataType: "json",
    });

    peticionLista.done(function (data) {
        $(data.lista).each(function (index, element) {
            let template = `<option value="${element.codigoMaterial}">${element.codigoMaterial} . ${element.nombreMaterial} </option>`;
            $("#listaMat").append(template);
        });
    });*/


    /*
    $(data.lista).each(function (index, element) {
        let template = `<option value="${element.codigoMaterial}">${element.codigoMaterial}</option>`;
        $("#lista" + newCodigo).append(template);
        $("#lista" + newCodigo).dropdown();
    });*/

    /*
    $("#listaMat").change(function () {
        
    });*/

    /*
    $(".input-cantidad").each(function (index, element) {
        $(element).change(function () {
            var precio = $(`#${$(element).attr("data-precio")}`).val().substr(1),
                total = $(`#${$(element).attr("data-total")}`),
                cantidad = $(element).val();

            total = ();
        });
    });*/

    /*
    $("#txtCantidad").change(function () {
        console.log(this);
        var precio = $("#txtPrecio").val().substr(1);
        var cantidad = $("#txtCantidad").val();

        $("#txtPrecioTotal").val(parseFloat(precio) * parseInt(cantidad));
    });*/

    $("#btnAgregar").click(function () {

        if ($("#tags").val() != "") {
            var codigoM = $("#tags").val();
            var codM = codigoM.substr(0, 1);
            var codigosEnTabla = new Array();

            $(".codigos").each(function (indice, elemento) {
                codigosEnTabla.push($(elemento).val());
            });

            /*Aqui permite evaluar si en esa lista de los codigos de la tabla busque un codigo donde ese codigo sea igual al que yo seleccione
             * en el input si me regresa un valor es que ya esta en la tabla por lo que ya no lo vuelve a agregar en cambio si me devuelve undefined
             * si lo agrega
             */
            var buscado = codigosEnTabla.find(mat => mat === codM);

            if (buscado === undefined) {
                var codigos = new Array;
                var newCodigo = 1;

                // Permite colocarle el còdigo correlativo a las filas de la tabla
                if ($("#tblFacturas tbody tr").length > 0) {
                    $("#tblFacturas tbody tr").each(function (indice, fila) {
                        var codF = `${$(fila).attr("id")}`;

                        var cod = codF.substr(4);
                        codigos.push(cod);

                    });
                    codigos.sort();
                    newCodigo = newCodigo + parseInt(codigos[codigos.length - 1]);
                }

                /*// Permite asignar el listMAtLocal el valor del local storage llamado lista Materiales, el parseo es para poder pasarlo a una lista
                // luego al matLocal le asigno de la lista de materiales busco si existe que me devuelva un material donde el codigo de ese material
                // sea igual al que yo jalo con el substring lo casto por que de la DB el codigo viene como int entonces ahì ya obtengo el material
                   con sus atributos.*/

                var listMatLocal = JSON.parse(localStorage.getItem("lista-materiales")),
                    matLocal = listMatLocal.find(mat => mat.codigoMaterial === parseInt(codM));

                var template = `<tr id="fila${newCodigo}">
                                <td>
                                    <div class="ui action input">
                                    <input id="idMat" class="codigos" value="${codM}" type="text" disabled placeholder="Código de material" data-descripcion="txtDescripcion${newCodigo}" data-precio="precio${newCodigo}">
                                    </div>
                                </td>
                                <td>
                                    <div class="ui fluid action input">
                                    <input type="text" placeholder="Detalle de material" value="${matLocal.detalleMaterial}" id="txtDescripcion${newCodigo}" disabled>
                                    </div>
                                </td>

                                <td>
                                    <div class="ui fluid action input">
                                    <input type="text" disabled placeholder="Precio unitario" id="precio${newCodigo}" value="${matLocal.precioMaterial}">
                                    </div>
                                </td>
                                <td>
                                    <div class="ui fluid action input">
                                    <input type="number" min="0" class="input-cantidad" data-total="total${newCodigo}" data-precio="precio${newCodigo}" placeholder="Cantidad">
                                    </div>
                                </td>
                                <td>
                                    <div class="ui fluid action input">
                                    <input type="text" placeholder="Precio total" id="total${newCodigo}" disabled>
                                    </div>
                                </td>
                                <td>
                                    <button class="ui button" id="btnEliminar${newCodigo}"> Eliminar </button>
                                </td>
                            </tr>`;

                $("#tblFacturas").append(template);
                $("#tags").val("");

                $(".input-cantidad").each(function (index, element) {
                    $(element).change(function () {
                        var precio = $(`#${$(element).attr("data-precio")}`).val().substr(1),
                            total = $(`#${$(element).attr("data-total")}`),
                            cantidad = $(element).val();

                        total = (parseFloat(precio) * parseInt(cantidad));

                        $(`#${$(element).attr("data-total")}`).val("Q" + total);
                    });
                });
            } else {
                $("#tags").val("");
                iziToast.show({
                    title: 'Atención',
                    message: 'El material que eligio ya exite dentro de la tabla'
                });
            }
        }
        else {
            iziToast.show({
                title: 'Atención',
                message: 'Debe de seleccionar un material'
            });
            /* });*/
        }

        /*Función que permite eliminar el registro de la tabla según el boton seleccionado*/
        $("#btnEliminar" + newCodigo).click(function () {
            $("#fila" + newCodigo).remove();

        });

    });

    
    /*Función que permite ejecutar un proceso al captar el evento click del boton pdf*/
    $("#btnPDF").click(function () {
        var NIT = $("#listaCli").val();
        var cantidad = false;
        var pdf = false;
        var men = true;

        $(".input-cantidad").each(function (id, input) {
            //var can = $(input).val();
            if ($(input).val() != "" && $(input).val() != "0") {
                cantidad = true;
            } else {
                cantidad = false;
            }
        });

        if (NIT != "") {
            pdf = true;
        } else {
            iziToast.warning({
                title: 'Atención',
                message: 'No a seleccionado un NIT'
            });
            men = false;
            pdf = false;
        }

        if (men) {
            if ($("#tblFacturas tbody tr").length > 0) {
                pdf = true;
            } else {
                iziToast.warning({
                    title: 'Atención',
                    message: 'Debe de agregar al menos un material'
                });
                men = false;
                pdf = false;
            }
        }

        if (men) {
            if (cantidad == true) {
                pdf = true;
            } else {
                iziToast.warning({
                    title: 'Atención',
                    message: 'Agregue todas las cantidades'
                });
                pdf = false;
            }
        }

        if (pdf) {
            var nit = $("#listaCli").val();
            var fecha = $("#fecha").val();
            var codPos = 1;
            var listaPos = new Array;
            var listaMat = new Array;
            var listaCantidad = new Array;

            $("#tblFacturas tbody tr").each(function (indi) {
                listaPos.push(codPos);
                codPos = codPos + 1;
            });

            $(".codigos").each(function (indi, inputMat) {
                var codMat = $(inputMat).val();
                listaMat.push(codMat);
            });

            $(".input-cantidad").each(function (indi, inputCant) {
                var cantidad = $(inputCant).val();
                listaCantidad.push(cantidad);
            });

            var path = `${$("html").attr("data-root")}/`;

            var peticionCrearFactura = $.ajax({
                url: `${path}Factura/CrearFactura`,
                type: "post",
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                dataType: "json",
                data: { nit: nit, fec: fecha, codPos: listaPos, codMat: listaMat, cantidad: listaCantidad }
            });

            peticionCrearFactura.done(function (data) {
                iziToast.success({
                    title: 'Realizado',
                    message: data.mensaje
                });
            });

            peticionCrearFactura.fail(function () {
                iziToast.warning({
                    title: 'Atención',
                    message: 'No se a ejecutado la consulta ajax'
                });
            });

            location.reload();
        }

    });

    $("#tblFacturasCreadas tbody tr").each(function (indic, filaFac) {
        var codigoFac = $(filaFac).attr("id");

        $.contextMenu({
            selector: '#'+codigoFac,
            callback: function (key, options) {
                var m = "clicked: " + key;
               // window.console && console.log(m) || alert(m);

                switch (key) {
                    case "copy":
                        var path = `${$("html").attr("data-root")}/`;

                        var peticionPDF = $.ajax({
                            url: `${path}Factura/GenerarReporte`,
                            type: "post",
                            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                            dataType: "json",
                            beforeSend: function () {
                                $("body").preloader({
                                    text: 'Cargando el PDF'
                                });
                                },
                            data: { codFac: codigoFac}
                        });

                        
                        //window.location.href = `${path}Factura/GenerarReport?codFac=${codigoFac}`;

                        peticionPDF.done(function (data) {
                            if (data.codigo == 1) {
                                //$("#PDF").preloader();
                                $("body").preloader('remove');
                                window.location.href = `${path}PDF/Factura.pdf`;
                            }
                        });
                        break;
                }
            },
            items: {
                "copy": { name: "Imprimir", icon: "edit" },
                "sep1": "---------",
                "quit": {
                    name: "Quit", icon: function () {
                        return 'context-menu-icon context-menu-icon-quit';
                    }
                }
            }
        });
    });
});  