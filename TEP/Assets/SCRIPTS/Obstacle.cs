using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
  public GameObject onDestroyEffect;

  private void OnTriggerEnter(Collider other) {
    if(other.CompareTag("Player")){
      Destroy(gameObject);
      Instantiate(onDestroyEffect, transform.position, transform.rotation);
      Debug.Log("el jugador choco con un objeto");
  }
}

}

