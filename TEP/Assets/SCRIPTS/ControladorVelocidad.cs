using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContralorVelocidad : MonoBehaviour
{

  private void OnTriggerEnter(Collider other) {
    if(other.CompareTag("Player")){
      Destroy(gameObject);
      Debug.Log("el jugador choco con un objeto");
  }
}

}
