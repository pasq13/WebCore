# WebCore
Se debe generar un API con dos controladores. Uno que gestione un login y otro que trate la información de los héroes.

LOGIN

Este controlador recibirá un usurario y una contraseña. Al recibir como usuario “Pulso” y contraseña “pUlSo”, se generará y devolverá un JWT. Este token se usará para validar el acceso al resto del API.

HEROES

Este controlador debe gestionar a los héroes, independiente de su casa (MARVEL o DC).
El controlador deber requerir el token generado en el LOGIN y debe realizar las propiedades CRUD. 
Además se realizará un servicio alternativo que obtenga todos los héroes devolviendo en la respuesta la casa a la que pertenecen.

El API sabrá la casa a la que pertenecen, porque en la url de petición al servicio se le indicará. http://localhost:5050/{casaHeroe}/Get 
Por ejemplo, para hacer una llamada a un héroe de Marvel:
	 http://localhost:5050/Marvel/Get?id=1 

Realizar la API con la arquitectura por capas más conveniente.

