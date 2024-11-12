using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Definición de los posibles estados del juego
    public enum GameState
    {
        MainMenu,
        Running,
        Flying,
        Syncing,
        Won,
        Lost
    }

    // Singleton
    public static StateManager Instance { get; private set; }

    // Evento que se lanza cuando el estado cambia
    public event Action<GameState, GameState> OnGameStateChanged;

    // Estado actual del juego
    // private GameState currentState = GameState.MainMenu;
     private GameState currentState = GameState.Running;

    private void Awake()
    {
        // Configurar Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para cambiar el estado del juego y lanzar el evento
    public void ChangeState(GameState newState)
    {
        if (currentState == newState) return;

        GameState previousState = currentState;
        currentState = newState;

        // Invocar el evento con el estado anterior y el nuevo estado
        OnGameStateChanged?.Invoke(previousState, newState);
    }

    // Método público para obtener el estado actual del juego
    public GameState GetCurrentState()
    {
        return currentState;
    }
}
