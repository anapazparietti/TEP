using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ControlEscenas : MonoBehaviour
{
 public static ControlEscenas Instance;
 private Scene currentScene;
 void Awake()
    {
        Instance = this;
    }
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
    if(currentScene.name == "Inicio" && Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
    {
     Cinematica();
    }
  }
  public void Perder()
  {
    StartCoroutine(nameof(Perdiendo));
  }
  IEnumerator Perdiendo()
  {
    yield return new WaitForSecondsRealtime(2);
    SceneManager.LoadScene("Perder");
  }
  public void Ganar()
  {
    StartCoroutine(nameof(Ganando));
  }
  IEnumerator Ganando()
  {
    yield return new WaitForSecondsRealtime(2);
    SceneManager.LoadScene("Ganar");
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
