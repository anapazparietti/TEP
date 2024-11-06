using System.Collections;
using UnityEngine;

public class PlayerFlight : MonoBehaviour
{
    private Vector3 originalPosition; // Para guardar la posición original en la pista
    private bool isFlying = false; // Estado de vuelo del jugador
    private GameObject iceWallInstance; // Instancia del muro de hielo

    public void StartFlight(float skyHeight, float flightDuration, GameObject iceWallPrefab)
    {
        if (isFlying) return; // Evita múltiples activaciones
        isFlying = true;

        // Guarda la posición original y eleva al jugador al cielo
        originalPosition = transform.position;
        transform.position = new Vector3(transform.position.x, skyHeight, transform.position.z);

        // Instancia el muro de hielo y activa la secuencia de pasos
        ShowIceWall(iceWallPrefab);

        // Inicia una rutina para terminar el vuelo después de un tiempo
        StartCoroutine(EndFlightAfterTime(flightDuration));
    }

    private void ShowIceWall(GameObject iceWallPrefab)
    {
        if (iceWallPrefab != null)
        {
            // Coloca el muro en el aire frente al jugador
            Vector3 wallPosition = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
            iceWallInstance = Instantiate(iceWallPrefab, wallPosition, Quaternion.identity);

            // Iniciar la secuencia de pasos en el muro
            IceWall iceWall = iceWallInstance.GetComponent<IceWall>();
            if (iceWall != null)
            {
                iceWall.StartStepSequence();
            }
        }
    }

    private IEnumerator EndFlightAfterTime(float duration)
    {
        // Espera el tiempo de vuelo
        yield return new WaitForSeconds(duration);
        EndFlight();
    }

    private void EndFlight()
    {
        isFlying = false;

        // Devuelve al jugador a la posición original en la pista
        transform.position = originalPosition;

        // Destruye el muro de hielo si existe
        if (iceWallInstance != null)
        {
            Destroy(iceWallInstance);
        }
    }
}
