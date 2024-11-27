using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public float moveSpeed = 10; 
    public float limitSpeed = 100;
    public float moveCostados = 9;
    private float lastInputTime = 0f; // Tiempo del último input de W
    private float lastIncreaseTime = 0f; // Tiempo del último aumento de velocidad
    public float increaseInterval = 1f; // Intervalo para aumentar velocidad
    private bool isHoldingW = false; // Indica si la tecla W está siendo mantenida
    public bool sincrOk;

    private Rigidbody jugador;
    void Start()
    {
        jugador = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
       RunnerUpdate();
    }

   public void RunnerUpdate()
    {
         Debug.Log("La velocidad actual es de " + moveSpeed);
        Movimiento();
        VerificarInactividad(); // Verifica si ha pasado tiempo sin pulsar W
    }

    void Movimiento()
    {   
        if (Input.GetKey(KeyCode.W))
        {
            lastInputTime = Time.time; // Registra el momento de la última pulsación

            if (Input.GetKeyDown(KeyCode.W)) 
            {
                isHoldingW = false; // No está en modo "mantener"
                if (Time.time - lastIncreaseTime > increaseInterval)
                {//Input.
                    AumentoVelocidad();
                    lastIncreaseTime = Time.time; // Actualiza el tiempo del último aumento
                }
            }
            else
            {
                isHoldingW = true;
            }

            // Movimiento hacia adelante si la tecla no se mantiene presionada
            if (!isHoldingW)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            }
        }

         //izquierda
       if(Input.GetKey(KeyCode.A))
       {
        //----para delimitar que el jugador no pase los límites de la pista
        if(this.gameObject.transform.position.x > LevelBoundary.leftSide){
            Debug.Log("el jugador se mueve a la izquierda");
            transform.Translate(Vector3.left * Time.deltaTime * moveCostados);
        }
       }
       if(Input.GetKey(KeyCode.D))
       { 
        if(this.gameObject.transform.position.x < LevelBoundary.rightSide)
        {
            Debug.Log("el jugador se mueve a la derecha");
            transform.Translate(Vector3.left * Time.deltaTime * moveCostados * -1);
        }
       }
    }

    void VerificarInactividad()
    {
        // Si han pasado más de 2 segundos desde la última pulsación de W
        if (Time.time - lastInputTime > 1f)
        {
            if (moveSpeed > 10)
            {
                moveSpeed = Mathf.Max(8, moveSpeed - 4 * Time.deltaTime); // Reducir gradualmente
            }
        }
    }

    // ---- CAMBIOS DE VELOCIDADES ----
    void AumentoVelocidad()
    {
        if (moveSpeed < limitSpeed)
        {
            Debug.Log("Al jugador se le aumenta la velocidad");
            moveSpeed += 20; 
        }
    }

    // ---- COLISIONES ----
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !sincrOk)
        {
            StartCoroutine(DisminuyeVelocidad(moveSpeed/2, 1f)); // Reduce velocidad a 5 por 2 segundos
        }
        else if (other.CompareTag("Obstacle") && sincrOk)
        {
            AumentoVelocidad();
        }
    }

    // CORRUTINA PARA DISMINUIR LA VELOCIDAD
    IEnumerator DisminuyeVelocidad(float nuevaVelocidad, float duracion)
    {
        Debug.Log("La velocidad se reduce temporalmente");
        moveSpeed = nuevaVelocidad;
        yield return new WaitForSeconds(duracion);
        moveSpeed = moveSpeed/2;
        Debug.Log("Velocidad restaurada");
    }
}