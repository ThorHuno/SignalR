using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR.Client; //Hace posible invocar y escuchar métodos del hub
using SignalR.Models;

namespace SignalR.Controllers
{
    public class VueloController : Controller
    {
        public VueloController()
        {
            //Creo la conexión al hub
            hubConnection = new HubConnection("http://localhost:64924/");
            eaaiProxy = hubConnection.CreateHubProxy("eaaiHub");
        }

        private readonly HubConnection hubConnection;
        private readonly IHubProxy eaaiProxy;
        private EAAIModel db = new EAAIModel();

        // GET: Vueloes
        public ActionResult Index()
        {
            return View(db.Vuelos.ToList());
        }

        // GET: Vueloes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vuelo vuelo = db.Vuelos.Find(id);
            if (vuelo == null)
            {
                return HttpNotFound();
            }
            return View(vuelo);
        }

        // GET: Vueloes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vueloes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Codigo,Origen,Destino,EsDirecto,HoraSalida,HoraLlegada")] Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                db.Vuelos.Add(vuelo);
                db.SaveChanges();
                //Si llega a este punto notifico a todos los clientes conectados al hub
                //Esto es asíncrono
                await AvisarGuardado(vuelo);
                return RedirectToAction("Index");
            }

            return View(vuelo);
        }

        // GET: Vueloes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vuelo vuelo = db.Vuelos.Find(id);
            if (vuelo == null)
            {
                return HttpNotFound();
            }
            return View(vuelo);
        }

        // POST: Vueloes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,Origen,Destino,EsDirecto,HoraSalida,HoraLlegada")] Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vuelo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vuelo);
        }

        // GET: Vueloes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vuelo vuelo = db.Vuelos.Find(id);
            if (vuelo == null)
            {
                return HttpNotFound();
            }
            return View(vuelo);
        }

        // POST: Vueloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vuelo vuelo = db.Vuelos.Find(id);
            db.Vuelos.Remove(vuelo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task AvisarGuardado(Vuelo vuelo)
        {
            //Verifico el estado de la conexión
            if (hubConnection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
            {
                //Invoco el método Hello del hub
                await eaaiProxy.Invoke("VueloGuardado", vuelo);
            }
            else
            {
                //Inicio la conexión (Esto es asíncrono)
                await hubConnection.Start();
                //Invoco el método Hello del hub
                await eaaiProxy.Invoke("VueloGuardado", vuelo);
            }
        }
    }
}
