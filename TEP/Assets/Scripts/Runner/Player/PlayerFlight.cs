using System.Collections;
using UnityEngine;

public class PlayerFlight : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isFlying = false;
    private GameObject iceWallInstance;

    [Header("Configuración de Vuelo")]
    public float flightForwardDistance = 10f;
    [SerializeField] private GameObject syncPlatform;

    [Header("Penalización de Velocidad")]
    public float penalizedSpeed = 3f;
    public float penalizationDuration = 2f;
    private float originalSpeed;
    private Player playerScript;

    private void Start()
    {
        playerScript = GetComponent<Player>();
        originalSpeed = playerScript.speed;
    }

    public void StartFlight(float verticalBoost, float flightDuration)
    {
        if (isFlying) return;
        isFlying = true;

        originalPosition = transform.position;
        StartCoroutine(FlyToSyncPlatform(verticalBoost, flightDuration));
    }

   private IEnumerator FlyToSyncPlatform(float verticalBoost, float duration)
{
    Vector3 targetPosition = syncPlatform != null ? syncPlatform.transform.position : new Vector3(0, 38.26f, 0);
    
    // Mueve al jugador hasta que esté muy cerca de la plataforma de sincronización
    while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
    {
        // Mueve al jugador hacia la posición objetivo a una velocidad constante
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, verticalBoost * Time.deltaTime);
        yield return null;
    }

    // Cambia al estado de sincronización y ajusta al jugador en la plataforma
    StateManager.Instance.ChangeState(StateManager.GameState.Syncing);
    PlaceOnSyncPlatform();

    // Inicia el tiempo en sincronización (ajusta el tiempo según lo necesario)
    StartCoroutine(SyncingDuration(5f)); // 5 segundos en sincronización, ajusta según necesidad
}

private void PlaceOnSyncPlatform()
{
    if (syncPlatform != null)
    {
        transform.position = syncPlatform.transform.position;
        // Asegúrate de que el jugador mire hacia el muro en la dirección deseada (ajusta la posición en Z si es necesario)
        transform.LookAt(new Vector3(syncPlatform.transform.position.x, syncPlatform.transform.position.y, syncPlatform.transform.position.z + 1));
    }
}
private IEnumerator SyncingDuration(float syncDuration)
{
    yield return new WaitForSeconds(syncDuration);
    EndFlight(); // Finaliza el estado de sincronización y baja al jugador
}

    public void CompleteSequence()
    {
        Debug.Log("Secuencia completada correctamente. Continuando vuelo.");
        EndFlight();
    }

    public void FailSequence()
    {
        Debug.Log("Secuencia fallida. Aplicando penalización.");
        StartCoroutine(ApplyPenalization());
    }

    private IEnumerator ApplyPenalization()
    {
        playerScript.speed = penalizedSpeed;
        yield return new WaitForSeconds(penalizationDuration);
        playerScript.speed = originalSpeed;
        EndFlight();
    }

    private void EndFlight()
    {
        isFlying = false;
        transform.position = originalPosition;

        if (iceWallInstance != null)
        {
            Destroy(iceWallInstance);
        }
    }
}
