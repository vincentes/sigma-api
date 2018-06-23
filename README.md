# sigma-api

La API está en el puerto 5000 de http://204.48.19.107. 


Actualizar la API:
1) Primero ejecutá `dotnet publish -c Release --output ./published` localmente para compilar la nueva versión.
2) Después, con SCP (WinSCP es recomendado) hacer SSH al servidor con los datos del usuario root. Puerto 22.
3) Arrastrar la carpeta "published" generada a la carpeta /root/ (o en otras palabras, a ~/) del servidor.
4) Pronto! Andá a http://204.48.19.107:5000 y tendrás acceso a la API.
