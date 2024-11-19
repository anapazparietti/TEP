using UnityEngine;

public class SyncManager : MonoBehaviour
{
    public string[] correctSequence;
    private int currentStep = 0;
    private string teclaApretada = "Null";

    private PlayerSyncController playerSyncController;

    private void Start()
    {
        playerSyncController = GetComponent<PlayerSyncController>();
    }

    private void Update()
    {
        // Verificar si el jugador está en modo sincro
        if (playerSyncController != null && playerSyncController.IsSyncing())
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
                playerSyncController.OnSequenceComplete(true);
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
