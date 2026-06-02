# Configuración Unity Game Services

Referencias oficiales:

- Authentication anónima: https://docs.unity.com/en-us/authentication/use-anon-sign-in
- Cloud Save Unity SDK: https://docs.unity.com/cloud-save/tutorials/unity-sdk
- Cloud Save get started: https://docs.unity.com/en-us/cloud-save/get-started

## Pasos

1. Abrir el proyecto en Unity.
2. Ir a `Edit > Project Settings > Services`.
3. Vincular el proyecto a una organización y Project ID.
4. Abrir `Window > Package Manager`.
5. Instalar desde Unity Registry:
   - `Authentication`
   - `Cloud Save`
6. En Unity Dashboard:
   - Activar Authentication.
   - Habilitar Anonymous Sign-In.
   - Activar Cloud Save.
7. Verificar que `UgsInitializer` esté en la escena.
8. Ejecutar la escena.
9. Confirmar en consola que hay login anónimo.
10. Adaptar `SaveServicesInstaller` para registrar el servicio Cloud usando la interfaz creada.
11. Cambiar el modo del installer a Cloud.
12. Completar una partida y revisar datos en Cloud Save.

## Nota para el examen

El proyecto usa `LocalSaveService` de forma directa al inicio. La tarea del alumno es crear la interfaz, registrar el servicio correcto desde el installer y validar `UgsCloudSaveService`.
