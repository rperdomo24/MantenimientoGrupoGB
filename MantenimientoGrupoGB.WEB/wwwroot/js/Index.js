$(document).ready(function () {
    $.fn.dataTable.moment("DD/MM/YYYY HH:mm:ss");
    $.fn.dataTable.moment("DD/MM/YYYY");

    $("#table").DataTable({
        // Design Assets
        stateSave: true,
        autoWidth: true,
        // ServerSide Setups
        processing: true,
        serverSide: true,
        // Paging Setups
        paging: true,
        // Searching Setups
        //searching: { regex: true },
        // Ajax Filter
        ajax: {
            url: "/AdministrarUsuario/Listar",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: function (d) {
                return JSON.stringify(d);
            }
        },
        // Columns Setups
        columns: [
            { data: "nombres" },
            { data: "apellidos" },
            { data: "dui" },
            { data: "nit" },
            { data: "isss" },
            { data: "telefono" },
            {
                data: "fechaNacimiento",
                render: function (data, type, row) {
                    // If display or filter data is requested, format the date
                    if (type === "display" || type === "filter") {
                        return moment(data).format("DD/MM/YYYY");
                    }
                    // Otherwise the data type requested (`type`) is type detection or
                    // sorting data, for which we want to use the raw date value, so just return
                    // that, unaltered
                    return data;
                }
            },
            {
                "mData": null,
                "mRender": function (data, type, row, meta) {
                    console.log(data);
                    var url = "/AdministrarUsuario";
                    var EditaData = "<a href='" + url + "/Update/" + data.idUsuario + "' class='btn btn-warning'>Modificar</a>";
                    var Deletedata = "<a href='#' onclick='EliminarProveedor('Confirmar eliminar', 'esta seguro que desea eliminar el usuario?', warning, " + data.idUsuario + ")' class='btn btn-danger'>Eliminar</a>";
                    var result = "<div class='btn-group mr-2' role='group' aria-label='First group'>";
                    result += EditaData;
                    result += Deletedata;
                    result += "</div>";
                    return result;
                }
            }
        ],
        //Column Definitions
        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" }
        ]
    });
});

function strtrunc(str, num) {
    if (str.length > num) {
        return str.slice(0, num) + "...";
    }
    else {
        return str;
    }
}