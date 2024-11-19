using UnityEngine;

public class Player2SyncController : MonoBehaviour
{
    [Header("Configuración del modo Sincro")]
    public float syncDuration = 5f; // Duración del modo sincro
    private float syncTimer;
    private bool isSyncing = false;

    private Player2 player;
    private SyncManager2 syncManager;

    private void Start()
    {
        // Obtén referencias a los componentes
        player = GetComponent<Player2>();
        syncManager = GetComponent<SyncManager2>();
    }

    private void Update()
    {
        // Verificar si estamos en el modo sincro
        if (isSyncing)
        {
            syncTimer -= Time.deltaTime;

            // Si se acaba el tiempo, salir del modo sincro
            if (syncTimer <= 0)
            {
                ExitSyncMode(false); // Sin éxito
            }
        }
    }

    // Método para entrar en el modo sincro
    public void EnterSyncMode()
    {
        isSyncing = true;
        syncTimer = syncDuration;
        player.isRunning = false; // Detener al jugador

        // Notificar en la consola
        Debug.Log($"{player.name} ha entrado en el modo sincro");
    }

    // Método para salir del modo sincro
    public void ExitSyncMode(bool success)
    {
        isSyncing = false;
        player.isRunning = true; // Reactivar al jugador

        // Enviar el resultado al script Player
        player.SetSyncingResult(success);

        if (success)
        {
            Debug.Log($"{player.name} completó el modo sincro con éxito");
        }
        else
        {
            Debug.Log($"{player.name} falló en el modo sincro");
        }
    }

    // Método para verificar si el jugador está en modo sincro
    public bool IsSyncing()
    {
        return isSyncing;
    }

    // Método llamado por SyncManager cuando se completa la secuencia
    public void OnSequenceComplete(bool success)
    {
        ExitSyncMode(success);
    }
}
