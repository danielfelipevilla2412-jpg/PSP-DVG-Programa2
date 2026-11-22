# PIP - Plan de Mejora del Proceso

## PIP-01: Mejorar la estimación de tiempo
Problema: planificación y diseño excedieron
sus estimaciones.

Acción: utilizar los tiempos reales de los
Programas 1 y 2 para estimar el Programa 3.

## PIP-02: Fortalecer la revisión de código
Problema: se detectaron defectos en el filtrado
de clases y en una condición sintáctica.

Acción: aplicar una lista de revisión estática
antes de la compilación.

## PIP-03: Mejorar la cobertura de pruebas
Problema: los casos aprobados no garantizan
exactitud para todas las construcciones de C#.

Acción: incorporar casos límite y comparar
los resultados con conteos independientes.

## PIP-04: Mejorar la documentación
Problema: se identificaron inconsistencias
en la tabla de tiempos del PPS.

Acción: comprobar los totales y la coherencia
entre PPS y PSP Tracker antes de cada commit.

## PIP-05: Controlar la calidad de commits
Problema: la evidencia de pruebas contenía
espacios sobrantes.

Acción: ejecutar git diff --cached --check
y corregir los avisos antes de cada commit.
