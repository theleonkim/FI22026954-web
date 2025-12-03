# Caso Práctico 2

| Curso                         | Programación Avanzada en Web            |
| :---------------------------- | :-------------------------------------- |
| Código                        | SC-701                                  |
| Profesor                      | Luis Andrés Rojas Matey                 |
| Estudiante                    | Kimberly León Ramírez                   |
| Repositorio                   | https://github.com/theleonkim/FI22026954-web |
| Fecha y hora de entrega final | Martes 2 de diciembre antes de las 9 pm |
| Valor                         | 15 %                                    |

<br />

- [Indicaciones generales](#indicaciones-generales)
- [Indicaciones específicas](#indicaciones-específicas)
  - [Entity Framework](#entity-framework)
  - [Dependency Injection](#dependency-injection)
  - [Unit Tests](#unit-tests)
- [Rúbrica de evaluación](#rúbrica-de-evaluación)
- [Entregables](#entregables)

<br />

## Indicaciones generales

Este _Solution_ (`CP2.sln`) contiene un solo _Project_ de tipo web con arquitectura **MVC** (_Model-View-Controller_) llamado `Transportation`, con el objetivo de evaluar tres secciones, cada una con un valor de 5 puntos, para un total de 15 puntos:

- **Entity Framework**
- **Dependency Injection**
- **Unit Tests**

El archivo de configuración del _Project_ (`Transportation.csproj`) contiene todo lo necesario para la compilación y ejecución adecuada del programa; es decir, no es necesario agregar ninguna configuración adicional ni paquetes nuevos (**NuGet**) para su funcionamiento.

<br />

## Indicaciones específicas

Para efectuar las diferentes secciones, se debe utilizar el _Framework_ `.NET 8.0`, así como tener instalada globalmente la versión más reciente de `Entity Framework 9.0` (específicamente `9.0.11`).

El único _Controller_ (`HomeController`) contiene un solo _Action_ (`Index`), que es la página que se cargará al ejecutarse el sitio en la ruta raíz.

También se debe tomar en cuenta lo siguiente:

- No se debe agregar manualmente ningún archivo nuevo en ninguna de las capas (_Controllers_, _Interfaces_, _Models_, _Services_, _Tests_, _Views_).
- Ningún _View_ debe ser modificado bajo ninguna circunstancia.
- No se deben modificar los _Namespaces_.
- No se deben agregar más _Actions_ al _Controller_.

<br />

### Entity Framework

El objetivo de esta sección es utilizar la estrategia _Database First_ para mostrar en `Index` los datos correctos provenientes de la base `Cars.db`. Esta base proviene de un [repositorio público](https://github.com/dtaivpp/car_company_database/blob/master/Car_Database.db). Su diagrama relacional puede consultarse [aquí](https://raw.githubusercontent.com/dtaivpp/car_company_database/refs/heads/master/Car_Database_ER_Diagram.png).

Acciones necesarias:

#### 1. Scaffolding (Database First)

Utilizando el **CLI (`dotnet`)**, se ejecuta el *scaffold* de la base `Cars.db` con el proveedor `Microsoft.EntityFrameworkCore.Sqlite`, generando:

- El `DbContext`
- Los `DbSet`
- Todos los *Entities* correspondientes

Salida generada dentro de la carpeta `Models`.

#### 2. Consulta del “carro de Minnie Mouse”

En el `HomeController → Index()` se deben obtener:

- El cliente con apellido **Mouse**
- La relación en `CustomerOwnership`
- El **VIN**
- El **Modelo**
- La **Marca**
- El **Dealer** correspondiente


Esto debe realizarse usando **la misma instancia de `CarsContext`** y **LINQ**.

Una vez ejecutado correctamente el scaffolding, la página mostrará también el Brand y el Model sin necesidad de editar la vista, únicamente descomentando las líneas indicadas por el profesor.

<br />

### Dependency Injection

El objetivo es mostrar **ambas implementaciones** de la interfaz `IAirplanes`:

- `Airbus`
- `Boeing`

Para lograrlo:

- Se deben registrar ambas implementaciones en `Program.cs` usando **Dependency Injection**.
- Se deben ajustar los argumentos del _Action_ `Index` para recibir **todas** las implementaciones mediante `IEnumerable<IAirplanes>`.

La página debe mostrarlas correctamente sin modificar la vista.

<br />

### Unit Tests

En `Models/Ships.cs` se encuentra una clase que retorna fechas históricas relacionadas con los barcos **Olympic**, **Titanic** y **Britannic**.

En `Tests/ShipsUnitTests.cs` se deben realizar:

- **Modificar `EndOfTitanic()`** para que retorne el `15 de abril de 1912`.  
  ```csharp
  // ChatGPT
  return new DateTime(1912, 4, 15);


