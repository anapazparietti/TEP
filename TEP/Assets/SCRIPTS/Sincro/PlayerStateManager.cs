using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour
{
private Runner runnerState;
private SincroState sincroState;
private Playerprueba flying;
private bool currentState = true;

void Start()
{
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
    if(currentState == false)
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
}
public void SwitchToSincro()
{
    flying.enabled = false;
    runnerState.enabled = false;
    sincroState.enabled = true;
    Debug.Log("Estado: Sincro");
}
}
