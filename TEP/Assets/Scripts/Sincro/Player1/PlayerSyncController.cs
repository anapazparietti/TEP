using UnityEngine;
using Assets.Scripts.Sincro;


public class PlayerSyncController : MonoBehaviour
{
    [Header("Configuración del modo Sincro")]
    public float syncDuration = 5f;
    public float penaltyDuration = 3f;
    public float penaltySpeedMultiplier = 0.5f;

    private int currentSequenceIndex = 0;
    private int currentStep = 0;
    public float syncTimer;
    private bool isSyncing = false;

    private Player player;
    public SyncManager syncManager;

    private void Start()
    {
        player = GetComponent<Player>();
        syncManager = FindObjectOfType<SyncManager>();
    }

    private void Update()
    {
        if (isSyncing)
        {
            syncTimer -= Time.deltaTime;
            if (syncTimer <= 0)
            {
                OnSequenceComplete(false); // Tiempo agotado
            }
        }
    }

    public void EnterSyncMode()
    {
        isSyncing = true;
        syncTimer = syncDuration;
        player.isRunning = false;
    }

    public void ExitSyncMode(bool success)
    {
        isSyncing = false;
        player.isRunning = true;

        if (!success)
        {
            ApplyPenalty();
        }
    }

    public void OnSequenceComplete(bool success)
    {
        ExitSyncMode(success);
        if (success)
        {
            currentStep = 0;
            currentSequenceIndex++;
        }
    }

    public bool IsSyncing() => isSyncing;
    public int GetSequenceIndex() => currentSequenceIndex;

    public bool IsCorrectStep(string key, string[] sequence)
    {
        return currentStep < sequence.Length && key == sequence[currentStep];
    }

    public void AdvanceStep()
    {
        currentStep++;
    }

    public bool IsSequenceComplete(int sequenceLength)
    {
        return currentStep >= sequenceLength;
    }

    private void ApplyPenalty()
    {
        StartCoroutine(PenaltyCoroutine());
    }

    private System.Collections.IEnumerator PenaltyCoroutine()
    {
        float originalSpeed = player.forwardSpeed;
        player.forwardSpeed *= penaltySpeedMultiplier;
        yield return new WaitForSeconds(penaltyDuration);
        player.forwardSpeed = originalSpeed;
    }
}
