using System.Collections;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    public string[] correctSequence = { "A", "S", "D", "W" };
    private int currentStep = 0;

    private void Update()
    {
        if (StateManager.Instance.GetCurrentState() == StateManager.GameState.Syncing)
        {
            // Esperar la entrada del usuario
            if (Input.anyKeyDown)
            {
                // Validar si la tecla en la secuencia es válida antes de procesarla
                if (currentStep < correctSequence.Length && IsValidKey(correctSequence[currentStep]))
                {
                    // Verificar si la tecla presionada es la correcta
                    if (Input.GetKeyDown(correctSequence[currentStep]))
                    {
                        currentStep++;
                        if (currentStep >= correctSequence.Length)
                        {
                            Debug.Log("¡Secuencia completada!");
                            StateManager.Instance.ChangeState(StateManager.GameState.Running);
                            currentStep = 0; // Reiniciar secuencia para la próxima vez
                        }
                    }
                    else
                    {
                        Debug.Log("Tecla incorrecta. Inténtalo de nuevo.");
                        currentStep = 0; // Reiniciar si se equivoca
                    }
                }
            }
        }
    }

    // Método para validar si una tecla es válida
    private bool IsValidKey(string key)
    {
        try
        {
            // Intenta verificar si la tecla es válida
            Input.GetKeyDown(key);
            return true;
        }
        catch
        {
            Debug.LogError($"Tecla no válida: {key}");
            return false;
        }
    }
}
