using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Inicio : MonoBehaviour
{
 private Scene currentScene;
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
    if(currentScene.name == "Inicio" && Input.GetKey(KeyCode.W))
    {
     Cinematica();
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
  {//agregar
   SceneManager.LoadScene("Inicio");
  }
}
