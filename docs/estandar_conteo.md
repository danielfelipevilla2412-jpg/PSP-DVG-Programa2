# Estándar de codificación y conteo

## Codificación

- Indentación de cuatro espacios.
- PascalCase para clases y métodos.
- camelCase para variables y parámetros.
- Llaves en líneas independientes.
- Una instrucción por línea cuando sea posible.

## Conteo físico (LOC)

- Contar las líneas que contienen código, incluyendo llaves.
- Incluir declaraciones, using y namespace.
- Excluir comentarios y líneas vacías.
- Contar las líneas que contienen código y comentarios una sola vez.
- Excluir archivos generados y directorios bin, obj y .git.

## Conteo lógico (LLOC)

- Contar declaraciones, instrucciones ejecutables y estructuras de control.
- Excluir llaves, comentarios, using y namespace.
- No depender del número de líneas físicas de cada instrucción.

## Conteo por clase

- Incluir métodos y constructores explícitos.
- Excluir propiedades, accesores y funciones locales del número de métodos.
- Evitar duplicidades entre clases anidadas.
- Agrupar las declaraciones parciales de una misma clase.
- Incluir el código externo a las clases en el total general.

Las LOC físicas serán la métrica principal de PSP1.
El código de pruebas se medirá por separado.
