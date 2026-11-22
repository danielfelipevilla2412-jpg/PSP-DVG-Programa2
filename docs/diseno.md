# Diseño - Programa 2

## Arquitectura

| Clase | Responsabilidad |
|---|---|
| Program | Coordinar la ejecución. |
| ConsoleUI | Mostrar menú y resultados. |
| SourceFileReader | Localizar y leer archivos C#. |
| CodeAnalyzer | Identificar clases y métodos con Roslyn. |
| LocCounter | Calcular LOC y LLOC. |
| ClassMetrics | Almacenar métricas por clase. |
| MetricsResult | Almacenar resultados generales. |

## Algoritmo

1. Solicitar y validar la ruta.
2. Obtener los archivos C#.
3. Interpretar el código mediante Roslyn.
4. Identificar las clases y sus métodos.
5. Calcular las líneas físicas y lógicas.
6. Consolidar y presentar los resultados.

## Conteo

Las LOC se obtienen identificando líneas con código válido.
Las LLOC se obtienen mediante los nodos del árbol sintáctico.
Los comentarios y las líneas vacías se excluyen.
Las clases anidadas no deben duplicar las métricas de su clase padre.

## Interfaz

1. Analizar archivo.
2. Analizar directorio.
3. Salir.

Se mantiene el estilo rojo y negro del Programa 1.

## Pruebas

Validar archivos individuales, comentarios, clases, métodos,
directorios y los programas 1 y 2 mediante xUnit y pruebas funcionales.

## Implementación

- Utilizar tokens de Roslyn para identificar líneas físicas.
- Utilizar nodos sintácticos para contar instrucciones lógicas.
- Agrupar clases parciales por su identidad completa.
- Contar métodos y constructores explícitos.

| Clase | Métodos principales |
|---|---|
| SourceFileReader | GetSourceFiles, ReadFile |
| CodeAnalyzer | Analyze |
| LocCounter | CountPhysicalLines, CountLogicalLines |
| ConsoleUI | ShowMenu, ShowResults |
