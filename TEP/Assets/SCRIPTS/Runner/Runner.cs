using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public static Runner Instance;
    public float moveSpeed = 0; 
    public float limitSpeed = 100;
    public float moveCostados = 9;
    private float lastInputTime = 0f; // Tiempo del último input de W
    private float lastIncreaseTime = 0f; // Tiempo del último aumento de velocidad
    public float increaseInterval = 1f; // Intervalo para aumentar velocidad
    private bool isHoldingW = false; // Indica si la tecla W está siendo mantenida
    public bool sincrOk;
    private float targetSpeed = 0f; // Velocidad objetivo para un movimiento progresivo
    private float deslizar = 1f;

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
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
    }

    void Movimiento()
    {   
            if (Input.GetKeyDown(KeyCode.W))
        {
            StopCoroutine(nameof(Desacelerar));
            lastInputTime = Time.time;

            if (Time.time - lastIncreaseTime > increaseInterval)
            {
                AumentoVelocidad();
                lastIncreaseTime = Time.time; // Actualiza el tiempo del último aumento
            }
            moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, Time.deltaTime * 5); // Suaviza la aceleración (más rápida)
            Invoke(nameof(LlamarDesaceleramiento),3);
        }

        // Movimiento lateral
        if (Input.GetKey(KeyCode.A))
        {
            if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
            {
                Debug.Log("el jugador se mueve a la izquierda");
                transform.Translate(Vector3.left * Time.deltaTime * moveCostados);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
            {
                Debug.Log("el jugador se mueve a la derecha");
                transform.Translate(Vector3.left * Time.deltaTime * moveCostados * -1);
            }
        }
    }

    void VerificarInactividad()
    {
        // Si han pasado más de 1 segundo desde la última pulsación de W
        if (Time.time - lastInputTime > targetSpeed)
        {
           LlamarDesaceleramiento();
        }
    }

void LlamarDesaceleramiento()
{
    Debug.Log("Llamando metodo");
    StartCoroutine(nameof(Desacelerar));
}
    // ---- CAMBIOS DE VELOCIDADES ----
    void AumentoVelocidad()
    {
        if (moveSpeed < limitSpeed)
        {
            Debug.Log("Al jugador se le aumenta la velocidad");
            targetSpeed += 5f; 
        }
    }

    // ---- COLISIONES ----
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("el jugador se choco");
            StartCoroutine(DisminuyeVelocidad(moveSpeed / 2, 1f)); 
        }
        if (other.CompareTag("MuroHielo") && sincrOk == false)
        {
            StartCoroutine(DisminuyeVelocidad(moveSpeed / 3, 2f)); 
        }
    }

IEnumerator Desacelerar()
    {
        Debug.Log("Llamando corutina");
        while(moveSpeed>0)
        {
            moveSpeed -= 0.25f*Time.deltaTime;
            if(moveSpeed<0)
            {
                moveSpeed=0;
            }
        }
        yield return null;
    }

    // CORRUTINA PARA DISMINUIR LA VELOCIDAD
    IEnumerator DisminuyeVelocidad(float nuevaVelocidad, float duracion)
    {
        Debug.Log("La velocidad se reduce temporalmente");
        moveSpeed = nuevaVelocidad;
        yield return new WaitForSeconds(duracion);
        moveSpeed = moveSpeed / 2;
        Debug.Log("Velocidad restaurada");
    }
}
