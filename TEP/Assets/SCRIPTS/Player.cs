using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool isSincroActive = false; // Indica si está en estado Sincro//no hace nada esto

    void Start()
    {
    }

    void Update()
    {
       
    }

    // ---- COLISIONES ----
    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("EntraSincro")) // Activa el estado Sincro al colisionar con el trigger
        {
            EnterSincroState();
        }
        if(other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Ganar");
        }
    }

    // ---- ESTADO SINCRO ----
    public void EnterSincroState()
    {
        isSincroActive = true; // Desactiva el movimiento
        Debug.Log("Entrando en estado Sincro. Movimiento desactivado.");
    }

    public void ExitSincroState()
    {
        isSincroActive = false; // Reactiva el movimiento
        Debug.Log("Saliendo del estado Sincro. Movimiento activado.");
    }
}
