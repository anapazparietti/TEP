using UnityEngine;

public class SincroTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStateManager>().SwitchToSincro();
        }
    }
}