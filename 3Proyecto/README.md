- 	Hasta el momento solo nos hemos limitado solamente a enviar mensaje del HUB desde JS.
- 	Se puede desde una clase de C# utilizar el HUB para enviarle un mensaje. Asi que el HUB se encargue de restribuir
	esos mensajes a todas las personas que esten conectadas en mi aplicación.

-	La idea es que desde la clase de C# de mi aplicación. Pueda enviar mensajes a traves del HUB a todos mis clientes
	conectados a dicho HUB para eso utilizaremos la interfaz de IHubContext. A traves de esta interfaz podemos enviar
	mensajes desde cualquier parte de mi aplicación.

-	En la clase que se quiera utilizar el IHubContext vamos a utilizar INYECCION DE DEPENCIA para obtener el servicio.
	que me va permitir utilizar cualquier hub desde cualquier clase de C#.