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

			
			// el signalR se debe de instalar y usaremos NPM para instalar el paquete(NODE JS)
const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", (user, message) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt")
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



        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
	
	
	D:\RepositorioGitHub\Signal-R\1Proyecto\HelloWork\wwwroot\lib\signalR
	
	
	D:\RepositorioGitHub\Signal-R\1Proyecto\HelloWork\node_modules\@aspnet\signalr\dist\browser