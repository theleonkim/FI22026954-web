# PP1 – Suma de números naturales (SC-701)

**Estudiante:** Kimberly León Ramírez — **Carné:** FI22026954  
**Curso:** Programación Avanzada en Web (SC-701)  
**Profesor:** Luis Andrés Rojas Matey  
**Framework:** .NET 8.0 (C#) – **CLI:** `dotnet`

> **Objetivo:** implementar dos métodos de suma de naturales (`SumFor`, `SumIte`) y, para cada uno, hallar:
> - el **último** `sum > 0` recorriendo `n` **ascendentemente** (`1 → int.MaxValue`), y  
> - el **primer** `sum > 0` recorriendo `n` **descendentemente** (`int.MaxValue → 1`).

---

## Estructura del repositorio

PP1/
├─ pp1.sln
├─ PP1Console/
│ ├─ PP1Console.csproj
│ └─ Program.cs
└─ README.md

yaml
Copy code

> Se excluyen `bin/` y `obj/` con `.gitignore`.

---

## Cómo compilar y ejecutar

```bash
# entrar al proyecto de consola
cd PP1/PP1Console

# compilar
dotnet build

# ejecutar (opción 1)
dotnet run

# ejecutar (opción 2, directo al binario si PowerShell da problemas)
.\bin\Debug\net8.0\PP1Console.exe
# o:
dotnet .\bin\Debug\net8.0\PP1Console.dll
Especificaciones implementadas
Aplicación de Consola (.NET 8).

Tipos: exclusivamente int (System.Int32); overflow permitido con unchecked.

Métodos requeridos:

SumFor(int n): fórmula n * (n + 1) / 2 (en unchecked).

SumIte(int n): suma iterativa equivalente a la recursiva dada.

Estrategias por método:

Ascendente: n = 1 … int.MaxValue → reporta último sum > 0.

Descendente: n = int.MaxValue … 1 → reporta primer sum > 0.

Salida en consola con el formato solicitado.

Salida esperada (valores de referencia)
yaml
Copy code
• SumFor:
        ◦ From 1 to Max → n: 46340 → sum: 1073720970
        ◦ From Max to 1 → n: 2147437306 → sum: 25487

• SumIte:
        ◦ From 1 to Max → n: 65535 → sum: 2147450880
        ◦ From Max to 1 → n: 2147483646 → sum: 1073741825
Explicación (por qué difieren los valores entre métodos/estrategias)
Causa: overflow de int y orden de evaluación.

SumFor (fórmula): calcula n*(n+1) antes de dividir por 2. Ese producto desborda el rango de int mucho antes; en unchecked el valor hace wrap-around (cambia de signo). El último sum > 0 ascendente ocurre cerca de √(2*int.Max) ≈ 46340. En descenso, el primer positivo aparece en n = 2,147,437,306.

SumIte (iterativa): el acumulador s crece por sumas sucesivas y se mantiene correcto hasta exceder int.MaxValue. El umbral ocurre más tarde que en la fórmula (no hay multiplicación); por eso el último positivo ascendente es n = 65,535. En descenso, el primer positivo es n = 2,147,483,646.

La estrategia (asc/desc) solo determina dónde se detiene la búsqueda (último o primero positivo); los umbrales cambian según dónde ocurre el overflow: en el producto (fórmula) vs en el acumulador (iterativo).

¿Y si fuera recursivo (SumRec)?
Rompe por StackOverflowException mucho antes de los umbrales aritméticos: la profundidad de la pila no soporta millones de llamadas (n llamadas anidadas). Por eso se usa la iterativa equivalente.

Comandos dotnet utilizados
bash
Copy code
# crear solución y proyecto (si se parte de cero)
dotnet new sln -n pp1
dotnet new console -n PP1Console -f net8.0
dotnet sln .\pp1.sln add .\PP1Console\PP1Console.csproj

# compilar y ejecutar
cd PP1Console
dotnet build
dotnet run
Recursos y referencias
Documentación .NET/C#: int, unchecked, CLI dotnet.

Artículo sobre la anécdota de Gauss (contexto histórico citado en el enunciado).

## IA / Prompts usados (requisito de la PP1)

- **Herramienta:** ChatGPT (GPT-5 Thinking)
- **Fechas de uso:** 19-22 set 2025
- **Objetivo:** Implementar `SumFor` / `SumIte` en C# (.NET 8, `int` con `unchecked`), y hallar últimos/primeros `sum > 0` en estrategias asc/desc.
- **Evidencia completa (transcripción/capturas):** `PP1/docs/ia-prompts.md`
- **Enlace público de la conversación (opcional):** _(pegar aquí si generas el link compartido)_

### Resumen de interacciones clave
| Tema | Prompt resumido | Respuesta/Acción de la IA |
|---|---|---|
| Diseño | “Programa de **Consola** con `SumFor` (fórmula) y `SumIte` (iterativa); solo `int`; .NET 8; dos recorridos (1→Max y Max→1); imprimir último/primer `sum > 0`.” | `Program.cs` con `SumFor`, `SumIte`, `FindAsc`, `FindDesc` y salida en el formato del enunciado. |
| Resultados esperados | “¿Cuáles son los valores que deben salir?” | **SumFor asc:** n=46340, sum=1073720970 · **SumFor desc:** n=2147437306, sum=25487 · **SumIte asc:** n=65535, sum=2147450880 · **SumIte desc:** n=2147483646, sum=1073741825. |
| Git/estructura | “Quiero todo en carpeta `PP1/` y empujar a `main`.” | Instrucciones para mover solución/proyecto a `PP1/`, crear `docs/ia-prompts.md`, actualizar `.gitignore` y hacer `git add/commit/push`. |

> **Nota de uso responsable:** La IA se usó como apoyo para acelerar implementación y documentación. La verificación, pruebas y decisiones finales son del estudiante.
