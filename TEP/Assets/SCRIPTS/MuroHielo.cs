using UnityEngine;

public class MuroHielo : MonoBehaviour
{
    public GameObject failEfect;
    public Runner runner;

  private void OnTriggerEnter(Collider other) 
  {
    if(other.CompareTag("Player") && runner.sincrOk == false)
    {
     Instantiate(failEfect, transform.position, transform.rotation); 
     Destroy(gameObject);
    }
  }
}
