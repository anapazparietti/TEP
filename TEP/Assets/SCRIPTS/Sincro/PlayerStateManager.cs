using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public enum PlayerState { Runner, Sincro }
    public PlayerState currentState = PlayerState.Runner;

    public Player runnerScript;
    public SincroState sincroScript;

    void Start()
    {
        SwitchToRunner();
    }

    void Update()
    {
        if (currentState == PlayerState.Runner)
        {
            runnerScript.RunnerUpdate();
        }
        else if (currentState == PlayerState.Sincro)
        {
            sincroScript.SincroUpdate();
        }
    }

    public void SwitchToRunner()
    {
        currentState = PlayerState.Runner;
        runnerScript.enabled = true;
        sincroScript.enabled = false;
        Debug.Log("Estado: Runner");
    }

    public void SwitchToSincro()
    {
        currentState = PlayerState.Sincro;
        runnerScript.enabled = false;
        sincroScript.enabled = true;
        Debug.Log("Estado: Sincro");
    }
}
