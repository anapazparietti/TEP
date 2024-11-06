using UnityEngine;

public class FlightTrigger : MonoBehaviour
{
    public float skyHeight = 20f; // Altura a la que el jugador se eleva
    public float flightDuration = 5f; // Duración del vuelo en segundos
    public GameObject iceWallPrefab; // Prefab del muro de hielo

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activa el vuelo en el jugador
            PlayerFlight playerFlight = other.GetComponent<PlayerFlight>();
            if (playerFlight != null)
            {
                playerFlight.StartFlight(skyHeight, flightDuration, iceWallPrefab);
            }
        }
    }
}
