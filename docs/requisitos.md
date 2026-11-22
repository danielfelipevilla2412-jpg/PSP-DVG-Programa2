# Programa 2 - Conteo de líneas de código

## Objetivo

Desarrollar una aplicación de consola en C# y .NET 9
que permita medir el tamaño del código fuente.

## Requisitos funcionales

| ID | Requisito |
|---|---|
| RF01 | Analizar archivos y directorios de código C#. |
| RF02 | Identificar las clases y sus métodos. |
| RF03 | Contabilizar las LOC de cada clase. |
| RF04 | Calcular las LOC totales del programa. |
| RF05 | Contabilizar líneas lógicas de código (LLOC). |
| RF06 | Excluir comentarios, líneas vacías y archivos generados. |
| RF07 | Mostrar los resultados en consola. |
| RF08 | Controlar errores de lectura y rutas inválidas. |

## Requisitos no funcionales

- Utilizar C#, .NET 9 y Roslyn.
- Separar las responsabilidades del programa.
- Mantener el estilo visual del Programa 1.

## Criterios de aceptación

- Obtener resultados acordes al estándar de conteo.
- Identificar correctamente clases y métodos.
- Evitar duplicidades en el conteo.
- Validar los resultados de los programas 1 y 2.
- Controlar las entradas inválidas.
