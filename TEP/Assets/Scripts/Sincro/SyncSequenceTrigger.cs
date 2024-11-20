using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Sincro;


public class SyncSequenceTrigger : MonoBehaviour
{
    public string[] sequence1;
    public string[] sequence2;

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< Updated upstream
        if (other.CompareTag("Player2"))
=======
        var playerController = other.GetComponent<PlayerSyncController>();
        if (playerController != null)
>>>>>>> Stashed changes
        {
            SyncManager syncManager = FindObjectOfType<SyncManager>();
            syncManager.sequences = new List<string[]>
            {
                sequence1,
                sequence2
            };

            Debug.Log($"Secuencias actualizadas para {other.name}");
        }
    }

    
}
