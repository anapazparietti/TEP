using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;

    private Runner runnerState;
    private Playerprueba flying;
    
    public GameObject sincro;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
         runnerState = GetComponent<Runner>();
         flying = GetComponent<Playerprueba>();
         SwitchToRunner();
    }

    public void SwitchToRunner()
    { 
        flying.enabled = true;
        sincro.SetActive(false);
        runnerState.enabled = true;
        Debug.Log("Estado: Runner");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EntraSincro"))
        {
            SwitchToSincro();
            Destroy(other.gameObject);
        }
    }
    public void SwitchToSincro()
    {
        flying.enabled = false;
        runnerState.enabled = false;
        sincro.SetActive(true);
        SimonSaysManager.instance.IniciarSimonDice(Random.Range(1, 5));
    }
}
