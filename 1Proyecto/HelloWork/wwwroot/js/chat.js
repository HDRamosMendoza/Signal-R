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