using System.Collections.Generic; // Necesario para listas
using UnityEngine;

public class SyncSequenceTrigger : MonoBehaviour
{
    [Header("Nuevas Secuencias para Asignar")]
    public string[] sequence1;
    public string[] sequence2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SyncManager2 syncManager = other.GetComponent<SyncManager2>();
            if (syncManager != null)
            {
                // Crea una nueva lista de secuencias
                var newSequences = new List<string[]>
                {
                    sequence1,
                    sequence2
                };

                // Actualiza las secuencias del SyncManager
                syncManager.SetSequences(newSequences);
                Debug.Log("Secuencias actualizadas desde el trigger");
            }
        }
    }
}
