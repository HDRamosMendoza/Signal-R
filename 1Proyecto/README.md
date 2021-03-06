** APOYO
npm install @aspnet/signalr
instalar donet 2.1

** Crear proyecto (Visual Studio 2017).
    A. File > New > Project
    B. New Project > Installed > Visual C# > .NET Core > ASP.NET Core Web Application
    C. Agregamos el nombre.
    D. .NET CORE | ASP.NET Core 2.1 | Web Application (Model View Controller) | Configure for HTTPS.

Hub. Es la clase que se encarga de coordinar la comunicación bidireccional entre el CLIENTE y el SERVIDOR.
     Desde esta clase vamos a llamar funciones desde JS. Desde esta clase podré mandar un mensajes a los
     clientes y en este mensaje voy a indicar la función de JS que quiero que se ejecute.

1. Crear una carpeta Hubs al nivel de las carpetas Controllers, Models y Views/Home/Index
	
	Hubs/ ChatHub.cs 
	
	using Microsoft.AspNetCore.SignalR;
	public class ChatHub: Hub
    {
        // La función se encargara de distribuir a todos los clientes que estan conectados a nuestra aplicación de chat.
        public async Task SendMessage(string user, string message)
        {
            // ReceiveMessage. Es el nombre de la función de JS que quiero que se ejecute.
            // await Clients.All.SendAsync("ReceiveMessage", user, message);
            // await Clients.Others.SendAsync("ReceiveMessage", user, message);
            // await Clients.Caller.SendAsync("ReceiveMessage", user, message);
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
            // Tiempo
            // await Task.Delay(1000);
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }
    }

2. js/Chat.js
			 
// El signalR se debe de instalar y usaremos NPM para instalar el paquete(NODE JS)
// La función .withUrl tiene un segundo parametro que podemos indicar si queremo indicar algún otro protocolo. 
// Por ejemplo: 
//    .withUrl("/chatHub", signalR.HttpTransportType.ServerSentEvents).build()
//    .withUrl("/chatHub", signalR.HttpTransportType.LongPolling).build()
//    .withUrl("/chatHub").build() => Web Sockets

const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", (user, message) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    const fecha = new Date().toLocaleTimeString();
    const mensajeAMostrar = fecha + " <strong>" + user + "</strong>:" + msg;
    const li = document.createElement("li");
    li.innerHTML = mensajeAMostrar;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(err => console.error(err.toString()));

document.getElementById("sendButton").addEventListener("click", event => {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    // Llamando desde C#
    connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()))
    event.preventDefault();
});

3. Se agrega Views/Home/Index.cshtml
<div class="row">
    <div class="col-6">&nbsp;</div>
    <div class="col-6">
        Usuario <input type="text" id="userInput" />
        <br />
        Mensaje <input type="text" id="messageInput" />
        <input type="button" id="sendButton" value="Enviar Mensaje" />
    </div>
</div>

<div class="row">
    <div class="col-12">
        <hr />
    </div>
</div>

<div class="row">
    <div class="col-6">&nbsp;</div>
    <div class="col-6">
        <ul id="messagesList"></ul>
    </div>
</div>

<!-- Signal R debe de estar creado antes que chat.js -->
<script src="~/lib/signalR/signalr.js"></script>
<script src="~/js/chat.js"></script>

4. En Startup.cs

	4.1 De bajo de services.Configure<CookiePolicyOptions>
	
	// Agregar SignalR
    services.AddSignalR();
	
	4.2 De bajo de app.UseCookiePolicy();
	
	// Configurar nueva ruta ""/chatHub" de js
	app.UseSignalR(x =>
	{
		// Registro de los Hubs
		x.MapHub<ChatHub>("/chatHub");
	});

5. Instalar las SIGNAL R
    - Vamos a la ruta. D:\RepositorioGitHub\Signal-R\1Proyecto\HelloWork
    - CMD => 
        npm init -y
        npm install @aspnet/signalr

6. Nos vamos a NODE_MODULES. Buscamos "signalr.js" y lo copiamos.
    D:\RepositorioGitHub\Signal-R\1Proyecto\HelloWork\node_modules\@aspnet\signalr\dist\browser

7. Pegamos en la carpeta "signalr". Que previamente debe de ser creada.
    D:\RepositorioGitHub\Signal-R\1Proyecto\HelloWork\wwwroot\lib\signalR