using UnityEngine;
using System.Collections;

public class Playerprueba : MonoBehaviour
{
    private Runner movControlado;
    private bool auto = false;
    
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
        if (other.CompareTag("VelocidadNecesaria"))
        {
            auto = true;
        }
        if (other.CompareTag("EntraSincro"))
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
