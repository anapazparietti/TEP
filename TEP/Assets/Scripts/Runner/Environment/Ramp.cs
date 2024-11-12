using UnityEngine;

public class Ramp : MonoBehaviour
{
    public float boostForwardSpeed = 15f; // Velocidad hacia adelante al tocar la rampa
    public float boostVerticalSpeed = 20f; // Velocidad vertical para alcanzar la altura
    public float boostDuration = 1.5f; // Duración del boost

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerFlight playerFlight = other.GetComponent<PlayerFlight>();
            if (playerFlight != null)
            {
                // Pasa valores numéricos en lugar de 'null' para el tercer parámetro
                playerFlight.StartFlight(boostVerticalSpeed, boostForwardSpeed); 
            }
        }
    }
}
