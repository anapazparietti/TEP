using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    [SerializeField] int min, seg;
    [SerializeField] Text tiempo;

    private float restante;
    private bool enMovimiento;
    public SimonSaysManager simonSays;

    private void Awake()
    {
        restante = (min * 60) + seg;
        enMovimiento = true;
    }

    void Update()
    {
        // Verificar si el temporizador está en movimiento
        if (enMovimiento)
        {
            restante -= Time.deltaTime;

            // Si el tiempo restante se agota
            if (restante < 1)
            {
                enMovimiento = false;
                SceneManager.LoadScene("Perder");
            }

            // Actualizar texto del temporizador
            int tempMin = Mathf.FloorToInt(restante / 60);
            int tempSeg = Mathf.FloorToInt(restante % 60);
            tiempo.text = string.Format("{0:00}:{1:00}", tempMin, tempSeg);
        }

        if(PlayerStateManager.Instance.enSincro == true && simonSays.playerTurn == false)
        {
            Debug.Log("Tiempo en pausa");
            PararTiempo();
        } else
        {
           Debug.Log("Vuelve tiempo");
           ReanudarTiempo();
        } 
       
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            AlternarMovimiento();
        }*/
    }
    public void PararTiempo()
    {
      enMovimiento = false;
    } 
    public void ReanudarTiempo()
    {
      enMovimiento = true;
    } 
/*    public void AlternarMovimiento()
    {
        enMovimiento = !enMovimiento;
    }
    */
}
