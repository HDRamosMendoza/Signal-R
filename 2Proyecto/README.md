APOYO

Protocolos de Transporte en Signal R

Signal R tiene la capacitadad de detectar si tu servidor soporta las siguientes técnicas para
manejar conexiones en tiempo real.

 WebSockets
 Server - SentEvents (por defecto)
 Long Polling

* Web Sockets
	- Full dúplex
	- Soporta texto y binario.
	
	Proceso.
	Te permite una comunicación vía direccional. Se puede enviar mensajes desde el cliente y viceversa de manera simultanea.
	Soporta texto y binario.
 
* Server - Sent Events
	- El servidor envía mensajes al cliente.
	- Comunicación de una vía.
	- Solo texto.
	
	Proceso.
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
	- No es soportado por Signal R
	- Requiere muchas llamadas HTTP
	 
	Proceso.
	Se manda una peticion por JS al servidor. Si a teniado algún tipo de actualización.
	Se realiza peticiones por AJAX de forma repetitiva al servidor.
	
EN CONSOLA:

> negotiate > Preview > availableTransports

** Receptores de Mensaje.

- All: Todos van a recibir el mensaje.
- Caller: El emisor es el que recibirá el mensaje.
- Others: Todos excepto el emisor recibirán el mensaje.
- Group: Todos los mienbros de un grupo dado recibirán el mensaje.
- Client: Un conjunto de personas específicas recibirán el mensaje.
- User: Un conjunto de usuarios registrados en tu página recibirá el mensaje.



	