using UnityEngine;
using Assets.Scripts.Sincro;

public class Player2 : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float forwardSpeed;
    [SerializeField] private string inputNameHorizontal;

    private Rigidbody rb;
    private float inputHorizontal;
    private float originalForwardSpeed;
    public bool isRunning = true;

    private SyncManager syncManager;
    private Player2SyncController playerSyncController;

      // Estado local del jugador
    private PlayerState playerState = PlayerState.Running;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalForwardSpeed = forwardSpeed;

        // Inicializar SyncManager y PlayerSyncController
        syncManager = GetComponent<SyncManager>();
        playerSyncController = GetComponent<Player2SyncController>();
    }

    private void Update()
    {
        if (isRunning)
        {
            inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);
            transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed, Space.World);
        }

        // Mensaje para ver la velocidad del jugador en la consola
        Debug.Log($"{name} velocidad: {forwardSpeed}");
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            rb.linearVelocity = new Vector3(inputHorizontal * speed, rb.linearVelocity.y, forwardSpeed);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    // Método para manejar la entrada en el modo sincro al tocar un trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EntraSincro"))
        {
            // Cambiar al estado Syncing solo si el juego está en estado Running
            if (playerState == PlayerState.Running)
            {
                isRunning = false;
                playerState = PlayerState.Syncing;

                // Usa el PlayerSyncController para entrar en el modo sincro
                if (playerSyncController != null)
                {
                    playerSyncController.EnterSyncMode();
                }
            }
        }
    }

    // Método para definir el resultado del Syncing
    public void SetSyncingResult(bool success)
    {
        if (!success)
        {
            ApplySpeedPenalty();
        }
        else
        {
            RestoreSpeed();
        }

        // Regresar al estado Running
        isRunning = true;
        playerState = PlayerState.Running;
    }

    public void ApplySpeedPenalty()
    {
        playerState = PlayerState.Penalized;
        forwardSpeed /= 2f;
        Debug.Log($"{name} penalizado a velocidad: {forwardSpeed}");
    }

    private void RestoreSpeed()
    {
        forwardSpeed = originalForwardSpeed;
        Debug.Log($"{name} velocidad restaurada: {forwardSpeed}");
    }
}
