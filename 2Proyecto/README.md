APOYO

Protocolos de Transporte en Signal R

El servidor web tendra la capacidad de enviar mensajes a los clientes que se conecten a dicha aplicación web.

Signal R tiene la capacitadad de detectar si tu servidor o tu cliente no soporta WEB SOCKETS puede utilizar otros tipos de protocolos como:

SignalR soporta las siguientes técnicas para manejar conexiones en tiempo real.

 WebSockets
 Server - SentEvents (por defecto)
 Long Polling

SignalR. Se encargara de abstraer la logica que determina si tu cliente o tu servidor puede soportar algún tipo de conexion de WEB SOCKETS, sino lo 
soporta utiliza SERVER -SENT EVENTS o caso contrario utiliza LONG POLLING.

* Web Sockets
	- Full dúplex(mensajes bidireccional). Puede enviar mensajes desde el servidor hasta el cliente y biseversa de forma simultanea.
	- Soporta texto y binario.
	
	Proceso.
	Te permite una comunicación vía direccional. Se puede enviar mensajes desde el cliente y viceversa de manera simultanea.
	Soporta texto y binario.
 
* Server - Sent Events
	- El servidor envía mensajes al cliente.
	- Comunicación de una vía.
	- Solo texto.
	
	Proceso.
	Tu vas a crear un objeto llamado EventServer cual va a permitir que a travez de el vallan llegando mensajes desde el servidor.
	El servidor solo puede enviar mensaje al cliente. La desventaja que solo se envia texto y no se puede enviar data binaria.
 
* Long Polling.
	- Se hace una petición cada X tiempo.
	- Texto y binario.
	
	Proceso.
	Se envia petición al servidor a través de AJAX pero el servidor va a dejar abierta está conexión HTTP
	hasta que exista una actualización en la página (actualización en el servidor). Cuando el servidor detecte
	una actualización entonces el servidor va a devolver un mensaje al cliente. Si por alguna razón la conexión expira
	el cliente se encarga de volver a llamar una petición HTTP al servidor. Por ello decimo que el servidor realizar 
	una petición cada X tiempo. Ya sea cuando exista respuesta del servidor o cuando la conexión HTTP expire.
	Respuesta en texto como en binario.
 
* Polling 
	- No es soportado por Signal R.
	- Requiere muchas llamadas HTTP.
	- No es una tecnica que escale muy bien.
	 
	Proceso.
	Se manda una peticion por JS al servidor. Si a teniado algún tipo de actualización.
	Se realiza peticiones por AJAX de forma repetitiva al servidor.
	
EN CONSOLA DEL NAVEGADOR:

> negotiate > Preview > availableTransports

** Receptores de Mensaje.

- All: Todos van a recibir el mensaje.
- Caller: El emisor es el que recibirá el mensaje.
- Others: Todos excepto el emisor recibirán el mensaje.
- Group: Todos los mienbros de un grupo dado recibirán el mensaje.
- Client: Un conjunto de personas específicas recibirán el mensaje.
- User: Un conjunto de usuarios registrados en tu página recibirá el mensaje.



	