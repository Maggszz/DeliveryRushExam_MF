# Consigna para alumnos

## Proyecto

`Delivery Rush Exam Project`

Vas a trabajar sobre un mini juego 2D de pedidos. El juego ya funciona, pero necesita mejoras técnicas.

## Tareas

1. Ejecutar la escena DeliveyRushExam.
2. Analizar el juego con Unity Profiler.
3. Detectar problemas de rendimiento.
4. Implementar Object Pooling para los popups de score.
5. Conectar el guardado con Unity Cloud Save.
6. Crear una interfaz para el sistema de guardado.
7. Usar `ServiceLocator` y `SaveServicesInstaller` para elegir entre guardado local o UGS.
8. Entregar evidencia de pruebas.

## Requisitos técnicos

- Usar Authentication anónima.
- Usar Cloud Save para guardar progreso.
- Crear una abstracción común para los servicios de guardado.
- El `SaveManager` no debe depender directamente de `LocalSaveService`.
- La selección entre Local y Cloud debe hacerse desde `SaveServicesInstaller`.
- No eliminar datos de `PlayerProgressData`.
- No romper el flujo de partida.
- No reemplazar todo el proyecto.
- Mantener código claro y separado por responsabilidades.

## Entrega

Incluir:

- Proyecto Unity funcionando.
- Breve informe técnico.
- Capturas de los datos en UGS.
- Capturas o datos del Profiler antes y después.
- Explicación de cambios realizados.
- Problemas encontrados.
- Decisiones tomadas y justificación.

## Datos que deben guardarse

- `playerName`
- `bestScore`
- `totalCoins`
- `completedOrders`
- `unlockedLevel`
- `lastSaveDate`

## Evaluación

Se evalúa diagnóstico, implementación, claridad técnica, uso correcto de UGS y justificación.
