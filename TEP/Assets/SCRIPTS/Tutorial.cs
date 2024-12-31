using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Tutorial : MonoBehaviour
{
    public Image cuadro;
    public Sprite[] cuadroChange;
    public float segundos = 1;
    public GameObject tutorial;
    public Sprite cuadroSincro;
    private int currentIndex = 0; 
    private String Estado;
    void Start()
    {
        StartCoroutine(nameof(IniciarTutorial));
    }
    void Update()
    {
        // Detectar la tecla para avanzar al siguiente sprite
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) 
        {
            if(Estado == "TutoRunner")
            {
                IncrementarIndice();
            }
            if(Estado == "TutoSincro")
            {
                SalirTuto();
            }
        }
        Debug.Log(currentIndex);
    }
    void IncrementarIndice()
    {
        if (currentIndex < 3)
        {
            currentIndex++;
            cuadro.sprite = cuadroChange[currentIndex];
        }
        else if(currentIndex == 3)
        {
            SalirTuto();
        }
    }
    void SalirTuto()
    {
        Debug.Log("Último sprite alcanzado");
        Time.timeScale = 1;
        tutorial.SetActive(false);
    } 
    IEnumerator IniciarTutorial()
    {
        Estado = "TutoRunner";
        tutorial.SetActive(true);
        Time.timeScale = 0;
        for (int i = 0; i < 4; i++)
        {
            cuadro.sprite = cuadroChange[i];
            currentIndex = i; // Sincroniza el índice
            yield return new WaitForSecondsRealtime(segundos);
        }
        Time.timeScale = 1;
        tutorial.SetActive(false);
    }

    IEnumerator TutorialSincro()
    {
        Estado = "TutoSincro";
        cuadro.sprite = cuadroSincro;
        tutorial.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(segundos);
        Time.timeScale = 1;
        tutorial.SetActive(false);
    }

    public void IniciarTutorialSincro()
    {
        StartCoroutine(TutorialSincro());
    }
}
