# 📘 Práctica Programada 3 – SC-701 Programación Avanzada en Web

**Estudiante:** Kimberly León  
**Carné:** FI22026954  
**Profesor:** Luis Andrés Rojas Matey  
**Entrega:** Martes 21 de octubre de 2025 – antes de las 6:00 p.m.  
**Valor:** 4%

---

## 🧭 Introducción

Esta práctica consiste en implementar una **Web API mínima (Minimal API)** utilizando **ASP.NET Core 8.0**, con el objetivo de comprender cómo funcionan los endpoints HTTP y cómo se estructuran los parámetros, respuestas y validaciones en una arquitectura moderna basada en servicios RESTful.

El proyecto se desarrolló completamente en **Visual Studio Code**, aprovechando la terminal integrada y los comandos CLI de .NET.  
El código fue versionado con **Git** y alojado en **GitHub** en el repositorio:  
👉 [https://github.com/theleonkim/FI22026954-web](https://github.com/theleonkim/FI22026954-web)

---

## 🎯 Objetivo

Aplicar los conocimientos adquiridos en la creación de servicios web con **ASP.NET Core Minimal API**, comprendiendo el uso de los verbos HTTP, las validaciones, los formatos de respuesta (JSON y XML) y el manejo de errores utilizando los códigos de estado adecuados.

---

## ⚙️ Especificaciones técnicas

- Lenguaje: **C#**
- Framework: **.NET 8.0**
- Tipo de proyecto: **ASP.NET Core Web (Minimal API)**
- Editor: **Visual Studio Code**
- Librerías adicionales:
  - `Swashbuckle.AspNetCore` para documentación **Swagger UI**
- Control de versiones: **Git + GitHub**

### 📂 Estructura del proyecto

FI22026954-web/
└── PP3/
├── TextApi/ # Proyecto Minimal API
│ ├── Program.cs
│ └── TextApi.csproj
├── PP3.sln # Solución .NET
├── README.md # Documentación
└── .gitignore


---

## 🚀 Comandos utilizados (CLI .NET)

```bash
# Crear solución y proyecto
dotnet new sln -n PP3
dotnet new web -n TextApi
dotnet sln PP3.sln add TextApi/TextApi.csproj

# Instalar Swagger
dotnet add TextApi package Swashbuckle.AspNetCore

# Restaurar y compilar
dotnet restore
dotnet build

# Ejecutar el proyecto
dotnet run --project TextApi/TextApi.csproj

🧩 Endpoints implementados
1️⃣ GET /

Redirige al Swagger UI, donde se puede visualizar y probar el resto de endpoints.

📍 Ejemplo:
GET http://localhost:5183/
📤 Respuesta: redirección a http://localhost:5183/swagger/index.html

2️⃣ POST /include/{position}?value=...

Inserta una palabra en una posición específica dentro de una oración.

Parámetro	Tipo	Desde	Validación
position	int	Route	≥ 0
value	string	Query	Longitud > 0
text	string	Form	Longitud > 0
xml	bool	Header	Opcional (por defecto false)

Ejemplo JSON

{
  "ori": "This is just a test that shows how the 'include' endpoint works...",
  "new": "Hello This is just a test that shows how the 'include' endpoint works..."
}


Ejemplo XML

<Result>
  <Ori>This is just a test that shows how the 'include' endpoint works...</Ori>
  <New>Hello This is just a test that shows how the 'include' endpoint works...</New>
</Result>


Ejemplo de error

{ "error": "'position' must be 0 or higher" }

3️⃣ PUT /replace/{length}?value=...

Reemplaza todas las palabras que tengan una longitud específica dentro de una oración.

Parámetro	Tipo	Desde	Validación
length	int	Route	> 0
value	string	Query	Longitud > 0
text	string	Form	Longitud > 0
xml	bool	Header	Opcional

Ejemplo JSON

{
  "ori": "This is just a test that shows how the 'replace' endpoint works...",
  "new": "hello is hello a hello hello shows how the 'replace' endpoint works..."
}

4️⃣ DELETE /erase/{length}

Elimina las palabras que tengan una longitud específica dentro de una oración.

Parámetro	Tipo	Desde	Validación
length	int	Route	> 0
text	string	Form	Longitud > 0
xml	bool	Header	Opcional

Ejemplo JSON

{
  "ori": "This is just a test that shows how the 'erase' endpoint works...",
  "new": "is a shows how the 'erase' endpoint works of sentence"
}

💻 Ejecución y pruebas en Visual Studio Code

Abrir el proyecto FI22026954-web en Visual Studio Code.

Abrir la terminal integrada (Ctrl + ñ).

Ejecutar los comandos:

cd PP3/TextApi
dotnet restore
dotnet run


Abrir en el navegador la URL mostrada por la consola, por ejemplo:
👉 http://localhost:5183/swagger

Probar cada endpoint directamente desde Swagger o con la extensión Thunder Client de VS Code.

🧠 Preguntas teóricas
1️⃣ ¿Es posible enviar valores en el Body (por ejemplo, en el Form) del Request de tipo GET?

No. Según la especificación HTTP, el método GET no debe tener cuerpo (body).
Aunque algunos clientes podrían permitirlo, la mayoría de los servidores lo ignoran.
Por eso, en ASP.NET Core, los parámetros de tipo GET deben enviarse en la URL o en los encabezados, nunca en el cuerpo.

2️⃣ ¿Qué ventajas y desventajas observa con el Minimal API si se compara con la opción de utilizar Controllers?
Ventajas del Minimal API	Desventajas del Minimal API
Código más simple y ligero.	Menos separación de responsabilidades.
Ideal para microservicios y prototipos rápidos.	Dificultad para escalar proyectos grandes.
Configuración mínima y mejor rendimiento.	No soporta todos los filtros ni atributos de Controllers.
Rutas y lógica centralizadas en un solo archivo.	Menor legibilidad si el proyecto crece demasiado.
🤖 Prompts y respuestas de IA (ChatGPT)

Estas fueron las consultas realizadas con inteligencia artificial para apoyo durante el desarrollo.

Prompt 1:

“Cómo hacer que un endpoint devuelva XML o JSON dependiendo de un header.”
Respuesta (resumen):
ChatGPT propuso serializar la respuesta con XmlSerializer y detectar el header xml en HttpContext.
Esta solución se aplicó a los endpoints /include, /replace y /erase.

Prompt 2:

“Cómo escribir un archivo .gitignore correcto para proyectos de .NET en OneDrive.”
Respuesta (resumen):
Se recomendó excluir bin/, obj/, .vs/ y archivos temporales para evitar errores de compilación o conflictos con la sincronización de OneDrive.

Prompt 3:

“Cómo crear una solución y un proyecto ASP.NET Core Minimal API desde Visual Studio Code.”
Respuesta (resumen):
ChatGPT explicó los comandos dotnet new sln, dotnet new web, dotnet sln add, cómo agregar Swagger y cómo ejecutar con dotnet run.

🌐 Fuentes consultadas

Documentación oficial de Microsoft:
https://learn.microsoft.com/es-es/aspnet/core/fundamentals/minimal-apis

Swashbuckle (Swagger para ASP.NET Core):
https://github.com/domaindrivendev/Swashbuckle.AspNetCore

Blog oficial .NET 8 – Novedades y mejoras.

ChatGPT (OpenAI) – Asistencia técnica, corrección y redacción del README.

✅ Conclusión

Esta práctica permitió aplicar de forma práctica los conceptos de servicios web RESTful, manejo de endpoints, uso de verbos HTTP y serialización de datos en JSON y XML.
El uso de Visual Studio Code y la terminal CLI fortaleció la comprensión del ciclo de vida de un proyecto en .NET y del control de versiones con Git.
El resultado final cumple con todas las especificaciones funcionales y técnicas solicitadas por el profesor.

📦 Repositorio del proyecto:
👉 https://github.com/theleonkim/FI22026954-web
