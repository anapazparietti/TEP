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
    private bool syncSuccess = true; // Indicará si completó el Syncing correctamente

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalForwardSpeed = forwardSpeed;
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);

        if (isRunning)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
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

    // Método llamado por SyncManager para definir el resultado del Syncing
    public void SetSyncingResult(bool success)
    {
        isRunning = true;
        syncSuccess = success;

        if (!success)
        {
            ApplySpeedPenalty(); // Aplicar penalización si falló
        }
    }

    // Penalización de velocidad si se falla el Syncing
    public void ApplySpeedPenalty()
    {
        forwardSpeed /= 2f; // Reducir la velocidad a la mitad (ajústalo como desees)
        Debug.Log($"{inputNameHorizontal} falló la secuencia. Penalización aplicada.");
    }

    // Método para detectar colisión con "EntraSincro"
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EntraSincro"))
        {
            isRunning = false; // Detener al jugador
        }
    }

    // Método para restaurar la velocidad original (llamar desde WallManager)
    public void RestoreSpeed()
    {
        forwardSpeed = originalForwardSpeed;
    }

    // Método para verificar si el jugador completó correctamente el Syncing
    public bool HasSyncSuccess()
    {
        return syncSuccess;
    }
}
