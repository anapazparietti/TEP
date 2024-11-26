using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    [SerializeField] int min, seg;
    [SerializeField] Text tiempo;
    
    private float restante;

    private bool enMovimiento;

    private void Awake(){

        restante = (min * 60) + seg;
        enMovimiento = true;
    }

    void Update()
    {
      if(enMovimiento)
      {
        restante -= Time.deltaTime;
        if(restante < 1)
        {
            enMovimiento = false;
            SceneManager.LoadScene("Perder");
        }
        int tempMin = Mathf.FloorToInt(restante / 60);
        int tempSeg = Mathf.FloorToInt(restante % 60);
        tiempo.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
      }
    }

}
