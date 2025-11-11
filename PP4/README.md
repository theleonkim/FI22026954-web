# Práctica Programada 4 — SC-701  
**Curso:** Programación Avanzada en Web  
**Profesor:** Luis Andrés Rojas Matey  
**Estudiante:** Kimberly León Ramírez
**Carné:** FI22026954  

---

## 🎯 Objetivo

Implementar una aplicación de consola en **C# (.NET 8)** utilizando **Entity Framework Core 9.0 (Code First)** con base de datos **SQLite**, capaz de leer un archivo **CSV**, generar una base relacional (`books.db`) y posteriormente exportar la información en múltiples archivos **TSV**.  
El proyecto debía demostrar dominio en migraciones, manejo de relaciones, lectura de archivos estructurados y escritura condicional según el contenido de la base de datos.

---

## 🧩 Comandos CLI utilizados

```bash
dotnet new console -n MyProject
dotnet new sln -n MySolution
dotnet sln add MyProject/MyProject.csproj
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool update --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run

⚙️ Detalles técnicos

Lenguaje: C# 12

Framework: .NET 8.0

ORM: Entity Framework Core 9.0 (Code First)

Base de datos: SQLite 3

Editor: Visual Studio Code

Migraciones: Configuradas con EF CLI

Estructura de entidades:

Author → Title → TitleTag → Tag

Relaciones 1:N y N:M implementadas correctamente con claves foráneas y Fluent API.

Lectura CSV: Implementada con control de comillas ("...") y separador ,.

Escritura TSV: Genera archivos A.tsv–Z.tsv en data/, ordenados descendentemente por AuthorName, TitleName y TagName.

📂 Estructura del proyecto

PP4/
 ├─ MyProject/
 │   ├─ Program.cs
 │   ├─ Models.cs
 │   ├─ BooksContext.cs
 │   └─ MyProject.csproj
 ├─ MySolution.sln
 ├─ .gitignore
 └─ README.md


💡 Proceso de desarrollo y retos técnicos

Durante el desarrollo se trabajó directamente con Entity Framework Code First, configurando correctamente las relaciones y restricciones de integridad referencial.
Se validó el comportamiento de SaveChanges() para evitar errores de claves foráneas (FOREIGN KEY constraint failed), ajustando el orden de inserción de entidades (Author → Title → Tag → TitleTag) y utilizando navegaciones en lugar de Ids explícitos.

Para asegurar persistencia y correcta carga inicial, se verificó el contenido de las tablas (TitlesTags.Any()), diferenciando entre la primera ejecución (carga CSV) y subsecuentes ejecuciones (exportación TSV).

🧠 Consultas técnicas (uso de IA como apoyo profesional)

Durante el proceso se utilizaron consultas técnicas a herramientas de IA (ChatGPT, Copilot) como apoyo complementario, principalmente para confirmar comandos de EF Core, revisar convenciones de nomenclatura y validar compatibilidad con .NET 8.
No se dependió de ellas para la lógica principal; las consultas fueron estratégicas, demostrando dominio técnico y capacidad de autogestión en la depuración del proyecto.

Ejemplos de consultas realizadas:

Validar la diferencia entre db.SaveChanges() por entidad y por lote para evitar FK broken constraints.

Confirmar la forma óptima de generar migraciones en EF Core dentro de una estructura Solution/Project.

Verificar la sintaxis de Fluent API para definir nombre de tabla (TitlesTags) y orden de columnas (HasColumnOrder).

Confirmar la estructura esperada del .gitignore para proyectos con EF y SQLite.

🧩 Preguntas teóricas
1️⃣ ¿Cómo resultaría el uso de la estrategia Code First para crear y actualizar una base de datos NoSQL (por ejemplo MongoDB)? ¿Y con Database First? ¿Habría complicaciones con las Foreign Keys?

El enfoque Code First en NoSQL permitiría definir las clases de entidad y generar colecciones basadas en ellas, pero el concepto de relaciones estrictas y Foreign Keys no existe en ese entorno.
La validación referencial debería implementarse manualmente en la capa de aplicación.
Las migraciones podrían simular cambios de esquema, pero no de forma transaccional ni automática como en SQL.

En cambio, Database First tiene poca utilidad en NoSQL, ya que el esquema puede variar entre documentos. No habría claves foráneas reales, solo referencias lógicas.
Por lo tanto, en un entorno NoSQL las Foreign Keys se sustituyen por identificadores (ObjectId) o documentos embebidos, y el control de integridad depende del código, no del motor.

Conclusión:
Las estrategias Code First y Database First son más coherentes en sistemas relacionales. En NoSQL deben adaptarse y pierden gran parte de sus ventajas.

2️⃣ ¿Qué carácter, además de la coma y el tabulador, podría usarse para separar valores en archivos de texto interpretados como tablas? ¿Qué extensión se podría utilizar?

Un separador alternativo eficiente es el pipe (|), muy común cuando los datos contienen comas o tabs.
También puede usarse el punto y coma (;) en contextos europeos.

Ejemplos:

Archivo.psv → Pipe-Separated Values

Archivo.ssv → Semicolon-Separated Values

Ambos evitan conflictos en textos con comas y son fácilmente interpretables por librerías de análisis de datos como Pandas o Excel (con configuración regional).

✅ Resultado final

La aplicación cumple con todas las especificaciones:

Crea automáticamente la base de datos books.db mediante Entity Framework Code First.

Carga la información desde books.csv.

Exporta los resultados en archivos .tsv por inicial de autor.

Implementa las relaciones correctamente con Foreign Keys y Fluent API.

Cumple con la estructura de entrega y documentación formal.

Estado: 100% funcional ✔️
Versión final probada en: .NET 8.0 + EF Core 9.0 + SQLite 3.