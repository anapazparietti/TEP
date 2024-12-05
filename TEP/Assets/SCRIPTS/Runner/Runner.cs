using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public static Runner Instance;
    public float numDesacelerar=0.3f;
    public float moveSpeed = 0; 
    public float limitSpeed = 100;
    public float moveCostados = 9;
    private float lastInputTime = 0f; // Tiempo del último input de W
    private float lastIncreaseTime = 0f; // Tiempo del último aumento de velocidad
    public float increaseInterval = 1f; // Intervalo para aumentar velocidad
    //private bool isHoldingW = false; // Indica si la tecla W está siendo mantenida
    public bool sincrOk;
    private float targetSpeed = 0f; // Velocidad objetivo para un movimiento progresivo
    //private float deslizar = 1f;

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
        VerificarInactividad();
        moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, Time.deltaTime * 2); // Suaviza la transición
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
    }

    void Movimiento()
    {   
         if (Input.GetKey(KeyCode.W))
        {
            lastInputTime = Time.time; // Actualiza el tiempo del último input
            StopCoroutine(nameof(Desacelerar)); // Detén la desaceleración, si está activa
            if (Time.time - lastIncreaseTime > increaseInterval)
            {
            AumentoVelocidad();
            lastIncreaseTime = Time.time;
            }
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
    if (Time.time - lastInputTime > 3f) // Verifica si pasan 3 segundos sin input
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
        targetSpeed += 1f; // Aumenta la velocidad objetivo de forma gradual
        if (targetSpeed > limitSpeed) targetSpeed = limitSpeed; // Limita la velocidad máxima
        Debug.Log("Aumentando velocidad objetivo: " + targetSpeed);
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
            moveSpeed -= numDesacelerar*Time.deltaTime;
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
