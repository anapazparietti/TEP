using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;  // Velocidad del movimiento controlado
    [SerializeField] private float forwardSpeed;  // Velocidad del movimiento automático hacia adelante
    [SerializeField] private string inputNameHorizontal;

    private Rigidbody rb;
    private float inputHorizontal;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Captura la entrada horizontal del usuario
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
    }

    private void FixedUpdate()
    {
        // Aplica movimiento automático en el eje Z y controlado en el eje X
        rb.linearVelocity = new Vector3(inputHorizontal * speed, rb.linearVelocity.y, forwardSpeed);
    }
}
