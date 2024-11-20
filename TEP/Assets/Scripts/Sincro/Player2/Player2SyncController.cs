using UnityEngine;
using Assets.Scripts.Sincro;

public class Player2SyncController : MonoBehaviour
{
    [Header("Configuración del modo Sincro")]
    public float syncDuration = 5f; // Duración del modo sincro
    public float penaltyDuration = 3f; // Penalización en segundos
    public float penaltySpeedMultiplier = 0.5f; // Velocidad reducida durante la penalización

    private int currentSequenceIndex = 0; // Índice actual de la secuencia
    private int currentStep = 0; // Paso actual dentro de la secuencia
    private float syncTimer; // Temporizador para el modo sincro
    private bool isSyncing = false; // Indicador del estado de sincronización

    private Player2 player; // Referencia al script del jugador 2
    private SyncManager syncManager; // Referencia al SyncManager compartido

    private void Start()
    {
        player = GetComponent<Player2>();
        syncManager = FindObjectOfType<SyncManager>(); // Encontrar el SyncManager en la escena
    }

    private void Update()
    {
        if (isSyncing)
        {
            syncTimer -= Time.deltaTime;
            if (syncTimer <= 0)
            {
                OnSequenceComplete(false); // Finalizar sincronización sin éxito
            }
        }
    }

    public void EnterSyncMode()
    {
        isSyncing = true;
        syncTimer = syncDuration;
        player.isRunning = false; // Detener al jugador durante el modo sincro
        Debug.Log($"{player.name} ha entrado en modo sincro");
    }

    public void ExitSyncMode(bool success)
    {
        isSyncing = false;
        player.isRunning = true; // Reactivar al jugador

        if (!success)
        {
            ApplyPenalty(); // Aplicar penalización en caso de fallo
        }

        Debug.Log(success ? $"{player.name} completó el modo sincro con éxito" : $"{player.name} falló en el modo sincro");
    }

    public void OnSequenceComplete(bool success)
    {
        ExitSyncMode(success);

        if (success)
        {
            currentStep = 0; // Reiniciar pasos
            currentSequenceIndex++; // Avanzar a la siguiente secuencia
        }
    }

    public bool IsSyncing() => isSyncing; // Verificar si está en modo sincro

    public int GetSequenceIndex() => currentSequenceIndex; // Obtener el índice de la secuencia actual

    public bool IsCorrectStep(string key, string[] sequence)
    {
        // Verificar si el paso actual es correcto
        return currentStep < sequence.Length && key == sequence[currentStep];
    }

    public void AdvanceStep()
    {
        currentStep++; // Avanzar al siguiente paso
    }

    public bool IsSequenceComplete(int sequenceLength)
    {
        // Verificar si se completó la secuencia
        return currentStep >= sequenceLength;
    }

    private void ApplyPenalty()
    {
        StartCoroutine(PenaltyCoroutine());
    }

    private System.Collections.IEnumerator PenaltyCoroutine()
    {
        float originalSpeed = player.forwardSpeed; // Guardar la velocidad original
        player.forwardSpeed *= penaltySpeedMultiplier; // Reducir la velocidad

        yield return new WaitForSeconds(penaltyDuration); // Esperar la penalización

        player.forwardSpeed = originalSpeed; // Restaurar la velocidad original
        Debug.Log($"{player.name} ha recuperado su velocidad normal");
    }
}
