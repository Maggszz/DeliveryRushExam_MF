# Delivery Rush Exam Project - Arquitectura

Mini juego 2D para examen práctico. Funciona de punta a punta, pero incluye puntos de mejora controlados.

## Carpetas

- `Scripts/Core`: gameplay y estado de partida.
- `Scripts/Data`: modelos serializables.
- `Scripts/Save`: persistencia local y Cloud Save.
- `Scripts/UGS`: inicialización de Unity Gaming Services.
- `Scripts/UI`: HUD, pedidos y popups.
- `Editor`: generador automático de escena.
- `Docs`: material y consigna.

## Scripts

`GameManager`
   - Controla inicio, timer y final de partida.
   - Llama al guardado al terminar.

`OrderManager`
   - Genera pedidos.
   - Mantiene pedidos activos.
   - Permite completar pedidos.

`ScoreManager`
   - Maneja score, monedas y pedidos completados.

`UIManager`
   - Actualiza HUD.
   - Crea botones de pedidos.
   - Muestra popups de score.

`OrderButtonView`
   - Vista de un pedido activo.
   - Botón para completar pedido.

`ScorePopupView`
   - Popup flotante de puntos.

`LocalSaveService`
   - Implementación funcional con `PlayerPrefs`.

`UgsCloudSaveService`
   - Implementación parcial para Cloud Save.
   - Requiere paquetes UGS y símbolo `DELIVERY_RUSH_UGS`.

`SaveManager`
   - Actualmente usa `LocalSaveService` de forma directa.
   - Punto de refactor para usar una abstracción.

`ServiceLocator`
   - Registro simple de servicios.
   - Falta conectarlo al flujo de guardado.

`SaveServicesInstaller`
   -  Permite elegir modo local o cloud.
   - Actualmente registra clases concretas.

`UgsInitializer`
   - Inicializa UGS.
   - Hace login anónimo cuando el símbolo UGS está activo.

`PlayerProgressData`
   - Datos persistidos del jugador.

`OrderData`
   - Datos runtime de un pedido.

`ExamSceneBuilder`
   - Crea la escena base desde `Delivery Rush Exam/Build Exam Scene`.

## Flujo

`GameManager` inicia partida -> `OrderManager` genera pedidos -> `UIManager` muestra pedidos -> el jugador completa pedidos -> `ScoreManager` suma recompensas -> `UIManager` muestra popup -> al terminar, `SaveManager` guarda progreso.

## Refactor esperado en guardado

El proyecto inicia con `SaveManager` acoplado a `LocalSaveService`. El examen pide crear una interfaz de guardado, registrar la implementación elegida en `SaveServicesInstaller` y resolverla desde `ServiceLocator`.
