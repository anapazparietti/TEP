using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;

    private Runner runnerState;
    private Playerprueba flying;
    public GameObject sincro;
    public int dificultad = 0; //en el inspector está puesta en 1, public para poder cambiarla
    public bool enSincro;


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
        enSincro = false;
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
        if(other.CompareTag("Finish"))
        {
          SceneManager.LoadScene("Ganar");
        }
    }
    public void SwitchToSincro()
    {
        enSincro = true;
        flying.enabled = false;
        runnerState.enabled = false;
        sincro.SetActive(true);
        SimonSaysManager.instance.IniciarSimonDice(dificultad);
    }
}
