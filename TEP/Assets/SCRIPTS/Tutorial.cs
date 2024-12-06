using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
public class Tutorial : MonoBehaviour
{
    public Image cuadro;
    public Sprite[] cuadroChange;
    public float segundos=1;
    public GameObject tutorialMov;
    public GameObject tutoSincro;

    void Start()
    {
       StartCoroutine(nameof(IniciarTutorial));
    }

   IEnumerator IniciarTutorial()
   {
    tutorialMov.SetActive(true);
    Time.timeScale = 0;
    for(int i=0; i<cuadroChange.Length; i++)
    {
        yield return new WaitForSecondsRealtime(segundos);
        cuadro.sprite= cuadroChange[i];
    }
    Time.timeScale = 1;

   tutorialMov.SetActive(false);
   }
 
IEnumerator TutorialSincro()
{
 Time.timeScale = 0;
 tutoSincro.SetActive(true);
 yield return new WaitForSecondsRealtime(segundos);
 Time.timeScale = 1;
 tutoSincro.SetActive(false);
}

public void IniciarTutorialSincro()
{
    StartCoroutine(TutorialSincro());
}
}
