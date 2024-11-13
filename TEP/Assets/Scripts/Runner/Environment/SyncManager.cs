using System.Collections;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    [Header("Configuración de la Secuencia de Teclas")]
    public string[] correctSequence; // Secuencia personalizada por jugador
    private int currentStep = 0;
    private string teclaApretada = "Null";

    private Player player; // Referencia al script del Player

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!player.isRunning)
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

    // Método para recibir la tecla presionada y validar el paso actual
    public void recibirTecla(string tecla)
    {
        if (currentStep < correctSequence.Length && tecla == correctSequence[currentStep])
        {
            currentStep++;
            if (currentStep >= correctSequence.Length)
            {
                // Secuencia completada correctamente
                player.SetSyncingResult(true); // Éxito
                currentStep = 0; // Reiniciar la secuencia
            }
        }
        else
        {
            // Falló la secuencia
            player.SetSyncingResult(false); // Fallo
            currentStep = 0; // Reiniciar
        }
    }
}
