using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SimonSaysManager : MonoBehaviour
{
    public static SimonSaysManager instance;
    public List<Image> colorButtons; // Lista de botones de colores
    public float flashDuration = 0.5f; // Duraci�n del destello
    public float delayBetweenFlashes = 0.5f; // Retraso entre flashes
    private int secuenciaAmount;

    //public TextMeshProUGUI hudTXT; //Texto para la interfaz y el feedback

    private List<int> simonSequence = new List<int>(); // Secuencia generada por Simon
    private int playerIndex = 0; // �ndice actual del jugador
    private bool playerTurn = false; // Si es el turno del jugador
    public Playerprueba playerprueba;
    public Runner runner;

    public Image pose;
    public Sprite[] posetochange;


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
        pose.sprite = posetochange[Random.Range(0,posetochange.Length)];
        secuenciaAmount = secuenciaAmount2;
        StartCoroutine(StartGame());
       // hudTXT.text = "CPU turn!";
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
       // hudTXT.text = "CPU turn!";
    }

    IEnumerator PlaySimonSequence()
    {
        playerTurn = false;
        yield return new WaitForSeconds(1f);

        foreach (int index in simonSequence)
        {
            FlashButton(colorButtons[index]);
            yield return new WaitForSeconds(flashDuration + delayBetweenFlashes);
        }

        playerIndex = 0;
        playerTurn = true;
       // hudTXT.text = "Player turn!";
    }

    void FlashButton(Image image)
    {
        StartCoroutine(FlashCoroutine(image));
    }

    IEnumerator FlashCoroutine(Image image)
    {
        Color originalColor = image.color;
        image.color = Color.black; // Cambia el color a negro 
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
                playerprueba.auto = true;
                runner.sincrOk = true;
                PlayerStateManager.Instance.SwitchToRunner();
                playerTurn = false;
            }
        }
        else
        {
            playerprueba.auto = true;
            runner.sincrOk = false;
            PlayerStateManager.Instance.SwitchToRunner();
            playerTurn = false;
           // hudTXT.text = "Wrong :( restarting game";
        }
    }
}
