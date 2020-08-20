using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR_SqlDependency.Hubs;
using SignalR_SqlDependency.Models;

namespace SignalR_SqlDependency.Controllers
{
    public class HomeController : Controller
    {
        /* En nuestra aplicacion podemos tener mas de un HUB */
        private readonly IHubContext<ChatHub> chatHub;

        /* Como parametro debemos de mandar el HUB que nosotros vamos a utilizar */
        /* Se inicializa el chatHub para tenerlo disponible en todo el controlador */
        public HomeController(IHubContext<ChatHub> chatHub)
        {
            this.chatHub = chatHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            /* Para enviar notificaciones a un grupos de usuarios/clientes 
               determinados. Otra opcion sería usar SQL DEPENDENCY */
            /* Vamos a emitir una alerta que le va a salir a todas la 
               personas que este conecta al chat */
            chatHub.Clients.All.SendAsync("ReceiveMessage", "Admin", "Alguien ha entrado a las página");
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
