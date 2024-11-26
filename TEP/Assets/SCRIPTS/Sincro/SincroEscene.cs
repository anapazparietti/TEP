using UnityEngine;
using UnityEngine.UI;

public class SincroEscene : MonoBehaviour
{
    private PlayerStateManager estadosJugador;
    public GameObject sincro1;
    public GameObject sincro2;
    public GameObject sincro3;
    public GameObject sincro4;  
    void Start()
    {
        estadosJugador = GetComponent<PlayerStateManager>();
        
    }

    void Update()
    {
        if(estadosJugador.currentState == false)
        { Debug.Log("esto pasa");
            sincro1.SetActive(true);
        }
    }
}
