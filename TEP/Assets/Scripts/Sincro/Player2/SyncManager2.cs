using System.Collections.Generic; // Necesario para listas
using UnityEngine;

public class SyncManager2 : MonoBehaviour
{
    [Header("Configuración de Secuencias")]
    public List<string[]> sequences = new List<string[]>(); // Lista de series de secuencias
    private int currentSequenceIndex = 0; // Índice de la secuencia actual
    private int currentStep = 0; // Paso actual dentro de la secuencia
    private string teclaApretada = "Null";

    private Player2SyncController player2SyncController;

    private void Start()
    {
        player2SyncController = GetComponent<Player2SyncController>();
    }
 private void Update()
    {
        // Verificar si el jugador está en modo sincro
        if (player2SyncController != null && player2SyncController.IsSyncing() && sequences.Count > 0)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kcode))
                    {
                        teclaApretada = kcode.ToString();
                        recibirTecla(teclaApretada);
                        break;
                    }
                }
            }
        }
    }


    // Método para recibir la tecla presionada y validar la secuencia
   public void recibirTecla(string tecla)
    {
        if (currentSequenceIndex < sequences.Count)
        {
            string[] currentSequence = sequences[currentSequenceIndex];
            if (currentStep < currentSequence.Length && tecla == currentSequence[currentStep])
            {
                currentStep++;
                if (currentStep >= currentSequence.Length)
                {
                    // Secuencia completada
                    player2SyncController.OnSequenceComplete(true);
                    AdvanceToNextSequence();
                }
            }
            else
            {
                // Falló la secuencia
                Debug.Log("Secuencia incorrecta");
                player2SyncController.OnSequenceComplete(false);
                player2SyncController.EnterSyncMode(); // Salir del modo sincro
            }
        }
    }
    public void AdvanceToNextSequence()
    {
        currentStep = 0; // Reinicia el paso
        currentSequenceIndex++;

        if (currentSequenceIndex >= sequences.Count)
        {
            Debug.Log("Todas las secuencias completadas");
            player2SyncController.EnterSyncMode(); // Termina el modo sincro si no hay más secuencias
        }
        else
        {
            Debug.Log($"Avanzando a la secuencia {currentSequenceIndex + 1}");
        }
    }

    public void SetSequences(List<string[]> newSequences)
    {
        sequences = newSequences;
        ResetSequences();
    }

    public void ResetSequences()
    {
        currentSequenceIndex = 0;
        currentStep = 0;
    }

    public void AddSequence(string[] newSequence)
    {
        sequences.Add(newSequence);
    }
}
