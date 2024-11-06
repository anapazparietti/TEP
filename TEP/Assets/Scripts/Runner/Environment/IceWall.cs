using System.Collections;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    public KeyCode[] keySequence; // Secuencia de teclas para sincronizar pasos
    public float stepTimeLimit = 2f; // Tiempo límite para cada paso
    private int currentStep = 0; // Paso actual en la secuencia

    public void StartStepSequence()
    {
        StartCoroutine(HandleStepSequence());
    }

    private IEnumerator HandleStepSequence()
    {
        while (currentStep < keySequence.Length)
        {
            KeyCode currentKey = keySequence[currentStep];
            float startTime = Time.time;
            bool stepCompleted = false;

            // Muestra la tecla a presionar y espera hasta que se complete el paso o se acabe el tiempo
            Debug.Log("Presiona: " + currentKey);

            while (Time.time < startTime + stepTimeLimit)
            {
                if (Input.GetKeyDown(currentKey))
                {
                    stepCompleted = true;
                    break;
                }
                yield return null;
            }

            if (stepCompleted)
            {
                Debug.Log("Paso " + currentStep + " completado.");
                currentStep++;
            }
            else
            {
                Debug.Log("Fallo en la secuencia. Reiniciando...");
                currentStep = 0; // Reinicia la secuencia si falla
            }
        }

        Debug.Log("¡Secuencia completada!");
    }
}
