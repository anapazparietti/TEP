using UnityEngine;

public class SyncManager2 : MonoBehaviour
{
    public string[] correctSequence;
    private int currentStep = 0;
    private string teclaApretada = "Null";

    private Player2SyncController player2SyncController;

    private void Start()
    {
        player2SyncController = GetComponent<Player2SyncController>();
    }

    private void Update()
    {
        // Verificar si el jugador está en modo sincro
        if (player2SyncController != null && player2SyncController.IsSyncing())
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
        if (currentStep < correctSequence.Length && tecla == correctSequence[currentStep])
        {
            currentStep++;
            if (currentStep >= correctSequence.Length)
            {
                // Secuencia completada con éxito
                player2SyncController.OnSequenceComplete(true);
                currentStep = 0; // Reiniciar secuencia
            }
        }
        else
        {
            // Falló la secuencia
            Debug.Log("Secuencia incorrecta");
        }
    }
}
