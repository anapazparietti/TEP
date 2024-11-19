using System.Collections;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    public KeyCode[] keySequence; // Secuencia de teclas para sincronizar pasos
    public float stepTimeLimit = 2f; // Tiempo límite para cada paso
    private int currentStep = 0; // Paso actual en la secuencia
    public GameObject onDestroyEffect; //efecto al pasar el muro de hielo
  

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
               
            }
        }

        Debug.Log("¡Secuencia completada!");
        GetComponent<Collider>().isTrigger = true; // Activa Is Trigger para que el jugador pase
    }

private void OnTriggerEnter(Collider other) {

  if(other.CompareTag("Player")){
    //cuando el jugador entre en esta Trigger zone, se va a destruir el collectible
  Destroy(gameObject);
//para que se produzca el efecto no solo hay que agregar el archivo, sino que además hay que Instanciarlo:
Instantiate(onDestroyEffect, transform.position, transform.rotation);
  }
  
}

}
