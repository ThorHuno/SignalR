$(document).ready(function () {
    //Creo conexión al hub
    var connection = $.hubConnection();
    //Nombre del hub en camelCase
    var hub = connection.createHubProxy('eaaiHub');

    //Creo un evento de escucha
    //Nombre del evento del hub en camelCase
    hub.on('vuelo', function (vuelo) {
        AgregarVueloATabla(vuelo);
    });

    //Inicio la conexión
    connection.start().done(function () {
        console.log('conexión con eaaiHub exitosa');
    }).fail(function () {
        console.error('conexión con eaaiHub fallida');
    });

    var AgregarVueloATabla = function (vuelo) {
        var fila = ArmarFila(vuelo);
        var ultimaFila = $('#tblVuelos > tbody:last-child');
        ultimaFila.append(fila);
    }

    var ArmarFila = function (json) {
        var checkAttribute = json.EsDirecto ? 'checked' : '';
        return '<tr><td>' + json.Codigo + '</td><td>' + json.Origen + '</td><td>' + json.Destino + '</td><td><input type="checkbox"' + checkAttribute + ' disabled class="check-box"/></td><td>' + json.HoraSalida + '</td><td>' + json.HoraLlegada + '</td><td><a href= "/vuelo/Edit/' + json.Id + '"> Edit</a> | <a href="/vuelo/Details/' + json.Id + '">Details</a> | <a href="/vuelo/Delete/' + json.Id + '">Delete</a></td ></tr>';
    }
});