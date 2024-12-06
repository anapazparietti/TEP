using System;
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
    public float aumentarVelocidad = 1f;

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
         if (Input.GetKeyDown(KeyCode.W))
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
        targetSpeed += aumentarVelocidad; // Aumenta la velocidad objetivo de forma gradual
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
            DisminuyeVelocidad(); 
        }
        if (other.CompareTag("MuroHielo") && sincrOk == false)
        {
            DisminuyeVelocidad(); 
        }
    }

IEnumerator Desacelerar()
    {
        Debug.Log("Llamando corutina");
        while(targetSpeed>0)
        {
            targetSpeed -= numDesacelerar*Time.deltaTime;
            if(targetSpeed<0)
            {
               targetSpeed=0;
            }
        }
        yield return null;
    }

    // CORRUTINA PARA DISMINUIR LA VELOCIDAD
    void DisminuyeVelocidad()
    {
        Debug.Log("La velocidad se reduce");
        targetSpeed = targetSpeed/2;
        moveSpeed = targetSpeed;
    }
}
