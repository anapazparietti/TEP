using UnityEngine;
using System.Collections;

public class Playerprueba : MonoBehaviour
{
    private Runner movControlado;
    public bool auto = false;
    
    void Start()
    {
        movControlado =GetComponent<Runner>();
    }
   
   void Update()
   {
    if(auto)
    {
        MovAuto();
    }
   }
     // ---- COLISIONES ----
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rampa"))
        {
            auto = true;
        }
        if (other.CompareTag("EntraSincro") || other.CompareTag("vuelveRunner"))
        {
            auto = false;
            movControlado.enabled = true;
        }
    }
     void MovAuto()
    {
        movControlado.enabled = false;
        transform.Translate(Vector3.forward * Time.deltaTime * 10, Space.World);
    }    
}
