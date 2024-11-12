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
    public event Action GameStateChanged;

    // Estado actual del juego
    private GameState currentState;

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

        currentState = newState;
        OnGameStateChanged();
    }

    // Invocar el evento de cambio de estado
    protected virtual void OnGameStateChanged()
    {
        GameStateChanged?.Invoke(); // Invocar el evento sin parámetros adicionales
    }

    // Método público para obtener el estado actual del juego
    public GameState GetCurrentState()
    {
        return currentState;
    }
}
