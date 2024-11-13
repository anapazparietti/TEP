using UnityEngine;

public class WallManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            // Verificar si el jugador completó correctamente el Syncing
            if (!player.HasSyncSuccess())
            {
                player.ApplySpeedPenalty(); // Aplicar penalización si falló
            }
            else
            {
                player.RestoreSpeed(); // Restaurar la velocidad si tuvo éxito
            }
        }
    }
}
