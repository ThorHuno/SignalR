using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalR.Models;

namespace SignalR
{
    //Nombre del hub en CamelCase
    [HubName("eaaiHub")]
    public class EAAIHub : Hub
    {
        public void VueloGuardado(Vuelo vuelo)
        {
            //Nombrar los métodos en PascalCase
            Clients.All.Vuelo(vuelo);
        }
    }
}