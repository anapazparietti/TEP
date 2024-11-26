using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerStateManager : MonoBehaviour
{
private Runner runnerState;
private SincroState sincroState;
private Playerprueba flying;
public bool currentState;

void Start()
{
 currentState=true;
 runnerState = GetComponent<Runner>();
 sincroState = GetComponent<SincroState>();
 flying = GetComponent<Playerprueba>();
 SwitchToRunner();
}

void Update()
{
    if(currentState)
    {
        SwitchToRunner();
    }
    else
    {
        SwitchToSincro();
    }
}

public void SwitchToRunner()
{ 
    runnerState.enabled = true;
    sincroState.enabled = false;
    Debug.Log("Estado: Runner");

}
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("EntraSincro"))
    {
        currentState = false;
    }
    if(other.CompareTag("Finish"))
    {
            SceneManager.LoadScene("Ganar");
    }
}
public void SwitchToSincro()
{
    flying.enabled = false;
    runnerState.enabled = false;
    sincroState.enabled = true;
    Debug.Log("Estado: Sincro");
}
}
