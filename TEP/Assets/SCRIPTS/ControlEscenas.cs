using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Inicio : MonoBehaviour
{
 private Scene currentScene;
 public VideoPlayer vid;
  void Update()
  {
    currentScene = SceneManager.GetActiveScene();
    if (currentScene.name == "Ganar" || currentScene.name == "Perder")
      {
        Invoke("Reiniciar", 2);
      }
    if(currentScene.name == "Cinematica")
    {
     Invoke("EscenaJuego",12);
    }
  }
  
  public void Cinematica()
  {
    SceneManager.LoadScene("Cinematica");
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
