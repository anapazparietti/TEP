using UnityEngine;
using System.Collections;

public class Playerprueba : MonoBehaviour
{
    private Player movControlado;
    private bool auto = false;
    
    void Start()
    {
        movControlado =GetComponent<Player>();
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
    }
     void MovAuto()
    {
        movControlado.enabled = false;
        transform.Translate(Vector3.forward * Time.deltaTime * 10, Space.World);
    }    
}
