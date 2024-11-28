using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
  void Update()
  {
    Scene currentScene = SceneManager.GetActiveScene();
    if (currentScene.name != "Ganar" || currentScene.name != "Perder")
      {
        Invoke("Reiniciar", 2);
      }
  }
  public void Cinemaica()
  {
    
  }
  public void EscenaJuego()
  {
   SceneManager.LoadScene("Juego");
  }
  public void Reiniciar()
  {
   SceneManager.LoadScene("Inicio");
  }
}
