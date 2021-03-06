﻿// El signalR se debe de instalar y usaremos NPM para instalar el paquete(NODE JS)
// La función .withUrl tiene un segundo parametro que podemos indicar si queremo indicar algún otro protocolo. 
// Por ejemplo: 
//    .withUrl("/chatHub", signalR.HttpTransportType.ServerSentEvents).build() ---> eventsource(objeto)
//    .withUrl("/chatHub", signalR.HttpTransportType.LongPolling).build() ---> pending(objeto)
//    .withUrl("/chatHub").build() => Web Sockets

const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// Obtengo un queryString de mi URL
// ?group=Chat_Sala2 => Para entrar al CHAT2
var urlParams = new URLSearchParams(window.location.search);
const group = urlParams.get('group') || "Chat_Home";
console.log(group);

document.getElementById('titulo-sala').innerText = group;

connection.on("ReceiveMessage", (user, message) => {
    const msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    const fecha = new Date().toLocaleTimeString();
    const mensajeAMostrar = fecha + " <strong>" + user + "</strong>:" + msg;
    const li = document.createElement("li");
    li.innerHTML = mensajeAMostrar;
    document.getElementById("messagesList").appendChild(li);
});

function pruebaStart(thisGroup) {
    connection.start().then(() => {
        // Este bloque de código se ejecuta cuando se establece la conexión con el servidor
        connection.invoke("AddToGroup", thisGroup).catch(err => console.error(err.toString()));
    }).catch(err => {
        console.error(err.toString());
        event.target.disabled = false;
        });
}

pruebaStart(group);


/* 
// Esto es mas que todo para realizar la función de conectar a travez de un BUTTON.
document.getElementById("connect").addEventListener("click", event => {
    // connection.start().catch(err => console.error(err.toString()));
    // Es una promesa .start(). Cuando la conexion sea exitosa agrego la función "AddToGroup".
    connection.start().then(() => {
        // Este bloque de código se ejecuta cuando se establece la conexión con el servidor
        connection.invoke("AddToGroup", group).catch(err => console.error(err.toString()));
    }).catch(err => {
        console.error(err.toString());
        event.target.disabled = false;
    });
});
*/
document.getElementById("sendButton").addEventListener("click", event => {
    
    // 1 = conectado
    // Podemos verificar si tu usuario esta conectado o no a tu HUB.
    if (connection.connection.connectionState !== 1) {
        alert("Usted no está conectado con el servicio");
        return;
    }

    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;
    // Llamando desde C#
    connection.invoke("SendMessage", user, message, group).catch(err => console.error(err.toString()))
    event.preventDefault();
});

/* Conectar a SALA */
document.getElementById("sala").addEventListener("click", event => {
    event.preventDefault();
    let sala = "Chat_Sala2";
    connection.invoke("AddToGroup",sala).catch(err => console.error(err.toString()))
    document.getElementById('titulo-sala').innerText = sala;
    pruebaStart(sala);
});