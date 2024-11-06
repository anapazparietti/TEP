using System.Collections;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    public KeyCode[] keySequence; // Secuencia de teclas para sincronizar pasos
    public float stepTimeLimit = 2f; // Tiempo límite para cada paso
    private int currentStep = 0; // Paso actual en la secuencia
    private PlayerFlight playerFlight; // Referencia al PlayerFlight para comunicación

    // Método para inicializar la secuencia, recibe el script PlayerFlight
    public void StartStepSequence(PlayerFlight player)
    {
        playerFlight = player;
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
                Debug.Log("Fallo en la secuencia. Aplicando penalización.");
                playerFlight.FailSequence(); // Llama a la penalización en PlayerFlight
                yield break;
            }
        }

        Debug.Log("¡Secuencia completada!");
        GetComponent<Collider>().isTrigger = true; // Activa Is Trigger para que el jugador pase
        playerFlight.CompleteSequence(); // Llama a completar el vuelo en PlayerFlight
    }
}
