using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimonSaysManager : MonoBehaviour
{
    public List<Button> colorButtons; // Lista de botones de colores
    public float flashDuration = 0.5f; // Duraci�n del destello
    public float delayBetweenFlashes = 0.5f; // Retraso entre flashes

    public TextMeshProUGUI hudTXT;

    private List<int> simonSequence = new List<int>(); // Secuencia generada por Simon
    public string[] syncSequence = { "W", "A", "S", "D" }; // Secuencia de teclas.
    private int playerIndex = 0; // �ndice actual del jugador
    private bool playerTurn = false; // Si es el turno del jugador

    void Start()
    {
        StartCoroutine(StartGame());
        hudTXT.text = "CPU turn!";
    }

    IEnumerator StartGame()
    {
        // Reinicia el juego
        simonSequence.Clear();
        playerTurn = false;        
        yield return new WaitForSeconds(1f);
        AddToSequence();
    }

    void AddToSequence()
    {
        // A�ade un color aleatorio a la secuencia
        simonSequence.Add(Random.Range(0, colorButtons.Count));
        StartCoroutine(PlaySimonSequence());
        hudTXT.text = "CPU turn!";
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
        hudTXT.text = "Player turn!";
    }

    void FlashButton(Button button)
    {
        StartCoroutine(FlashCoroutine(button));
    }

    IEnumerator FlashCoroutine(Button button)
    {
        Color originalColor = button.image.color;
        button.image.color = Color.black; // Cambia el color a blanco (destello)
        yield return new WaitForSeconds(flashDuration);
        button.image.color = originalColor; // Restaura el color original
    }

    public void OnButtonPress(int buttonIndex)
    {
        if (!playerTurn) return;

        if (buttonIndex == simonSequence[playerIndex])
        {
            playerIndex++;
            if (playerIndex >= simonSequence.Count)
            {
                playerTurn = false;
                Invoke("AddToSequence", 1f); // A�ade un nuevo color tras un peque�o retraso
            }
        }
        else
        {
            hudTXT.text = "Wrong :( restarting game";
            StartCoroutine(StartGame());
        }
    }
}
