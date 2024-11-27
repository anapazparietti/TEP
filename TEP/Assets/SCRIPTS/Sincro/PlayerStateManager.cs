using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;

    private Runner runnerState;
    private SincroState sincroState;
    private Playerprueba flying;
    private bool currentState = true;
    
    public GameObject sincro;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
         runnerState = GetComponent<Runner>();
         sincroState = GetComponent<SincroState>();
         flying = GetComponent<Playerprueba>();
         SwitchToRunner();
    }

    void Update()
    {
    //     if(currentState)
    //     {
    //         SwitchToRunner();
    //     }
    //     if(currentState == false)
    //     {
    //         SwitchToSincro();
    //     }
    }

    public void SwitchToRunner()
    { 
        flying.enabled = true;
        sincro.SetActive(false);
        runnerState.enabled = true;
        sincroState.enabled = false;
        Debug.Log("Estado: Runner");
        currentState = true;
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
        sincroState.enabled = true;
        sincro.SetActive(true);
        SimonSaysManager.instance.IniciarSimonDice(Random.Range(1, 5));
    }
}
