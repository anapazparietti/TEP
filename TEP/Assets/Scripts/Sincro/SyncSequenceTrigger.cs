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
        if (other.CompareTag("Player2"))
        {
            SyncManager2 syncManager = other.GetComponent<SyncManager2>();
            if (syncManager != null);
            }
        }
    }

    