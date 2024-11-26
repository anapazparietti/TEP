using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
  public void EscenaJuego()
  {
   SceneManager.LoadScene("Juego");
  }
  public void Reiniciar()
  {
   SceneManager.LoadScene("Inicio");
  }
}
