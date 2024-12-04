using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class SimonSaysManager : MonoBehaviour
{
    public static SimonSaysManager instance;

    [Header("Flechas")]
    public List<Image> colorButtons; // Lista de botones de colores
    public float flashDuration = 0.5f; // Duraci�n del destello
    public float delayBetweenFlashes = 0.5f; // Retraso entre flashes
    private int secuenciaAmount;
    public GameObject escenaSincro;
    public GameObject dinoCaja;
    public GameObject texto;
    public TextMeshProUGUI hudTXT; //Texto para la interfaz y el feedback
    private List<int> simonSequence = new List<int>(); // Secuencia generada por Simon
    private int playerIndex = 0; // �ndice actual del jugador
    public bool playerTurn = false; 
    public Playerprueba playerprueba;
    public Runner runner;
    [Header("Muros Sanos")]
    public GameObject muroSano;
    public Image pose;
    public Sprite[] posetochange;
    [Header("Muros Rotos")]
    public GameObject muroRoto;
    public Image poseRota;
    public Sprite[] poseRotarray;
    [Header("Fondo")]
    public Image fondo;
    public Sprite[] fondotochange;
    [Header("Huellas")]
    private int numfondo = 0;    
    private int numPose = 0;


    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))            
        {
            OnButtonPress(0);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            OnButtonPress(1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OnButtonPress(2);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnButtonPress(3);
        }

    }

    public void IniciarSimonDice(int secuenciaAmount2)
    {
        escenaSincro.SetActive(true);
        fondo.sprite = fondotochange[numfondo];
        pose.sprite = posetochange[numPose];
        poseRota.sprite = poseRotarray[numPose];
        secuenciaAmount = secuenciaAmount2;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        // Reinicia el juego
        simonSequence.Clear();
        playerTurn = false;        
        yield return new WaitForSeconds(1f);
        StartSequence();
    }

    void StartSequence()
    {
        // A�ade un color aleatorio a la secuencia
        for (int i = 0; i < secuenciaAmount; i++)
        {
            simonSequence.Add(Random.Range(0, colorButtons.Count));
        }
        StartCoroutine(PlaySimonSequence());
    }

    IEnumerator PlaySimonSequence()
    {
        playerTurn = false;
        if(PlayerStateManager.Instance.dificultad<=3)
        {
            yield return new WaitForSeconds(0.5f);
        }else
        {
          yield return new WaitForSeconds(0.5f);
        }

        foreach (int index in simonSequence)
        {
            FlashButton(colorButtons[index]);
            yield return new WaitForSeconds(flashDuration + delayBetweenFlashes);
        }

        playerIndex = 0;
        playerTurn = true;
        dinoCaja.SetActive(true);
        texto.SetActive(true);
        hudTXT.text = "¡Tu turno!";
    }

    void FlashButton(Image image)
    {
        StartCoroutine(FlashCoroutine(image));
    }

    IEnumerator FlashCoroutine(Image image)
    {
        Color originalColor = image.color;
        image.color = Color.cyan; // Cambia el color a negro 
        yield return new WaitForSeconds(flashDuration);
        image.color = originalColor; // Restaura el color original
    }

    public void OnButtonPress(int buttonIndex)
    {
        if (!playerTurn) return;
        StartCoroutine(FlashCoroutine(colorButtons[buttonIndex]));

        if (buttonIndex == simonSequence[playerIndex])
        {
            playerIndex++;
            if (playerIndex >= simonSequence.Count)
            {  
                runner.sincrOk = true;
                hudTXT.text = "Bien hecho";
               Invoke("SalirSincro", 2);

            }
        }
        else
        {
            dinoCaja.SetActive(false);
            texto.SetActive(false);
            muroSano.SetActive(false);
            muroRoto.SetActive(true);
            runner.sincrOk = false;
          if( PlayerStateManager.Instance.dificultad==5)
            {
                SceneManager.LoadScene("Perder");
            }
            Invoke("SalirSincro", 2);
        }
    }
    void SalirSincro()
    {
        escenaSincro.SetActive(false);
        playerTurn = false;
        if(numfondo<4)
        {
          numfondo ++;
        }
        if(numPose<3)
        {
          numPose ++;
        }
        dinoCaja.SetActive(false);
        texto.SetActive(false);
        playerprueba.auto = true;
        muroSano.SetActive(true);
        muroRoto.SetActive(false);
        PlayerStateManager.Instance.SwitchToRunner();
        if(PlayerStateManager.Instance.dificultad<3)
        {
         PlayerStateManager.Instance.dificultad+=2;
        }else
        {
         PlayerStateManager.Instance.dificultad+=1;
        }
    }
}
