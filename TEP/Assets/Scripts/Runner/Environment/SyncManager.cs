using System.Collections;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    // Secuencias para cada jugador
    public string[] correctSequencePlayer1 = { "A", "S", "D", "W" };
    public string[] correctSequencePlayer2 = { "LeftArrow", "DownArrow", "RightArrow", "UpArrow" };

    private int pasoActualPlayer1 = 0;
    private int pasoActualPlayer2 = 0;

    private string teclaApretadaPlayer1 = "Null";
    private string teclaApretadaPlayer2 = "Null";

    private void Update()
    {
        if (StateManager.Instance.GetCurrentState() == StateManager.GameState.Syncing)
        {
            // Escuchar entradas para Player 1
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kcode))
                    {
                        teclaApretadaPlayer1 = kcode.ToString();
                        recibirTeclaPlayer1(teclaApretadaPlayer1);
                        break;
                    }
                }
            }

            // Escuchar entradas para Player 2
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(kcode))
                    {
                        teclaApretadaPlayer2 = kcode.ToString();
                        recibirTeclaPlayer2(teclaApretadaPlayer2);
                        break;
                    }
                }
            }
        }
    }

    // Método para recibir la tecla presionada y validar el paso actual para Player 1
    public void recibirTeclaPlayer1(string tecla)
    {
        if (pasoActualPlayer1 < correctSequencePlayer1.Length && tecla == correctSequencePlayer1[pasoActualPlayer1])
        {
            Debug.Log($"Player 1 Correcto: {tecla} en paso {pasoActualPlayer1 + 1}");
            pasoActualPlayer1++;

            if (pasoActualPlayer1 >= correctSequencePlayer1.Length)
            {
                Debug.Log("¡Player 1 completó su secuencia!");
                StateManager.Instance.ChangeState(StateManager.GameState.Running);
                pasoActualPlayer1 = 0; // Reiniciar para la próxima vez
            }
        }
        else
        {
            Debug.Log($"Player 1 Tecla incorrecta: {tecla}. Reiniciando secuencia.");
            pasoActualPlayer1 = 0; // Reiniciar si la tecla es incorrecta
        }
    }

    // Método para recibir la tecla presionada y validar el paso actual para Player 2
    public void recibirTeclaPlayer2(string tecla)
    {
        if (pasoActualPlayer2 < correctSequencePlayer2.Length && tecla == correctSequencePlayer2[pasoActualPlayer2])
        {
            Debug.Log($"Player 2 Correcto: {tecla} en paso {pasoActualPlayer2 + 1}");
            pasoActualPlayer2++;

            if (pasoActualPlayer2 >= correctSequencePlayer2.Length)
            {
                Debug.Log("¡Player 2 completó su secuencia!");
                StateManager.Instance.ChangeState(StateManager.GameState.Running);
                pasoActualPlayer2 = 0; // Reiniciar para la próxima vez
            }
        }
        else
        {
            Debug.Log($"Player 2 Tecla incorrecta: {tecla}. Reiniciando secuencia.");
            pasoActualPlayer2 = 0; // Reiniciar si la tecla es incorrecta
        }
    }
}
