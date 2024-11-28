using UnityEngine;

public class MuroHielo : MonoBehaviour
{
    public GameObject succesEfect;
    public GameObject failEfect;

  private void OnTriggerEnter(Collider other) {
    if(other.CompareTag("Player")){
    Debug.Log("el jugador pasa un muro");
    Instantiate(succesEfect, transform.position, transform.rotation);
    Destroy(gameObject);
  }
}
}
