# Prueba de Xamarin para OIS

## Descripcion de el requerimiento

- Crear una primera vista que contenga un login(Entry para usuario y password, con botón de
iniciar sesión).
- Crear una vista que permita registrar un usuario para poder utilizarlo como inicio de sesión.
Una vez que inicie sesión, debe descargar la información de productos desde la siguiente API
publica(https://fakestoreapi.com/products) al dispositivo, guardar esta información en una base
local utilizando sqlite.
- Cuando se está descargando la información, debe de evaluar si tiene conexión a internet, sino
tiene debe de revisar si tiene información en su base de datos, si fuese la primera vez, debe de
mostrar un mensaje de que necesita conexión a internet, sino debe de dejar iniciar sesión
cargando la información desde la base de datos (Debe de poder cargar la información sin
conexión a internet).
- Crear una vista que se muestre al finalizar la descarga de la información de productos, esta vista
debe de contener una lista que muestre todos los productos descargados, el Source de la lista
debe de venir de una consulta a la base local.
Las celdas de la lista de productos debe de contener “Nombre de Producto”, “Imagen de
Producto”, “Precio” y “Categoría”.
- Cada celda de producto debe de contener una estrella que se mostrará sólo cuando un producto
tenga un rating que sea igual o mayor a 4.0.
- Crear una vista que se muestre al seleccionar un ítem de la lista de productos, esta vista debe de
contener “Nombre del Producto”, “Imagen de Producto”, “Precio”, “Categoría”, “Rating” y
“Descripción de Producto”.

## Limitantes

- Por cuestiones de tiempo las vistas quedaron algo simples y no alargar mas el requerimiento

- Queria usar grid pero por lo anterior antes mencionado lo mas rapido para lo nivel visual fue el StackLayout


- Al no poseer un dispositivo IOS no puedo realizar emulacion para esa terminal, por ende no se configuro la dblocal en IOS