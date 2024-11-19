using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GanarPerder : MonoBehaviour

{
    public void OnTriggerEnter(Collider other) {

  if(other.CompareTag("Player")){
SceneManager.LoadScene("GanarP1");
  }else if(other. CompareTag("Player2")){
    SceneManager.LoadScene("GanarP2");
  }
  
}

}
