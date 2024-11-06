using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;  // Velocidad del movimiento controlado
    [SerializeField] private float forwardSpeed;  // Velocidad del movimiento automático hacia adelante
    [SerializeField] private string inputNameHorizontal;

    private Rigidbody rb;
    private float inputHorizontal;
    private float originalForwardSpeed;  // Almacena la velocidad original

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalForwardSpeed = forwardSpeed;  // Guardamos la velocidad original
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(ReduceSpeedTemporarily());
        }
    }

    private IEnumerator ReduceSpeedTemporarily()
    {
        // Reduce la velocidad hacia adelante
        forwardSpeed = originalForwardSpeed / 2;  // Reduce la velocidad a la mitad (ajustable)

        // Espera 1 segundo
        yield return new WaitForSeconds(1f);

        // Restaura la velocidad original
        forwardSpeed = originalForwardSpeed;
    }
}
