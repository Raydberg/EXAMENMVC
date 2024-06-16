$(document).ready(function () {
    var initialMarcaId = '@Model.Modelo.MarcaIDMARCA';

    if (initialMarcaId) {
        $('#marcaDropdown').val(initialMarcaId);
        cargarModelos(initialMarcaId, '@Model.ModeloIDMODELO');
    }

    $('#marcaDropdown').change(function () {
        var marcaId = $(this).val();
        cargarModelos(marcaId, null);
    });

    function cargarModelos(marcaId, selectedModeloId) {
        $("#modeloDropdown").empty().append('<option value="">Seleccione Modelo</option>').prop("disabled", true);

        if (marcaId) {
            $.ajax({
                url: '/Vehiculos/ObtenerModelos',
                type: 'GET',
                data: { idMarca: marcaId },
                success: function (data) {
                    $.each(data, function (i, modelo) {
                        var option = $('<option></option>').attr("value", modelo.idModelo).text(modelo.noM_MODELO);
                        if (modelo.idModelo == selectedModeloId) {
                            option.attr("selected", "selected");
                        }
                        $('#modeloDropdown').append(option);
                    });
                    $("#modeloDropdown").prop("disabled", false);
                }
            });
        }
    }
});