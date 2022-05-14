# WepsysPrueba

Para ejecutar el proyecto:

1-Ejecutar en el Package Manager Console el comando (update-database) para que cree la base de datos y la  tabla del proyecto.
Ojo: (si le da algun error, Revise el archivo appsettings.json ahi se encuentra en connectionstring, si usted no tiene una instancia localhost
      cambiela por el nombre de su instacia de sql server donde dice Data Source={SuInstancia}).

2-Asegurese de que la solucion tenga configurado <Multiple Startup Projects> para que corra el api y la app al mismo tiempo.
  Si no esta configurado
  Click derecho en la solucion, en el menu que se despliega selecionar (set startup Project)
  esto abrira una nueva venta, en la parte derecha de esta seleccionar la segunda opcion (Multiple startup projects)
  ya aqui en la columna action selecionar los dos proyectos en run.
  
Listo ya con estos pasos el proyecto deberia funcionar correctamente.
