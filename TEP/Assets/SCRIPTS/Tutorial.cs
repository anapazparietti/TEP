/*
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
public class Tutorial : MonoBehaviour
{
    public GameObject tutorial;
    public Image cuadro;
    public Sprite[] cuadroChange;
    private int numCuadro = 0;
    public SimonSaysManager simonSays;
    public bool TutOn;

    void Start()
    {
       tutorial.SetActive(true);
    }

    void Update()
    {
        StartCoroutine(CambioCuadro());
        TutorialSincro();
        cuadro.sprite = cuadroChange[numCuadro];
        if(numCuadro==3 || numCuadro==4)
        {
            StartCoroutine(SalirTuto());
        }

    }
    IEnumerator SalirTuto()
    {
        yield return new WaitForSeconds(1f);
        TutOn = false;
        tutorial.SetActive(false);
    }
    IEnumerator CambioCuadro()
    {
        for (int i = 0; i < 3; i++) 
        {
            yield return new WaitForSeconds(1f);
            numCuadro = 1;
        }
    }

    public void TutorialSincro()
    {
        if(simonSays.sincrOn == true && simonSays.numfondo == 0) 
        {
            TutOn = true;
            numCuadro=4;
        }
    }

}
*/