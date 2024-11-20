using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private string inputNameHorizontal;

    private Rigidbody rb;
    private float inputHorizontal;
    private float originalForwardSpeed;
    public bool isRunning = true;

    private SyncManager syncManager;
    private PlayerSyncController playerSyncController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalForwardSpeed = forwardSpeed;

        // Inicializar SyncManager y PlayerSyncController
        syncManager = GetComponent<SyncManager>();
        playerSyncController = GetComponent<PlayerSyncController>();
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
            if (StateManager.Instance.GetCurrentState() == StateManager.GameState.Running)
            {
                isRunning = false;
                StateManager.Instance.ChangeState(StateManager.GameState.Syncing);

                // Usa el PlayerSyncController para entrar en el modo sincro
                if (playerSyncController != null)
                {
                    playerSyncController.EnterSyncMode();
                }
            }
        }
     if (other.CompareTag("Obstacle"))
     {
ApplySpeedPenalty();
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
        StateManager.Instance.ChangeState(StateManager.GameState.Running);
    }

    public void ApplySpeedPenalty()
    {
        forwardSpeed /= 2f;
        Debug.Log($"{name} penalizado a velocidad: {forwardSpeed}");
    }

    private void RestoreSpeed()
    {
        forwardSpeed = originalForwardSpeed;
        Debug.Log($"{name} velocidad restaurada: {forwardSpeed}");
    }
}
