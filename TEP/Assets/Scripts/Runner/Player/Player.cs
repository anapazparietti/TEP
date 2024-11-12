using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;  // Velocidad del movimiento controlado (en horizontal)
    [SerializeField] private float forwardSpeed;  // Velocidad del movimiento automático hacia adelante
    [SerializeField] private string inputNameHorizontal;

    private Rigidbody rb;
    private float inputHorizontal;
    private float originalForwardSpeed;  // Almacena la velocidad original
    private Renderer playerRenderer;  // Para ocultar el jugador
    private Collider playerCollider;  // Para desactivar colisiones

 private void HandleGameStateChanged(StateManager.GameState oldState, StateManager.GameState newState)
    {
        if (newState == StateManager.GameState.Syncing)
        {
            // Detener al jugador cuando entra en estado Syncing
            rb.linearVelocity = Vector3.zero;
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        playerCollider = GetComponent<Collider>();
        originalForwardSpeed = forwardSpeed;  // Guardamos la velocidad original
         StateManager.Instance.OnGameStateChanged += HandleGameStateChanged;

    }

    private void Update()
    {
        // Captura la entrada horizontal del usuario
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);
       // Solo permitir movimiento si el estado es Running
        if (StateManager.Instance.GetCurrentState() == StateManager.GameState.Running)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
    }

    private void FixedUpdate()
    {
        // Aplica movimiento automático en el eje Z solo si está en Running
        if (StateManager.Instance.GetCurrentState() == StateManager.GameState.Running)
        {
            rb.linearVelocity = new Vector3(inputHorizontal * speed, rb.linearVelocity.y, forwardSpeed);
        }
        else
        {
            rb.linearVelocity = Vector3.zero; // Detener al jugador en otros estados
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(ReduceSpeedTemporarily());
        
        } else if (other.CompareTag("EntraSincro"))
        {
            // Cambiar el estado a Syncing
            StateManager.Instance.ChangeState(StateManager.GameState.Syncing);
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

    // private IEnumerator DisappearTemporarily()
    // {
    //     // Detiene al jugador y lo hace desaparecer
    //     forwardSpeed = 0;
    //     speed = 0;

    //     playerRenderer.enabled = false;  // Oculta el modelo

    //     // Espera 2 segundos (ajustable) antes de reaparecer
    //     yield return new WaitForSeconds(2f);

    //     // Restaura el movimiento y reaparece el jugador
    //     forwardSpeed = originalForwardSpeed;
    //     speed = originalForwardSpeed / 2; // Puedes ajustar esta velocidad si es necesario

    //     playerRenderer.enabled = true;  // Muestra el modelo
    // }
    private void OnDestroy()
    {
        // Desuscribirse del evento al destruir el objeto
        if (StateManager.Instance != null)
            StateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }
}
