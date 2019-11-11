** APOYO

npm install @aspnet/signalr

<script src="~/lib/signalR/signalr.js"></script>
<script src="~/js/chat.js"></script>


   // Configurar nueva ruta ""/chatHub" de js
            app.UseSignalR(x =>
            {
                // Registro de los Hubs
                x.MapHub<ChatHub>("/chatHub");
            });

1. Crear una carpeta Hubs al nivel de las carpetas Controllers, Models y Views/Home/Index
	
	Hubs/ ChatHub.cs
	
	public class ChatHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // await Clients.All.SendAsync("ReceiveMessage", user, message);
            // await Clients.Others.SendAsync("ReceiveMessage", user, message);
            // await Clients.Caller.SendAsync("ReceiveMessage", user, message);
            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
            // Tiempo
            await Task.Delay(1000);

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


<script src="~/lib/signalR/signalr.js"></script>
<script src="~/js/chat.js"></script>


	public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
	
	
	D:\RepositorioGitHub\Signal-R\1Proyecto\HelloWork\wwwroot\lib\signalR
	
	
	D:\RepositorioGitHub\Signal-R\1Proyecto\HelloWork\node_modules\@aspnet\signalr\dist\browser