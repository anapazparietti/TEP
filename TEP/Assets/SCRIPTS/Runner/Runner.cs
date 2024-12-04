using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public static Runner Instance;
    public float moveSpeed = 0; 
    public float limitSpeed = 300;
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
                {
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
                // Incremento progresivo de la velocidad
                targetSpeed = Mathf.Min(limitSpeed, targetSpeed + 10); // Incrementa más rápido
                moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, Time.deltaTime * 4); // Suaviza la aceleración (más rápida)
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
            }
        }
        else
        {
            // Reducción progresiva de velocidad cuando no se presiona W
            transform.Translate(Vector3.forward * Time.deltaTime * deslizar, Space.World);
            targetSpeed = moveSpeed;
            moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, Time.deltaTime * 10); // Suaviza la desaceleración (más lenta)
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
        if (Time.time - lastInputTime > 1f)
        {
            if (moveSpeed > 10)
            {
                moveSpeed = Mathf.Max(8, moveSpeed - 2 * Time.deltaTime); // Reducir más lentamente
            }
        }
    }

    // ---- CAMBIOS DE VELOCIDADES ----
    void AumentoVelocidad()
    {
        if (moveSpeed < limitSpeed)
        {
            Debug.Log("Al jugador se le aumenta la velocidad");
            moveSpeed += 10; 
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
