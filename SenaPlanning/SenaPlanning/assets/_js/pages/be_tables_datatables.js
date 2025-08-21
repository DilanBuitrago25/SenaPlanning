/*
 *  Document   : be_tables_datatables.js
 *  Author     : pixelcave
 *  Description: Custom JS code used in DataTables Page
 */

// DataTables, for more examples you can check out https://www.datatables.net/
class pageTablesDatatables {
    /*
     * Init DataTables functionality
     *
     */
    static initDataTables() {
        // Override a few default classes
        window.jQuery.extend(window.jQuery.fn.DataTable.ext.classes, {
            sWrapper: "dataTables_wrapper dt-bootstrap5",
            sFilterInput: "form-control form-control-sm",
            sLengthSelect: "form-select form-select-sm",
        })

        // Override a few defaults
        window.jQuery.extend(true, window.jQuery.fn.DataTable.defaults, {
            language: {
                lengthMenu: "_MENU_",
                search: "_INPUT_",
                searchPlaceholder: "Search..",
                info: "Page <strong>_PAGE_</strong> of <strong>_PAGES_</strong>",
                paginate: {
                    first: '<i class="fa fa-angle-double-left"></i>',
                    previous: '<i class="fa fa-angle-left"></i>',
                    next: '<i class="fa fa-angle-right"></i>',
                    last: '<i class="fa fa-angle-double-right"></i>',
                },
            },
        })

        // Override buttons default classes
        window.jQuery.extend(true, window.jQuery.fn.DataTable.Buttons.defaults, {
            dom: {
                button: {
                    className: "btn btn-sm btn-success", // Cambio a btn-success para usar verde del SENA
                },
            },
        })

        // Init full DataTable
        window.jQuery(".js-dataTable-full").DataTable({
            pageLength: 10,
            lengthMenu: [
                [5, 10, 15, 20],
                [5, 10, 15, 20],
            ],
            autoWidth: false,
        })

        // Init full extra DataTable
        window.jQuery(".js-dataTable-full-pagination").DataTable({
            pagingType: "full_numbers",
            pageLength: 10,
            lengthMenu: [
                [5, 10, 15, 20],
                [5, 10, 15, 20],
            ],
            autoWidth: false,
        })

        // Init simple DataTable
        window.jQuery(".js-dataTable-simple").DataTable({
            pageLength: 10,
            lengthMenu: false,
            searching: false,
            autoWidth: false,
            dom: "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-6'i><'col-sm-6'p>>",
        })

        // Init DataTable with Buttons
        window.jQuery(".js-dataTable-buttons").DataTable({
            pageLength: 10,
            lengthMenu: [
                [5, 10, 15, 20],
                [5, 10, 15, 20],
            ],
            autoWidth: false,
            buttons: [
                {
                    extend: "copy",
                    className: "btn btn-success btn-sm",
                    text: '<i class="fa fa-copy"></i> Copiar', // Agregando texto personalizado
                    exportOptions: {
                        columns: ":not(.no-export)", // Excluir columnas con clase no-export (Acciones)
                    },
                    action: function (e, dt, button, config) {
                        console.log("[v0] Ejecutando exportación Copy")
                        window.jQuery.fn.dataTable.ext.buttons.copyHtml5.action.call(this, e, dt, button, config)
                    },
                },
                {
                    extend: "csv",
                    className: "btn btn-success btn-sm",
                    text: '<i class="fa fa-file-csv"></i> CSV', // Agregando texto personalizado
                    title: "SenaPlanning - Reporte",
                    filename: () => {
                        var date = new Date()
                        return (
                            "SenaPlanning_" +
                            date.getFullYear() +
                            "-" +
                            String(date.getMonth() + 1).padStart(2, "0") + // Formato mejorado de fecha
                            "-" +
                            String(date.getDate()).padStart(2, "0") +
                            "_" +
                            String(date.getHours()).padStart(2, "0") +
                            "-" +
                            String(date.getMinutes()).padStart(2, "0")
                        )
                    },
                    exportOptions: {
                        columns: ":not(.no-export)",
                    },
                    action: function (e, dt, button, config) {
                        console.log("[v0] Ejecutando exportación CSV")
                        window.jQuery.fn.dataTable.ext.buttons.csvHtml5.action.call(this, e, dt, button, config)
                    },
                },
                {
                    extend: "excel",
                    className: "btn btn-success btn-sm",
                    text: '<i class="fa fa-file-excel"></i> Excel', // Agregando texto personalizado
                    title: "SenaPlanning - Reporte",
                    filename: () => {
                        var date = new Date()
                        return (
                            "SenaPlanning_" +
                            date.getFullYear() +
                            "-" +
                            String(date.getMonth() + 1).padStart(2, "0") + // Formato mejorado de fecha
                            "-" +
                            String(date.getDate()).padStart(2, "0") +
                            "_" +
                            String(date.getHours()).padStart(2, "0") +
                            "-" +
                            String(date.getMinutes()).padStart(2, "0")
                        )
                    },
                    exportOptions: {
                        columns: ":not(.no-export)",
                    },
                    customize: (xlsx) => {
                        var sheet = xlsx.xl.worksheets["sheet1.xml"]

                        // Agregar bordes a todas las celdas
                        window.jQuery("row c", sheet).each(function () {
                            window.jQuery(this).attr("s", "25")
                        })

                        // Personalizar encabezados con color verde del SENA
                        window.jQuery("row:first c", sheet).each(function () {
                            window.jQuery(this).attr("s", "32")
                        })

                        // Agregar información de generación en la primera fila
                        var date = new Date()
                        var dateStr = date.toLocaleDateString("es-CO") + " " + date.toLocaleTimeString("es-CO")

                        // Insertar fila con información de generación
                        window
                            .jQuery("sheetData", sheet)
                            .prepend(
                                '<row r="1">' +
                                '<c r="A1" t="inlineStr"><is><t>Reporte generado el: ' +
                                dateStr +
                                "</t></is></c>" +
                                "</row>",
                            )
                    },
                },
                {
                    extend: "pdf",
                    className: "btn btn-success btn-sm",
                    text: '<i class="fa fa-file-pdf"></i> PDF', // Agregando texto personalizado
                    title: "SenaPlanning - Reporte",
                    filename: () => {
                        var date = new Date()
                        return (
                            "SenaPlanning_" +
                            date.getFullYear() +
                            "-" +
                            String(date.getMonth() + 1).padStart(2, "0") + // Formato mejorado de fecha
                            "-" +
                            String(date.getDate()).padStart(2, "0") +
                            "_" +
                            String(date.getHours()).padStart(2, "0") +
                            "-" +
                            String(date.getMinutes()).padStart(2, "0")
                        )
                    },
                    exportOptions: {
                        columns: ":not(.no-export)",
                    },
                    customize: (doc) => {
                        var date = new Date()
                        var dateStr = date.toLocaleDateString("es-CO") + " a las " + date.toLocaleTimeString("es-CO") // Formato colombiano

                        // Configurar colores del SENA
                        doc.defaultStyle.fontSize = 10
                        doc.styles.tableHeader.fillColor = "#39A900" // Verde SENA
                        doc.styles.tableHeader.color = "white"
                        doc.styles.tableHeader.bold = true
                        doc.styles.tableHeader.alignment = "center" // Centrar encabezados

                        doc.content.splice(0, 1, {
                            text: "SenaPlanning - Sistema de Planeación",
                            style: "title",
                            alignment: "center",
                            color: "#39A900",
                            fontSize: 18,
                            bold: true,
                            margin: [0, 0, 0, 5],
                        })

                        // Agregar fecha de generación
                        doc.content.splice(1, 0, {
                            text: "Reporte generado el: " + dateStr,
                            style: "subheader",
                            alignment: "center",
                            fontSize: 11,
                            margin: [0, 0, 0, 20],
                        })

                        if (doc.content[2] && doc.content[2].table) {
                            // Configurar anchos automáticos
                            doc.content[2].table.widths = Array(doc.content[2].table.body[0].length).fill("*")

                            // Agregar bordes personalizados
                            doc.content[2].layout = {
                                hLineWidth: (i, node) => 1,
                                vLineWidth: (i, node) => 1,
                                hLineColor: (i, node) => "#39A900",
                                vLineColor: (i, node) => "#39A900",
                                fillColor: (rowIndex, node, columnIndex) => {
                                    return rowIndex === 0 ? "#39A900" : null
                                },
                            }
                        }

                        doc.footer = (currentPage, pageCount) => ({
                            columns: [
                                {
                                    text: "SenaPlanning © " + new Date().getFullYear() + " - Sistema de Planeación SENA",
                                    alignment: "left",
                                    fontSize: 8,
                                    color: "#39A900",
                                },
                                {
                                    text: "Página " + currentPage + " de " + pageCount,
                                    alignment: "right",
                                    fontSize: 8,
                                    color: "#39A900",
                                },
                            ],
                            margin: [40, 10],
                        })
                    },
                },
                {
                    extend: "print",
                    className: "btn btn-success btn-sm",
                    text: '<i class="fa fa-print"></i> Imprimir', // Agregando texto personalizado
                    title: "SenaPlanning - Reporte",
                    exportOptions: {
                        columns: ":not(.no-export)",
                    },
                    customize: (win) => {
                        var date = new Date()
                        var dateStr = date.toLocaleDateString("es-CO") + " a las " + date.toLocaleTimeString("es-CO") // Formato colombiano

                        window
                            .jQuery(win.document.body)
                            .css({
                                "font-size": "12px",
                                "font-family": "Arial, sans-serif",
                            })
                            .prepend(
                                '<div style="text-align: center; margin-bottom: 30px; border-bottom: 2px solid #39A900; padding-bottom: 15px;">' +
                                '<h1 style="color: #39A900; margin: 0; font-size: 24px;">SenaPlanning</h1>' +
                                '<h2 style="color: #666; margin: 5px 0; font-size: 16px;">Sistema de Planeación SENA</h2>' +
                                '<p style="margin: 10px 0; font-size: 12px; color: #333;">Reporte generado el: ' +
                                dateStr +
                                "</p>" +
                                "</div>",
                            )

                        // Personalizar tabla con mejores estilos
                        window.jQuery(win.document.body).find("table").css({
                            "border-collapse": "collapse",
                            border: "2px solid #39A900",
                            width: "100%",
                            margin: "20px 0",
                        })

                        // Personalizar encabezados
                        window.jQuery(win.document.body).find("table thead th").css({
                            "background-color": "#39A900",
                            color: "white",
                            border: "1px solid #39A900",
                            padding: "12px 8px",
                            "text-align": "center",
                            "font-weight": "bold",
                            "font-size": "13px",
                        })

                        // Personalizar celdas
                        window.jQuery(win.document.body).find("table tbody td").css({
                            border: "1px solid #39A900",
                            padding: "8px",
                            "text-align": "left",
                        })

                        window
                            .jQuery(win.document.body)
                            .append(
                                '<div style="position: fixed; bottom: 20px; width: 100%; text-align: center; font-size: 10px; color: #39A900; border-top: 1px solid #39A900; padding-top: 10px; background: white;">' +
                                "<strong>SenaPlanning © " +
                                new Date().getFullYear() +
                                " - Sistema de Planeación SENA</strong>" +
                                "</div>",
                            )
                    },
                },
            ],
            dom:
                "<'row'<'col-sm-12'<'text-center bg-body-light py-2 mb-2'B>>>" +
                "<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
        })

        // Init responsive DataTable
        window.jQuery(".js-dataTable-responsive").DataTable({
            pagingType: "full_numbers",
            pageLength: 10,
            lengthMenu: [
                [5, 10, 15, 20],
                [5, 10, 15, 20],
            ],
            autoWidth: false,
            responsive: true,
        })

        console.log("[v0] DataTables inicializados correctamente")
    }

    /*
     * Init functionality
     *
     */
    static init() {
        this.initDataTables()
    }
}

// Initialize when page loads
window.One.onLoad(pageTablesDatatables.init())
