using System.Collections;
using UnityEngine;

public class PlayerFlight : MonoBehaviour
{
    private Vector3 originalPosition; // Para guardar la posición original en la pista
    private bool isFlying = false; // Estado de vuelo del jugador
    private GameObject iceWallInstance; // Instancia del muro de hielo

    [Header("Penalización de velocidad")]
    public float penalizedSpeed = 3f; // Velocidad cuando el jugador falla la secuencia
    public float penalizationDuration = 2f; // Duración de la penalización en segundos
    private float originalSpeed; // Para almacenar la velocidad original del jugador
    private Player playerScript; // Referencia al script del jugador

    private void Start()
    {
        playerScript = GetComponent<Player>();
        originalSpeed = playerScript.speed; // Guarda la velocidad original del jugador
    }

    public void StartFlight(float skyHeight, float flightDuration, GameObject iceWallPrefab)
    {
        if (isFlying) return; // Evita múltiples activaciones
        isFlying = true;

        // Guarda la posición original y eleva al jugador al cielo
        originalPosition = transform.position;
        transform.position = new Vector3(transform.position.x, skyHeight, transform.position.z);

        // Instancia el muro de hielo y activa la secuencia de pasos
        ShowIceWall(iceWallPrefab);
    }

    private void ShowIceWall(GameObject iceWallPrefab)
    {
        if (iceWallPrefab != null)
        {
            // Coloca el muro en el aire frente al jugador
            Vector3 wallPosition = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
            iceWallInstance = Instantiate(iceWallPrefab, wallPosition, Quaternion.identity);

            // Inicia la secuencia de pasos en el muro
            IceWall iceWall = iceWallInstance.GetComponent<IceWall>();
            if (iceWall != null)
            {
                iceWall.StartStepSequence(this); // Pasa una referencia de PlayerFlight a IceWall
            }
        }
    }

    // Método que se llama desde IceWall al completar la secuencia correctamente
    public void CompleteSequence()
    {
        Debug.Log("Secuencia completada correctamente. Continuando vuelo.");
        EndFlight(); // Termina el vuelo normalmente
    }

    // Método que se llama desde IceWall al fallar la secuencia
    public void FailSequence()
    {
        Debug.Log("Secuencia fallida. Aplicando penalización.");
        StartCoroutine(ApplyPenalization());
    }

    private IEnumerator ApplyPenalization()
    {
        playerScript.speed = penalizedSpeed; // Reduce la velocidad
        yield return new WaitForSeconds(penalizationDuration); // Espera el tiempo de penalización
        playerScript.speed = originalSpeed; // Restaura la velocidad original
        EndFlight(); // Baja al jugador a la pista
    }

    private void EndFlight()
    {
        isFlying = false;
        transform.position = originalPosition; // Devuelve al jugador a la pista

        // Destruye el muro de hielo si existe
        if (iceWallInstance != null)
        {
            Destroy(iceWallInstance);
        }
    }
}
