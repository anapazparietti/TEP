using System.Collections.Generic;
using UnityEngine;

public class SyncManager2 : MonoBehaviour
{
    [Header("Configuración de Secuencias")]
    public List<string[]> sequences = new List<string[]>(); // Lista de series de secuencias
    private int currentSequenceIndex = 0; // Índice de la secuencia actual
    private int currentStep = 0; // Paso actual dentro de la secuencia
    private string teclaApretada = "Null";

    private Player2SyncController player2SyncController;

    void Start()
    {
        player2SyncController = GetComponent<Player2SyncController>();

        // Verificar si se detecta correctamente el componente
        if (player2SyncController == null)
        {
            Debug.LogError("Player2SyncController no está asignado. Verifica que esté en el mismo GameObject.");
        }

        // Mostrar las secuencias iniciales cargadas desde el inspector
        LogSequences();
    }

    void Update()
    {
        // Mostrar cuántas secuencias están configuradas
        Debug.Log($"Cantidad de secuencias configuradas: {sequences.Count}");

        // Verificar si el jugador está en modo sincro
        if (player2SyncController != null && player2SyncController.IsSyncing() && sequences.Count > 0)
        {
            Debug.Log("El jugador está en modo sincro.");

            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kcode))
                    {
                        teclaApretada = kcode.ToString();
                        Debug.Log($"Tecla presionada: {teclaApretada}");
                        recibirTecla(teclaApretada);
                        break;
                    }
                }
            }
        }
    }

    void LogSequences()
    {
        Debug.Log("Mostrando todas las secuencias:");
        for (int i = 0; i < sequences.Count; i++)
        {
            string[] sequence = sequences[i];
            string sequenceContent = string.Join(", ", sequence); // Convierte el array en un string separado por comas
            Debug.Log($"Secuencia {i + 1}: [{sequenceContent}]");
        }
    }

    public void recibirTecla(string tecla)
    {
        Debug.Log($"Validando tecla '{tecla}' para la secuencia actual.");

        if (currentSequenceIndex < sequences.Count)
        {
            string[] currentSequence = sequences[currentSequenceIndex];

            Debug.Log($"Secuencia actual: {string.Join(", ", currentSequence)}");
            Debug.Log($"Paso actual: {currentStep}");

            if (currentStep < currentSequence.Length && tecla == currentSequence[currentStep])
            {
                Debug.Log($"Tecla correcta para el paso {currentStep + 1}.");

                currentStep++;

                if (currentStep >= currentSequence.Length)
                {
                    Debug.Log("Secuencia completada.");
                    player2SyncController.OnSequenceComplete(true);
                    AdvanceToNextSequence();
                }
            }
            else
            {
                Debug.Log($"Tecla incorrecta. Se esperaba '{currentSequence[currentStep]}' pero se recibió '{tecla}'.");
                player2SyncController.OnSequenceComplete(false);
                player2SyncController.EnterSyncMode(); // Salir del modo sincro
            }
        }
        else
        {
            Debug.LogWarning("No hay más secuencias por validar.");
        }
    }

    public void AdvanceToNextSequence()
    {
        Debug.Log("Avanzando a la siguiente secuencia.");

        currentStep = 0; // Reinicia el paso
        currentSequenceIndex++;

        if (currentSequenceIndex >= sequences.Count)
        {
            Debug.Log("Todas las secuencias completadas.");
            player2SyncController.EnterSyncMode(); // Termina el modo sincro si no hay más secuencias
        }
        else
        {
            Debug.Log($"Nueva secuencia activa: {currentSequenceIndex + 1}.");
        }
    }

    public void SetSequences(List<string[]> newSequences)
    {
        sequences = newSequences;
        ResetSequences();
        LogSequences(); // Mostrar las nuevas secuencias configuradas
    }

    public void ResetSequences()
    {
        currentSequenceIndex = 0;
        currentStep = 0;
        Debug.Log("Secuencias reseteadas.");
    }

    public void AddSequence(string[] newSequence)
    {
        sequences.Add(newSequence);
        Debug.Log($"Nueva secuencia añadida: {string.Join(", ", newSequence)}.");
    }
}
