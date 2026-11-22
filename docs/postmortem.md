# Postmortem - Programa 2

## 1. Objetivo
Evaluar las estimaciones, el tiempo, el tamaño,
los defectos y los resultados de calidad del
Programa 2 desarrollado mediante PSP1.

## 2. Estimación de tamaño
- LOC planificadas: 420
- LOC reales preliminares: 444
- Diferencia: +24 LOC
- Desviación: +5,71 %
- LLOC reportadas: 206

## 3. Resultados de pruebas
- Pruebas automatizadas: 9 aprobadas.
- Pruebas funcionales: 4 aprobadas.
- Fallos observados en las ejecuciones reportadas: 0.
- Compilación: exitosa, sin errores ni advertencias.

## 4. Defectos
Durante la revisión de código se identificó
y corrigió un defecto de sintaxis en LocCounter.

También se revisó el filtrado de clases anidadas.

El total definitivo de defectos debe coincidir
con el registro de PSP Tracker.

## 5. Evaluación del proceso
La planificación y el diseño superaron los tiempos
estimados. Las pruebas requirieron menos tiempo
del previsto.

La estimación de tamaño fue inferior al resultado
preliminar en 24 LOC.

## 6. Limitaciones
El conteo LOC fue generado por el propio Programa 2.
No se realizó una verificación independiente de
todos los valores obtenidos.

Los resultados de pruebas solo acreditan los casos
efectivamente ejecutados.

## 7. Cierre
Tiempo final: 237 minutos (3 h 57 min).
Productividad final: 112,4 LOC por hora (444 LOC / 237 minutos).
