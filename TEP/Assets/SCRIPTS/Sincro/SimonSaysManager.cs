using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SimonSaysManager : MonoBehaviour
{
    public static SimonSaysManager instance;
    public Playerprueba playerprueba;
    public Runner runner;
    public PlayerStateManager playerStateManager;
    [Header("Flechas")]
    public List<GameObject> colorButtons; // Lista de botones de colores
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
    public int numPose = 0;
    public Tutorial tutorial;
    private bool tutoHecho;
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //se debe bloquear la tecla w en el sincro
        if(Input.GetKeyDown(KeyCode.A))            
        {
            OnButtonPress(0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
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
        if(!tutoHecho)
        {
        tutorial.IniciarTutorialSincro();
        tutoHecho = true;
        }

        escenaSincro.SetActive(true);
        fondo.sprite = fondotochange[numPose];
        pose.sprite = posetochange[numPose];
        poseRota.sprite = poseRotarray[numPose];
        secuenciaAmount = secuenciaAmount2;
        StartCoroutine(StartGame());
        if(numPose<4)
        {
          numPose ++;
        }
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
        dinoCaja.SetActive(true);
        texto.SetActive(true);
        hudTXT.text = "Espera...";
        StartCoroutine(PlaySimonSequence());
    }

    IEnumerator PlaySimonSequence()
    {
        playerIndex = 0;
        playerTurn = false;
        if(playerStateManager.dificultad<=3)
        {
            yield return new WaitForSeconds(0.5f);
        }else
        {
          yield return new WaitForSeconds(0.1f);
        }

        foreach (int index in simonSequence)
        {
            PlayPressedAnimCPU(colorButtons[index]);
            yield return new WaitForSeconds(flashDuration + delayBetweenFlashes);
        }

       
        playerTurn = true;
        dinoCaja.SetActive(true);
        texto.SetActive(true);
        hudTXT.text = "¡Tu turno!";
    }

    void PlayPressedAnim(GameObject obj)
    {
       obj.GetComponent<Animator>().Play("pressed");
    }

    void PlayPressedAnimCPU(GameObject obj)
    {
       obj.GetComponent<Animator>().Play("pressedCPU");
    }

   public void OnButtonPress(int buttonIndex)
{
    if (!playerTurn) return;

    // Ejecutar animación del botón presionado
    PlayPressedAnim(colorButtons[buttonIndex]);
if(playerIndex<simonSequence.Count)
{
    // Comparar el botón presionado con la secuencia
    if (buttonIndex == simonSequence[playerIndex]) 
    {
      playerIndex++;
        // Verificar si el jugador ha completado toda la secuencia
        if (playerIndex == simonSequence.Count)
        {
            Debug.Log("Secuencia completada correctamente");
            runner.sincrOk = true;
            hudTXT.text = "¡Bien!";
            if (playerStateManager.dificultad == 5)
        {
            ControlEscenas.Instance.Ganar();
        }
            Invoke("SalirSincro", 2);
            return;   
        }
    }
    else
    {
        // El jugador falló en la secuencia
        Debug.LogWarning("Secuencia incorrecta");
        dinoCaja.SetActive(false);
        texto.SetActive(false);
        muroSano.SetActive(false);
        muroRoto.SetActive(true);
        runner.sincrOk = false;

        if (playerStateManager.dificultad == 5)
        {
            ControlEscenas.Instance.Perder();
        }
        Invoke("SalirSincro", 2);
        return;   
    }
}
    
}

    void SalirSincro()
    {
        playerIndex=0;
        escenaSincro.SetActive(false);
        playerTurn = false;
        dinoCaja.SetActive(false);
        texto.SetActive(false);
        playerprueba.auto = true;
        muroSano.SetActive(true);
        muroRoto.SetActive(false);
        playerStateManager.SwitchToRunner();
        if(playerStateManager.dificultad<3)
        {
         playerStateManager.dificultad+=2;
        }else
        {
         playerStateManager.dificultad+=1;
        }
    }
}
