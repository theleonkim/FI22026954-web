# PP2 – Binary Calculator (ASP.NET Core MVC, .NET 8)

**Nombre:** Kimberly León  
**Carné:** FI22026954
**Repo principal:** https://github.com/theleonkim/FI22026954-web

---

## Descripción

Aplicación web hecha con **ASP.NET Core MVC (.NET 8)** que:

- Valida dos **cadenas binarias** `a` y `b` con las reglas:
  - Solo caracteres `0` y `1`.
  - Longitud **exactamente** 2, 4, 6 u 8 (no vacío).
- Si la validación pasa, calcula:
  - **Operaciones binarias:** `a AND b`, `a OR b`, `a XOR b` (**implementadas iterando carácter por carácter sobre strings**).
  - **Operaciones aritméticas:** `a + b` y `a • b`.
  - **Cambio de bases:** Binario / Octal / Decimal / Hexadecimal.
- Muestra en la página por defecto (**Home/Index**) un formulario y una **tabla única** con todos los resultados.
- `a` y `b` se presentan **padded a 8 bits** en la columna Bin (ej.: `00001010`).

---

## CLI (comandos `dotnet` utilizados)

En la terminal de VS Code (PowerShell):

```bash
# crear solución y proyecto
dotnet new sln -n PP2
dotnet new mvc -n PP2.Web -f net8.0
dotnet sln PP2.sln add PP2.Web/PP2.Web.csproj

# ignorar archivos compilados
dotnet new gitignore

# ejecutar con recarga
dotnet watch run --project PP2.Web

## Respuestas a las preguntas

### 1) Número resultante de la multiplicación con valores máximos permitidos
Los valores máximos válidos (longitud 8) son:
- a = `11111111` (255 dec)
- b = `11111111` (255 dec)

Producto: **255 × 255 = 65025**

- **Binario:** `1111111000000001`
- **Octal:** `177001`
- **Decimal:** `65025`
- **Hexadecimal:** `FE01`

### 2) ¿Es posible hacer las operaciones en otra capa?
Sí. Las validaciones y operaciones (bit a bit, aritméticas y conversiones) pueden implementarse en una **capa de servicios** separada (por ejemplo, una librería de clases con `IBinaryService` y su implementación).  
El Controller consume esa capa por inyección de dependencias, lo que mejora la **separación de responsabilidades**, facilita **pruebas unitarias** y promueve **reutilización**.
## Prompts de IA utilizados (ChatGPT)

> A continuación se listan las consultas hechas al asistente y un resumen de las respuestas empleadas para construir la solución.

### Prompt 1
**Consulta:**  
“Especificación de salida: a y b deben mostrarse con 8 bits, pero las operaciones pueden tener más bits (p. ej. multiplicación). ¿Confirmamos que solo a y b van padded a 8 y el resto se muestra en su longitud natural?”

**Respuesta (resumen):**
Sí. a y b se muestran padded a 8; el resto de resultados (bitwise, suma, multiplicación) se muestran con su longitud natural (igual que en el enunciado).

---

### Prompt 2
**Consulta:**  

“Especificación de salida: a y b deben mostrarse con 8 bits, pero las operaciones pueden tener más bits (p. ej. multiplicación). ¿Confirmamos que solo a y b van padded a 8 y el resto se muestra en su longitud natural?”

**Respuesta (resumen):**
Sí. a y b se muestran padded a 8; el resto de resultados (bitwise, suma, multiplicación) se muestran con su longitud natural (igual que en el enunciado).